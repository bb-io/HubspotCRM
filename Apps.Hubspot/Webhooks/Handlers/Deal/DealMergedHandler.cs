namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.merge";

    public DealMergedHandler() : base(SubscriptionEvent) { }
}