using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class CustomerInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}
