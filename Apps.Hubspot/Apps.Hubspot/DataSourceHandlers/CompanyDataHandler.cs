using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Filters;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class CompanyDataHandler : HubspotInvocable, IAsyncDataSourceHandler
{
    public CompanyDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = "/crm/v3/objects/companies/search";
        var payload = new FilterRequest($"*{context.SearchString}*", "name",
            "CONTAINS_TOKEN", new[] { "name" });

        var request = new HubspotRequest(endpoint, Method.Post, Creds).WithJsonBody(payload, JsonConfig.Settings);

        var companies = await Client.ExecuteWithErrorHandling<MultipleObjects<BaseObjectWithProperties<CompanyProperties>>>(request);
       
        return companies.Results
            .ToDictionary(k => k.Id, v => v.Properties.Name!);
    }
}