using Apps.Hubspot.Crm.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Associations.Request.Base;

public class AssociationRequest
{
    [JsonIgnore]
    [Display("Source object type")]
    [DataSource(typeof(AssociationTypeHandler))]
    public string FromObjectType { get; set; }

    [JsonIgnore]
    [Display("Target object type")]
    [DataSource(typeof(AssociationTypeHandler))]
    public string ToObjectType { get; set; }
}