namespace Apps.Hubspot.Crm.Polling.Memory;

public record DateTimeMemory
{
    public DateTime? LastPollingTime { get; set; }
}
