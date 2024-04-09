using Blackbird.Applications.Sdk.Common.Invocation;
namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;

public class TicketMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "ticket.merge";

    public TicketMergedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}