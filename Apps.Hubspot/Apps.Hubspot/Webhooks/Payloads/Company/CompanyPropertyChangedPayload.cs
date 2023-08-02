using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Payloads.Company;

public class CompanyPropertyChangedPayload : GenericPayload
{
    [Display("Property name")]
    public string PropertyName { get; set; }  
    
    [Display("Property value")]
    public string PropertyValue { get; set; }    
}