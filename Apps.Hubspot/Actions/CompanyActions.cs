using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Companies.Request;
using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Filters;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions;

[ActionList("Companies")]
public class CompanyActions(InvocationContext invocationContext) : HubspotInvocable(invocationContext)
{
    [Action("Search companies", Description = "Search companies with optional filters (status, create date, update date)")]
    public async Task<SearchCompaniesResponse> SearchCompanies([ActionParameter] SearchCompaniesRequest input)
    {
        static string ToUnixMs(DateTime dt)
        {
            var utc = dt.Kind == DateTimeKind.Utc ? dt : dt.ToUniversalTime();
            return new DateTimeOffset(utc).ToUnixTimeMilliseconds().ToString();
        }

        var filters = new List<object>();

        if (input.Status?.Any() == true)
        {
            filters.Add(new
            {
                propertyName = "lifecyclestage",
                @operator = "IN",
                values = input.Status
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .ToArray()
            });
        }

        if (input.CreatedFrom.HasValue)
        {
            filters.Add(new
            {
                propertyName = "createdate",
                @operator = "GTE",
                value = ToUnixMs(input.CreatedFrom.Value)
            });
        }

        if (input.CreatedTo.HasValue)
        {
            filters.Add(new
            {
                propertyName = "createdate",
                @operator = "LTE",
                value = ToUnixMs(input.CreatedTo.Value)
            });
        }

        if (input.UpdatedFrom.HasValue)
        {
            filters.Add(new
            {
                propertyName = "hs_lastmodifieddate",
                @operator = "GTE",
                value = ToUnixMs(input.UpdatedFrom.Value)
            });
        }

        if (input.UpdatedTo.HasValue)
        {
            filters.Add(new
            {
                propertyName = "hs_lastmodifieddate",
                @operator = "LTE",
                value = ToUnixMs(input.UpdatedTo.Value)
            });
        }

        var all = new List<CompanyEntity>();
        string? after = null;

        var pages = 0;
        var maxPages = 200;

        do
        {
            pages++;

            var payload = new
            {
                filterGroups = new[]
                {
                    new { filters = filters }
                },
                sorts = new[] { "-hs_lastmodifieddate" },
                properties = new[]
                {
                    "domain", "name", "city", "industry", "phone", "state", "lifecyclestage"
                },
                limit = 100,
                after = after
            };

            var request = new HubspotRequest("/crm/v3/objects/companies/search", Method.Post, Creds)
                .WithJsonBody(payload, JsonConfig.Settings);

            var hs = await Client.ExecuteWithErrorHandling<SearchResponse<Apps.Hubspot.Crm.Models.Companies.Response.CompanyProperties>>(request);

            if (hs.Results?.Any() == true)
                all.AddRange(hs.Results.Select(r => new CompanyEntity(r)));

            after = hs.Paging?.Next?.After;

        } while (!string.IsNullOrWhiteSpace(after) && pages < maxPages);

        return new SearchCompaniesResponse { Companies = all };
    }

    [Action("Get company", Description = "Get information of a specific company")]
    public async Task<CompanyEntity> GetCompany([ActionParameter] CompanyRequest company)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds)
            .AddQueryParameter("associations", "contacts");

        var response = await Client.GetFullObject<CompanyProperties>(request);
        return new(response)
        {
            Id = response.Id,
            Name = response.Properties.Name,
            Domain = response.Properties.Domain,
            ContactIds = response.Associations?["contacts"].GetDistinctIds(),
            StreetAddress1 = (await Client.GetProperty(request, "address")).Value,
            StreetAddress2 = (await Client.GetProperty(request, "address2")).Value,
            PostalCode = (await Client.GetProperty(request, "zip")).Value,
            Country = (await Client.GetProperty(request, "country")).Value
        };
    }

    [Action("Get company by custom property", Description = "Get company by custom property value")]
    public async Task<CompanyEntity> GetCompanyByCustomValue(
        [ActionParameter][DataSource(typeof(CompanyPropertiesDataHandler))][Display("Property")] string property,
        [ActionParameter][Display("Value")] string value)
    {
        var payload = new FilterRequest(value, property.ToApiPropertyName(), "EQ", new[] { value });
        var request = new HubspotRequest("/crm/v3/objects/companies/search", Method.Post, Creds)
            .WithJsonBody(payload, JsonConfig.Settings);

        var companies = await Client.GetMultipleObjects(request);

        if (companies == null || !companies.Any())
        {
            return new CompanyEntity { };
        }

        return await GetCompany(new()
        {
            CompanyId = companies.First().Id
        });
    }

    [Action("Get company property", Description = "Get a specific property of a company")]
    public Task<CustomPropertyEntity> GetCompanyProperty(
        [ActionParameter] CompanyRequest company,
        [ActionParameter][DataSource(typeof(CompanyPropertiesDataHandler))][Display("Property")] string property)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);
      
        return Client.GetProperty(request, property);
    }

    [Action("Set company property", Description = "Set a specific property of a company")]
    public async Task<CompanyEntity> SetCompanyProperty(
        [ActionParameter] CompanyRequest company,
        [ActionParameter][DataSource(typeof(CompanyPropertiesDataHandler))][Display("Property")] string property,
        [ActionParameter][Display("Value")] string value)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);
        
        var response = await Client.
            SetProperty<CompanyProperties>(request, property, value);
        
        return new(response);
    }

    [Action("Create company", Description = "Create a new company")]
    public async Task<CompanyEntity> CreateCompany([ActionParameter] CompanyProperties company)
    {
        var request = new HubspotRequest("/crm/v3/objects/companies", Method.Post, Creds)
            .AddObject(company);

        var response = await Client.GetFullObject<CompanyProperties>(request);
        return new(response);
    }

    [Action("Delete company", Description = "Delete a company")]
    public Task DeleteCompany([ActionParameter] CompanyRequest company)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);
       
        return Client.ExecuteWithErrorHandling(request);
    }
}