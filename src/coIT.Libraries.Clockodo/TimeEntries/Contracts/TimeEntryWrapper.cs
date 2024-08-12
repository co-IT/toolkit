using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class TimeEntryWrapper
    {
        [JsonProperty("paging")]
        public ClockodoPaging Paging { get; set; }

        [JsonProperty("entries")]
        public RawTimeEntry[] Entries { get; set; }
    }
}
