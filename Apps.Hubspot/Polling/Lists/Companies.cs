using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Helper;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Pagination;
using Apps.Hubspot.Crm.Polling.Inputs;
using Apps.Hubspot.Crm.Polling.Memory;
using Apps.Hubspot.Crm.Polling.Outputs;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Hubspot.Crm.Polling.Lists;

[PollingEventList("Companies")]
public class Companies(InvocationContext invocationContext) : HubspotInvocable(invocationContext)
{
    [PollingEvent("On company custom property changed", Description = "On company custom or default property changed")]
    public async Task<PollingEventResponse<DateTimeMemory, CompanyPropertiesResponse>> OnCompanyPropertyChanged(
        PollingEventRequest<DateTimeMemory> request,
        [PollingEventParameter] OnCompanyPropertyChangedRequest input)
    {
        var currentDateTime = DateTime.UtcNow;
        
        if (request.Memory?.LastPollingTime is null)
            return PollingHelper.DontFlyBird<CompanyPropertiesResponse>(currentDateTime);

        var searchCompaniesBody = new Dictionary<string, object?>
        {
            ["filterGroups"] = new[]
            {
                new
                {
                    filters = new[]
                    {
                        new
                        {
                            propertyName = "hs_lastmodifieddate",
                            @operator = "GTE",
                            value = request.Memory.LastPollingTime.Value.ToIso8601()
                        }
                    }
                }
            },
            ["properties"] = new[] { input.Property }
        };
        string searchEndpoint = "crm/objects/2026-03/company/search";
        var searchCompaniesRequest = new HubspotRequest(searchEndpoint, Method.Post, Creds).AddJsonBody(searchCompaniesBody);
        var searchCompaniesResult = await Client.PaginateSearch<BaseObjectWithProperties<CompanyProperties>>(searchCompaniesRequest);
        
        var companyIds = searchCompaniesResult.Select(x => x.Id).ToList();
        if (companyIds.Count == 0)
            return PollingHelper.DontFlyBird<CompanyPropertiesResponse>(currentDateTime);
        
        var batchCompanyBody = new Dictionary<string, object?>
        {
            { "inputs", companyIds.Select(x => new { id = x }) },
            { "propertiesWithHistory", new[] { input.Property } }
        };
        string batchEndpoint = "crm/objects/2026-03/company/batch/read";
        var batchRequest = new HubspotRequest(batchEndpoint, Method.Post, Creds).AddJsonBody(batchCompanyBody);
        var batchResult = await Client.ExecuteWithErrorHandling<MultipleObjects<CompanyWithHistoryEntity>>(batchRequest);
        
        var changed = batchResult.Results
            .Where(c => c.PropertiesWithHistory != null && 
                        c.PropertiesWithHistory.TryGetValue(input.Property, out var hist) && 
                        hist.Count > 0 && 
                        hist[0].Timestamp > request.Memory.LastPollingTime.Value)
            .ToList();
        
        if (changed.Count == 0)
            return PollingHelper.DontFlyBird<CompanyPropertiesResponse>(currentDateTime);
        
        var result = changed.Select(c =>
        {
            var latest = c.PropertiesWithHistory![input.Property][0];
            return new PropertyChangedPayload
            {
                ObjectId = c.Id,
                PropertyName = input.Property,
                PropertyValue = latest.Value ?? string.Empty
            };
        }).ToArray();
        
        return new PollingEventResponse<DateTimeMemory, CompanyPropertiesResponse>
        {
            FlyBird = true,
            Memory = new DateTimeMemory(currentDateTime),
            Result = new(result)
        };
    }
}