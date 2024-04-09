using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Quotes.Response;

public class QuoteProperties
{
    [Display("Title")]
    [JsonProperty("hs_title")] 
    public string HsTitle { get; set; }

    [Display("Expiration date")]
    [JsonProperty("hs_expiration_date")] 
    public DateTime? HsExpirationDate { get; set; }
}