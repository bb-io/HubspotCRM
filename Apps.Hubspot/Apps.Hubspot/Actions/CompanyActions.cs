using Apps.Hubspot.Crm.Models;
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
        public Company? GetCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter]string companyId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/{companyId}", Method.Get, authenticationCredentialsProviders);
            return client.GetObject<Company>(request);
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

        [Action("Update company", Description = "Update a company's information")]
        public Company? UpdateCompany(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string companyId, [ActionParameter] Company company)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/companies/${companyId}", Method.Patch, authenticationCredentialsProviders);
            request.AddObject(company);
            return client.PatchObject<Company>(request);
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
