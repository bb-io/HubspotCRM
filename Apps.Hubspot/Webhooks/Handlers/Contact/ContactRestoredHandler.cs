namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.restore";

    public ContactRestoredHandler() : base(SubscriptionEvent) { }
}