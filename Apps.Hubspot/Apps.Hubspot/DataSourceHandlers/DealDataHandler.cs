using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Deals.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Filters;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class DealDataHandler : HubspotInvocable, IAsyncDataSourceHandler
{
    public DealDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = "/crm/v3/objects/deals/search";
        var payload = new FilterRequest($"*{context.SearchString}*", "dealname", "CONTAINS_TOKEN",
            new[] { "dealname" });

        var request = new HubspotRequest(endpoint, Method.Post, Creds).WithJsonBody(payload, JsonConfig.Settings);

        var deals =
            await Client.ExecuteWithErrorHandling<MultipleObjects<BaseObjectWithProperties<DealProperties>>>(request);
      
        return deals.Results
            .ToDictionary(k => k.Id, 
                v => v.Properties.Dealname!);
    }
}