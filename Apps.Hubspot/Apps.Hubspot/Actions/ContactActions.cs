using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;

namespace Apps.Hubspot.Crm.Actions
{
    [ActionList]
    public class ContactActions
    {
        [Action("Get all contacts", Description = "Get a list of all contacts")]
        public IEnumerable<BaseObject> GetContacts(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/contacts", Method.Get, authenticationCredentialsProviders);
            return client.GetMultipleObjects(request);
        }

        [Action("Get contact", Description = "Get information of a specific contact")]
        public Contact? GetContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string contactId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts/{contactId}", Method.Get, authenticationCredentialsProviders);
            return client.GetObject<Contact>(request);
        }

        [Action("Create contact", Description = "Create a new contact")]
        public BaseObject? CreateContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] Contact contact)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts", Method.Post, authenticationCredentialsProviders);
            request.AddObject(contact);
            return client.PostObject(request);
        }

        [Action("Update contact", Description = "Update a contact's information")]
        public Contact? UpdateContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string contactId, [ActionParameter] Contact contact)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts/${contactId}", Method.Patch, authenticationCredentialsProviders);
            request.AddObject(contact);
            return client.PatchObject<Contact>(request);
        }

        [Action("Delete contact", Description = "Delete a contact")]
        public void DeleteContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] string contactId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts/{contactId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
