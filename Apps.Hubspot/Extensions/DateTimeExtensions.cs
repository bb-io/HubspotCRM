namespace Apps.Hubspot.Crm.Extensions;

public static class DateTimeExtensions
{
    public static string ToUnixMs(this DateTime dateTime)
    {
        var utc = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return new DateTimeOffset(utc).ToUnixTimeMilliseconds().ToString();
    }
}
