using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class EntryServices
    {
        [JsonProperty("services")]
        public IEnumerable<ServiceInfo> Services { get; set; }
    }
}
