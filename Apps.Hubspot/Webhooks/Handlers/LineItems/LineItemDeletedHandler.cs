namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemDeletedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.deletion";

    public LineItemDeletedHandler() : base(SubscriptionEvent)
    {
    }
}