using Apps.Hubspot.Crm.Models.Deals.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class DealEntity
{
    public DealEntity(BaseObjectWithProperties<DealProperties> obj)
    {
        Id = obj.Id;
        Amount = obj.Properties.Amount;
        Dealname = obj.Properties.Dealname;
        Dealstage = obj.Properties.Dealstage;
        Pipeline = obj.Properties.Pipeline;
        Hubspot_owner_id = obj.Properties.Hubspot_owner_id;
        Closedate = obj.Properties.Closedate;
    }

    [Display("ID")]
    public string? Id { get; set; }

    public string? Amount { get; set; }
    [Display("Deal name")]
    public string? Dealname { get; set; }

    [Display("Deal stage")]
    public string? Dealstage { get; set; }

    [Display("Pipeline")]
    public string? Pipeline { get; set; }

    [Display("Hubspot owner ID")]
    public string? Hubspot_owner_id { get; set; }

    [Display("Date closed")]
    public DateTime? Closedate { get; set; }
}