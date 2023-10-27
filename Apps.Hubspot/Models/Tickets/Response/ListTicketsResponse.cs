using Apps.Hubspot.Crm.Models.Entities;

namespace Apps.Hubspot.Crm.Models.Tickets.Response;

public record ListTicketsResponse(List<TicketEntity> Tickets);