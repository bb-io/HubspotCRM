using Apps.Hubspot.Crm.Models.Contacts.Response;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Entities;

public class ContactEntity
{
    [Display("ID")] public string? Id { get; set; }

    public string? Email { get; set; }
    [Display("First name")] public string? Firstname { get; set; }

    [Display("Last name")] public string? Lastname { get; set; }
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string? Website { get; set; }

    [Display("Job title")] public string? Jobtitle { get; set; }

    public ContactEntity(BaseObjectWithProperties<ContactProperties> obj)
    {
        Id = obj.Id;
        Email = obj.Properties.Email;
        Firstname = obj.Properties.Firstname;
        Lastname = obj.Properties.Lastname;
        Phone = obj.Properties.Phone;
        Company = obj.Properties.Company;
        Website = obj.Properties.Website;
        Jobtitle = obj.Properties.Jobtitle;
    }
}