using System.Net.Http.Headers;
using coIT.Libraries.Clockodo.Account.Contracts;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Account;

public class AccountService
{
    private readonly JsonSerializerSettings _jsonSettings;

    ///api/v2/aggregates/users/me
    private readonly string _relativeEndpointUri;

    private readonly ApiConnectionSettings _serviceSettings;

    public AccountService(ApiConnectionSettings serviceSettings)
    {
        _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        _serviceSettings = serviceSettings;
        _relativeEndpointUri = @"/api/v2/aggregates/users/me";
    }

    public async Task<AccountInformation?> GetMyAccount()
    {
        using (var client = CreateClockodoHttpClient(_serviceSettings))
        {
            using (var request = CreateRequestForEndpoint(_relativeEndpointUri))
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    using (var content = response.Content)
                    {
                        var rawUserAsJson = await content.ReadAsStringAsync();
                        var rawAccountInformations = Deserialize(rawUserAsJson, _jsonSettings);
                        var accountInformation = Map(rawAccountInformations);

                        return accountInformation;
                    }
                }
            }
        }
    }

    private static AccountInformation Map(RawAccountObject rawAccountInformations)
    {
        if (rawAccountInformations?.Self == null)
            return new AccountInformation();

        return new AccountInformation
        {
            Number = rawAccountInformations.Self.Number,
            Name = rawAccountInformations.Self.Name,
            Email = rawAccountInformations.Self.Email,
            Id = rawAccountInformations.Self.Id,
        };
    }

    private static RawAccountObject? Deserialize(
        string responseBody,
        JsonSerializerSettings jsonSettings
    )
    {
        return JsonConvert.DeserializeObject<RawAccountObject>(responseBody, jsonSettings);
    }

    private static HttpClient CreateClockodoHttpClient(ApiConnectionSettings serviceSettings)
    {
        var baseAddress = serviceSettings.BaseAddress.AbsoluteUri;

        return new HttpClient
        {
            BaseAddress = new Uri(baseAddress),
            DefaultRequestHeaders =
            {
                { "Accept", "application/json" },
                { "X-ClockodoApiUser", serviceSettings.EmailAddress },
                { "X-ClockodoApiKey", serviceSettings.ApiToken },
                {
                    "X-Clockodo-External-Application",
                    $"{serviceSettings.ApplicationName};{serviceSettings.ContactEmail}"
                }
            }
        };
    }

    private static HttpRequestMessage CreateRequestForEndpoint(string apiEndpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, apiEndpoint);
        return request;
    }
}
