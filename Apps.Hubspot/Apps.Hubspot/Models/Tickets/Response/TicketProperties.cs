using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Tickets.Response;

public class TicketProperties
{
    public string Content { get; set; }

    [Display("Hubspot priority")]
    [JsonProperty("hs_ticket_priority")]
    public string HsTicketPriority { get; set; }

    public string Subject { get; set; }
}