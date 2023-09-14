﻿namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealCreationHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.creation";

    public DealCreationHandler() : base(SubscriptionEvent) { }
}