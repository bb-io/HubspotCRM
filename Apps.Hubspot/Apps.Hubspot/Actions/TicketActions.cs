using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions
{
    [ActionList]
    public class TicketActions
    {
        [Action("Get ticket", Description = "Get information of a specific ticket")]
        public TicketEntity? GetTicket(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Ticket ID")] string ticketId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/tickets/{ticketId}", Method.Get, authenticationCredentialsProviders);
            request.AddQueryParameter("associations", "companies");
            var response = client.GetFullObject<Models.Ticket>(request);
            return new TicketEntity
            {
                Id = response.Id,
                Description = response.Properties.Content,
                Subject = response.Properties.Subject,
                Priority = response.Properties.hs_ticket_priority,
                CompanyIds = response.Associations?["companies"].GetDistinctIds()
            };
        }

        [Action("Get ticket property", Description = "Get a specific property of a ticket")]
        public CustomPropertyEntity GetTicketProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Ticket ID")] string ticketId, [ActionParameter][Display("Property")] string property)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/ticket/{ticketId}", Method.Get, authenticationCredentialsProviders);
            return client.GetProperty(request, property);
        }

        [Action("Set ticket property", Description = "Set a specific property of a ticket")]
        public Models.Ticket SetTicketProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Ticket ID")] string ticketId, [ActionParameter][Display("Property")] string property, [ActionParameter][Display("Property")] string value)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/ticket/{ticketId}", Method.Patch, authenticationCredentialsProviders);
            return client.SetProperty<Models.Ticket>(request, property, value);
        }
    }
}
