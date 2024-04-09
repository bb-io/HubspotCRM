using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.associationChange";

    public CompanyAssociationChangedHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}