using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class ClockodoUserReports
    {
        [JsonProperty("userreports")]
        public IEnumerable<UserReport> UserReports { get; set; }
    }
}
