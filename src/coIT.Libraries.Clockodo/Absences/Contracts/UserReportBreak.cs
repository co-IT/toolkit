using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class UserReportBreak
    {
        [JsonProperty("since")]
        public DateTime Start { get; set; }

        [JsonProperty("until")]
        public DateTime End { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
    }
}
