namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.propertyChange";

    public TicketPropertyChangedHandler() : base(SubscriptionEvent)
    {
    }
}