using Apps.Hubspot.Crm.Webhooks.Handlers;
using Apps.Hubspot.Crm.Webhooks.Handlers.Contact;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Contacts : BaseWebhookList
    {
        [Webhook("On contact created", typeof(ContactCreationHandler), Description = "On contact created")]
        public Task<WebhookResponse<GenericPayload>> ContactCreatedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On contact deleted", typeof(ContactDeletionHandler), Description = "On contact deleted")]
        public Task<WebhookResponse<GenericPayload>> ContactDeletedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);
    }
}
