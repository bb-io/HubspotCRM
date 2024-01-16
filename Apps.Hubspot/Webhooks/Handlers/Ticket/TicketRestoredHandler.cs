using Blackbird.Applications.Sdk.Common.Invocation;
namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.restore";

    public TicketRestoredHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}