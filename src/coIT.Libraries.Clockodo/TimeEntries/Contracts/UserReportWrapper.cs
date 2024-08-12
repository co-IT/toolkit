using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class UserReportWrapper
    {
        [JsonProperty("userreports")]
        public IEnumerable<UserReport> Userreports { get; set; }
    }
}
