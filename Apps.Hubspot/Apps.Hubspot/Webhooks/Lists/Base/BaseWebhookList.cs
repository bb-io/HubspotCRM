using System.Net;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Webhooks.Lists.Base;

public class BaseWebhookList
{
    protected Task<WebhookResponse<T>> HandleWebhookResponse<T>(WebhookRequest webhookRequest) where T : class
    {
        var data = JsonConvert.DeserializeObject<T>(webhookRequest.Body.ToString())
                   ?? throw new InvalidCastException(nameof(webhookRequest.Body));

        return Task.FromResult(new WebhookResponse<T>
        {
            HttpResponseMessage = null,
            Result = data
        });
    }

    protected Task<WebhookResponse<PropertyChangedPayload>> HandlePropertyChangedWebhookResponse(
        WebhookRequest webhookRequest,
        [WebhookParameter] PropertyChangedInput input)
    {
        var data = JsonConvert.DeserializeObject<PropertyChangedPayload>(webhookRequest.Body.ToString())
                   ?? throw new InvalidCastException(nameof(webhookRequest.Body));

        if (input.Property is not null && input.Property != data.PropertyName)
            return Task.FromResult(new WebhookResponse<PropertyChangedPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });

        return Task.FromResult(new WebhookResponse<PropertyChangedPayload>
        {
            HttpResponseMessage = null,
            Result = data
        });
    }
    protected Task<WebhookResponse<AssociationChangedPayload>> HandleAssociationChangedWebhookResponse(
        WebhookRequest webhookRequest,
        [WebhookParameter] AssociationChangedInput input)
    {
        var data = JsonConvert.DeserializeObject<AssociationChangedPayload>(webhookRequest.Body.ToString())
                   ?? throw new InvalidCastException(nameof(webhookRequest.Body));

        if (input.AssociationType is not null &&
            !input.AssociationType.Equals(data.AssociationType, StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(new WebhookResponse<AssociationChangedPayload>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            });

        return Task.FromResult(new WebhookResponse<AssociationChangedPayload>
        {
            HttpResponseMessage = null,
            Result = data
        });
    }
}