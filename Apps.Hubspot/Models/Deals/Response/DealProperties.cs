using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Deals.Response;

public class DealProperties
{
    public string? Amount { get; set; }
    public string? Dealname { get; set; }
    public string? Dealstage { get; set; }
    public string? Pipeline { get; set; }
    
    [JsonProperty("hubspot_owner_id")]
    public string? HubspotOwnerId { get; set; }
    public DateTime? Closedate { get; set; }
}