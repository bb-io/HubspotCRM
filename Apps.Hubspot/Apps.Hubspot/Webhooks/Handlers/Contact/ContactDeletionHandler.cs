namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact
{
    public class ContactDeletionHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "contact.deletion";

        public ContactDeletionHandler() : base(SubscriptionEvent) { }
    }
}
