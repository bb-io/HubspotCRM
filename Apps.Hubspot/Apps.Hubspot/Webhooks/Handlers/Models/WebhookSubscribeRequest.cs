namespace Apps.Hubspot.Crm.Webhooks.Handlers.Models
{
    internal class WebhookSubscribeRequest
    {
        public bool Active { get; set; }
        public string EventType { get; set; }
        public string PropertyName { get; set; }
    }
}
