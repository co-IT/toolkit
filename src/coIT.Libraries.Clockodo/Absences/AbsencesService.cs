// ReSharper disable ConvertToUsingDeclaration
// ReSharper disable ParameterTypeCanBeEnumerable.Local

using System.Collections.Immutable;
using System.Net.Http.Headers;
using coIT.Libraries.Clockodo.Absences.Contracts;
using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences;

public class AbsencesService : IEmployeesService, IAbsencesService, IUserReportService
{
    private readonly List<ClockodoAbsenceType> _absenceTypes;
    private readonly ClockodoApiAdresses _apiAdresses;
    private readonly AbsencesServiceSettings _clockodoSettings;
    private readonly JsonSerializerSettings _jsonSettings;

    public AbsencesService(
        AbsencesServiceSettings clockodoSettings,
        List<ClockodoAbsenceType> absenceTypes
    )
    {
        _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        _clockodoSettings = clockodoSettings;
        _absenceTypes = absenceTypes;
        _apiAdresses = new ClockodoApiAdresses();
    }

    public async Task<IImmutableList<Absence>> AllAbsences(ClockodoPeriodFilter periodFilter)
    {
        using (var client = CreateClockodoHttpClient())
        {
            var allAbsences = new List<Absence>();

            var years = periodFilter.YearsOfPeriod;

            foreach (var year in years)
            {
                var endPointForAbsencesPerYear = _apiAdresses.AbsencesOfYear(year);

                using (var request = CreateRequestForEndpoint(endPointForAbsencesPerYear))
                {
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var content = response.Content)
                        {
                            var absencesAsJson = await content.ReadAsStringAsync();

                            var absences = DeserializeAbsences(absencesAsJson, _jsonSettings)
                                .ApplyElectronicalCertificateOfDisability()
                                .SplitAbsencesByMonth(_absenceTypes)
                                .FilterByPeriod(periodFilter);

                            allAbsences.AddRange(absences);
                        }
                    }
                }
            }

            return allAbsences.ToImmutableList();
        }
    }

    // fügt alle Mitarbeiter mit den dazugehörigen Informationen zu einer Konstante Liste hinzu
    public async Task<IImmutableList<EmployeeInfo>> AllEmployees()
    {
        using var client = new HttpClient { BaseAddress = new Uri(_clockodoSettings.BaseAdress) };
        using var request = CreateRequestForEndpoint(_apiAdresses.AllEmployees);
        using var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        using var content = response.Content;

        var responseBody = await content.ReadAsStringAsync();

        var employees = JsonConvert.DeserializeObject<ClockodoEmployees>(
            responseBody,
            _jsonSettings
        );

        return employees.Employees.ToImmutableList();
    }

    public async Task<IImmutableList<UserReport>> AllUserReports(ClockodoPeriodFilter periodFilter)
    {
        using (var client = new HttpClient { BaseAddress = new Uri(_clockodoSettings.BaseAdress) })
        {
            var allEmployees = await AllEmployees();

            var allUserReports = AllUserReportsQuery(client, periodFilter)
                .Select(report => IncludeEmployees(report, allEmployees));

            return allUserReports.ToImmutableList();
        }
    }

    private IList<Absence> DeserializeAbsences(
        string responseBody,
        JsonSerializerSettings jsonSettings
    )
    {
        var absencesOrNothing = JsonConvert.DeserializeObject<Contracts.Absences>(
            responseBody,
            jsonSettings
        );

        var absences = absencesOrNothing?.AllAbsences ?? new List<Absence>();

        return absences.ToList();
    }

    private HttpClient CreateClockodoHttpClient()
    {
        return new HttpClient { BaseAddress = new Uri(_clockodoSettings.BaseAdress) };
    }

    private static UserReport IncludeEmployees(
        UserReport report,
        IImmutableList<EmployeeInfo> allEmployees
    )
    {
        var employee = allEmployees.Single(emp => emp.Id == report.UserId);

        report.Employee = employee;

        return report;
    }

    private IEnumerable<UserReport> AllUserReportsQuery(
        HttpClient client,
        ClockodoPeriodFilter periodFilter
    )
    {
        var years = periodFilter.YearsOfPeriod;
        var allReports = new List<UserReport>();

        foreach (var year in years)
        {
            var endpointForUserReportsPerYear = _apiAdresses.UserReportsOfYear(year);
            using var request = CreateRequestForEndpoint(endpointForUserReportsPerYear);
            using var response = client.SendAsync(request).Result;

            response.EnsureSuccessStatusCode();

            using var content = response.Content;

            var responseBody = content.ReadAsStringAsync().Result;

            var reportsOfYear = JsonConvert.DeserializeObject<ClockodoUserReports>(
                responseBody,
                _jsonSettings
            );

            var withYear = reportsOfYear.UserReports.Select(report => IncludeYear(report, year));

            allReports.AddRange(withYear);
        }

        return allReports;
    }

    private static UserReport IncludeYear(UserReport report, int year)
    {
        report.Year = year;

        return report;
    }

    private static TimeEntry IncludeEmployees(
        TimeEntry entry,
        IImmutableList<EmployeeInfo> allEmployees
    )
    {
        entry.EmployeeInfo = allEmployees.Single(employee => employee.Id == entry.EmployeeId);

        return entry;
    }

    private static TimeEntry IncludeServices(
        TimeEntry entry,
        IImmutableList<ServiceInfo> allServices
    )
    {
        if (entry.ServiceId == 0)
            return entry;

        entry.ServiceInfo = allServices.Single(service => service.Id == entry.ServiceId);

        return entry;
    }

    private static TimeEntry IncludeCustomer(
        TimeEntry entry,
        IImmutableList<CustomerInfo> allCustomers
    )
    {
        entry.CustomerInfo = allCustomers.Single(customer => customer.Id == entry.CustomerId);

        return entry;
    }

    private IImmutableList<CustomerInfo> AllCustomers(HttpClient client)
    {
        using var request = CreateRequestForEndpoint(_apiAdresses.AllCustomers);
        using var response = client.SendAsync(request).Result;

        response.EnsureSuccessStatusCode();

        using var content = response.Content;

        var responseBody = content.ReadAsStringAsync().Result;

        var customers = JsonConvert.DeserializeObject<ClockodoCustomers>(
            responseBody,
            _jsonSettings
        );

        return customers.Customers.ToImmutableList();
    }

    //Erstellt Verbindung zu Clockodo her und gibt Emailadresse und Token mit
    private HttpRequestMessage CreateRequestForEndpoint(string apiEndpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, apiEndpoint);

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        request.Headers.Add("X-ClockodoApiUser", _clockodoSettings.EmailAddress);
        request.Headers.Add("X-ClockodoApiKey", _clockodoSettings.ApiToken);

        return request;
    }

    private IImmutableList<ServiceInfo> AllServices(HttpClient client)
    {
        using var request = CreateRequestForEndpoint(_apiAdresses.AllServices);
        using var response = client.SendAsync(request).Result;

        response.EnsureSuccessStatusCode();

        using var content = response.Content;

        var responseBody = content.ReadAsStringAsync().Result;

        var services = JsonConvert.DeserializeObject<EntryServices>(responseBody, _jsonSettings);

        return services.Services.ToImmutableList();
    }

    private IList<TimeEntry> GetPlainEntries(HttpClient client, ClockodoPeriodFilter periodFilter)
    {
        var allTimeEntries = new List<TimeEntry>();
        Entry allEntriesOfPage;

        var page = 1;
        do
        {
            using var request = CreateRequestForEndpoint(
                _apiAdresses.AllTimeEntries(periodFilter, page)
            );
            using var response = client.SendAsync(request).Result;

            page++;

            response.EnsureSuccessStatusCode();

            using var content = response.Content;

            var responseBody = content.ReadAsStringAsync().Result;

            allEntriesOfPage = JsonConvert.DeserializeObject<Entry>(responseBody, _jsonSettings);

            allTimeEntries = allTimeEntries.Concat(allEntriesOfPage.Entries).ToList();
        } while (allEntriesOfPage.Paging.CountPages >= page);

        return allTimeEntries;
    }

    private static bool EntryIsCurrentlyNotRunning(TimeEntry entry)
    {
        return !string.IsNullOrWhiteSpace(entry.TimeUntil);
    }
}
