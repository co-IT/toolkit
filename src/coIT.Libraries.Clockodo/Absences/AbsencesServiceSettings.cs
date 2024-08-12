using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences
{
    public class AbsencesServiceSettings
    {
        public string EmailAddress { get; private set; }
        public string ApiToken { get; private set; }
        public string BaseAdress { get; private set; }

        [JsonConstructor]
        public AbsencesServiceSettings(string emailAddress, string apiToken, string baseAdress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentOutOfRangeException(
                    nameof(emailAddress),
                    "Email Address missing"
                );

            if (string.IsNullOrWhiteSpace(apiToken))
                throw new ArgumentOutOfRangeException(nameof(apiToken), "Api Token missing");

            if (string.IsNullOrWhiteSpace(baseAdress))
                throw new ArgumentOutOfRangeException(nameof(apiToken), "Api Token missing");

            BaseAdress = baseAdress;
            EmailAddress = emailAddress;
            ApiToken = apiToken;
        }
    }
}
