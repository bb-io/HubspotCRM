using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.propertyChange";

    public ContactPropertyChangedHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}