using Apps.Hubspot.Crm.Models.Associations.Request.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Request;

public class ManageAssociationRequest : AssociationRequest
{
    [Display("Source object ID")] public string FromObjectId { get; set; }
    [Display("Target object ID")] public string ToObjectId { get; set; }
}