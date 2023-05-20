using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact
{
    public class ContactCreationHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "contact.creation";

        public ContactCreationHandler() : base(SubscriptionEvent) { }
    }
}
