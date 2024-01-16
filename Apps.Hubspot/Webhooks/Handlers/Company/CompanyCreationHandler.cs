using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyCreationHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.creation";

    public CompanyCreationHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}