using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Utils.Webhooks.Bridge;
using Blackbird.Applications.Sdk.Utils.Webhooks.Bridge.Models.Request;
using RestSharp;

namespace Apps.Hubspot.Crm.Webhooks.Handlers;

public class BaseWebhookHandler : BaseInvocable, IWebhookEventHandler
{
    private readonly string _subscriptionEvent;

    protected BaseWebhookHandler(InvocationContext context ,string subEvent) : base(context)
    {
        _subscriptionEvent = subEvent;
    }

    public Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var (input, creds2) = GetBridgeServiceInputs(values, creds);
        return BridgeService.Subscribe(input, creds2);
    }

    public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var (input, creds2) = GetBridgeServiceInputs(values, creds);
        return BridgeService.Unsubscribe(input, creds2);
    }

    protected (BridgeRequest webhookData, BridgeCredentials bridgeCreds) GetBridgeServiceInputs(
        Dictionary<string, string> values, IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var webhookData = new BridgeRequest
        {
            Event = _subscriptionEvent,
            Id = GetPortalId(creds),
            Url = values["payloadUrl"],
        };

        var bridgeCreds = new BridgeCredentials
        {
            ServiceUrl = InvocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/') + "/hubspot",
            Token = ApplicationConstants.BlackbirdToken
        };
        return (webhookData, bridgeCreds);
    }

    private string GetPortalId(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var client = new HubspotClient();
        var detailsRequest = new HubspotRequest("/account-info/v3/details", Method.Get, creds);
        var details = client.ExecuteWithErrorHandling<AccountEntity>(detailsRequest).Result;

        return details?.PortalId ?? throw new Exception("Could not fetch account details");
    }
}