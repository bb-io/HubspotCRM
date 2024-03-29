﻿using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.Webhooks.Handlers.Products;

public class ProductMergedHandler : BaseWebhookHandler
{
    const string SubscriptionEvent = "product.merge";

    public ProductMergedHandler(InvocationContext context) : base(context, SubscriptionEvent)
    {
    }
}