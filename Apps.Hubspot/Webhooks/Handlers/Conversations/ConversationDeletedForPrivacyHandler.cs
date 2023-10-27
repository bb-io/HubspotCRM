namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationDeletedForPrivacyHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.privacyDeletion";

    public ConversationDeletedForPrivacyHandler() : base(SubscriptionEvent)
    {
    }
}