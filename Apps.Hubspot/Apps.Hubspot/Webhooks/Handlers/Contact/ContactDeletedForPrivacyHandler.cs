namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactDeletedForPrivacyHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.privacyDeletion";

    public ContactDeletedForPrivacyHandler() : base(SubscriptionEvent) { }
}