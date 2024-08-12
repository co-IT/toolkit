using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Clock.Contracts;

public class StartClockDto
{
    [JsonProperty("customers_id")]
    public int CustomerId { get; set; }

    [JsonProperty("projects_id")]
    public int? ProjectsId { get; set; }

    [JsonProperty("services_id")]
    public int? ServicesId { get; set; }

    [JsonProperty("text")]
    public string Description { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }
}
