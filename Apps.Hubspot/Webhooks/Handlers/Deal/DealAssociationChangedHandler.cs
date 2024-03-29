﻿using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Deal;

public class DealAssociationChangedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "deal.associationChange";

    public DealAssociationChangedHandler(InvocationContext context) : base(context, SubscriptionEvent) { }
}