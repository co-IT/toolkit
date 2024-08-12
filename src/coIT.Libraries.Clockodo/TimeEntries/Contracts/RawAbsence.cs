using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class RawAbsence
    {
        [JsonProperty("id")]
        public int Id { get; init; }

        [JsonProperty("users_id")]
        public int UserId { get; init; }

        [JsonProperty("date_since")]
        public string StartDate { get; init; }

        [JsonProperty("date_until")]
        public string EndDate { get; init; }

        [JsonProperty("status")]
        public int Status { get; init; }

        [JsonProperty("type")]
        public int Type { get; init; }

        [JsonProperty("note")]
        public string Note { get; init; }

        [JsonProperty("count_days")]
        public float CountDays { get; init; }

        [JsonProperty("count_hours")]
        public float CountHours { get; init; }

        [JsonProperty("date_enquired")]
        public string DateEnquired { get; init; }

        [JsonProperty("date_approved")]
        public string DateApproved { get; init; }

        [JsonProperty("approved_by")]
        public int Approver { get; init; }
    }
}
