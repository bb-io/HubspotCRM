namespace Apps.Hubspot.Crm.Models.Entities.Base;

public class BaseObjectWithProperties<T> : BaseObject
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public T Properties { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Dictionary<string, ObjectWithAssociations>? Associations { get; set; }
}