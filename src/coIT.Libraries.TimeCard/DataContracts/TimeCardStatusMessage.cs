using Newtonsoft.Json;

namespace coIT.Libraries.TimeCard.DataContracts
{
    internal class TimeCardStatusMessage
    {
        [JsonProperty("Value")]
        public int Id { get; set; }

        [JsonProperty("Code")]
        public int Code { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}
