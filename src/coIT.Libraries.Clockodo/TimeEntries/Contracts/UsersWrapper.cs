using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class UsersWrapper
    {
        [JsonProperty("users")]
        public RawUser[] Users { get; set; }
    }
}
