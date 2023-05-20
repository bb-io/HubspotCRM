using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class Quote
    {
        public string? hs_title { get; set; }
        public DateTime hs_expiration_date { get; set; }
    }
}
