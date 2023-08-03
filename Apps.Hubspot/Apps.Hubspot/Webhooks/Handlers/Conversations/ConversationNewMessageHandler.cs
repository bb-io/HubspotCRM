namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationNewMessageHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.newMessage";

    public ConversationNewMessageHandler() : base(SubscriptionEvent)
    {
    }
}