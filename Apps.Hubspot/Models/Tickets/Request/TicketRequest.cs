using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Models.Tickets.Request;

public class TicketRequest
{
    [Display("Ticket ID")]
    [DataSource(typeof(TicketDataHandler))]
    public string TicketId { get; set; }
}