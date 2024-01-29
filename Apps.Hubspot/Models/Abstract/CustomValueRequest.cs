using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Abstract;

public abstract class CustomValueRequest
{
    [Display("Custom property name")]
    public string CustomPropertyName { get; set; }

    [Display("Custom property value")]
    public string CustomPropertyValue { get; set; }
}
