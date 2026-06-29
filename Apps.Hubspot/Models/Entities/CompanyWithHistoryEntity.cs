using Apps.Hubspot.Crm.Models.Entities.Base;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Entities;

public class CompanyWithHistoryEntity : BaseObject
{
    [JsonProperty("propertiesWithHistory")]
    public Dictionary<string, List<PropertyHistoryEntry>>? PropertiesWithHistory { get; set; }
}