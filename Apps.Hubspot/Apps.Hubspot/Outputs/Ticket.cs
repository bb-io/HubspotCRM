using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Outputs
{
    public class Ticket
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
