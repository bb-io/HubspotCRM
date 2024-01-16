using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketDeletionHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.deletion";

    public TicketDeletionHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}