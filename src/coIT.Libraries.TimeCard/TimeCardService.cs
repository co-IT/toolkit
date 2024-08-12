using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Net;
using System.Threading.Tasks.Dataflow;
using System.Web;
using coIT.Libraries.TimeCard.DataContracts;
using Newtonsoft.Json;

namespace coIT.Libraries.TimeCard
{
    public class TimeCardService : IPersonsService, IAbsenceService, IDisposable
    {
        private TimeCardSettings _settings;
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        private IImmutableSet<TimeCardAbsenceType> _absenceTypes;

        private HttpClient _client;

        public static async Task<TimeCardService> Login(TimeCardSettings settings)
        {
            var service = new TimeCardService(settings);

            await service.StartHttpSession();
            await service.Login();
            await service.GetAllAbsenceTypes();

            return service;
        }

        private TimeCardService(TimeCardSettings settings)
        {
            _settings = settings;
        }

        private async Task StartHttpSession()
        {
            var handler = new HttpClientHandler();
            handler.MaxConnectionsPerServer = 30;
            handler.CookieContainer = new CookieContainer();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _client = new HttpClient(handler);
            _client.Timeout = TimeSpan.FromSeconds(30);
            _client.DefaultRequestHeaders.Add("User-Agent", "Custom");
        }

        private async Task Login()
        {
            var usernameEncoded = HttpUtility.UrlEncode(_settings.Username);
            var passwordEncoded = HttpUtility.UrlEncode(_settings.Password);

            var postData = $"?Username={usernameEncoded}&Password={passwordEncoded}";
            var loginUri = new Uri($"https://{_settings.WebAddress}/Home/Login{postData}");

            await _client.PostAsync(loginUri, null);
        }

        public async Task<IImmutableList<TimeCardEmployeesWithGroups>> AllEmployees()
        {
            var uriPersons = new Uri($"https://{_settings.WebAddress}/Persons/GetPersons");
            var wrapper = await GetRequest<TimeCardDataWrapper<TimeCardEmployee>>(uriPersons);
            var employees = wrapper.Data.ToImmutableList();
            var personDetailList = new List<TimeCardEmployeesWithGroups>();

            foreach (var employee in employees)
            {
                var uriPersonDetails = new Uri(
                    $"https://{_settings.WebAddress}//Persons/GetPersonDetails?id={employee.Id}"
                );
                var personWithGroups = await GetRequest<TimeCardPersonGroups>(uriPersonDetails);
                var personWithGroup = new TimeCardEmployeesWithGroups(
                    employee,
                    personWithGroups.Groups
                );

                if (personWithGroup.Groups.Contains(_settings.NoExportGroup))
                    continue;

                personDetailList.Add(personWithGroup);
            }

            return personDetailList.ToImmutableList();
        }

        public async Task<ImmutableHashSet<TimeCardAbsenceType>> GetAllAbsenceTypes()
        {
            var unstructuredAbsenceReport = await GetReportOfAbsenceTypes();
            var structuredAbsenceReport = TraversFullTextToLines(unstructuredAbsenceReport);

            var absenceReport = FilterUnusedLines(structuredAbsenceReport);

            var absenceTypes = ConvertToAbsenceTypes(absenceReport).ToImmutableHashSet();

            _absenceTypes = absenceTypes;
            return absenceTypes;
        }

        private IEnumerable<TimeCardAbsenceType> ConvertToAbsenceTypes(IEnumerable<string> csvLines)
        {
            var csvSeperatorCharacter = ";";

            return csvLines
                .Select(line => line.Split(csvSeperatorCharacter))
                .Select(line => new TimeCardAbsenceType(int.Parse(line[0]), line[1]));
        }

        private async Task<string> GetReportOfAbsenceTypes()
        {
            var queryParameter = "{\"name\": \"ListAllAbsenceTypes\",\"exportFormat\": \"csv\"}";

            var formVariables = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("rParameters", queryParameter)
            };
            var formContent = new FormUrlEncodedContent(formVariables);

            var loginUri = new Uri($"https://{_settings.WebAddress}/ReportApi/RunReport");
            var postResult = await _client.PostAsync(loginUri, formContent);
            var commaSperatedAbsenceTypeList = await postResult.Content.ReadAsStringAsync();

            return commaSperatedAbsenceTypeList ?? string.Empty;
        }

        private IEnumerable<string> FilterUnusedLines(IEnumerable<string> existingLines)
        {
            var headerLine = 1;
            return existingLines.Where(line => !string.IsNullOrWhiteSpace(line)).Skip(headerLine);
        }

        private IEnumerable<string> TraversFullTextToLines(string commaSperatedAbsenceTypeList)
        {
            if (string.IsNullOrWhiteSpace(commaSperatedAbsenceTypeList))
                return new string[0];

            var lineBreaks = new string[] { "\r\n", "\r", "\n" };
            return commaSperatedAbsenceTypeList.Split(lineBreaks, StringSplitOptions.None);
        }

        public async Task<IImmutableList<TimeCardAbsence>> AllAbsences(
            List<TimeCardEmployeesWithGroups> employees,
            DateTime date
        )
        {
            var list = new List<EmployeeDayAbsenceQuestion>();

            foreach (var employee in employees)
            {
                var uriEmployeePair = new EmployeeDayAbsenceQuestion(
                    employee.Id,
                    date,
                    employee.Department ?? string.Empty
                );

                list.Add(uriEmployeePair);
            }

            return await ParallelGetRequest(list);
        }

        public ImmutableList<GroupedTimeCardAbsence> GroupOngoingSingleDayEvents(
            IImmutableList<TimeCardAbsence> absences,
            TimeCardAbsenceType holidayType
        )
        {
            var employeeGroupedAbsencesRaw = GroupAbsencesByEmployee(absences.ToList());

            var groupedAbsences = new List<GroupedTimeCardAbsence>();
            foreach (var employeeAbsences in employeeGroupedAbsencesRaw)
            {
                var employeeGroupedAbsences = GroupAbsences(employeeAbsences.Value, holidayType);
                groupedAbsences.AddRange(employeeGroupedAbsences);
            }

            return groupedAbsences.ToImmutableList();
        }

        public ImmutableList<DateTime> FilterHolidays(
            IImmutableList<TimeCardAbsence> absences,
            TimeCardAbsenceType holidayType
        )
        {
            return absences
                .Where(a => a.AbsenceType == holidayType)
                .GroupBy(a => a.Date)
                .Select(a => a.Key)
                .ToImmutableList();
        }

        private List<GroupedTimeCardAbsence> GroupAbsences(
            List<TimeCardAbsence> absences,
            TimeCardAbsenceType holidayType
        )
        {
            var orderedAbsences = absences.OrderBy(a => a.Date);

            var groupNumber = -1;
            var absenceGroups = new List<List<TimeCardAbsence>>();

            var lastAbsenceDay = new DateTime(2000, 1, 1);
            var lastAbsenceTime = "";
            var currentGroupType = -1;
            var holidayBonus = 0;
            foreach (var absence in orderedAbsences)
            {
                var differenceToLastAbsence = (absence.Date - lastAbsenceDay).Days;

                if (absence.AbsenceType == holidayType)
                {
                    holidayBonus += 1;

                    if (absence.Date.DayOfWeek == DayOfWeek.Monday)
                        holidayBonus += 2;

                    continue;
                }

                if (
                    (
                        differenceToLastAbsence > 1 + holidayBonus
                        && absence.Date.DayOfWeek != DayOfWeek.Monday
                    )
                    || (
                        differenceToLastAbsence > 3 + holidayBonus
                        && absence.Date.DayOfWeek == DayOfWeek.Monday
                    )
                    || currentGroupType != absence.AbsenceType.Id
                    || lastAbsenceTime != absence.Time
                )
                {
                    currentGroupType = absence.AbsenceType.Id;
                    lastAbsenceTime = absence.Time;
                    groupNumber++;
                    absenceGroups.Add(new List<TimeCardAbsence>());
                }

                absenceGroups[groupNumber].Add(absence);
                lastAbsenceDay = absence.Date;

                holidayBonus -= holidayBonus > 0 ? 1 : 0;
            }

            var groupedAbsences = new List<GroupedTimeCardAbsence>();
            foreach (var group in absenceGroups)
            {
                if (group.Count == 0)
                    continue;

                var grouped = new GroupedTimeCardAbsence(group.First(), group.Last());
                groupedAbsences.Add(grouped);
            }

            return groupedAbsences;
        }

        private Dictionary<int, List<TimeCardAbsence>> GroupAbsencesByEmployee(
            List<TimeCardAbsence> absences
        )
        {
            var employeeGroupedAbsences = new Dictionary<int, List<TimeCardAbsence>>();

            foreach (var absence in absences)
            {
                if (!employeeGroupedAbsences.ContainsKey(absence.Employee))
                    employeeGroupedAbsences.Add(absence.Employee, new());

                employeeGroupedAbsences[absence.Employee].Add(absence);
            }

            return employeeGroupedAbsences;
        }

        private async Task<T> GetRequest<T>(Uri uri)
        {
            var response = await _client.GetStringAsync(uri);

            var wrapper = JsonConvert.DeserializeObject<T>(response, _jsonSettings);

            return wrapper;
        }

        private async Task<IImmutableList<TimeCardAbsence>> ParallelGetRequest(
            List<EmployeeDayAbsenceQuestion> employeeDayAbsenceQuestions
        )
        {
            var bag = new ConcurrentBag<TimeCardAbsence>();

            var action = RequestAbsenceForEmployeeAndAddToBag;
            var requestList = new ActionBlock<EmployeeDayAbsenceQuestion>(
                action,
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 20 }
            );

            Parallel.ForEach(
                employeeDayAbsenceQuestions,
                (pair) =>
                {
                    pair.Bag = bag;
                    requestList.SendAsync(pair);
                }
            );

            requestList.Complete();
            await requestList.Completion;

            foreach (var timeCardAbsence in bag)
            {
                if (timeCardAbsence.Reason == "Krank")
                    Console.Write("");

                timeCardAbsence.AbsenceType = _absenceTypes.FirstOrDefault(absenceType =>
                    absenceType.DisplayText == timeCardAbsence.Reason
                );
            }

            return bag.ToImmutableList();
        }

        private void RequestAbsenceForEmployeeAndAddToBag(
            EmployeeDayAbsenceQuestion absenceQuestion
        )
        {
            var uri = absenceQuestion.GetUri(_settings.WebAddress);
            Console.WriteLine(uri + " angefragt");

            var result = _client.GetStringAsync(uri).Result;

            if (result.Contains("Code"))
            {
                var error = JsonConvert.DeserializeObject<TimeCardStatusMessage>(
                    result,
                    _jsonSettings
                );

                Console.WriteLine($"Fehler {error.Code} - {error.Text}");
                return;
            }

            var wrapper = JsonConvert.DeserializeObject<TimeCardDataWrapper<TimeCardAbsence>>(
                result,
                _jsonSettings
            );

            foreach (var data in wrapper.Data)
            {
                var absence = data;
                absence.Employee = absenceQuestion.EmployeeId;
                absence.Date = absenceQuestion.Date;
                absence.Department = absenceQuestion.Department;

                if (absence.Type == 6)
                    absenceQuestion.Bag.Add(absence);

                if (absence.Type == 11)
                {
                    absence.Reason = "Feiertag";
                    absenceQuestion.Bag.Add(absence);
                }
            }

            Console.WriteLine(uri + " done");
        }

        public void Dispose()
        {
            Console.WriteLine($"HTTP Client disposed in {this.GetType().FullName}");
            ((IDisposable)_client).Dispose();
        }
    }
}
