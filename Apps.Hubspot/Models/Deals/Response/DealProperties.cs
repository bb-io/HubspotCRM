using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Deals.Response;

public class DealProperties
{
    public string? Amount { get; set; }

    [Display("Deal name")]
    public string? Dealname { get; set; }

    [Display("Deal stage")]
    public string? Dealstage { get; set; }
    public string? Pipeline { get; set; }
    
    [JsonProperty("hubspot_owner_id")]
    [Display("Hubspot owner ID")]
    public string? HubspotOwnerId { get; set; }

    [Display("Closed date")]
    public DateTime? Closedate { get; set; }
}