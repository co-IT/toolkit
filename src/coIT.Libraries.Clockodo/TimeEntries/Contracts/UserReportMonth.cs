using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class UserReportMonth
    {
        [JsonProperty("nr")]
        public int Index { get; set; }

        [JsonProperty("week_details")]
        public IEnumerable<UserReportWeek> Weeks { get; set; }
    }
}
