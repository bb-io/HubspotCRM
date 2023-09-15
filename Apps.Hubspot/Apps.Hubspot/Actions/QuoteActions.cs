using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Properties.Request;
using Apps.Hubspot.Crm.Models.Quotes.Request;
using Apps.Hubspot.Crm.Models.Quotes.Response;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class QuoteActions : HubspotInvocable
{
    public QuoteActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get all quotes", Description = "Get a list of all quotes")]
    public async Task<ListItemsResponse> GetQuotes()
    {
        var request = new HubspotRequest("/crm/v3/objects/quotes", Method.Get, Creds);

        var response = await Client.GetMultipleObjects(request);
        return new(response);
    }

    [Action("Get quote", Description = "Get information of a specific quote")]
    public async Task<QuoteEntity> GetQuote([ActionParameter] QuoteRequest quote)
    {
        var endpoint = $"/crm/v3/objects/quotes/{quote.QuoteId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var response = await Client.GetFullObject<QuoteProperties>(request);
        return new(response);
    }

    [Action("Get quote property", Description = "Get a specific property of a quote")]
    public Task<CustomPropertyEntity> GetQuoteProperty(
        [ActionParameter] QuoteRequest quote,
        [ActionParameter] GetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/quotes/{quote.QuoteId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        return Client.GetProperty(request, property.Property);
    }

    [Action("Set quote property", Description = "Set a specific property of a quote")]
    public async Task<QuoteEntity> SetQuoteProperty(
        [ActionParameter] QuoteRequest quote,
        [ActionParameter] SetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/quotes/{quote.QuoteId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var response = await Client
            .SetProperty<QuoteProperties>(request, property.Property, property.Value);

        return new(response);
    }

    [Action("Create quote", Description = "Create a new quote")]
    public async Task<QuoteEntity> CreateQuote([ActionParameter] QuoteProperties quote)
    {
        var request = new HubspotRequest("/crm/v3/objects/quotes", Method.Post, Creds)
            .AddObject(quote);

        var response = await Client.GetFullObject<QuoteProperties>(request);
        return new(response);
    }

    [Action("Delete quote", Description = "Delete a quote")]
    public Task DeleteQuote([ActionParameter] QuoteRequest quote)
    {
        var endpoint = $"/crm/v3/objects/quotes/{quote.QuoteId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }
}