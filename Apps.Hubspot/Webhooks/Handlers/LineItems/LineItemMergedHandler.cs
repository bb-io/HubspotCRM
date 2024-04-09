using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.merge";

    public LineItemMergedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}