namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductCreatedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.creation";

    public ProductCreatedHandler() : base(SubscriptionEvent)
    {
    }
}