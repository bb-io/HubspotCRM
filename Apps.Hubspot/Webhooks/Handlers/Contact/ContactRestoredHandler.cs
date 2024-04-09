using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.restore";

    public ContactRestoredHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}