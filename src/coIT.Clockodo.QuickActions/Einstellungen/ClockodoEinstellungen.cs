using coIT.Libraries.Clockodo;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Clockodo.QuickActions.Einstellungen;

public class ClockodoEinstellungen
{
    public string ApiToken { get; set; }
    public string EmailAddress { get; set; }

    public TimeEntriesServiceSettings ClockodoCredentials =>
        new(EmailAddress, ApiToken, "co-IT Clockodo Quick Actions", "info@co-it.eu");

    public ApiConnectionSettings CreateApiConnectionSettings =>
        new ApiConnectionSettings(
            EmailAddress,
            ApiToken,
            "co-IT Clockodo Quick Actions",
            "info@co-it.eu",
            new Uri("https://my.clockodo.com/")
        );
}
