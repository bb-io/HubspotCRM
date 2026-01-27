using Apps.Hubspot.Crm.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Companies.Response
{
    public class SearchCompaniesResponse
    {
        [Display("Companies")]
        public IEnumerable<CompanyEntity> Companies { get; set; } = Array.Empty<CompanyEntity>();
    }
}
