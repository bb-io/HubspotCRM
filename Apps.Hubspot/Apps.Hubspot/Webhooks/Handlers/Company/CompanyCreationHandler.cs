namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyCreationHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.creation";

    public CompanyCreationHandler() : base(SubscriptionEvent) { }
}