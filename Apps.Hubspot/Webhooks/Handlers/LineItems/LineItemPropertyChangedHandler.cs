using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;

public class LineItemPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "line_item.propertyChange";

    public LineItemPropertyChangedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}