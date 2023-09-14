using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Response;

public class AssociationType
{
    [Display("Type ID")]
    public string TypeId { get; set; }
    
    public string? Label { get; set; }
    
    public string Category { get; set; }
}