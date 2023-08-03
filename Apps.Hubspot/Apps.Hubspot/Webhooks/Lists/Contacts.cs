using Apps.Hubspot.Crm.Webhooks.Handlers.Contact;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Contacts : BaseWebhookList
    {
        [Webhook("On contact created", typeof(ContactCreationHandler), Description = "On contact created")]
        public Task<WebhookResponse<GenericPayload>> OnContactCreated(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On contact deleted", typeof(ContactDeletionHandler), Description = "On contact deleted")]
        public Task<WebhookResponse<GenericPayload>> OnContactDeleted(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On contact deleted for privacy", typeof(ContactDeletedForPrivacyHandler), Description = "On contact deleted for privacy")]
        public Task<WebhookResponse<AssociationChangedPayload>> OnContactDeletedForPrivacy(WebhookRequest webhookRequest)
            => HandleWebhookResponse<AssociationChangedPayload>(webhookRequest);

        [Webhook("On contact association changed", typeof(ContactAssociationChangedHandler), Description = "On contact association changed")]
        public Task<WebhookResponse<AssociationChangedPayload>> OnContactAssociationChanged(
            WebhookRequest webhookRequest, [WebhookParameter] AssociationChangedInput input)
            => HandleAssociationChangedWebhookResponse(webhookRequest, input);

        [Webhook("On contact restored", typeof(ContactRestoredHandler), Description = "On contact restored")]
        public Task<WebhookResponse<GenericPayload>> OnContactRestored(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On contact merged", typeof(ContactMergedHandler), Description = "On contact merged")]
        public Task<WebhookResponse<GenericPayload>> OnContactMerged(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On contact property changed", typeof(ContactPropertyChangedHandler), Description = "On contact property changed")]
        public Task<WebhookResponse<PropertyChangedPayload>> OnContactPropertyChanged(
            WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
            => HandlePropertyChangedWebhookResponse(webhookRequest, input);
    }
}
