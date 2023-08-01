using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities
{
    public class CompanyEntity
    {
        [Display("ID")]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Domain { get; set; }

        [Display("Contact IDs")]
        public IEnumerable<ContactId>? ContactIds { get; set; }
    }

    public class ContactId
    {
        [Display("ID")]
        public string Id { get; set; }
    }
}
