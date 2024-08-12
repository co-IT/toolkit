using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace coIT.Libraries.Clockodo
{
    public class ApiConnectionSettings
    {
        public ApiConnectionSettings(
            string emailAddress,
            string apiToken,
            string applicationName,
            string contactEmail,
            Uri baseAddress
        )
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentOutOfRangeException(
                    nameof(emailAddress),
                    "Email Address missing"
                );

            if (string.IsNullOrWhiteSpace(apiToken))
                throw new ArgumentOutOfRangeException(nameof(apiToken), "Api Token missing");

            if (!baseAddress.IsAbsoluteUri)
                throw new ArgumentOutOfRangeException(
                    nameof(baseAddress),
                    "Base Address must be absolute"
                );

            EmailAddress = emailAddress;
            ApiToken = apiToken;
            ApplicationName = applicationName;
            ContactEmail = contactEmail;
            BaseAddress = baseAddress;
        }

        public string EmailAddress { get; }
        public string ApiToken { get; }
        public string ApplicationName { get; }
        public string ContactEmail { get; }
        public Uri BaseAddress { get; }
    }
}
