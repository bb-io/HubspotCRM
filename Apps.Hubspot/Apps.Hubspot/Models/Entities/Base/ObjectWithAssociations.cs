namespace Apps.Hubspot.Crm.Models.Entities.Base;

public class ObjectWithAssociations
{
    public List<AssociationEntity>? Results { get; set; }

    internal IEnumerable<string> GetDistinctIds() => Results.Select(x => x.Id).Distinct();
}