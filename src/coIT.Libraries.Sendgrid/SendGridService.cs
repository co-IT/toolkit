using System.Net.Http.Headers;
using System.Text;
using CSharpFunctionalExtensions;
using ManuellePhishingMails.Domain.Csv.Ziel.SendgridDto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManuellePhishingMails.Domain.Csv.Ziel
{
    public class SendGridService
    {
        private readonly HttpClient _httpClient;
        private readonly IContractResolver _sendgridContractResolver;

        public SendGridService(string token)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.sendgrid.com/v3/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );

            _sendgridContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() { OverrideSpecifiedNames = false }
            };
        }

        public async Task<Result> MailSenden(Mail mail)
        {
            var mailAlsJson = MailAlsJson(mail);
            var httpContent = new StringContent(mailAlsJson, Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.PostAsync("mail/send", httpContent);

            try
            {
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                var antwort = await httpResponse.Content.ReadAsStringAsync();
                return Result.Failure($"{exception.Message}, Antwort: {antwort}");
            }

            return Result.Success();
        }

        private string MailAlsJson(Mail mail)
        {
            return JsonConvert.SerializeObject(
                mail,
                new JsonSerializerSettings
                {
                    ContractResolver = _sendgridContractResolver,
                    Formatting = Formatting.Indented
                }
            );
        }
    }
}
