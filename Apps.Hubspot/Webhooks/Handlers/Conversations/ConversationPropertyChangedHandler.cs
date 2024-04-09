using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.propertyChange";

    public ConversationPropertyChangedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}