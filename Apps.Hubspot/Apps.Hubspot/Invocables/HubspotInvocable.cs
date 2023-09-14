using Apps.Hubspot.Crm.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Invocables;

public class HubspotInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected HubspotClient Client { get; }
    
    public HubspotInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new();
    }
}