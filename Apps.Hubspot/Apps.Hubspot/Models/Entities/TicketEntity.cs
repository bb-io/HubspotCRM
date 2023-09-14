using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Tickets.Response;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class TicketEntity
{
    public TicketEntity(BaseObjectWithProperties<TicketProperties> response)
    {
        Id = response.Id;
        Description = response.Properties.Content;
        Subject = response.Properties.Subject;
        Priority = response.Properties.hs_ticket_priority;
        CompanyIds = response.Associations?["companies"].GetDistinctIds();
    }

    [Display("ID")]
    public string? Id { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }

    [Display("Company IDs")]
    public IEnumerable<string>? CompanyIds { get; set; }
    public string? Priority { get; set; }
}