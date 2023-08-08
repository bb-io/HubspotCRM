using Apps.Hubspot.Crm.DynamicHandlers;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Tickets.Request;
using Apps.Hubspot.Crm.Models.Tickets.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions
{
    [ActionList]
    public class TicketActions
    {
        [Action("List tickets", Description = "List all tickets")]
        public ListTicketsResponse ListTickets(
            IEnumerable<AuthenticationCredentialsProvider> creds)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/tickets", Method.Get, creds)
                .AddQueryParameter("associations", "companies");
            
            var response = client.Paginate<BaseObjectWithProperties<TicketProperties>>(request);
            var tickets = response.Select(x => new TicketEntity(x)).ToList();

            return new(tickets);
        }

        [Action("Get ticket", Description = "Get information of a specific ticket")]
        public TicketEntity? GetTicket(
            IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] [Display("Ticket ID")][DataSource(typeof(TicketHandler))]
            string ticketId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/tickets/{ticketId}", Method.Get,
                authenticationCredentialsProviders);
            request.AddQueryParameter("associations", "companies");
            var response = client.GetFullObject<TicketProperties>(request);

            return new TicketEntity(response);
        }

        [Action("Get ticket property", Description = "Get a specific property of a ticket")]
        public CustomPropertyEntity GetTicketProperty(
            IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] [Display("Ticket ID")][DataSource(typeof(TicketHandler))]
            string ticketId, [ActionParameter] [Display("Property")] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/ticket/{ticketId}", Method.Get,
                authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set ticket property", Description = "Set a specific property of a ticket")]
        public TicketProperties SetTicketProperty(
            IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] [Display("Ticket ID")][DataSource(typeof(TicketHandler))]
            string ticketId, [ActionParameter] [Display("Property name")] string property,
            [ActionParameter] [Display("Property value")] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/ticket/{ticketId}", Method.Patch,
                authenticationCredentialsProviders);
            return client.SetProperty<TicketProperties>(request, property, value);
        }
        
        
        [Action("Create ticket", Description = "Create a ticket with the given properties")]
        public TicketEntity? CreateTicket(
            IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] CreateTicketRequest input)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/tickets", Method.Post,
                authenticationCredentialsProviders);
            request.AddObject(input);
            
            var response = client.GetFullObject<TicketProperties>(request);
            return new TicketEntity(response);
        }
        
        [Action("Delete ticket", Description = "Move ticket to the recycling bin")]
        public void DeleteTicket(
            IEnumerable<AuthenticationCredentialsProvider> creds,
            [ActionParameter] [Display("Ticket ID")][DataSource(typeof(TicketHandler))]
            string ticketId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/tickets/{ticketId}", Method.Delete, creds);
            client.ExecuteWithError(request);
        }
    }
}