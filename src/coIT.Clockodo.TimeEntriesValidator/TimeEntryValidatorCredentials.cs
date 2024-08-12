using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Clockodo.TimeEntriesValidator
{
    internal class TimeEntryValidatorCredentials
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public TimeEntryValidatorCredentials(string name, string email, string token)
        {
            Name = name;
            Email = email;
            Token = token;
        }

        public TimeEntriesServiceSettings ToTimeEntriesServiceSettings()
        {
            return new TimeEntriesServiceSettings(Email, Token, "ClockodoChecker", "info@co-IT.eu");
        }
    }
}
