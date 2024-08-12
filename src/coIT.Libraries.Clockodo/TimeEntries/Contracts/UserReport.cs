using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class UserReport
    {
        [JsonProperty("users_id")]
        public int Id { get; set; }

        [JsonProperty("users_name")]
        public string Name { get; set; }

        [JsonProperty("month_details")]
        public IEnumerable<UserReportMonth> Months { get; set; }
    }
}
