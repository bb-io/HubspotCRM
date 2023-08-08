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
    public class QuoteHandler : BaseInvocable, IDataSourceHandler
    {
        public QuoteHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public Dictionary<string, string> GetData(DataSourceContext context)
        {
            var contextInv = InvocationContext;
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/quote/search", Method.Post, contextInv.AuthenticationCredentialsProviders);
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
                                propertyName = "hs_title",
                                @operator = "CONTAINS_TOKEN"
                            }
                        }
                    }
                },
                properties = new[] { "hs_title" }
            });
            var companies = client.Execute<MultipleObjects<QuoteIdName>>(request).Data.Results;
            return companies.ToDictionary(k => k.Id, v => v.Properties.Quotename);
        }
    }

    public class QuoteIdName
    {
        public string Id { get; set; }

        public QuoteNameProperty Properties { get; set; }
    }

    public class QuoteNameProperty
    {
        [JsonPropertyName("hs_title")]
        public string Quotename { get; set; }
    }
}
