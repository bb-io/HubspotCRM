using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Webhooks.Handlers;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company
{
    public class CompanyCreationHandler : BaseWebhookHandler
    {
        const string SubscriptionEvent = "company.creation";

        public CompanyCreationHandler() : base(SubscriptionEvent) { }
    }
}
