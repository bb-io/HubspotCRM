namespace Apps.Hubspot.Crm.Models.Filters;

public class FilterRequest
{
    public FilterGroup[] FilterGroup { get; set; }
    public string[] Properties { get; set; }

    public FilterRequest(string value, string property, string @operator, string[] properties)
    {
        Properties = properties;
        FilterGroup = new[]
        {
            new FilterGroup()
            {
                Filters = new[]
                {
                    new Filter()
                    {
                        Value = value,
                        PropertyName = property,
                        Operator = @operator
                    }
                }
            }
        };
    }
}