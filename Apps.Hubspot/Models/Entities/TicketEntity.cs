using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Tickets.Response;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class TicketEntity
{
    [Display("Ticket ID")] public string? Id { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }

    [Display("Company IDs")] public IEnumerable<string>? CompanyIds { get; set; }
    public string? Priority { get; set; }

    public TicketEntity(BaseObjectWithProperties<TicketProperties> response)
    {
        Id = response.Id;
        Description = response.Properties.Content;
        Subject = response.Properties.Subject;
        Priority = response.Properties.HsTicketPriority;
        CompanyIds = response.Associations?["companies"].GetDistinctIds();
    }
}