using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts;

public class Absence
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("users_id")]
    public int UserId { get; set; }

    [JsonProperty("date_since")]
    public DateTime Start { get; set; }

    [JsonProperty("date_until")]
    public DateTime End { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("type")]
    public int TypeId { get; set; }

    public ClockodoAbsenceType AbsenceType { get; set; }

    [JsonProperty("note")]
    public string? Note { get; set; }

    [JsonProperty("count_days")]
    public float Duration { get; set; }

    [JsonProperty("count_hours")]
    public float? HoursOvertime { get; set; }

    [JsonProperty("date_enquired")]
    public string? DateEnquired { get; set; }

    [JsonProperty("date_approved")]
    public string? DateApproved { get; set; }

    [JsonProperty("approved_by")]
    public int? Approver { get; set; }

    [JsonProperty("sick_note")]
    public bool? HasElectronicalCertificateOfDisability { get; set; }
}
