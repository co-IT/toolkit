using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class RawChangeRequestInterval
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("time_since")]
        public string TimeSince { get; set; }

        [JsonProperty("time_until")]
        public string TimeUntil { get; set; }
    }
}
