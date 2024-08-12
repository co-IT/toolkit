using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class TeamWrapper
    {
        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }
    }
}
