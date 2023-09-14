using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Hubspot.Crm.DataSourceHandlers.EnumHandlers;

public class AssociationTypeHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"contacts", "Contacts"},
        {"companies", "Companies"},
        {"deals", "Deals"},
        {"tickets", "Tickets"},
        {"schemas", "Schemas"},
        {"calls", "Calls"},
        {"emails", "Emails"},
        {"meetings", "Meetings"},
        {"notes", "Notes"},
        {"tasks", "Tasks"},
        {"products", "Products"},
        {"postal_mail", "Postal mail"},
        {"communications", "Communications"},
        {"marketing_events", "Marketing events"},
        {"feedback_submissions", "Feedback submissions"},
    };
}