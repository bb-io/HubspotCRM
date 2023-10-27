using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class AssociationEntity
{

    [Display("ID")]
    public string? Id { get; set; }
    
    public string? Type { get; set; }
}