using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.Hubspot.Crm.Api;

public class HubspotRequest : BlackBirdRestRequest
{
    public HubspotRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) : base(
        endpoint, method, creds)
    {
    }

    public RestRequest AddObject<T>(T obj)
        => this.WithJsonBody(new ObjectWithProperties<T> { Properties = obj }, JsonConfig.Settings);

    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var token = creds.Get(CredsNames.AccessToken).Value;

        this.AddHeader("Authorization", $"Bearer {token}");
        this.AddHeader("accept", "*/*");
    }
}