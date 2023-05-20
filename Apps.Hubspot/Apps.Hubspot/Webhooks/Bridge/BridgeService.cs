using Apps.Hubspot.Crm.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Webhooks.Bridge
{
    public class BridgeService
    {
        internal int PortalId { get; set; }

        public BridgeService(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) 
        {
            var client = new HubspotClient();
            var detailsRequest = new HubspotRequest("/account-info/v3/details", Method.Get, authenticationCredentialsProviders);
            var details = client.Get<Details>(detailsRequest);

            if (details == null) throw new Exception("Could not fetch account details");
            PortalId = details.PortalId;

        }
        public void Subscribe(string _event, string url)
        {
            var client = new RestClient(ApplicationConstants.BridgeServiceUrl);
            var request = new RestRequest(ApplicationConstants.BridgeServicePath, Method.Post);
            request.AddJsonBody(new BridgeHook { PortalId = PortalId, Event = _event, Url = url });
            client.Execute(request);
        }

        public void Unsubscribe(string _event)
        {
            var client = new RestClient(ApplicationConstants.BridgeServiceUrl);
            var request = new RestRequest(ApplicationConstants.BridgeServicePath, Method.Delete);
            request.AddQueryParameter("id", PortalId);
            request.AddQueryParameter("event", _event);
            client.Execute(request);
        }
    }
}
