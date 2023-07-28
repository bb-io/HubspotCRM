using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class Association
    {

        [Display("ID")]
        public string? Id { get; set; }
        public string? Type { get; set; }
    }
}
