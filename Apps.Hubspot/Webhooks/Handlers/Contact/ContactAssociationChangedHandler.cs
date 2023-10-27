namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact;

public class ContactAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "contact.associationChange";

    public ContactAssociationChangedHandler() : base(SubscriptionEvent) { }
}