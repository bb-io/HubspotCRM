using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Quotes.Response;

public class QuoteProperties
{
    [JsonProperty("hs_title")] public string HsTitle { get; set; }

    [JsonProperty("hs_expiration_date")] public DateTime HsExpirationDate { get; set; }
}