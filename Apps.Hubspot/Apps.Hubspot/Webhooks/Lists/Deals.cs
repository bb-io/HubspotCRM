using Apps.Hubspot.Crm.Webhooks.Handlers.Deal;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Deals : BaseWebhookList
    {
        [Webhook("On deal created", typeof(DealCreationHandler), Description = "On deal created")]
        public Task<WebhookResponse<GenericPayload>> DealCreatedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On deal deleted", typeof(DealDeletionHandler), Description = "On deal deleted")]
        public Task<WebhookResponse<GenericPayload>> DealDeletedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);
    }
}
