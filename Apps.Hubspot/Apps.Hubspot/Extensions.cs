namespace Apps.Hubspot.Crm
{
    public static class Extensions
    {
        public static string ToApiPropertyName(this string propertyName) => propertyName.ToLower().Replace(" ", "_");
    }
}
