namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.restore";

    public TicketRestoredHandler() : base(SubscriptionEvent)
    {
    }
}