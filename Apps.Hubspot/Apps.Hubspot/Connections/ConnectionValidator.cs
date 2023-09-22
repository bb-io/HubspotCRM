using Apps.Hubspot.Crm.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.Hubspot.Crm.Connections;

public class ConnectionValidator : IConnectionValidator
{
    private HubspotClient Client => new();

    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        try
        {
            var request = new HubspotRequest("/crm/v3/objects/companies", Method.Get, authProviders);
            var response = await Client.GetMultipleObjects(request);

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}