using Apps.Hubspot.Crm.Webhooks.Handlers.Company;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists;

[WebhookList]
public class Companies : BaseWebhookList
{
    [Webhook("On company created", typeof(CompanyCreationHandler), Description = "On company created")]
    public Task<WebhookResponse<GenericPayload>> OnCompanyCreated(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);

    [Webhook("On company deleted", typeof(CompanyDeletionHandler), Description = "On company deleted")]
    public Task<WebhookResponse<GenericPayload>> OnCompanyDeleted(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);

    [Webhook("On company merged", typeof(CompanyMergeHandler), Description = "On company merged")]
    public Task<WebhookResponse<GenericPayload>> OnCompanyMerged(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);

    [Webhook("On company association changed", typeof(CompanyAssociationChangedHandler),
        Description = "On company association changed")]
    public Task<WebhookResponse<AssociationChangedPayload>> OnCompanyAssociationChanged(
        WebhookRequest webhookRequest, [WebhookParameter] AssociationChangedInput input)
        => HandleAssociationChangedWebhookResponse(webhookRequest, input);

    [Webhook("On company property changed", typeof(CompanyPropertyChangedHandler),
        Description = "On company property changed")]
    public Task<WebhookResponse<PropertyChangedPayload>> OnCompanyPropertyChanged(
        WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
        => HandlePropertyChangedWebhookResponse(webhookRequest, input);
}