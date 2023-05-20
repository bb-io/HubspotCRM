using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;

namespace Apps.Hubspot.Crm.Actions
{
    [ActionList]
    public class DealActions
    {
        [Action("Get all deals", Description = "Get a list of all deals")]
        public IEnumerable<BaseObject> GetDeals(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/deals", Method.Get, authenticationCredentialsProviders);
            return client.GetMultipleObjects(request);
        }

        [Action("Get deal", Description = "Get information of a specific deal")]
        public Deal? GetDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string dealId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Get, authenticationCredentialsProviders);
            return client.GetObject<Deal>(request);
        }

        [Action("Create deal", Description = "Create a new deal")]
        public BaseObject? CreateDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] Deal deal)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals", Method.Post, authenticationCredentialsProviders);
            request.AddObject(deal);
            return client.PostObject(request);
        }

        [Action("Update deal", Description = "Update a deal's information")]
        public Deal? UpdateDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string dealId, [ActionParameter] Deal deal)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/${dealId}", Method.Patch, authenticationCredentialsProviders);
            request.AddObject(deal);
            return client.PatchObject<Deal>(request);
        }

        [Action("Delete deal", Description = "Delete a deal")]
        public void DeleteDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string dealId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
