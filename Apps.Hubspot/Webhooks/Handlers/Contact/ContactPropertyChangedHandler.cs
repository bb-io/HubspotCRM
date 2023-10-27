namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.propertyChange";

    public ContactPropertyChangedHandler() : base(SubscriptionEvent) { }
}