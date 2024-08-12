using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class UserReport
    {
        [JsonProperty("users_id")]
        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public EmployeeInfo Employee { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int Year { get; set; }

        [JsonProperty("month_details")]
        public IEnumerable<MonthReport> Months { get; set; }
    }
}
