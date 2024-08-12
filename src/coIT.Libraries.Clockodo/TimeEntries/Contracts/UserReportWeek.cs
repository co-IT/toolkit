using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class UserReportWeek
    {
        [JsonProperty("nr")]
        public int Index { get; set; }

        [JsonProperty("day_details")]
        public IEnumerable<UserReportDay> Days { get; set; }
    }
}
