using System.Text.Json.Serialization;
using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class DealHandler : HubspotInvocable, IDataSourceHandler
{
    public DealHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        var request = new HubspotRequest("/crm/v3/objects/deals/search", Method.Post, Creds);
        request.AddJsonBody(new
        {
            filterGroups = new[]
            {
                new
                {
                    filters = new[]
                    {
                        new
                        {
                            value = $"*{context.SearchString}*",
                            propertyName = "dealname",
                            @operator = "CONTAINS_TOKEN"
                        }
                    }
                }
            },
            properties = new[] { "dealname" }
        });
        var companies = Client.Execute<MultipleObjects<DealIdName>>(request).Data.Results;
        return companies.ToDictionary(k => k.Id, v => v.Properties.Dealname);
    }
}

public class DealIdName
{
    public string Id { get; set; }

    public DealNameProperty Properties { get; set; }
}

public class DealNameProperty
{
    [JsonPropertyName("dealname")]
    public string Dealname { get; set; }
}