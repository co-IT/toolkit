using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class Customer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public string? Number { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }

        [JsonProperty("billable_default")]
        public bool IsBillable { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }
    }
}
