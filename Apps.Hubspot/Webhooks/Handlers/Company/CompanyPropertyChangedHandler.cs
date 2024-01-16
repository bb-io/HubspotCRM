using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyPropertyChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.propertyChange";

    public CompanyPropertyChangedHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}