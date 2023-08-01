using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Response;

public class AssociationResponse
{
    [Display("Target object ID")]
    public string ToObjectId { get; set; }
    
    [Display("Association types")]
    public List<AssociationType> AssociationTypes { get; set; }
}