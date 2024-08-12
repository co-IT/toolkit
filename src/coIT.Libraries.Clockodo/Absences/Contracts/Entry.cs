using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class Entry
    {
        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("entries")]
        public TimeEntry[] Entries { get; set; }
    }
}
