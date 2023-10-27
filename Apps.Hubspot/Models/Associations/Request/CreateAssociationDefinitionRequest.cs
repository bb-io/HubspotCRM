using Apps.Hubspot.Crm.Models.Associations.Request.Base;

namespace Apps.Hubspot.Crm.Models.Associations.Request;

public class CreateAssociationDefinitionRequest : AssociationRequest
{
    public string Label { get; set; }
    public string Name { get; set; }
}