using System.Diagnostics;
using System.Text;
using System.Web;
using coIT.Libraries.Clockodo.Clock.Contracts;
using Newtonsoft.Json;

// ReSharper disable ConvertToUsingDeclaration
#pragma warning disable IDE0063
#pragma warning disable IDE0063

namespace coIT.Libraries.Clockodo.Clock;

public class ClockService
{
    private readonly JsonSerializerSettings _jsonSettings;

    ///api/v2/aggregates/users/me
    private readonly string _relativeEndpointUri;

    private readonly ApiConnectionSettings _serviceSettings;

    public ClockService(ApiConnectionSettings serviceSettings)
    {
        _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        _serviceSettings = serviceSettings;
        _relativeEndpointUri = @"/api/v2/clock";
    }

    public async Task<RunningClockDto?> GetRunningClockEntry()
    {
        try
        {
            using (var client = CreateClockodoHttpClient(_serviceSettings))
            {
                //Todo: Enhanced List zeigt nicht die richtigen Werte an
                var queryParameters = HttpUtility.ParseQueryString(string.Empty);
                queryParameters["enhanced_list"] = "1";
                var endpointUriWithEnhancedList = $"{_relativeEndpointUri}?{queryParameters}";

                using (var request = CreateRequestForEndpointGet(endpointUriWithEnhancedList))
                {
                    using (var response = await client.GetAsync(request.RequestUri))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var answer = response.Content)
                        {
                            var plainAnswer = await answer.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<RunningClockEntriesDto>(
                                plainAnswer,
                                _jsonSettings
                            );
                            return result?.Running;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<int?> StartClock(StartClockDto startClockoDto)
    {
        try
        {
            using (var client = CreateClockodoHttpClient(_serviceSettings))
            {
                using (var request = CreateRequestForEndpointPost(_relativeEndpointUri))
                {
                    var body = JsonConvert.SerializeObject(startClockoDto);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    request.Content = content;
                    using (
                        var response = await client.PostAsync(request.RequestUri, request.Content)
                    )
                    {
                        response.EnsureSuccessStatusCode();

                        using (var answer = response.Content)
                        {
                            var plainAnswer = await answer.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<RunningClockEntriesDto>(
                                plainAnswer,
                                _jsonSettings
                            );

                            Debug.WriteLine(plainAnswer);
                            return result?.Running?.Id;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<bool> StopClock(int idOfRunningEntry)
    {
        try
        {
            using (var client = CreateClockodoHttpClient(_serviceSettings))
            {
                using (
                    var request = CreateRequestForEndpointDelete(
                        $"{_relativeEndpointUri}/{idOfRunningEntry}"
                    )
                )
                {
                    using (var response = await client.DeleteAsync(request.RequestUri))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var answer = response.Content)
                        {
                            var plainAnswer = await answer.ReadAsStringAsync();
                            Debug.WriteLine(plainAnswer);
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return false;
        }
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

    private static HttpRequestMessage CreateRequestForEndpointPost(string apiEndpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, apiEndpoint);
        return request;
    }

    private static HttpRequestMessage CreateRequestForEndpointGet(string apiEndpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, apiEndpoint);
        return request;
    }

    private static HttpRequestMessage CreateRequestForEndpointDelete(string apiEndpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, apiEndpoint);
        return request;
    }
}
