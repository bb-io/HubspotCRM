using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Tickets.Request;

public class CreateTicketRequest
{
    [JsonProperty("hs_pipeline_stage")]
    [Display("HubSpot pipeline stage")]
    public string HsPipelineStage { get; set; }
    
    [JsonProperty("hs_pipeline")]
    [Display("HubSpot pipeline")]
    public string? HsPipeline { get; set; }

    [JsonProperty("hs_ticket_priority")]
    [Display("HubSpot ticket priority")]
    public string? HsTicketPriority { get; set; }

    [JsonProperty("hubspot_owner_id")]
    [Display("HubSpot owner ID")]
    public string? HubSpotOwnerId { get; set; }

    [JsonProperty("subject")]
    [Display("Subject")]
    public string? Subject { get; set; }
}