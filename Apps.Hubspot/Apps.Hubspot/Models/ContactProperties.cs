using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models
{
    public class ContactProperties
    {
        public string? Email { get; set; }
        
        [Display("First name")]
        public string? Firstname { get; set; }
        
        [Display("Last name")]
        public string? Lastname { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public string? Website { get; set; }
        
        [Display("Job title")]
        public string? Jobtitle { get; set; }
        
        [Display("Lifecycle stage")]
        public string? Lifecyclestage { get; set; }
    }
}
