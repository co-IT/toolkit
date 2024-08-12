using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class EntryService
    {
        [JsonProperty("service")]
        public ServiceInfo Service { get; set; }
    }
}
