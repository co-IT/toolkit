using System.Collections.Immutable;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using coIT.Libraries.Clockodo.TimeEntries.Filter;
using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries;

public class TimeEntriesService : ITimeEntriesService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerSettings _jsonSettings;

    public TimeEntriesService(TimeEntriesServiceSettings credentials)
    {
        _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        _client = new HttpClient
        {
            DefaultRequestHeaders =
            {
                { "Accept", "application/json" },
                { "X-ClockodoApiUser", credentials.EmailAddress },
                { "X-ClockodoApiKey", credentials.ApiToken },
                {
                    "X-Clockodo-External-Application",
                    $"{credentials.ApplicationName};{credentials.ContactEmail}"
                }
            }
        };
    }

    public async Task<IImmutableList<TimeEntry>> GetTimeEntriesAsync(
        ClockodoPeriod period,
        int employee = 0
    )
    {
        var rawEntries = await GetRawTimeEntriesAsync(period, employee);
        var customerList = await GetAllCustomers();

        var runningEntryFilter = new RunningEntryFilter();
        var finishedRawEntries = runningEntryFilter.Filter(rawEntries);

        var combinedEntries = finishedRawEntries
            .Select(rawEntry =>
            {
                var customer = customerList.Single(customer => customer.Id == rawEntry.CustomerId);
                return new TimeEntry(rawEntry, customer);
            })
            .ToImmutableList();

        return combinedEntries;
    }

    public async Task<IEnumerable<UserReportDayWithUser>> GetDailyUserreports(ClockodoPeriod period)
    {
        var years = period.GetAllYearsInPeriod();
        var dailyReports = new List<UserReportDayWithUser>();
        foreach (var year in years)
        {
            var userreports = await GetAllUserReports(year);
            var dailyUserreports = userreports
                .ToList()
                .SelectMany(userreport =>
                    userreport.Months.SelectMany(reportMonth =>
                        reportMonth.Weeks.SelectMany(reportWeek =>
                            reportWeek.Days.Select(reportDay => new UserReportDayWithUser(
                                reportDay,
                                userreport.Id,
                                userreport.Name
                            ))
                        )
                    )
                );

            dailyReports.AddRange(dailyUserreports);
        }

        return dailyReports
            .Where(d => d.Date > period.Start.AddDays(-1) && d.Date < period.End)
            .ToList();
    }

    #region Absences
    public async Task<IImmutableDictionary<DateOnly, double>> GetSicknesses(
        int year,
        int employee = 0
    )
    {
        var sicknessTypes = new List<int>() { 4, 5, 11, 12 };
        return await GetEmployeeAbsenceDaysFor(year, employee, sicknessTypes);
    }

    public async Task<IImmutableDictionary<DateOnly, double>> GetRegularHolidays(
        int year,
        int employee = 0
    )
    {
        var holidayTypes = new List<int> { 1 };
        return await GetEmployeeAbsenceDaysFor(year, employee, holidayTypes);
    }

    public async Task<IImmutableDictionary<DateOnly, double>> GetMaternityProtection(
        int year,
        int employee = 0
    )
    {
        var maternityProtectionTypes = new List<int> { 7 };
        return await GetEmployeeAbsenceDaysFor(year, employee, maternityProtectionTypes);
    }

    public async Task<IImmutableDictionary<DateOnly, double>> GetSpecialVacation(
        int year,
        int employee = 0
    )
    {
        var specialVacationTypes = new List<int> { 2, 10 };
        return await GetEmployeeAbsenceDaysFor(year, employee, specialVacationTypes);
    }

    internal async Task<IImmutableDictionary<DateOnly, double>> GetEmployeeAbsenceDaysFor(
        int year,
        int employee,
        List<int> absenceTypes
    )
    {
        var rawAbsences = await GetRawAbsencesAsync(year, employee);
        return rawAbsences
            .Where(rawAbsence => absenceTypes.Contains(rawAbsence.Type))
            .Select(rawAbsence =>
                (
                    DateTime.Parse(rawAbsence.StartDate),
                    DateTime.Parse(rawAbsence.EndDate),
                    rawAbsence.CountDays % 1 == 0 ? 1 : 0.5
                )
            )
            .SelectMany(absencePeriodTuple =>
                Enumerable
                    .Range(0, 1 + absencePeriodTuple.Item2.Subtract(absencePeriodTuple.Item1).Days)
                    .Select(offset =>
                        (absencePeriodTuple.Item1.AddDays(offset), absencePeriodTuple.Item3)
                    )
                    .ToArray()
            )
            .ToDictionary(a => DateOnly.FromDateTime(a.Item1), a => a.Item2)
            .ToImmutableDictionary();
    }

    internal async Task<IImmutableList<RawAbsence>> GetRawAbsencesAsync(int year, int employee = 0)
    {
        var uri = ApiAdressesBuilder.AllAbsencesUri(year, employee);

        var allAbsencesInYearWrapper = await GetAllAsync<AbsencesWrapper>(uri);

        return allAbsencesInYearWrapper.Absences.ToImmutableList();
    }
    #endregion

    public async Task<IEnumerable<UserWithTeam>> GetAllUsers()
    {
        var teams = await GetAllTeams();
        var noTeamTeam = new Team() { Id = 0, Name = "Kein Team" };

        var rawUsers = await GetRawUsers();
        var usersWithTeam = rawUsers.Select(user =>
        {
            var team = teams.FirstOrDefault(team => team.Id == user.TeamId) ?? noTeamTeam;
            return UserWithTeam.FromRawUserAndTeam(user, team);
        });

        return usersWithTeam;
    }

    private async Task<IImmutableList<RawTimeEntry>> GetRawTimeEntriesAsync(
        ClockodoPeriod period,
        int employee = 0
    )
    {
        var allTimeEntries = new List<RawTimeEntry>();
        TimeEntryWrapper allEntriesOfPage;
        var page = 1;

        do
        {
            var uri = ApiAdressesBuilder.AllEntriesUri(period, page, employee, true);

            allEntriesOfPage = await GetAllAsync<TimeEntryWrapper>(uri);
            allTimeEntries.AddRange(allEntriesOfPage.Entries);

            page++;
        } while (allEntriesOfPage.Paging.CountPages >= page);

        return allTimeEntries.ToImmutableList();
    }

    private async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        var allTimeEntries = new List<Customer>();
        CustomersWrapper allEntriesOfPage;
        var page = 1;

        do
        {
            var uri = ApiAdressesBuilder.AllCustomersUri();

            allEntriesOfPage = await GetAllAsync<CustomersWrapper>(uri);
            allTimeEntries.AddRange(allEntriesOfPage.Customers);

            page++;
        } while (allEntriesOfPage.Paging.CountPages >= page);

        return allTimeEntries.ToImmutableList();
    }

    private async Task<IEnumerable<Team>> GetAllTeams()
    {
        var uri = ApiAdressesBuilder.AllTeamsUri();
        var userreportWrapper = await GetAllAsync<TeamWrapper>(uri);
        return userreportWrapper.Teams;
    }

    private async Task<IEnumerable<RawUser>> GetRawUsers()
    {
        var uri = ApiAdressesBuilder.AllUsersUri();
        var userreportWrapper = await GetAllAsync<UsersWrapper>(uri);
        return userreportWrapper.Users;
    }

    private async Task<IEnumerable<UserReport>> GetAllUserReports(int year)
    {
        var uri = ApiAdressesBuilder.AllUserreportsUri(year);
        var userreportWrapper = await GetAllAsync<UserReportWrapper>(uri);
        return userreportWrapper.Userreports;
    }

    public async Task<IImmutableList<ChangeRequest>> GetAllChangeRequestsAsync(
        ClockodoPeriod period,
        int employee = 0
    )
    {
        var rawChangeRequests = await GetAllRawChangeRequests(period, employee);
        var users = await GetAllUsers();

        return rawChangeRequests
            .Select(rawChangeRequest => new ChangeRequest(rawChangeRequest, users))
            .ToImmutableList();
    }

    private async Task<IEnumerable<RawChangeRequest>> GetAllRawChangeRequests(
        ClockodoPeriod period,
        int employee = 0
    )
    {
        var allChangeRequests = new List<RawChangeRequest>();
        ChangeRequestWrapper allEntriesOfPage;
        var page = 1;

        do
        {
            var uri = ApiAdressesBuilder.AllChangeRequestsUri(period, page, employee);

            allEntriesOfPage = await GetAllAsync<ChangeRequestWrapper>(uri);
            allChangeRequests.AddRange(allEntriesOfPage.ChangeRequests);

            page++;
        } while (allEntriesOfPage.Paging.CountPages >= page);

        return allChangeRequests;
    }

    private async Task<T> GetAllAsync<T>(string route)
    {
        var response = await _client.GetAsync(route);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<T>(content, _jsonSettings);
        return deserialized ?? default;
    }
}
