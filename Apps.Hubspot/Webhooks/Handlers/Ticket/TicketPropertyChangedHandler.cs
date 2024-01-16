using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.propertyChange";

    public TicketPropertyChangedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}