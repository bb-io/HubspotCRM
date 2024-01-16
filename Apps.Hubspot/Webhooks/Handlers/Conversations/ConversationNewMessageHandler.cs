using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationNewMessageHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.newMessage";

    public ConversationNewMessageHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}