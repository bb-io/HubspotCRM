using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Contacts.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Filters;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class ContactDataHandler : HubspotInvocable, IAsyncDataSourceHandler
{
    public ContactDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var endpoint = "/crm/v3/objects/contacts/search";
        var payload = new FilterRequest($"*{context.SearchString}*", "firstname", "CONTAINS_TOKEN",
            new[] { "firstname", "lastname" });

        var request = new HubspotRequest(endpoint, Method.Post, Creds).WithJsonBody(payload);

        var contacts =
            await Client
                .ExecuteWithErrorHandling<MultipleObjects<BaseObjectWithProperties<ContactProperties>>>(request);

        return contacts.Results
            .ToDictionary(k => k.Id,
                v => $"{v.Properties.Firstname} {v.Properties.Lastname}");
    }
}