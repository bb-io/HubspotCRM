using Apps.Hubspot.Crm.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Webhooks.Inputs
{
    public class ContactAssociationChangedInput
    {
        [Display("Association type")]
        [DataSource(typeof(ContactAssociationEnumDataHandler))]
        public string? AssociationType { get; set; }

        [Display("Is primary assosiation")]
        public bool? IsPrimaryAssosiation { get; set; }
    }
}
