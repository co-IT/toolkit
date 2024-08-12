namespace coIT.Libraries.Clockodo.TimeEntries.Contracts;

public class TimeEntriesServiceSettings
{
    public TimeEntriesServiceSettings(
        string emailAddress,
        string apiToken,
        string applicationName,
        string contactEmail
    )
    {
        EmailAddress = emailAddress;
        ApiToken = apiToken;
        ApplicationName = applicationName;
        ContactEmail = contactEmail;
    }

    public string EmailAddress { get; set; }
    public string ApiToken { get; set; }
    public string ApplicationName { get; set; }
    public string ContactEmail { get; set; }
}
