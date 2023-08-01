namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact
{
    public class ContactCreationHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "contact.creation";

        public ContactCreationHandler() : base(SubscriptionEvent) { }
    }
}
