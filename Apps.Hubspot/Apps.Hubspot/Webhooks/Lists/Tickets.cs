using Apps.Hubspot.Crm.Webhooks.Handlers.Ticket;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Tickets : BaseWebhookList
    {
        [Webhook("On ticket created", typeof(TicketCreationHandler), Description = "On ticket created")]
        public Task<WebhookResponse<GenericPayload>> OnTicketCreated(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On ticket deleted", typeof(TicketDeletionHandler), Description = "On ticket deleted")]
        public Task<WebhookResponse<GenericPayload>> OnTicketDeleted(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On ticket merged", typeof(TicketMergedHandler), Description = "On ticket merged")]
        public Task<WebhookResponse<GenericPayload>> OnTicketMerged(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On ticket restored", typeof(TicketRestoredHandler), Description = "On ticket restored")]
        public Task<WebhookResponse<GenericPayload>> OnTicketRestored(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On ticket association changed", typeof(TicketAssociationChangedHandler),
            Description = "On ticket association changed")]
        public Task<WebhookResponse<AssociationChangedPayload>> OnTicketAssociationChanged(
            WebhookRequest webhookRequest, [WebhookParameter] AssociationChangedInput input)
            => HandleAssociationChangedWebhookResponse(webhookRequest, input);

        [Webhook("On ticket property changed", typeof(TicketPropertyChangedHandler),
            Description = "On ticket property changed")]
        public Task<WebhookResponse<PropertyChangedPayload>> OnTicketPropertyChanged(
            WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
            => HandlePropertyChangedWebhookResponse(webhookRequest, input);
    }
}