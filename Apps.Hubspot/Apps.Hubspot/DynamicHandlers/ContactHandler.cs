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
    public class ContactHandler : BaseInvocable, IDataSourceHandler
    {
        public ContactHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public Dictionary<string, string> GetData(DataSourceContext context)
        {
            var contextInv = InvocationContext;
            var client = new HubspotClient();
            var request = new HubspotRequest("/crm/v3/objects/contacts/search", Method.Post, contextInv.AuthenticationCredentialsProviders);
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
                                propertyName = "firstname",
                                @operator = "CONTAINS_TOKEN"
                            }
                        }
                    }
                },
                properties = new[] { "firstname", "lastname" }
            });
            var companies = client.Execute<MultipleObjects<ContactIdName>>(request).Data.Results;
            return companies.ToDictionary(k => k.Id, v => $"{v.Properties.Firstname} {v.Properties.Lastname}");
        }
    }

    public class ContactIdName
    {
        public string Id { get; set; }

        public FirstNameProperty Properties { get; set; }
    }

    public class FirstNameProperty
    {
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }
    }
}
