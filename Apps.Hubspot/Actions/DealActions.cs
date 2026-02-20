using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Deals.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models.Deals.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Models.Filters;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Apps.Hubspot.Crm.Extensions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers;
using Apps.Hubspot.Crm.Models;

namespace Apps.Hubspot.Crm.Actions;

[ActionList("Deals")]
public class DealActions(InvocationContext invocationContext) : HubspotInvocable(invocationContext)
{
    [Action("Search deals", Description = "Search deals with optional filters (status, create date, update date)")]
    public async Task<SearchDealsResponse> SearchDeals([ActionParameter] SearchDealsRequest input)
    {
        var filters = new List<object>();

        if (input.Status?.Any() == true)
        {
            filters.Add(new
            {
                propertyName = "dealstage",
                @operator = "IN",
                values = input.Status
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .ToArray()
            });
        }

        if (input.CreatedFrom.HasValue)
        {
            filters.Add(new
            {
                propertyName = "createdate",
                @operator = "GTE",
                value = input.CreatedFrom.Value.ToUnixMs()
            });
        }

        if (input.CreatedTo.HasValue)
        {
            filters.Add(new
            {
                propertyName = "createdate",
                @operator = "LTE",
                value = input.CreatedTo.Value.ToUnixMs()
            });
        }

        if (input.UpdatedFrom.HasValue)
        {
            filters.Add(new
            {
                propertyName = "hs_lastmodifieddate",
                @operator = "GTE",
                value = input.UpdatedFrom.Value.ToUnixMs()
            });
        }

        if (input.UpdatedTo.HasValue)
        {
            filters.Add(new
            {
                propertyName = "hs_lastmodifieddate",
                @operator = "LTE",
                value = input.UpdatedTo.Value.ToUnixMs()
            });
        }

        var all = new List<DealEntity>();
        string? after = null;

        var pages = 0;
        var maxPages = 200;

        do
        {
            pages++;

            var payload = new
            {
                filterGroups = new[]
                {
                    new { filters = filters }
                },
                sorts = new[] { "-hs_lastmodifieddate" },
                properties = new[]
                {
                    "amount", "dealname", "dealstage", "pipeline", "hubspot_owner_id", "closedate"
                },
                limit = 100,
                after = after
            };

            var request = new HubspotRequest("/crm/v3/objects/deals/search", Method.Post, Creds)
                .WithJsonBody(payload, JsonConfig.Settings);

            var hs = await Client.ExecuteWithErrorHandling<SearchResponse<Apps.Hubspot.Crm.Models.Deals.Response.DealProperties>>(request);

            if (hs.Results?.Any() == true)
                all.AddRange(hs.Results.Select(r => new DealEntity(r)));

            after = hs.Paging?.Next?.After;

        } while (!string.IsNullOrWhiteSpace(after) && pages < maxPages);

        return new SearchDealsResponse { Deals = all };
    }

    [Action("Get deal", Description = "Get information of a specific deal")]
    public async Task<DealEntity> GetDeal([ActionParameter] DealRequest deal)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var response = await Client.GetFullObject<DealProperties>(request);
        return new(response);
    }

    [Action("Get deal by custom property", Description = "Get a deal by a custom property")]
    public async Task<DealEntity> GetDealByCustomProperty(
    [ActionParameter][DataSource(typeof(DealPropertiesDataHandler))][Display("Property")] string property,
    [ActionParameter][Display("Value")] string value)
    {
        var payload = new FilterRequest(value, property.ToApiPropertyName(), "EQ", new[] { value });
        var request = new HubspotRequest("/crm/v3/objects/deals/search", Method.Post, Creds)
            .WithJsonBody(payload, JsonConfig.Settings);

        var deals = await Client.GetMultipleObjects(request);

        if (deals == null || !deals.Any())
        {
            return new DealEntity { };
        }

        return await GetDeal(new()
        {
            DealId = deals.First().Id
        });
    }

    [Action("Get deal property", Description = "Get a specific property of a deal")]
    public Task<CustomPropertyEntity> GetDealProperty(
        [ActionParameter] DealRequest deal,
        [ActionParameter][DataSource(typeof(DealPropertiesDataHandler))][Display("Property")] string property)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        return Client.GetProperty(request, property);
    }

    [Action("Set deal property", Description = "Set a specific property of a deal")]
    public async Task<DealEntity> SetDealProperty(
        [ActionParameter] DealRequest deal,
        [ActionParameter][DataSource(typeof(DealPropertiesDataHandler))][Display("Property")] string property,
        [ActionParameter][Display("Value")] string value)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var response = await Client
            .SetProperty<DealProperties>(request, property, value);

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

    [Action("Get deal boolean property", Description = "Get a boolean (single checkbox) property of a deal")]
    public async Task<BooleanPropertyEntity> GetDealBooleanProperty(
        [ActionParameter] DealRequest deal,
        [ActionParameter][DataSource(typeof(DealBooleanPropertiesDataHandler))][Display("Property")] string property)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var result = await Client.GetProperty(request, property);

        if (result?.Value == null)
            return new BooleanPropertyEntity { Value = null };

        if (bool.TryParse(result.Value, out var b))
            return new BooleanPropertyEntity { Value = b };

        return new BooleanPropertyEntity { Value = null };
    }

    [Action("Set deal boolean property", Description = "Set a boolean (single checkbox) property of a deal")]
    public async Task<DealEntity> SetDealBooleanProperty(
        [ActionParameter] DealRequest deal,
        [ActionParameter][DataSource(typeof(DealBooleanPropertiesDataHandler))][Display("Property")] string property,
        [ActionParameter][Display("Value")] bool value)
    {
        var endpoint = $"/crm/v3/objects/deals/{deal.DealId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var payload = new
        {
            properties = new Dictionary<string, object>
            {
                [property] = value
            }
        };

        request.WithJsonBody(payload, JsonConfig.Settings);

        var response = await Client.GetFullObject<DealProperties>(request);
        return new DealEntity(response);
    }
}