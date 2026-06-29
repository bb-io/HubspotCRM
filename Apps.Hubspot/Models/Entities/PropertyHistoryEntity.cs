using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Entities;

public class PropertyHistoryEntry
{
    [JsonProperty("value")]
    public string? Value { get; set; }
    
    [JsonProperty("timestamp")] 
    public DateTime Timestamp { get; set; }
    
    [JsonProperty("sourceType")] 
    public string? SourceType { get; set; }
}