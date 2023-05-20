using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class Error
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public List<SubError> Errors { get; set; }

        public override string ToString()
        {
            if (Errors == null)
                return Message;
            return $"{Message}: ${string.Join(" - ", Errors.Select(x => x.Message))}";
        }
    }

    public class SubError
    {
        public string SubCategory { get; set; }
        public string Message { get; set; }
    }
}
