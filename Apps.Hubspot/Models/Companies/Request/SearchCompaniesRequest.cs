using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Models.Companies.Request
{
    public class SearchCompaniesRequest
    {
        [Display("Status")]
        [DataSource(typeof(CompanyLifecycleStageDataHandler))]
        public IEnumerable<string>? Status { get; set; }

        [Display("Created from")]
        public DateTime? CreatedFrom { get; set; }

        [Display("Created to")]
        public DateTime? CreatedTo { get; set; }

        [Display("Updated from")]
        public DateTime? UpdatedFrom { get; set; }

        [Display("Updated to")]
        public DateTime? UpdatedTo { get; set; }
    }
}
