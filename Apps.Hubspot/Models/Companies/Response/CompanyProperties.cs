﻿using System.Text.Json.Serialization;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Hubspot.Crm.Models.Companies.Response;

public class CompanyProperties
{
    public string? Domain { get; set; }
    
    public string? Name { get; set; }
    
    public string? City { get; set; }
    
    public string? Industry { get; set; }
    
    public string? Phone { get; set; }
    
    public string? State { get; set; }
        
    [Display("Lifecycle stage")]
    [JsonProperty("lifecyclestage")]
    public string? Lifecyclestage { get; set; }

    [Display("Street address 1")]
    [JsonProperty("streetaddress1")]
    public string? StreetAddress1 { get; set; }

    [Display("Street address 2")]
    [JsonProperty("streetaddress2")]
    public string? StreetAddress2 { get; set; }

    [Display("Postal code")]
    [JsonProperty("postalcode")]
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}