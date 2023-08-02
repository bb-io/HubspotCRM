namespace Apps.Hubspot.Crm.Webhooks.Handlers.Company;

public class CompanyMergeHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "company.merge";

    public CompanyMergeHandler() : base(SubscriptionEvent) { }
}