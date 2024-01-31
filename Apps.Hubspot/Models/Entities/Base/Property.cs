using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models.Entities.Base
{
    public class Property
    {
        public string? Name { get; set; }
        public string? Label { get; set; }
        public string? Type { get; set; }
        public string? FieldType { get; set; }
        public string? Description { get; set; }
    }
}
