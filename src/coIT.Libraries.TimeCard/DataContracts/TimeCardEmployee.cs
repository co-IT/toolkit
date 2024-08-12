using Newtonsoft.Json;

namespace coIT.Libraries.TimeCard.DataContracts
{
    public class TimeCardEmployee
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("ParentId")]
        public int ParentId { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("PersonNo")]
        public int PersonNo { get; set; }

        [JsonProperty("PersonNoString")]
        public string? PersonNoString { get; set; }

        [JsonProperty("Firstname")]
        public string? Firstname { get; set; }

        [JsonProperty("Lastname")]
        public string? Lastname { get; set; }

        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonProperty("Department")]
        public string? Department { get; set; }

        [JsonProperty("HasNoSupervisor")]
        public bool HasNoSupervisor { get; set; }

        [JsonProperty("Roles")]
        public int Roles { get; set; }
    }
}
