namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.associationChange";

    public TicketAssociationChangedHandler() : base(SubscriptionEvent)
    {
    }
}