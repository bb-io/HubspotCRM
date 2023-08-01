using Apps.Hubspot.Crm.Models.Associations.Request.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Request;

public class ListAssociationsRequest : AssociationRequest
{
    [Display("Source object ID")] public string FromObjectId { get; set; }
}