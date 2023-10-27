using Apps.Hubspot.Crm.Models.Associations.Request.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Request;

public class DeleteAssociationDefinitionRequest : AssociationRequest
{
    [Display("Association type ID")]
    public string AssociationTypeId { get; set; }
}