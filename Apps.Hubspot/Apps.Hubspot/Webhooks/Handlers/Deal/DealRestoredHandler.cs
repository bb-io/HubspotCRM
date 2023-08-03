namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.restore";

    public DealRestoredHandler() : base(SubscriptionEvent) { }
}