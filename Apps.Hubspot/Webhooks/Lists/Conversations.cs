using Apps.Hubspot.Crm.Webhooks.Handlers.Conversations;
using Apps.Hubspot.Crm.Webhooks.Inputs;
using Apps.Hubspot.Crm.Webhooks.Lists.Base;
using Apps.Hubspot.Crm.Webhooks.Payloads;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Hubspot.Crm.Webhooks.Lists;

[WebhookList]
public class Conversations : BaseWebhookList
{
    [Webhook("On conversation created", typeof(ConversationCreatedHandler), Description = "On conversation created")]
    public Task<WebhookResponse<GenericPayload>> OnConversationCreated(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On conversation deleted", typeof(ConversationDeletedHandler), Description = "On conversation deleted")]
    public Task<WebhookResponse<GenericPayload>> OnConversationDeleted(WebhookRequest webhookRequest)
        => HandleWebhookResponse<GenericPayload>(webhookRequest);
    
    [Webhook("On conversation deleted for privacy", typeof(ConversationDeletedForPrivacyHandler), Description = "On conversation deleted for privacy")]
    public Task<WebhookResponse<AssociationChangedPayload>> OnConversationDeletedForPrivacy(WebhookRequest webhookRequest)
        => HandleWebhookResponse<AssociationChangedPayload>(webhookRequest);
    
    [Webhook("On conversation new message", typeof(ConversationNewMessageHandler), Description = "On conversation new message")]
    public Task<WebhookResponse<AssociationChangedPayload>> OnConversationNewMessage(WebhookRequest webhookRequest)
        => HandleWebhookResponse<AssociationChangedPayload>(webhookRequest);
    
    [Webhook("On conversation property changed", typeof(ConversationPropertyChangedHandler), Description = "On conversation property changed")]
    public Task<WebhookResponse<PropertyChangedPayload>> OnConversationPropertyChanged(
        WebhookRequest webhookRequest, [WebhookParameter] PropertyChangedInput input)
        => HandlePropertyChangedWebhookResponse(webhookRequest, input);
}