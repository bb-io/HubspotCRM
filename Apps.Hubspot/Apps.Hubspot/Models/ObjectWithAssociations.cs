namespace Apps.Hubspot.Crm.Models
{
    public class ObjectWithAssociations
    {
        public List<Association>? Results { get; set; }

        internal List<string> GetDistinctIds() => Results.Select(x => x.Id).Distinct().ToList();
    }
}
