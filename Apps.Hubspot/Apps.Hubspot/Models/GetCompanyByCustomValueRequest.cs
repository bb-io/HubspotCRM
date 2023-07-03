using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class GetCompanyByCustomValueRequest
    {

        [Display("Custom property name")]
        public string CustomPropertyName { get; set; }

        [Display("Custom property value")]
        public string CustomPropertyValue { get; set; }
    }
}
