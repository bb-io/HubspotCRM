using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactDeletionHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.deletion";

    public ContactDeletionHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}