using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Contacts.Request;
using Apps.Hubspot.Crm.Models.Contacts.Response;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Properties.Request;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class ContactActions : HubspotInvocable
{
    public ContactActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get all contacts", Description = "Get a list of all contacts")]
    public async Task<ListItemsResponse> GetContacts()
    {
        var request = new HubspotRequest("/crm/v3/objects/contacts", Method.Get, Creds);

        var response = await Client.GetMultipleObjects(request);
        return new(response);
    }

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
        [ActionParameter] GetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        return Client.GetProperty(request, property.Property);
    }

    [Action("Set contact property", Description = "Set a specific property of a contact")]
    public async Task<ContactEntity> SetContactProperty(
        [ActionParameter] ContactRequest contact,
        [ActionParameter] SetPropertyRequest property)
    {
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var response = await Client
            .SetProperty<ContactProperties>(request, property.Property, property.Value);

        return new(response);
    }

    [Action("Create contact", Description = "Create a new contact")]
    public Task<BaseObject> CreateContact([ActionParameter] ContactProperties contact)
    {
        var request = new HubspotRequest("/crm/v3/objects/contacts", Method.Post, Creds)
            .AddObject(contact);

        return Client.PostObject(request);
    }

    [Action("Delete contact", Description = "Delete a contact")]
    public Task DeleteContact([ActionParameter] ContactRequest contact)
    {
        var endpoint = $"/crm/v3/objects/contacts/{contact.ContactId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }
}