using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Webhooks.Bridge
{
    public class BridgeHook
    {
        public int PortalId { get; set; }
        public string Event { get; set; }
        public string Url { get; set; }
    }
}
