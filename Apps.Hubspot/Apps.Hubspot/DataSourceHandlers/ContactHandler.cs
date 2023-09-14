using System.Text.Json.Serialization;
using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class ContactHandler : HubspotInvocable, IDataSourceHandler
{
    public ContactHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        var request = new HubspotRequest("/crm/v3/objects/contacts/search", Method.Post, Creds);
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
                            propertyName = "firstname",
                            @operator = "CONTAINS_TOKEN"
                        }
                    }
                }
            },
            properties = new[] { "firstname", "lastname" }
        });
        var companies = Client.Execute<MultipleObjects<ContactIdName>>(request).Data.Results;
        return companies.ToDictionary(k => k.Id, v => $"{v.Properties.Firstname} {v.Properties.Lastname}");
    }
}

public class ContactIdName
{
    public string Id { get; set; }

    public FirstNameProperty Properties { get; set; }
}

public class FirstNameProperty
{
    [JsonPropertyName("firstname")]
    public string Firstname { get; set; }

    [JsonPropertyName("lastname")]
    public string Lastname { get; set; }
}