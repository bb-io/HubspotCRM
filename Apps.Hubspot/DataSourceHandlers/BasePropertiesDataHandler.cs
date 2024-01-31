using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Filters;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.DataSourceHandlers
{
    public abstract class BasePropertiesDataHandler : HubspotInvocable, IAsyncDataSourceHandler
    {
        protected abstract string ObjectType { get; }
        public BasePropertiesDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
            CancellationToken cancellationToken)
        {
            var endpoint = $"/crm/v3/properties/{ObjectType}";
            var request = new HubspotRequest(endpoint, Method.Get, Creds);

            var properties = await Client.ExecuteWithErrorHandling<MultipleObjects<Property>>(request);

            return properties.Results
                .Where(x => x.Type == "string" && x.Name != null && x.Label != null)
                .Where(x => context.SearchString == null ||
                                   x.Label!.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(k => k.Name!, v => v.Label!);
        }
    }
}
