using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers
{
    public class CompanyLifecycleStageDataHandler(InvocationContext invocationContext) : HubspotInvocable(invocationContext), IAsyncDataSourceHandler
    {
        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new HubspotRequest("/crm/v3/properties/companies/lifecyclestage", Method.Get, Creds);
            var response = await Client.ExecuteWithErrorHandling<HubspotPropertyResponse>(request);

            var search = (context.SearchString ?? string.Empty).Trim();

            var options = response.Options ?? new List<HubspotPropertyOption>();

            var filtered = options
                .Where(o => o != null)
                .Where(o => o.Hidden != true)
                .Where(o => string.IsNullOrWhiteSpace(search) ||
                            (o.Label?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (o.Value?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToDictionary(
                    o => o.Value ?? string.Empty,
                    o => string.IsNullOrWhiteSpace(o.Label) ? (o.Value ?? string.Empty) : o.Label!
                );

            filtered.Remove(string.Empty);
            return filtered;
        }
    }

    public class HubspotPropertyResponse
    {
        [JsonProperty("options")]
        public List<HubspotPropertyOption>? Options { get; set; }
    }

    public class HubspotPropertyOption
    {
        [JsonProperty("label")]
        public string? Label { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }

        [JsonProperty("hidden")]
        public bool? Hidden { get; set; }
    }
}