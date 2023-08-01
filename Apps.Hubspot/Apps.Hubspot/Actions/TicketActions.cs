using Apps.Hubspot.Crm.Outputs;
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
        public Outputs.Ticket? GetTicket(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter][Display("Ticket ID")] string ticketId)
        {
            var client = new HubspotClient();
            var request = new HubspotRequest($"/crm/v3/objects/tickets/{ticketId}", Method.Get, authenticationCredentialsProviders);
            request.AddQueryParameter("associations", "companies");
            var response = client.GetFullObject<Models.Ticket>(request);
            return new Outputs.Ticket
            {
                Id = response.Id,
                Description = response.Properties.Content,
                Subject = response.Properties.Subject,
                Priority = response.Properties.hs_ticket_priority,
                CompanyId = response.Associations?["companies"].GetSingleId()
            };
        }

        [Action("Get ticket property", Description = "Get a specific property of a ticket")]
        public CustomProperty GetTicketProperty(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
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
