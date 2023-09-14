namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyDeletionHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.deletion";

    public CompanyDeletionHandler() : base(SubscriptionEvent) { }
}