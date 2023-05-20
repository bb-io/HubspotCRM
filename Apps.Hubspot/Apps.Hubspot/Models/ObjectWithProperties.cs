using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class ObjectWithProperties<T>
    {
        public T Properties { get; set; }
    }
}
