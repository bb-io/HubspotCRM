using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Models.Companies.Request;

public class CompanyRequest
{
    [Display("Company")]
    [DataSource(typeof(CompanyHandler))]
    public string CompanyId { get; set; }
}