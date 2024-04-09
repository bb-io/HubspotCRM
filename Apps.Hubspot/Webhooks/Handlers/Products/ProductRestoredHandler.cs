using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.restore";

    public ProductRestoredHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}