using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models.Contacts.Request;
using Apps.Hubspot.Crm.Models.Contacts.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common.Invocation;
using Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Actions;

[ActionList("Contacts")]
public class ContactActions(InvocationContext invocationContext) : HubspotInvocable(invocationContext)
{
    [Action("Get contact", Description = "Get information of a specific contact")]
    public async Task<ContactEntity> GetContact([ActionParameter] ContactRequest contact)
    {
        var properties = new[] { "firstname", "lastname", "email", "phone", "company", "website", "jobtitle" };
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}?properties={string.Join(',', properties)}";

        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var response = await Client.GetFullObject<ContactProperties>(request);
        return new(response);
    }

    [Action("Get contact property", Description = "Get a specific property of a contact")]
    public Task<CustomPropertyEntity> GetContactProperty(
        [ActionParameter] ContactRequest contact,
        [ActionParameter][DataSource(typeof(ContactPropertiesDataHandler))][Display("Property")] string property)
    {
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        return Client.GetProperty(request, property);
    }

    [Action("Set contact property", Description = "Set a specific property of a contact")]
    public async Task<ContactEntity> SetContactProperty(
        [ActionParameter] ContactRequest contact,
        [ActionParameter][DataSource(typeof(ContactPropertiesDataHandler))][Display("Property")] string property,
        [ActionParameter][Display("Value")] string value)
    {
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var response = await Client
            .SetProperty<ContactProperties>(request, property, value);

        return new(response);
    }

    [Action("Create contact", Description = "Create a new contact")]
    public async Task<ContactEntity> CreateContact([ActionParameter] ContactProperties contact)
    {
        var request = new HubspotRequest("/crm/v3/objects/contacts", Method.Post, Creds)
            .AddObject(contact);

        var response = await Client.GetFullObject<ContactProperties>(request);
        return new(response);
    }

    [Action("Delete contact", Description = "Delete a contact")]
    public Task DeleteContact([ActionParameter] ContactRequest contact)
    {
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }
}