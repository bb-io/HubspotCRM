using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.associationChange";

    public TicketAssociationChangedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}