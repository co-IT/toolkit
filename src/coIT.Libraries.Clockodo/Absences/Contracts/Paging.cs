using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class Paging
    {
        [JsonProperty("items_per_page")]
        public long ItemsPerPage { get; set; }

        [JsonProperty("current_page")]
        public long CurrentPage { get; set; }

        [JsonProperty("count_pages")]
        public long CountPages { get; set; }

        [JsonProperty("count_items")]
        public long CountItems { get; set; }
    }
}
