using Apps.Hubspot.Crm.Extensions;

namespace Apps.Hubspot.Crm.Utils;

public static class IntParser
{
    public static int? Parse(string? input, string errorName)
    {
        if (input is null)
            return null;
        
        if (!int.TryParse(input, out var intValue))
            throw new Exception($"{errorName.ToPascalCase()} should be a number");

        return intValue;
    }  
}