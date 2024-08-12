using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class ServiceInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
