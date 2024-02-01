using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Inputs;

public class AssociationChangedInput
{
    [Display("Association type")]
    public string? AssociationType { get; set; }

    [Display("Is primary assosiation")]
    public bool? IsPrimaryAssosiation { get; set; }

    [Display("Association removed")]
    public bool? AssociationRemoved { get; set; }
}
