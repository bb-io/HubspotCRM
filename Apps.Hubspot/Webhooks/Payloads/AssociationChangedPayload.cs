using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Payloads;

public class AssociationChangedPayload : GenericPayload
{
    [Display("Source object ID")]
    public string FromObjectId { get; set; }
    
    [Display("Target object ID")]
    public string ToObjectId { get; set; }
    
    [Display("Association type")]
    public string AssociationType { get; set; }
    
    [Display("Association removed")]
    public bool AssociationRemoved { get; set; }

    [Display("Is primary association")]
    public bool IsPrimaryAssociation { get; set; }
}