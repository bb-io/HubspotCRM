using System.Text.Json.Serialization;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Request.Base;

public class AssociationRequest
{
    [JsonIgnore]
    [Display("Source object type")]
    public string FromObjectType { get; set; }

    [JsonIgnore]
    [Display("Target object type")]
    public string ToObjectType { get; set; }
}