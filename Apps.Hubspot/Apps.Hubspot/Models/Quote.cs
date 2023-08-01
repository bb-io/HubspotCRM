using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models
{
    public class Quote
    {
        [Display("Hubspot title")]
        public string? hs_title { get; set; }

        [Display("Hubspot expiration date")]
        public DateTime hs_expiration_date { get; set; }
    }
}
