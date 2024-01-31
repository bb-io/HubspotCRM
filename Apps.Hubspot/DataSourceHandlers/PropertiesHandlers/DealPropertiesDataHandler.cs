using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers
{
    public class DealPropertiesDataHandler : BasePropertiesDataHandler
    {
        protected override string ObjectType => "deals";

        public DealPropertiesDataHandler(InvocationContext invocationContext) : base(invocationContext) { }
    }
}
