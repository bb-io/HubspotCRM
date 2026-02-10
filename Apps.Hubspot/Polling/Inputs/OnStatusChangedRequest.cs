using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Polling.Inputs;

public class OnStatusChangedRequest
{
    [Display("Deal ID"), DataSource(typeof(DealDataHandler))]
    public string? DealId { get; set; }

    [Display("Status"), DataSource(typeof(DealStageDataHandler))]
    public string Status { get; set; }
}
