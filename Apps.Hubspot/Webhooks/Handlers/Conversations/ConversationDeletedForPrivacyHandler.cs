using Blackbird.Applications.Sdk.Common.Invocation;
namespace Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;

public class ConversationDeletedForPrivacyHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "conversation.privacyDeletion";

    public ConversationDeletedForPrivacyHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}