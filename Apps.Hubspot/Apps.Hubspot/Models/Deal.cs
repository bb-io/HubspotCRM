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
        public string? Dealname { get; set; }
        public string? Dealstage { get; set; }
        public string? Pipeline { get; set; }
        public string? Hubspot_owner_id { get; set; }
        public DateTime? Closedate { get; set; }
    }
}
