namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.merge";

    public TicketMergedHandler() : base(SubscriptionEvent)
    {
    }
}