using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities
{
    public class TicketEntity
    {
        [Display("ID")]
        public string? Id { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }

        [Display("Company ID")]
        public IEnumerable<string>? CompanyIds { get; set; }
        public string? Priority { get; set; }
    }
}
