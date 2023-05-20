using Apps.Hubspot.Crm.Webhooks.Handlers.Deal;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json;

namespace Apps.Hubspot.Crm.Webhooks
{
    [WebhookList]
    public class Deals
    {
        [Webhook("On deal created", typeof(DealCreationHandler), Description = "On deal created")]
        public async Task<WebhookResponse<GenericPayload>> DealCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<GenericPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<GenericPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }

        [Webhook("On deal deleted", typeof(DealDeletionHandler), Description = "On deal deleted")]
        public async Task<WebhookResponse<GenericPayload>> DealDeletedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<GenericPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<GenericPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }
    }
}
