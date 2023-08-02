using Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Tickets : BaseWebhookList
    {
        [Webhook("On ticket created", typeof(TicketCreationHandler), Description = "On ticket created")]
        public Task<WebhookResponse<GenericPayload>> TicketCreatedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On ticket deleted", typeof(TicketDeletionHandler), Description = "On ticket deleted")]
        public Task<WebhookResponse<GenericPayload>> TicketDeletedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);
    }
}
