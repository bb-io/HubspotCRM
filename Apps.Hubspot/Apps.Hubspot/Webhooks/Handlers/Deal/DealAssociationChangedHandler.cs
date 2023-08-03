namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.associationChange";

    public DealAssociationChangedHandler() : base(SubscriptionEvent) { }
}