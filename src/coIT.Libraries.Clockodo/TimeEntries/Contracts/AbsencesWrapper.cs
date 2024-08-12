using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    internal class AbsencesWrapper
    {
        [JsonProperty("absences")]
        public RawAbsence[] Absences { get; set; }
    }
}
