using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models
{
    public class Company
    {
        public string? Domain { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Industry { get; set; }
        public string? Phone { get; set; }
        public string? State { get; set; }
        
        [Display("Lifecycle stage")]
        public string? Lifecyclestage { get; set; }
    }
}
