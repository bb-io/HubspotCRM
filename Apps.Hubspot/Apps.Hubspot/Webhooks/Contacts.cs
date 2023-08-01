using Apps.Hubspot.Crm.Webhooks.Handlers.Contact;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Webhooks
{
    [WebhookList]
    public class Contacts
    {
        [Webhook("On contact created", typeof(ContactCreationHandler), Description = "On contact created")]
        public async Task<WebhookResponse<GenericPayload>> ContactCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<GenericPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<GenericPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }

        [Webhook("On contact deleted", typeof(ContactDeletionHandler), Description = "On contact deleted")]
        public async Task<WebhookResponse<GenericPayload>> ContactDeletedHandler(WebhookRequest webhookRequest)
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
