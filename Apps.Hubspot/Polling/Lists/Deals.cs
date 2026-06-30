using RestSharp;
using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Helper;
using Apps.Hubspot.Crm.Polling.Inputs;
using Apps.Hubspot.Crm.Polling.Memory;
using Apps.Hubspot.Crm.Models.Deals.Response;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;

namespace Apps.Hubspot.Crm.Polling.Lists;

[PollingEventList("Deals")]
public class Deals(InvocationContext invocationContext) : HubspotInvocable(invocationContext)
{
    [PollingEvent("On deal status changed", Description = "On deal status changed")]
    public async Task<PollingEventResponse<DateTimeMemory, SearchDealsResponse>> OnDealStatusChanged(
        PollingEventRequest<DateTimeMemory> request,
        [PollingEventParameter] OnStatusChangedRequest input)
    {
        var currentDateTime = DateTime.UtcNow;

        if (request.Memory?.LastPollingTime is null)
            return PollingHelper.DontFlyBird<SearchDealsResponse>(currentDateTime);

        var filters = new List<object>
        {
            new
            {
                propertyName = "hs_lastmodifieddate",
                @operator = "GT",
                value = request.Memory.LastPollingTime.Value.ToUnixMs()
            },
            new
            {
                propertyName = "dealstage",
                @operator = "EQ",
                value = input.Status
            }
        };

        if (!string.IsNullOrWhiteSpace(input.DealId))
        {
            filters.Add(new
            {
                propertyName = "hs_object_id",
                @operator = "EQ",
                value = input.DealId
            });
        }

        var payload = new
        {
            filterGroups = new[] { new { filters } },
            sorts = new[] { "hs_lastmodifieddate" },
            properties = new[]
            {
                "amount", "dealname", "dealstage", "pipeline", "hubspot_owner_id", "closedate", "hs_lastmodifieddate"
            },
            limit = 100
        };

        string searchEndpoint = "/crm/v3/objects/deals/search";
        var searchRequest = new HubspotRequest(searchEndpoint, Method.Post, Creds).WithJsonBody(payload);

        var response = await Client.ExecuteWithErrorHandling<SearchResponse<DealProperties>>( searchRequest);

        if (response.Results.Count == 0)
        {
            return new PollingEventResponse<DateTimeMemory, SearchDealsResponse>
            {
                FlyBird = false,
                Memory = request.Memory,
                Result = null,
            };
        }
        
        var dealEntities = response.Results.Select(r => new DealEntity(r)).ToList();
        return PollingHelper.FlyBird(currentDateTime, new SearchDealsResponse { Deals = dealEntities });
    }
}
