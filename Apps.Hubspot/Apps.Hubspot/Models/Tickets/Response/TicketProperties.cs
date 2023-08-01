﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Tickets.Response
{
    public class TicketProperties
    {
        public string Content { get; set; }

        [Display("Hubspot priority")]
        public string hs_ticket_priority { get; set; }
        public string Subject { get; set; }
    }
}