using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Payloads;

public class PropertyChangedPayload : GenericPayload
{
    [Display("Property name")]
    public string PropertyName { get; set; }  
    
    [Display("Property value")]
    public string PropertyValue { get; set; }    
}