using System.Globalization;

namespace Apps.Hubspot.Crm.Extensions;

public static class DateTimeExtensions
{
    public static string ToUnixMs(this DateTime dateTime)
    {
        var utc = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return new DateTimeOffset(utc).ToUnixTimeMilliseconds().ToString();
    }

    public static string ToIso8601(this DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
    }
}
