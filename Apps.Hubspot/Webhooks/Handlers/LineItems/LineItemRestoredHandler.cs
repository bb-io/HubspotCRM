namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.restore";

    public LineItemRestoredHandler() : base(SubscriptionEvent)
    {
    }
}