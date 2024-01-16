using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.creation";

    public LineItemCreatedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}