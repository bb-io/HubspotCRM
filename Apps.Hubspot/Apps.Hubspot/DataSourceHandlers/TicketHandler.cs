using System.Text.Json.Serialization;
using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class TicketHandler : HubspotInvocable, IDataSourceHandler
{
    public TicketHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        var request = new HubspotRequest("/crm/v3/objects/tickets/search", Method.Post, Creds);
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
                            propertyName = "subject",
                            @operator = "CONTAINS_TOKEN"
                        }
                    }
                }
            },
            properties = new[] { "subject" }
        });
        var companies = Client.Execute<MultipleObjects<TicketIdName>>(request).Data.Results;
        return companies.ToDictionary(k => k.Id, v => v.Properties.Subject);
    }
}

public class TicketIdName
{
    public string Id { get; set; }

    public TicketNameProperty Properties { get; set; }
}

public class TicketNameProperty
{
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
}