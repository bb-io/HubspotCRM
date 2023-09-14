namespace Apps.Hubspot.Crm.Extensions;

public static class StringExtensions
{
    public static string ToApiPropertyName(this string propertyName) => propertyName.ToLower().Replace(" ", "_");
}