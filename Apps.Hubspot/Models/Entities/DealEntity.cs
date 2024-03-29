﻿using Apps.Hubspot.Crm.Models.Deals.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class DealEntity
{
    [Display("Deal ID")]
    public string? Id { get; set; }

    public string? Amount { get; set; }
    [Display("Deal name")]
    public string? Dealname { get; set; }

    [Display("Deal stage")]
    public string? Dealstage { get; set; }

    [Display("Pipeline")]
    public string? Pipeline { get; set; }

    [Display("Hubspot owner ID")]
    public string? HubspotOwnerId { get; set; }

    [Display("Date closed")]
    public DateTime? Closedate { get; set; }

    public DealEntity()
    {
        Id = null;
        Amount = null;
        Dealname = null;
        Dealstage = null;
        Pipeline = string.Empty;
        HubspotOwnerId = null;
        Closedate = null;
    }
    
    public DealEntity(BaseObjectWithProperties<DealProperties> obj)
    {
        Id = obj.Id;
        Amount = obj.Properties.Amount;
        Dealname = obj.Properties.Dealname;
        Dealstage = obj.Properties.Dealstage;
        Pipeline = obj.Properties.Pipeline;
        HubspotOwnerId = obj.Properties.HubspotOwnerId;
        Closedate = obj.Properties.Closedate;
    }
}