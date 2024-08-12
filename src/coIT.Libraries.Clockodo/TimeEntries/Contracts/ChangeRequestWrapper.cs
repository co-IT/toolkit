using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class ChangeRequestWrapper
    {
        [JsonProperty("paging")]
        public ClockodoPaging Paging { get; set; }

        [JsonProperty("change_requests")]
        public RawChangeRequest[] ChangeRequests { get; set; }
    }
}
