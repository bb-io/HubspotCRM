﻿using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class GetCompanyAddressResponse
    {
        [Display("Street address 1")]
        public string StreetAddress1 { get; set; }

        [Display("Street address 2")]
        public string StreetAddress2 { get; set; }

        [Display("Postal code")]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
