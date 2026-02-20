namespace Apps.Hubspot.Crm.Models.Entities.Base
{
    public class Property
    {
        public string? Name { get; set; }
        public string? Label { get; set; }
        public string? Type { get; set; }
        public string? FieldType { get; set; }
        public string? Description { get; set; }
        public bool? Calculated { get; set; }
        public bool? Archived { get; set; }
        public ModificationMetadata? ModificationMetadata { get; set; }
    }

    public class ModificationMetadata
    {
        public bool? ReadOnlyDefinition { get; set; }
        public bool? ReadOnlyValue { get; set; }
        public bool? Archivable { get; set; }
    }
}
