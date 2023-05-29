using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class Ticket
    {
        public string Content { get; set; }
        public string hs_ticket_priority { get; set; }
        public string Subject { get; set; }
    }
}
