using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm
{
    public class ApplicationConstants
    {
        public const string ClientId = "#{HUBSPOTCRM_CLIENT_ID}#";

        public const string Scope = "#{HUBSPOTCRM_SCOPE}#";

        public const string ClientSecret = "#{HUBSPOTCRM_SECRET}#";

        public const string BridgeServiceUrl = "#{HUBSPOTCRM_BRIDGE_SERVICE_TEMP}#"; // TO DO: remove

        public const string BlackbirdToken = "#{HUBSPOTCRM_BLACKBIRD_TOKEN}#"; // bridge validates this token
    }
}