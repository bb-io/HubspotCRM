using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductDeletedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.deletion";

    public ProductDeletedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}