using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Inputs;

public class AssociationChangedInput
{
    [Display("Association type")]
    public string? AssociationType { get; set; }
}
