using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.merge";

    public DealMergedHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}