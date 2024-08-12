using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class ClockodoEmployees
    {
        [JsonProperty("users")]
        public IEnumerable<EmployeeInfo> Employees { get; set; }
    }
}
