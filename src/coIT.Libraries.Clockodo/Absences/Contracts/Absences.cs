using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts;

public class Absences
{
    [JsonProperty("absences")]
    public IEnumerable<Absence> AllAbsences { get; set; }
}
