using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Outputs;

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

        [Action("Get deal property", Description = "Get a specific property of a deal")]
        public CustomProperty GetDealProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string dealId, [ActionParameter] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set deal property", Description = "Set a specific property of a deal")]
        public Models.Deal SetDealProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string dealId, [ActionParameter] string property, [ActionParameter] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Models.Deal>(request, property, value);
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
