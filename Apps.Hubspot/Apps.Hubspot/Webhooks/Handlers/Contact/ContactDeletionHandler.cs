using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Contact
{
    public class ContactDeletionHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "contact.deletion";

        public ContactDeletionHandler() : base(SubscriptionEvent) { }
    }
}
