﻿namespace Apps.Hubspot.Crm.Webhooks.Handlers.Models
{
    internal class WebhookThrottling
    {
        public int MaxConcurrentRequests { get; set; }
        public string Period { get; set; }
    }
}
