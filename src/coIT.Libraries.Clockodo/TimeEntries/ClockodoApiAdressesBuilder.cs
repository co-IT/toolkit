using System.Web;

namespace coIT.Libraries.Clockodo.TimeEntries;

internal static class ApiAdressesBuilder
{
    private static readonly string BaseAdress = @"https://my.clockodo.com/api";
    private static readonly string AllEntries = "v2/entries";
    private static readonly string AllCustomers = "v2/customers";
    private static readonly string AllUserreports = "userreports";
    private static readonly string AllTeams = "v2/teams";
    private static readonly string AllUsers = "users";
    private static readonly string AllAbsences = "absences";
    private static readonly string AllChangeRequests = "v2/workTimes/changeRequests";

    public static string AllEntriesUri(
        ClockodoPeriod period,
        int page,
        int employee = 0,
        bool enhancedList = false
    )
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["time_since"] = period.GetStartAsString();
        queryParameters["time_until"] = period.GetEndAsString();
        queryParameters["enhanced_list"] = enhancedList ? "true" : "false";
        queryParameters["page"] = page.ToString();

        if (employee != 0)
            queryParameters["filter[users_id]"] = employee.ToString();
        return $"{BaseAdress}/{AllEntries}?{queryParameters}";
    }

    public static string AllUsersUrl() => $"{BaseAdress}/{AllUsers}";

    public static string AllAbsencesUri(int year, int employee)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["year"] = year.ToString();
        queryParameters["users_id"] = employee.ToString();

        return $"{BaseAdress}/{AllAbsences}?{queryParameters}";
    }

    public static string AllUserreportsUri(int year)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["year"] = year.ToString();
        queryParameters["type"] = "4";

        return $"{BaseAdress}/{AllUserreports}?{queryParameters}";
    }

    public static string AllChangeRequestsUri(ClockodoPeriod period, int page, int employee = 0)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["time_since"] = period.GetStartAsString();
        queryParameters["time_until"] = period.GetEndAsString();
        queryParameters["page"] = page.ToString();

        if (employee != 0)
            queryParameters["users_id"] = employee.ToString();
        return $"{BaseAdress}/{AllChangeRequests}?{queryParameters}";
    }

    public static string AllCustomersUri() => $"{BaseAdress}/{AllCustomers}";

    public static string AllTeamsUri() => $"{BaseAdress}/{AllTeams}";

    public static string AllUsersUri() => $"{BaseAdress}/v2/{AllUsers}";
}
