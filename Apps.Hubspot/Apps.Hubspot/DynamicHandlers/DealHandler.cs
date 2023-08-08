using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.DynamicHandlers
{
    public class DealHandler : BaseInvocable, IDataSourceHandler
    {
        public DealHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public Dictionary<string, string> GetData(DataSourceContext context)
        {
            var contextInv = InvocationContext;
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/deals/search", Method.Post, contextInv.AuthenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                filterGroups = new[]
                {
                    new
                    {
                        filters = new[]
                        {
                            new
                            {
                                value = $"*{context.SearchString}*",
                                propertyName = "dealname",
                                @operator = "CONTAINS_TOKEN"
                            }
                        }
                    }
                },
                properties = new[] { "dealname" }
            });
            var companies = client.Execute<MultipleObjects<DealIdName>>(request).Data.Results;
            return companies.ToDictionary(k => k.Id, v => v.Properties.Dealname);
        }
    }

    public class DealIdName
    {
        public string Id { get; set; }

        public DealNameProperty Properties { get; set; }
    }

    public class DealNameProperty
    {
        [JsonPropertyName("dealname")]
        public string Dealname { get; set; }
    }
}
