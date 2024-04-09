using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.creation";

    public ProductCreatedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}