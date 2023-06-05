using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Outputs;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System.ComponentModel.Design;

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
        public Outputs.Company GetCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter]string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            request.AddQueryParameter("associations", "contacts");
            var response = client.GetFullObject<Models.Company>(request);
            return new Outputs.Company
            {
                Id = response.Id,
                Name = response.Properties.Name,
                Domain = response.Properties.Domain,
                ContactIds = response.Associations?["contacts"].GetDistinctIds(),
            };
        }

        [Action("Get company property", Description = "Get a specific property of a company")]
        public CustomProperty GetCompanyProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string companyId, [ActionParameter] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Get company address", Description = "Get company address")]
        public GetCompanyAddressResponse GetCompanyAddress(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            return new GetCompanyAddressResponse()
            {
                StreetAddress1 = client.GetProperty(request, "Street address").Property,
                StreetAddress2 = client.GetProperty(request, "Street address 2").Property,
                PostalCode = client.GetProperty(request, "Postal code").Property,
                City = client.GetProperty(request, "City").Property,
                State = client.GetProperty(request, "State/Region").Property,
                Country = client.GetProperty(request, "Country/Region").Property
            };
        }

        [Action("Set company property", Description = "Set a specific property of a company")]
        public Models.Company SetCompanyProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string companyId, [ActionParameter] string property, [ActionParameter] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Models.Company>(request, property, value);
        }

        [Action("Create company", Description = "Create a new company")]
        public BaseObject? CreateCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
            [ActionParameter] Models.Company company)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies", Method.Post, authenticationCredentialsProviders);
            request.AddObject(company);
            return client.PostObject(request);
        }

        [Action("Delete company", Description = "Delete a company")]
        public void DeleteCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
