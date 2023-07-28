using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Webhooks.Payloads
{
    public class GenericPayload
    {
        [Display("ID")]
        public string ObjectId { get; set; }
    }
}
