using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Properties.Request;
using Apps.Hubspot.Crm.Models.Tickets.Request;
using Apps.Hubspot.Crm.Models.Tickets.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class TicketActions : HubspotInvocable
{
    public TicketActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    //[Action("Get all tickets", Description = "Get a list of all tickets")]
    //public async Task<ListTicketsResponse> ListTickets()
    //{
    //    var request = new HubspotRequest("/crm/v3/objects/tickets", Method.Get, Creds)
    //        .AddQueryParameter("associations", "companies");

    //    var response = await Client.Paginate<BaseObjectWithProperties<TicketProperties>>(request);
    //    var tickets = response
    //        .Select(x => new TicketEntity(x))
    //        .ToList();

    //    return new(tickets);
    //}

    [Action("Get ticket", Description = "Get information of a specific ticket")]
    public async Task<TicketEntity> GetTicket([ActionParameter] TicketRequest ticket)
    {
        var endpoint = $"/crm/v3/objects/tickets/{ticket.TicketId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds)
            .AddQueryParameter("associations", "companies");

        var response = await Client.GetFullObject<TicketProperties>(request);

        return new(response);
    }

    [Action("Get ticket property", Description = "Get a specific property of a ticket")]
    public Task<CustomPropertyEntity> GetTicketProperty(
        [ActionParameter] TicketRequest ticket,
        [ActionParameter][DataSource(typeof(TicketPropertiesDataHandler))][Display("Property")] string property)
    {
        var endpoint = $"/crm/v3/objects/tickets/{ticket.TicketId}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        return Client.GetProperty(request, property);
    }

    [Action("Set ticket property", Description = "Set a specific property of a ticket")]
    public async Task<TicketEntity> SetTicketProperty(
        [ActionParameter] TicketRequest ticket,
        [ActionParameter][DataSource(typeof(TicketPropertiesDataHandler))][Display("Property")] string property,
        [ActionParameter][Display("Value")] string value)
    {
        var endpoint = $"/crm/v3/objects/tickets/{ticket.TicketId}";
        var request = new HubspotRequest(endpoint, Method.Patch, Creds);

        var response = await Client
            .SetProperty<TicketProperties>(request, property, value);

        return new(response);
    }

    [Action("Create ticket", Description = "Create a ticket with the given properties")]
    public async Task<TicketEntity> CreateTicket([ActionParameter] CreateTicketRequest input)
    {
        var request = new HubspotRequest("/crm/v3/objects/tickets", Method.Post, Creds)
            .AddObject(input);

        var response = await Client.GetFullObject<TicketProperties>(request);
        return new(response);
    }

    [Action("Delete ticket", Description = "Move ticket to the recycling bin")]
    public Task DeleteTicket([ActionParameter] TicketRequest ticket)
    {
        var endpoint = $"/crm/v3/objects/tickets/{ticket.TicketId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }
}