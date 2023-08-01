using System.Text.RegularExpressions;

namespace Apps.Hubspot.Crm.Extensions;

public static class StringExtensions
{
    public static string ToApiPropertyName(this string propertyName) => propertyName.ToLower().Replace(" ", "_");

    public static string ToPascalCase(this string input)
        => Regex.Replace(input, @"\b\p{Ll}", match => match.Value.ToUpper());
}