using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Ticket
{
    public class TicketCreationHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "ticket.creation";

        public TicketCreationHandler() : base(SubscriptionEvent) { }
    }
}
