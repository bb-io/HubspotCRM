using RestSharp;
using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Polling.Inputs;
using Apps.Hubspot.Crm.Polling.Memory;
using Apps.Hubspot.Crm.Models.Deals.Response;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;

namespace Apps.Hubspot.Crm.Polling.Lists;

[PollingEventList]
public class Deals(InvocationContext invocationContext) : HubspotInvocable(invocationContext)
{
    [PollingEvent("On deal status changed", Description = "On deal status changed")]
    public async Task<PollingEventResponse<DateTimeMemory, SearchDealsResponse>> OnDealStatusChanged(
        PollingEventRequest<DateTimeMemory> request,
        [PollingEventParameter] OnStatusChangedRequest input)
    {
        if (request.Memory is null || request.Memory.LastPollingTime is null)
        {
            return new PollingEventResponse<DateTimeMemory, SearchDealsResponse>
            {
                FlyBird = false,
                Memory = new DateTimeMemory(DateTime.UtcNow),
                Result = null,
            };
        }

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

        var requestRequest = new HubspotRequest("/crm/v3/objects/deals/search", Method.Post, Creds)
            .WithJsonBody(payload, JsonConfig.Settings);

        var response = await Client.ExecuteWithErrorHandling<SearchResponse<DealProperties>>(requestRequest);

        if (response.Results == null || response.Results.Count == 0)
        {
            return new PollingEventResponse<DateTimeMemory, SearchDealsResponse>
            {
                FlyBird = false,
                Memory = request.Memory,
                Result = null,
            };
        }

        var dealEntities = response.Results.Select(r => new DealEntity(r)).ToList();

        return new PollingEventResponse<DateTimeMemory, SearchDealsResponse>
        {
            FlyBird = true,
            Memory = new DateTimeMemory(DateTime.UtcNow),
            Result = new SearchDealsResponse { Deals = dealEntities }
        };
    }
}
