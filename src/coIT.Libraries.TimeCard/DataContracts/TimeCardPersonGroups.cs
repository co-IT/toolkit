using Newtonsoft.Json;

namespace coIT.Libraries.TimeCard.DataContracts
{
    internal class TimeCardPersonGroups
    {
        [JsonProperty("Value")]
        public int Id { get; set; }

        [JsonProperty("Groups")]
        public List<int> Groups { get; set; }
    }
}
