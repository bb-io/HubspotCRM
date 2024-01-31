using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers
{
    public class QuotePropertiesDataHandler : BasePropertiesDataHandler
    {
        protected override string ObjectType => "quotes";

        public QuotePropertiesDataHandler(InvocationContext invocationContext) : base(invocationContext) { }
    }
}
