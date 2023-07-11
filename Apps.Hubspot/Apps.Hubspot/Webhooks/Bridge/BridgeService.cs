using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Webhooks.Payloads;
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
            var request = new RestRequest($"/{PortalId}/{_event}", Method.Post);
            request.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
            request.AddBody(url);
            client.Execute(request);
        }

        public void Unsubscribe(string _event, string url)
        {
            var client = new RestClient(ApplicationConstants.BridgeServiceUrl);
            var requestGet = new RestRequest($"/{PortalId}/{_event}", Method.Get);
            requestGet.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
            var webhooks = client.Get<List<BridgeGetResponse>>(requestGet);
            var webhook = webhooks.FirstOrDefault(w => w.Value == url);

            var requestDelete = new RestRequest($"/{PortalId}/{_event}/{webhook.Id}", Method.Delete);
            requestDelete.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
            client.Delete(requestDelete);
        }
    }
}
