using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class Project
    {
        [JsonProperty("project")]
        public ProjectInfo ProjectInfo { get; set; }
    }
}
