namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductRestoredHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.restore";

    public ProductRestoredHandler() : base(SubscriptionEvent)
    {
    }
}