using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Models.Quotes.Request;

public class QuoteRequest
{
    [Display("Quote ID")]
    [DataSource(typeof(QuoteDtaHandler))]
    public string QuoteId { get; set; }
}