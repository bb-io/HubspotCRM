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
    public class TicketHandler : BaseInvocable, IDataSourceHandler
    {
        public TicketHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public Dictionary<string, string> GetData(DataSourceContext context)
        {
            var contextInv = InvocationContext;
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/tickets/search", Method.Post, contextInv.AuthenticationCredentialsProviders);
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
                                propertyName = "subject",
                                @operator = "CONTAINS_TOKEN"
                            }
                        }
                    }
                },
                properties = new[] { "subject" }
            });
            var companies = client.Execute<MultipleObjects<TicketIdName>>(request).Data.Results;
            return companies.ToDictionary(k => k.Id, v => v.Properties.Subject);
        }
    }

    public class TicketIdName
    {
        public string Id { get; set; }

        public TicketNameProperty Properties { get; set; }
    }

    public class TicketNameProperty
    {
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
    }
}
