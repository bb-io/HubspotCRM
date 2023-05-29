using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Outputs;

namespace Apps.Hubspot.Crm.Actions
{
    [ActionList]
    public class QuoteActions
    {
        [Action("Get all quotes", Description = "Get a list of all quotes")]
        public IEnumerable<BaseObject> GetQuotes(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/quotes", Method.Get, authenticationCredentialsProviders);
            return client.GetMultipleObjects(request);
        }

        [Action("Get quote", Description = "Get information of a specific quote")]
        public Quote? GetQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string quoteId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Get, authenticationCredentialsProviders);
            return client.GetObject<Quote>(request);
        }

        [Action("Get quote property", Description = "Get a specific property of a quote")]
        public CustomProperty GetQuoteProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string quoteId, [ActionParameter] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set quote property", Description = "Set a specific property of a quote")]
        public Models.Quote SetQuoteProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string quoteId, [ActionParameter] string property, [ActionParameter] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quote/{quoteId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Models.Quote>(request, property, value);
        }

        [Action("Create quote", Description = "Create a new quote")]
        public BaseObject? CreateQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] Quote quote)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes", Method.Post, authenticationCredentialsProviders);
            request.AddObject(quote);
            return client.PostObject(request);
        }

        [Action("Delete quote", Description = "Delete a quote")]
        public void DeleteQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string quoteId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
