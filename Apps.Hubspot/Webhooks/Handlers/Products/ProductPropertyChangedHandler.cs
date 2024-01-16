using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.propertyChange";

    public ProductPropertyChangedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}