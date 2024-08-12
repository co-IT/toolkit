using Newtonsoft.Json;

namespace coIT.Libraries.TimeCard.DataContracts
{
    public class TimeCardAbsence
    {
        public int Employee { get; set; }

        public DateTime Date { get; set; }

        public string Department { get; set; }

        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("Info")]
        public string? Info { get; set; }

        [JsonProperty("Color")]
        public string? Color { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }

        [JsonProperty("Time")]
        public string? Time { get; set; }

        [JsonProperty("Reason")]
        public string? Reason { get; set; }

        public TimeCardAbsenceType AbsenceType { get; internal set; }

        [JsonProperty("InconsistentType")]
        public int InconsistentType { get; set; }

        [JsonProperty("Comment")]
        public string? Comment { get; set; }
    }
}
