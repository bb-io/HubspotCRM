using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;

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
        public ContactEntity GetContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Contact ID")] string contactId)
        {
            var client = new HubspotClient();
            var properties = new[] { "firstname", "lastname", "email", "phone", "company", "website", "jobtitle" };
            var request = new HubspotRequest($"/crm/v3/objects/contacts/{contactId}?properties={string.Join(',', properties)}", Method.Get, authenticationCredentialsProviders);
           
            var response = client.GetFullObject<ContactProperties>(request);
            return new(response);
        }

        [Action("Get contact property", Description = "Get a specific property of a contact")]
        public CustomPropertyEntity GetContactProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Contact ID")] string contactId, [ActionParameter][Display("Property")] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts/{contactId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set contact property", Description = "Set a specific property of a contact")]
        public Models.ContactProperties SetContactProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Contact ID")] string contactId, [ActionParameter][Display("Property")] string property, [ActionParameter][Display("Value")] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contact/{contactId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Models.ContactProperties>(request, property, value);
        }

        [Action("Create contact", Description = "Create a new contact")]
        public BaseObject? CreateContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] ContactProperties contact)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts", Method.Post, authenticationCredentialsProviders);
            request.AddObject(contact);
            return client.PostObject(request);
        }

        [Action("Delete contact", Description = "Delete a contact")]
        public void DeleteContact(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Contact ID")] string contactId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/contacts/{contactId}", Method.Delete, authenticationCredentialsProviders);
            client.Execute(request);
        }
    }
}
