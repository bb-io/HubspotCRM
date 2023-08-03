namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.associationChange";

    public LineItemAssociationChangedHandler() : base(SubscriptionEvent)
    {
    }
}