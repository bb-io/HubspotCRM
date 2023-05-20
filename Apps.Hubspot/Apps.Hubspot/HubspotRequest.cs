using Apps.Hubspot.Crm.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm
{
    public class HubspotRequest : RestRequest
    {
        public HubspotRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
        {
            this.AddHeader("Authorization", authenticationCredentialsProviders.First(p => p.KeyName == "Authorization").Value);
            this.AddHeader("accept", "*/*");
        }

        public void AddObject<T>(T obj)
        {
            this.AddJsonBody(new ObjectWithProperties<T>{ Properties = obj });
        }
    }
}
