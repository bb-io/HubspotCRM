namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.propertyChange";

    public DealPropertyChangedHandler() : base(SubscriptionEvent) { }
}