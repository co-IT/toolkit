using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class DayReport
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("weekday")]
        public int WeekDay { get; set; }

        [JsonProperty("diff")]
        public int Difference { get; set; }

        [JsonProperty("nonbusiness")]
        public bool IsHoliday { get; set; }

        [JsonProperty("count_sick")]
        public bool IsSick { get; set; }

        [JsonProperty("count_holidays")]
        public bool IsPersonalHoliday { get; set; }

        [JsonProperty("target")]
        public int TargetHours { get; set; }

        [JsonProperty("hours")]
        public int WorkedHours { get; set; }

        [JsonProperty("breaks")]
        public IEnumerable<UserReportBreak> Breaks { get; set; }

        [JsonProperty("work_start")]
        public DateTime WorkStarted { get; set; }

        [JsonProperty("work_end")]
        public DateTime WorkEnded { get; set; }

        [JsonProperty("count_reduction_used")]
        public int ReductionByOvertime { get; set; }
    }
}
