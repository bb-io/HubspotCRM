using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Filters;
using Apps.Hubspot.Crm.Models.Pagination;
using Apps.Hubspot.Crm.Models.Quotes.Response;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class QuoteDtaHandler : HubspotInvocable, IAsyncDataSourceHandler
{
    public QuoteDtaHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = "/crm/v3/objects/quote/search";
        var payload = new FilterRequest($"*{context.SearchString}*", "hs_title", "CONTAINS_TOKEN",
            new[] { "hs_title" });

        var request = new HubspotRequest(endpoint, Method.Post, Creds).WithJsonBody(payload, JsonConfig.Settings);

        var quotes =
            await Client.ExecuteWithErrorHandling<MultipleObjects<BaseObjectWithProperties<QuoteProperties>>>(request);
        
        return quotes.Results
            .ToDictionary(k => k.Id, 
                v => v.Properties.HsTitle);
    }
}