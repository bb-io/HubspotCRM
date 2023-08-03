namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductDeletedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.deletion";

    public ProductDeletedHandler() : base(SubscriptionEvent)
    {
    }
}