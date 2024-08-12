using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class EmployeeInfo
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }
    }
}
