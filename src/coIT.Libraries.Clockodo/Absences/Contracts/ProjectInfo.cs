using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class ProjectInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }
    }
}
