namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationDeletedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.deletion";

    public ConversationDeletedHandler() : base(SubscriptionEvent)
    {
    }
}