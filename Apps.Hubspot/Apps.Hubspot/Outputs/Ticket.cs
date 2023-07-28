using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Outputs
{
    public class Ticket
    {
        [Display("ID")]
        public string? Id { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }

        [Display("Company ID")]
        public string? CompanyId { get; set; }
        public string? Priority { get; set; }
    }
}
