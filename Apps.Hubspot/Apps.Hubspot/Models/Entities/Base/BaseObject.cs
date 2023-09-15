using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities.Base;

public class BaseObject
{
    [Display("ID")] public string Id { get; set; }
    [Display("Created at")] public DateTime CreatedAt { get; set; }
    [Display("Updated at")] public DateTime UpdatedAt { get; set; }
}