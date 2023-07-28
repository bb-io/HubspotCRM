using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class Deal
    {
        public string? Amount { get; set; }
        [Display("Deal name")]
        public string? Dealname { get; set; }

        [Display("Deal stage")]
        public string? Dealstage { get; set; }

        [Display("Pipeline")]
        public string? Pipeline { get; set; }

        [Display("Hubspot owner ID")]
        public string? Hubspot_owner_id { get; set; }

        [Display("Date closed")]
        public DateTime? Closedate { get; set; }
    }
}
