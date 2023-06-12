using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Outputs
{
    public class Company
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Domain { get; set; }
        public IEnumerable<ContactId>? ContactIds { get; set; }
    }

    public class ContactId
    {
        public string Id { get; set; }
    }
}
