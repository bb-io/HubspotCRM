namespace Apps.Hubspot.Crm.Webhooks.Handlers.Models
{
    internal class WebhookSubscribeResponse
    {
        public IEnumerable<WebhookSubscription> Results { get; set; }
    }
}
