namespace Apps.Hubspot.Crm.Webhooks.Handlers.Models
{
    internal class WebhookSettings
    {
        public string TargerUrl { get; set; }
        public WebhookThrottling Throttling { get; set; }
    }
}
