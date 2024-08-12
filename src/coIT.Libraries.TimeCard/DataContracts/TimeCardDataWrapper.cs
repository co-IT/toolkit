using Newtonsoft.Json;

namespace coIT.Libraries.TimeCard.DataContracts
{
    internal class TimeCardDataWrapper<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
