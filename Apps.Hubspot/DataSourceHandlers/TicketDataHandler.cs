using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Filters;
using Apps.Hubspot.Crm.Models.Pagination;
using Apps.Hubspot.Crm.Models.Tickets.Response;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class TicketDataHandler : HubspotInvocable, IAsyncDataSourceHandler
{
    public TicketDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = "/crm/v3/objects/tickets/search";
        var payload = new FilterRequest($"*{context.SearchString}*", "subject", "CONTAINS_TOKEN",
            new[] { "subject" });

        var request = new HubspotRequest(endpoint, Method.Post, Creds).WithJsonBody(payload, JsonConfig.Settings);

        var tickets =
            await Client.ExecuteWithErrorHandling<MultipleObjects<BaseObjectWithProperties<TicketProperties>>>(request);

        return tickets.Results
            .ToDictionary(k => k.Id,
                v => v.Properties.Subject);
    }
}