using System.Net;
using Apps.Hubspot.Crm.Webhooks.Handlers.Company;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Apps.Hubspot.Crm.Webhooks.Payloads.Company;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Webhooks.Lists
{
    [WebhookList]
    public class Companies : BaseWebhookList
    {
        [Webhook("On company created", typeof(CompanyCreationHandler), Description = "On company created")]
        public Task<WebhookResponse<GenericPayload>> CompanyCreatedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On company deleted", typeof(CompanyDeletionHandler), Description = "On company deleted")]
        public Task<WebhookResponse<GenericPayload>> CompanyDeletedHandler(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On company merged", typeof(CompanyMergeHandler), Description = "On company merged")]
        public Task<WebhookResponse<GenericPayload>> OnCompanyMerged(WebhookRequest webhookRequest)
            => HandleWebhookResponse<GenericPayload>(webhookRequest);

        [Webhook("On company association changed", typeof(CompanyAssociationChangedHandler),
            Description = "On company association changed")]
        public Task<WebhookResponse<CompanyAssociationChangedPayload>> OnCompanyAssociationChanged(
            WebhookRequest webhookRequest)
            => HandleWebhookResponse<CompanyAssociationChangedPayload>(webhookRequest);

        [Webhook("On company property changed", typeof(CompanyPropertyChangedHandler),
            Description = "On company property changed")]
        public Task<WebhookResponse<CompanyPropertyChangedPayload>> OnCompanyPropertyChanged(
            WebhookRequest webhookRequest,
            [WebhookParameter] [Display("Property name")]
            string? property)
        {
            var data = JsonConvert.DeserializeObject<CompanyPropertyChangedPayload>(webhookRequest.Body.ToString())
                       ?? throw new InvalidCastException(nameof(webhookRequest.Body));

            if (property is not null && property != data.PropertyName)
                return Task.FromResult(new WebhookResponse<CompanyPropertyChangedPayload>
                {
                    HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                    ReceivedWebhookRequestType = WebhookRequestType.Preflight
                });
            
            return Task.FromResult(new WebhookResponse<CompanyPropertyChangedPayload>
            {
                HttpResponseMessage = null,
                Result = data
            });
        }
    }
}