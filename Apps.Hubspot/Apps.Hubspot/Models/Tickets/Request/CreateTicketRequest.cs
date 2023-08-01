using System.Text.Json.Serialization;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Tickets.Request;

public class CreateTicketRequest
{
    [JsonPropertyName("hs_pipeline_stage")]
    [Display("HubSpot pipeline stage")]
    public string HsPipelineStage { get; set; }
    
    [JsonPropertyName("hs_pipeline")]
    [Display("HubSpot pipeline")]
    public string? HsPipeline { get; set; }

    [JsonPropertyName("hs_ticket_priority")]
    [Display("HubSpot ticket priority")]
    public string? HsTicketPriority { get; set; }

    [JsonPropertyName("hubspot_owner_id")]
    [Display("HubSpot owner ID")]
    public string? HubSpotOwnerId { get; set; }

    [JsonPropertyName("subject")]
    [Display("Subject")]
    public string? Subject { get; set; }
}