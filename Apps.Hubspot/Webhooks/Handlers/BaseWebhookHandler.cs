using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Webhooks.Bridge;
using Blackbird.Applications.Sdk.Utils.Webhooks.Bridge.Models.Request;
using RestSharp;

namespace Apps.Hubspot.Crm.Webhooks.Handlers;

public class BaseWebhookHandler : BridgeWebhookHandler
{
    private readonly string _subscriptionEvent;

    protected BaseWebhookHandler(string subEvent)
    {
        _subscriptionEvent = subEvent;
    }

    protected override (BridgeRequest webhookData, BridgeCredentials bridgeCreds) GetBridgeServiceInputs(
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
            ServiceUrl = ApplicationConstants.BridgeServiceUrl,
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