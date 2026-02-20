using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers
{
    public abstract class BasePropertiesDataHandler : HubspotInvocable, IAsyncDataSourceHandler
    {
        protected abstract string ObjectType { get; }
        public BasePropertiesDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        protected virtual bool IncludeProperty(Property p)
        => string.Equals(p.Type, "string", StringComparison.OrdinalIgnoreCase);

        private static bool IsWritable(Property p)
            => p.Archived != true
               && p.Calculated != true
               && p.ModificationMetadata?.ReadOnlyValue != true;

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var endpoint = $"/crm/v3/properties/{ObjectType}";
            var request = new HubspotRequest(endpoint, Method.Get, Creds);

            var properties = await Client.ExecuteWithErrorHandling<MultipleObjects<Property>>(request);

            return properties.Results
                .Where(p => p.Name != null && p.Label != null)
                .Where(IncludeProperty)
                .Where(p => context.SearchString == null
                            || p.Label!.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase)
                            || p.Name!.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(k => k.Name!, v => BuildLabel(v));
        }

        private static string BuildLabel(Property p)
        {
            var label = p.Label ?? p.Name ?? string.Empty;
            return IsWritable(p) ? $"{label} (writable)" : $"{label} (read-only)";
        }
    }
}
