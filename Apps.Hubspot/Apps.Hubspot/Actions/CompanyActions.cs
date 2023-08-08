﻿using Apps.Hubspot.Crm.DynamicHandlers;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using RestSharp;
using System.Xml.Linq;

namespace Apps.Hubspot.Crm.Actions
{
    [ActionList]
    public class CompanyActions
    {

        [Action("Get all companies", Description = "Get a list of all companies")]
        public IEnumerable<BaseObject> GetCompanies(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/companies", Method.Get, authenticationCredentialsProviders);
            return client.GetMultipleObjects(request);
        }

        [Action("Get company", Description = "Get information of a specific company")]
        public CompanyEntity GetCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Company ID")][DataSource(typeof(CompanyHandler))] string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            request.AddQueryParameter("associations", "contacts");
            var response = client.GetFullObject<Company>(request);
            return new CompanyEntity
            {
                Id = response.Id,
                Name = response.Properties.Name,
                Domain = response.Properties.Domain,
                ContactIds = response.Associations?["contacts"].GetDistinctIds().Select(c => new ContactId() { Id = c }),
            };
        }

        [Action("Get company by custom property", Description = "Get company by custom property value")]
        public CompanyEntity GetCompanyByCustomValue(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetCompanyByCustomValueRequest input)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/search", Method.Post, authenticationCredentialsProviders);
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
            return GetCompany(authenticationCredentialsProviders, client.GetMultipleObjects(request).First().Id);
        }

        [Action("Get company property", Description = "Get a specific property of a company")]
        public CustomPropertyEntity GetCompanyProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Company ID")][DataSource(typeof(CompanyHandler))] string companyId, [ActionParameter][Display("Property")] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Get company address", Description = "Get company address")]
        public GetCompanyAddressResponse GetCompanyAddress(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Company ID")][DataSource(typeof(CompanyHandler))] string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            return new GetCompanyAddressResponse()
            {
                StreetAddress1 = client.GetProperty(request, "address").Property,
                StreetAddress2 = client.GetProperty(request, "address2").Property,
                PostalCode = client.GetProperty(request, "zip").Property,
                City = client.GetProperty(request, "city").Property,
                State = client.GetProperty(request, "state").Property,
                Country = client.GetProperty(request, "country").Property
            };
        }

        [Action("Set company property", Description = "Set a specific property of a company")]
        public Company SetCompanyProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Company ID")][DataSource(typeof(CompanyHandler))] string companyId, [ActionParameter][Display("Property")] string property, [ActionParameter][Display("Value")] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Company>(request, property, value);
        }

        [Action("Create company", Description = "Create a new company")]
        public BaseObject? CreateCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
            [ActionParameter] Company company)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies", Method.Post, authenticationCredentialsProviders);
            request.AddObject(company);
            return client.PostObject(request);
        }

        [Action("Delete company", Description = "Delete a company")]
        public void DeleteCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Company ID")][DataSource(typeof(CompanyHandler))] string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
