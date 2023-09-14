using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Quotes.Response;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Actions;

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
        [ActionParameter][Display("Quote ID")][DataSource(typeof(QuoteHandler))] string quoteId)
    {
        var client = new HubspotClient();
        var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Get, authenticationCredentialsProviders);
            
        var response = client.GetFullObject<QuoteProperties>(request);
        return new(response);
    }

    [Action("Get quote property", Description = "Get a specific property of a quote")]
    public CustomPropertyEntity GetQuoteProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter][Display("Quote ID")][DataSource(typeof(QuoteHandler))] string quoteId, [ActionParameter][Display("Property")] string property)
    {
        var client = new HubspotClient();
        var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Get, authenticationCredentialsProviders);
        return client.GetProperty(request, property);
    }

    [Action("Set quote property", Description = "Set a specific property of a quote")]
    public QuoteProperties SetQuoteProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter][Display("Quote ID")][DataSource(typeof(QuoteHandler))] string quoteId, [ActionParameter][Display("Property")] string property, [ActionParameter][Display("Value")] string value)
    {
        var client = new HubspotClient();
        var request = new HubspotRequest($"/crm/v3/objects/quote/{quoteId}", Method.Patch, authenticationCredentialsProviders);
        return client.SetProperty<QuoteProperties>(request, property, value);
    }

    [Action("Create quote", Description = "Create a new quote")]
    public BaseObject? CreateQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] QuoteProperties quote)
    {
        var client = new HubspotClient();
        var request = new HubspotRequest("/crm/v3/objects/quotes", Method.Post, authenticationCredentialsProviders)
            .AddObject(new
            {
                quote.hs_title,
                hs_expiration_date = quote.hs_expiration_date
            });
            
        return client.GetFullObject<QuoteProperties>(request);
    }

    [Action("Delete quote", Description = "Delete a quote")]
    public void DeleteQuote(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter][Display("Quote ID")][DataSource(typeof(QuoteHandler))] string quoteId)
    {
        var client = new HubspotClient();
        var request = new HubspotRequest($"/crm/v3/objects/quotes/{quoteId}", Method.Delete, authenticationCredentialsProviders);
        client.Execute(request);
    }
}