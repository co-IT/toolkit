using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class WeekReport
    {
        [JsonProperty("nr")]
        public int Index { get; set; }

        [JsonProperty("day_details")]
        public IEnumerable<DayReport> Days { get; set; }
    }
}
