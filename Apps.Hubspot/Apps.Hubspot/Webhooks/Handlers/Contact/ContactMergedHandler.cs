namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.merge";

    public ContactMergedHandler() : base(SubscriptionEvent) { }
}