using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class TimeEntry
    {
        [JsonProperty("id")]
        public int StoreId { get; set; }

        [JsonProperty("users_id")]
        public int EmployeeId { get; set; }

        [JsonProperty("projects_id")]
        public int ProjectId { get; set; }

        [JsonProperty("customers_id")]
        public int CustomerId { get; set; }

        [JsonProperty("billable")]
        public bool Billable { get; set; }

        [JsonProperty("time_since")]
        public string TimeSince { get; set; }

        [JsonProperty("time_until")]
        public string TimeUntil { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("services_id")]
        public int ServiceId { get; set; }

        [JsonProperty("lumpSums_id")]
        public int LumpSumsId { get; set; }

        [JsonProperty("lumpSum")]
        public double LumpSum { get; set; }

        [JsonProperty("lumpSums_amount")]
        public double LumpSumsAmount { get; set; }

        [JsonProperty("is_clocking")]
        public bool IsClocking { get; set; }

        [JsonProperty("customers_name")]
        public string CustomerName { get; set; }

        [JsonProperty("users_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("services_name")]
        public string ServiceName { get; set; }

        [JsonProperty("projects_name")]
        public string ProjectName { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonIgnore]
        public CustomerInfo CustomerInfo { get; set; }

        [JsonIgnore]
        public EmployeeInfo EmployeeInfo { get; set; }

        [JsonIgnore]
        public ServiceInfo ServiceInfo { get; set; }
    }
}
