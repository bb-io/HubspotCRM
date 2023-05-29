using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Outputs
{
    public class Ticket
    {
        public string? Id { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? CompanyId { get; set; }
        public string? Priority { get; set; }
    }
}
