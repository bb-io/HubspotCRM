using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.DynamicHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

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
        public DealEntity GetDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Deal ID")][DataSource(typeof(DealHandler))] string dealId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Get, authenticationCredentialsProviders);
            
            var response = client.GetFullObject<DealProperties>(request);
            return new(response);
        }

        [Action("Get deal property", Description = "Get a specific property of a deal")]
        public CustomPropertyEntity GetDealProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Deal ID")][DataSource(typeof(DealHandler))] string dealId, [ActionParameter][Display("Property")] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set deal property", Description = "Set a specific property of a deal")]
        public DealProperties SetDealProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Deal ID")][DataSource(typeof(DealHandler))] string dealId, [ActionParameter][Display("Property")] string property, [ActionParameter][Display("Value")] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<DealProperties>(request, property, value);
        }

        [Action("Create deal", Description = "Create a new deal")]
        public BaseObject? CreateDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] DealProperties deal)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals", Method.Post, authenticationCredentialsProviders);
            request.AddObject(deal);
            return client.PostObject(request);
        }

        [Action("Delete deal", Description = "Delete a deal")]
        public void DeleteDeal(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Deal ID")][DataSource(typeof(DealHandler))] string dealId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/deals/{dealId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
