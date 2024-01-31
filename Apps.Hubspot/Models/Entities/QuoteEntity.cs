using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Quotes.Response;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class QuoteEntity
{
    [Display("Quote ID")]
    public string? Id { get; set; }
    
    [Display("Title")]
    public string? HsTitle { get; set; }

    [Display("Expiration date")]
    public DateTime? HsExpirationDate { get; set; }
    
    public QuoteEntity(BaseObjectWithProperties<QuoteProperties> obj)
    {
        Id = obj.Id;
        HsTitle = obj.Properties.HsTitle;
        HsExpirationDate = obj.Properties.HsExpirationDate;
    }
}