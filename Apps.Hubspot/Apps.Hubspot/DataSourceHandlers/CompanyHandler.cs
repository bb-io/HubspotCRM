using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers;

public class CompanyHandler : HubspotInvocable, IDataSourceHandler 
{
    public CompanyHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public Dictionary<string, string> GetData(DataSourceContext context)
    {
        var request = new HubspotRequest("/crm/v3/objects/companies/search", Method.Post, Creds);
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
                            propertyName = "name",
                            @operator = "CONTAINS_TOKEN"
                        }
                    }
                }
            },
            properties = new[] { "name" }
        });
        var companies = Client.Execute<MultipleObjects<CompanyIdName>>(request).Data.Results;
        return companies.ToDictionary(k => k.Id, v => v.Properties.Name);
    }
}

public class CompanyIdName
{
    public string Id { get; set; }

    public NameProperty Properties { get; set; }
}

public class NameProperty
{
    public string Name { get; set; }
}