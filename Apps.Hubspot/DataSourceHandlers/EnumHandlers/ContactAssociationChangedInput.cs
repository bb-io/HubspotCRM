using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Hubspot.Crm.DataSourceHandlers.EnumHandlers
{
    public class ContactAssociationEnumDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            { "CONTACT_TO_CONTACT", "Contact to contact" },
            { "CONTACT_TO_COMPANY", "Contact to company" },
            { "CONTACT_TO_DEAL", "Contact to deal" },
            { "CONTACT_TO_TICKET", "Contact to ticket" },
            { "CONTACT_TO_SUBSCRIPTION", "Contact to subscription" },
            { "CONTACT_TO_INVOICE", "Contact to invoice" }
        };
    }
}
