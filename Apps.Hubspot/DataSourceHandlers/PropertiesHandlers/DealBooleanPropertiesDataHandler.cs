using Apps.Hubspot.Crm.Models.Entities.Base;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers
{
    public class DealBooleanPropertiesDataHandler : BasePropertiesDataHandler
    {
        protected override string ObjectType => "deals";

        public DealBooleanPropertiesDataHandler(InvocationContext invocationContext)
            : base(invocationContext) { }

        protected override bool IncludeProperty(Property p)
        {
            if (!string.IsNullOrWhiteSpace(p.FieldType) &&
                string.Equals(p.FieldType, "booleancheckbox", StringComparison.OrdinalIgnoreCase))
                return true;

            return !string.IsNullOrWhiteSpace(p.Type) &&
                   (string.Equals(p.Type, "bool", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(p.Type, "boolean", StringComparison.OrdinalIgnoreCase));
        }
    }
}
