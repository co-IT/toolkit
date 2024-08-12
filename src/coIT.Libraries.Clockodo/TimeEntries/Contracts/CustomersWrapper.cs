using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class CustomersWrapper
    {
        [JsonProperty("paging")]
        public ClockodoPaging Paging { get; set; }

        [JsonProperty("customers")]
        public Customer[] Customers { get; set; }
    }
}
