using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class ClockodoCustomers
    {
        [JsonProperty("customers")]
        public IEnumerable<CustomerInfo> Customers { get; set; }
    }
}
