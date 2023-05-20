using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal
{
    public class DealCreationHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "deal.creation";

        public DealCreationHandler() : base(SubscriptionEvent) { }
    }
}
