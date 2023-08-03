using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Inputs;

public class PropertyChangedInput
{
    [Display("Property name")]
    public string? Property { get; set; }
}