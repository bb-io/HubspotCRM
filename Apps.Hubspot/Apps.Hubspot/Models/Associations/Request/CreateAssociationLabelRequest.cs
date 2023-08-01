
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Request;

public class CreateAssociationLabelRequest : ManageAssociationRequest
{
    [Display("Is user defined")]
    public bool IsUserDefined { get; set; }
    
    [Display("Association type ID")]
    public string AssociationTypeId { get; set; }
}