using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactDeletedForPrivacyHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.privacyDeletion";

    public ContactDeletedForPrivacyHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}