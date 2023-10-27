namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.creation";

    public LineItemCreatedHandler() : base(SubscriptionEvent)
    {
    }
}