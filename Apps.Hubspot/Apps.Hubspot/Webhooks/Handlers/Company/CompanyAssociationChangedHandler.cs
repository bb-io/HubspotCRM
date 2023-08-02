namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.associationChange";

    public CompanyAssociationChangedHandler() : base(SubscriptionEvent) { }
}