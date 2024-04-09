using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemDeletedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.deletion";

    public LineItemDeletedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}