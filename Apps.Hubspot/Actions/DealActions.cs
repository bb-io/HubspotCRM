using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Deals.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models.Deals.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Properties.Request;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Filters;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Apps.Hubspot.Crm.Extensions;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class DealActions : HubspotInvocable
{
    public DealActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get all deals", Description = "Get a list of all deals")]
    public async Task<ListItemsResponse> GetDeals()
    {
        var request = new HubspotRequest("/crm/v3/objects/deals", Method.Get, Creds);

        var response = await Client.GetMultipleObjects(request);
        return new(response);
    }

    [Action("Get deal", Description = "Get information of a specific deal")]
    public async Task<DealEntity> GetDeal([ActionParameter] DealRequest deal)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var response = await Client.GetFullObject<DealProperties>(request);
        return new(response);
    }

    [Action("Get deal property", Description = "Get a specific property of a deal")]
    public Task<CustomPropertyEntity> GetDealProperty(
        [ActionParameter] DealRequest deal,
        [ActionParameter] GetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        return Client.GetProperty(request, property.Property);
    }

    [Action("Set deal property", Description = "Set a specific property of a deal")]
    public async Task<DealEntity> SetDealProperty(
        [ActionParameter] DealRequest deal,
        [ActionParameter] SetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var response = await Client
            .SetProperty<DealProperties>(request, property.Property, property.Value);

        return new(response);
    }

    [Action("Create deal", Description = "Create a new deal")]
    public async Task<DealEntity> CreateDeal([ActionParameter] DealProperties deal)
    {
        var request = new HubspotRequest("/crm/v3/objects/deals", Method.Post, Creds)
            .AddObject(deal);

        var response = await Client.GetFullObject<DealProperties>(request);
        return new(response);
    }

    [Action("Delete deal", Description = "Delete a deal")]
    public Task DeleteDeal([ActionParameter] DealRequest deal)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Get deal by custom property", Description = "Get a deal by a custom property")]
    public async Task<DealEntity> GetDealByCustomProperty([ActionParameter] GetDealByCustomValueRequest property)
    {
        var payload = new FilterRequest(property.CustomPropertyValue, property.CustomPropertyName.ToApiPropertyName(), "EQ", new[] { property.CustomPropertyValue });
        var request = new HubspotRequest("/crm/v3/objects/deals/search", Method.Post, Creds)
            .WithJsonBody(payload, JsonConfig.Settings);

        var deals = await Client.GetMultipleObjects(request);

        if (deals == null || !deals.Any())
            throw new InvalidOperationException("No deals found with the given custom property.");

        return await GetDeal(new()
        {
            DealId = deals.First().Id
        });
    }
}