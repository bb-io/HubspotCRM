using Apps.Hubspot.Crm.Webhooks.Handlers.Company;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json;

namespace Apps.Hubspot.Crm.Webhooks
{
    [WebhookList]
    public class Companies
    {
        [Webhook("On company created", typeof(CompanyCreationHandler), Description = "On company created")]
        public async Task<WebhookResponse<GenericPayload>> CompanyCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<GenericPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<GenericPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }

        [Webhook("On company deleted", typeof(CompanyDeletionHandler), Description = "On company deleted")]
        public async Task<WebhookResponse<GenericPayload>> CompanyDeletedHandler(WebhookRequest webhookRequest)
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
