using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealDeletionHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.deletion";

    public DealDeletionHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}