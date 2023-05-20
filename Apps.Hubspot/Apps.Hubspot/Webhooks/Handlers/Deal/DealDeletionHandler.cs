using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal
{
    public class DealDeletionHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "deal.deletion";

        public DealDeletionHandler() : base(SubscriptionEvent) { }
    }
}
