namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.creation";

    public ConversationCreatedHandler() : base(SubscriptionEvent)
    {
    }
}