using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.merge";

    public ContactMergedHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}