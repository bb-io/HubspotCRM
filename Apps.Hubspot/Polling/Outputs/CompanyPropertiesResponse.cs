using Apps.Hubspot.Crm.Webhooks.Payloads;

namespace Apps.Hubspot.Crm.Polling.Outputs;

public record CompanyPropertiesResponse(PropertyChangedPayload[] Properties);