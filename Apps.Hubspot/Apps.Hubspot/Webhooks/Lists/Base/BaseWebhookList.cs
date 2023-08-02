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
}