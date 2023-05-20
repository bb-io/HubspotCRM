using Apps.Hubspot.Crm.Webhooks.Bridge;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.Hubspot.Webhooks.Handlers
{
    public class BaseWebhookHandler : IWebhookEventHandler
    {

        //const string SubscriptionEvent = "contact.propertyChange";
        //const string PropertyName = "email";

        private string SubscriptionEvent;

        //private string PropertyName;

        public BaseWebhookHandler(string subEvent)
        {
            SubscriptionEvent = subEvent;
            //PropertyName = propertyName;
        }

        public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, Dictionary<string, string> values)
        {
            var bridge = new BridgeService(authenticationCredentialsProviders);
            bridge.Subscribe(SubscriptionEvent, values["payloadUrl"]);
        }

        public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, Dictionary<string, string> values)
        {
            var bridge = new BridgeService(authenticationCredentialsProviders);
            bridge.Unsubscribe(SubscriptionEvent);
        }
    }
}
