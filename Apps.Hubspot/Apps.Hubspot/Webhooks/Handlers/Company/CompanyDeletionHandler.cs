using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company
{
    public class CompanyDeletionHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "company.deletion";

        public CompanyDeletionHandler() : base(SubscriptionEvent) { }
    }
}
