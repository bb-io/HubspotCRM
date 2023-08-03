using Apps.Hubspot.Crm.Webhooks.Handlers.LineItems;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists;

[WebhookList]
public class LineItems : BaseWebhookList
{
    [Webhook("On line item created", typeof(LineItemCreatedHandler), Description = "On line item created")]
    public Task<WebhookResponse<GenericPayload>> OnLineItemCreated(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On line item deleted", typeof(LineItemDeletedHandler), Description = "On line item deleted")]
    public Task<WebhookResponse<GenericPayload>> OnLineItemDeleted(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On line item merged", typeof(LineItemMergedHandler), Description = "On line item merged")]
    public Task<WebhookResponse<GenericPayload>> OnLineItemMerged(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On line item restored", typeof(LineItemRestoredHandler), Description = "On line item restored")]
    public Task<WebhookResponse<GenericPayload>> OnLineItemRestored(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On line item association changed", typeof(LineItemAssociationChangedHandler), Description = "On line item association changed")]
    public Task<WebhookResponse<AssociationChangedPayload>> OnLineItemAssociationChanged(WebhookRequest webhookRequest)
        => HandleWebhookResponse<AssociationChangedPayload>(webhookRequest);

    [Webhook("On line item property changed", typeof(LineItemPropertyChangedHandler),
        Description = "On line item property changed")]
    public Task<WebhookResponse<PropertyChangedPayload>> OnLineItemPropertyChanged(
        WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
        => HandlePropertyChangedWebhookResponse(webhookRequest, input);
}