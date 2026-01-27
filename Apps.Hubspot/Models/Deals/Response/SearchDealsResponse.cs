using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Deals.Response
{
    public class SearchDealsResponse
    {
        [Display("Deals")]
        public IEnumerable<DealEntity> Deals { get; set; } = Array.Empty<DealEntity>();
    }
}
