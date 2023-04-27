using System;
using UnityEngine;

/// <summary>
/// Unix Time measures time by the number of seconds that have elapsed 
/// since 00:00:00 UTC on 1 January 1970, the beginning of the Unix epoch, 
/// without adjustments made due to leap seconds
/// </summary>
public class UnixTimestampConverter : MonoBehaviour
{
    public static DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return origin.AddSeconds(timestamp);
    }

    public static double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return Math.Floor(diff.TotalSeconds);
    }

    //DateTimeOffset.Now.ToUnixTimeMilliseconds()
}
