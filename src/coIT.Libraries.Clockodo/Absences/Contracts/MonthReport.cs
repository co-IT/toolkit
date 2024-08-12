using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class MonthReport
    {
        [JsonProperty("nr")]
        public int Index { get; set; }

        [JsonProperty("week_details")]
        public IEnumerable<WeekReport> Weeks { get; set; }
    }
}
