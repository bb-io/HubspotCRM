using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class QuoteEntity
{
    public QuoteEntity(BaseObjectWithProperties<QuoteProperties> obj)
    {
        Id = obj.Id;
        hs_title = obj.Properties.hs_title;
        hs_expiration_date = obj.Properties.hs_expiration_date;
    }

    [Display("ID")]
    public string? Id { get; set; }
    
    [Display("Hubspot title")]
    public string? hs_title { get; set; }

    [Display("Hubspot expiration date")]
    public DateTime hs_expiration_date { get; set; }
}