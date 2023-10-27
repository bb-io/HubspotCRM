using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Webhooks.Payloads;

public class GenericPayload
{
    [Display("ID")]
    public string ObjectId { get; set; }
}