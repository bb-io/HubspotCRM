using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm
{
    public static class Extensions
    {
        public static string ToApiPropertyName(this string propertyName) => propertyName.ToLower().Replace(" ", "_");
    }
}
