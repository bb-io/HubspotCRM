using Apps.Hubspot.Crm.Models.Companies.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class CompanyEntity : CompanyProperties
{
    [Display("ID")]
    public string? Id { get; set; }

    [Display("Contact IDs")]
    public IEnumerable<string>? ContactIds { get; set; }
    
    public CompanyEntity(BaseObjectWithProperties<CompanyProperties> response)
    {
        Id = response.Id;
        Name = response.Properties.Name;
        Domain = response.Properties.Domain;
        City = response.Properties.City;
        Industry = response.Properties.Industry;
        Phone = response.Properties.Phone;
        State = response.Properties.State;
        Lifecyclestage = response.Properties.Lifecyclestage;
        ContactIds = response.Associations?["contacts"].GetDistinctIds();
    }
}
