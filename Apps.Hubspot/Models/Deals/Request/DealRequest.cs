using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Models.Deals.Request;

public class DealRequest
{
    [Display("Deal")]
    [DataSource(typeof(DealDataHandler))]
    public string DealId { get; set; }
}