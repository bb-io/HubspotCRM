using Apps.Hubspot.Crm.Models.Entities.Base;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models
{
    public class SearchResponse<TProperties>
    {
        [JsonProperty("results")]
        public List<BaseObjectWithProperties<TProperties>> Results { get; set; } = new();

        [JsonProperty("paging")]
        public Paging? Paging { get; set; }
    }

    public class Paging
    {
        [JsonProperty("next")]
        public Next? Next { get; set; }
    }

    public class Next
    {
        [JsonProperty("after")]
        public string? After { get; set; }
    }
}
