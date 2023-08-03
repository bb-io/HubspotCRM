using Apps.Hubspot.Crm.Webhooks.Handlers.Deal;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Deals : BaseWebhookList
    {
        [Webhook("On deal created", typeof(DealCreationHandler), Description = "On deal created")]
        public Task<WebhookResponse<GenericPayload>> OnDealCreated(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On deal deleted", typeof(DealDeletionHandler), Description = "On deal deleted")]
        public Task<WebhookResponse<GenericPayload>> OnDealDeleted(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On deal merged", typeof(DealMergedHandler), Description = "On deal merged")]
        public Task<WebhookResponse<GenericPayload>> OnDealMerged(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On deal restored", typeof(DealRestoredHandler), Description = "On deal restored")]
        public Task<WebhookResponse<GenericPayload>> OnDealRestored(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On deal association changed", typeof(DealAssociationChangedHandler),
            Description = "On deal association changed")]
        public Task<WebhookResponse<AssociationChangedPayload>> OnDealAssociationChanged(WebhookRequest webhookRequest)
            => HandleWebhookResponse<AssociationChangedPayload>(webhookRequest);

        [Webhook("On deal property changed", typeof(DealPropertyChangedHandler),
            Description = "On deal property changed")]
        public Task<WebhookResponse<PropertyChangedPayload>> OnDealPropertyChanged(
            WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
            => HandlePropertyChangedWebhookResponse(webhookRequest, input);
    }
}