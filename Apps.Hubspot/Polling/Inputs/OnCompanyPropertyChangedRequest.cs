using Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Polling.Inputs;

public class OnCompanyPropertyChangedRequest
{
    [Display("Property name"), DataSource(typeof(CompanyPropertiesDataHandler))]
    public string Property { get; set; } = string.Empty;
}