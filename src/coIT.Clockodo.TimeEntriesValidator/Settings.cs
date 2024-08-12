using System.Text;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace coIT.Clockodo.TimeEntriesValidator;

internal class Settings
{
    private static string CredentialsFilename = "credentials.json";

    private Settings() { }

    public static bool CredentialsExist() => File.Exists(CredentialsFilename);

    public static Result<Settings> Load()
    {
        return Result.Success(new Settings());
    }

    internal static void CreateCredentials(string mail, string token, string name)
    {
        var credentials = new TimeEntryValidatorCredentials(name, mail, token);

        var serializedCredentials = JsonConvert.SerializeObject(credentials);
        File.WriteAllText(CredentialsFilename, serializedCredentials, Encoding.UTF8);
    }

    public TimeEntryValidatorCredentials Credentials()
    {
        var serializedCredentials = File.ReadAllText(CredentialsFilename, Encoding.UTF8);
        var credentials = JsonConvert.DeserializeObject<TimeEntryValidatorCredentials>(
            serializedCredentials
        );

        return credentials;
    }
}
