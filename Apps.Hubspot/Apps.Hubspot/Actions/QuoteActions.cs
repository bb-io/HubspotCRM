﻿using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;

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
        public QuoteEntity GetQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Quote ID")] string quoteId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Get, authenticationCredentialsProviders);
            
            var response = client.GetFullObject<QuoteProperties>(request);
            return new(response);
        }

        [Action("Get quote property", Description = "Get a specific property of a quote")]
        public CustomPropertyEntity GetQuoteProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Quote ID")] string quoteId, [ActionParameter][Display("Property")] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set quote property", Description = "Set a specific property of a quote")]
        public Models.QuoteProperties SetQuoteProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Quote ID")] string quoteId, [ActionParameter][Display("Property")] string property, [ActionParameter][Display("Value")] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quote/{quoteId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Models.QuoteProperties>(request, property, value);
        }

        [Action("Create quote", Description = "Create a new quote")]
        public BaseObject? CreateQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] QuoteProperties quote)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes", Method.Post, authenticationCredentialsProviders);
            request.AddObject(new
            {
                hs_title = quote.hs_title,
                hs_expiration_date = ((DateTimeOffset)quote.hs_expiration_date).ToUnixTimeSeconds()
            });
            
            return client.GetFullObject<QuoteProperties>(request);
        }

        [Action("Delete quote", Description = "Delete a quote")]
        public void DeleteQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Quote ID")] string quoteId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
