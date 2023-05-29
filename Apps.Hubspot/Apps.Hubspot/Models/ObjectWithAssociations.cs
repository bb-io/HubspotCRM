using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Hubspot.Crm.Models
{
    public class ObjectWithAssociations
    {
        public List<Association>? Results { get; set; }

        internal List<string> GetDistinctIds() => Results.Select(x => x.Id).Distinct().ToList();
        internal string? GetSingleId() => Results?.FirstOrDefault()?.Id;
    }
}
