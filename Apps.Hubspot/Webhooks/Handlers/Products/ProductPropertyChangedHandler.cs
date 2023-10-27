namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.propertyChange";

    public ProductPropertyChangedHandler() : base(SubscriptionEvent)
    {
    }
}