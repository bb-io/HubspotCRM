using Apps.Hubspot.Crm.Webhooks.Handlers.Products;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists;

[WebhookList]
public class Products : BaseWebhookList
{
    [Webhook("On product created", typeof(ProductCreatedHandler), Description = "On product created")]
    public Task<WebhookResponse<GenericPayload>> OnProductCreated(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On product deleted", typeof(ProductDeletedHandler), Description = "On product deleted")]
    public Task<WebhookResponse<GenericPayload>> OnProductDeleted(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On product merged", typeof(ProductMergedHandler), Description = "On product merged")]
    public Task<WebhookResponse<GenericPayload>> OnProductMerged(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On product restored", typeof(ProductRestoredHandler), Description = "On product restored")]
    public Task<WebhookResponse<GenericPayload>> OnProductRestored(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On product property changed", typeof(ProductPropertyChangedHandler),
        Description = "On product property changed")]
    public Task<WebhookResponse<PropertyChangedPayload>> OnProductPropertyChanged(
        WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
        => HandlePropertyChangedWebhookResponse(webhookRequest, input);
}