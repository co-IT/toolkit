using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Account.Contracts;

internal class RawAccountObject
{
    [JsonProperty("user")]
    public RawUser Self { get; set; }
}
