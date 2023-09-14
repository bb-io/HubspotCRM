using System.Text.Json.Serialization;
using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class QuoteHandler : HubspotInvocable, IDataSourceHandler
{
    public QuoteHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        var request = new HubspotRequest("/crm/v3/objects/quote/search", Method.Post, Creds);
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
                            propertyName = "hs_title",
                            @operator = "CONTAINS_TOKEN"
                        }
                    }
                }
            },
            properties = new[] { "hs_title" }
        });
        var companies = Client.Execute<MultipleObjects<QuoteIdName>>(request).Data.Results;
        return companies.ToDictionary(k => k.Id, v => v.Properties.Quotename);
    }
}

public class QuoteIdName
{
    public string Id { get; set; }

    public QuoteNameProperty Properties { get; set; }
}

public class QuoteNameProperty
{
    [JsonPropertyName("hs_title")]
    public string Quotename { get; set; }
}