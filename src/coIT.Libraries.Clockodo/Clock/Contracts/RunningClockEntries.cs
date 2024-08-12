using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Clock.Contracts;

internal class RunningClockEntriesDto
{
    [JsonProperty("running")]
    public RunningClockDto? Running { get; set; }
}

public class RunningClockDto
{
    public int Id { get; set; }

    [JsonProperty("time_since")]
    public DateTimeOffset TimeSince { get; set; }

    [JsonProperty("customers_name")]
    public string CustomerName { get; set; }

    [JsonProperty("projects_name")]
    public string ProjectName { get; set; }

    [JsonProperty("services_name")]
    public string ServiceName { get; set; }

    [JsonProperty("text")]
    public string Description { get; set; }
}
