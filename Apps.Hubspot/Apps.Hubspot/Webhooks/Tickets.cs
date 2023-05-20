using Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json;

namespace Apps.Hubspot.Crm.Webhooks
{
    [WebhookList]
    public class Tickets
    {
        [Webhook("On ticket created", typeof(TicketCreationHandler), Description = "On ticket created")]
        public async Task<WebhookResponse<GenericPayload>> TicketCreatedHandler(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<GenericPayload>(webhookRequest.Body.ToString());
            if (data is null) { throw new InvalidCastException(nameof(webhookRequest.Body)); }
            return new WebhookResponse<GenericPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }

        [Webhook("On ticket deleted", typeof(TicketDeletionHandler), Description = "On ticket deleted")]
        public async Task<WebhookResponse<GenericPayload>> TicketDeletedHandler(WebhookRequest webhookRequest)
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
