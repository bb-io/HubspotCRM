using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers
{
    public class TicketPropertiesDataHandler : BasePropertiesDataHandler
    {
        protected override string ObjectType => "tickets";

        public TicketPropertiesDataHandler(InvocationContext invocationContext) : base(invocationContext) { }
    }
}
