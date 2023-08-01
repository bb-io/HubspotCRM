namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket
{
    public class TicketCreationHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "ticket.creation";

        public TicketCreationHandler() : base(SubscriptionEvent) { }
    }
}
