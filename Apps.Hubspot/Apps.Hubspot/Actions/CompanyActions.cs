using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Companies.Request;
using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Properties.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class CompanyActions : HubspotInvocable
{
    public CompanyActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get all companies", Description = "Get a list of all companies")]
    public async Task<ListItemsResponse> GetCompanies()
    {
        var request = new HubspotRequest("/crm/v3/objects/companies", Method.Get, Creds);
        
        var response = await Client.GetMultipleObjects(request);
        return new(response);
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
        };
    }

    [Action("Get company by custom property", Description = "Get company by custom property value")]
    public async Task<CompanyEntity> GetCompanyByCustomValue([ActionParameter] GetCompanyByCustomValueRequest input)
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
                            value = input.CustomPropertyValue,
                            propertyName = input.CustomPropertyName.ToApiPropertyName(),
                            @operator = "EQ"
                        }
                    }
                }
            }
        });

        var companies = await Client.GetMultipleObjects(request);

        return await GetCompany(new()
        {
            CompanyId = companies.First().Id
        });
    }

    [Action("Get company property", Description = "Get a specific property of a company")]
    public Task<CustomPropertyEntity> GetCompanyProperty(
        [ActionParameter] CompanyRequest company,
        [ActionParameter] GetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);
      
        return Client.GetProperty(request, property.Property);
    }

    [Action("Get company address", Description = "Get company address")]
    public async Task<GetCompanyAddressResponse> GetCompanyAddress([ActionParameter] CompanyRequest company)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);
        
        return new()
        {
            StreetAddress1 = (await Client.GetProperty(request, "address")).Property,
            StreetAddress2 = (await Client.GetProperty(request, "address2")).Property,
            PostalCode = (await Client.GetProperty(request, "zip")).Property,
            City = (await Client.GetProperty(request, "city")).Property,
            State = (await Client.GetProperty(request, "state")).Property,
            Country = (await Client.GetProperty(request, "country")).Property
        };
    }

    [Action("Set company property", Description = "Set a specific property of a company")]
    public async Task<CompanyEntity> SetCompanyProperty([ActionParameter] CompanyRequest company,
        [ActionParameter] SetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);
        
        var response = await Client.
            SetProperty<CompanyProperties>(request, property.Property, property.Value);
        
        return new(response);
    }

    [Action("Create company", Description = "Create a new company")]
    public Task<BaseObject> CreateCompany([ActionParameter] CompanyProperties company)
    {
        var request = new HubspotRequest("/crm/v3/objects/companies", Method.Post, Creds)
            .AddObject(company);

        return Client.PostObject(request);
    }

    [Action("Delete company", Description = "Delete a company")]
    public Task DeleteCompany([ActionParameter] CompanyRequest company)
    {
        var endpoint = $"/crm/v3/objects/companies/{company.CompanyId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);
       
        return Client.ExecuteWithErrorHandling(request);
    }
}