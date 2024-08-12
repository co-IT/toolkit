using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class RawChangeRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("users_id")]
        public int UserId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("declined_at")]
        public string? DeclinedAt { get; set; }

        [JsonProperty("declined_by")]
        public string? DeclinedBy { get; set; }

        [JsonProperty("changes")]
        public IEnumerable<RawChangeRequestInterval> Changes { get; set; }
    }
}
