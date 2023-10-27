namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.propertyChange";

    public LineItemPropertyChangedHandler() : base(SubscriptionEvent)
    {
    }
}