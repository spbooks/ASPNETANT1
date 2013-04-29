using System;

public class HotlinkProtection
{
    //Site specific multiplier - might want to keep in web.config
    private const long multiplier = 298467;

    public static long GetKey()
    {
        int minutes = GetSecondCount();
        return (multiplier * minutes);
    }

    private static int GetSecondCount()
    {
        TimeSpan span = DateTime.Now - new DateTime(2005, 1, 1);
        return (int)span.TotalSeconds;
    }

    public static bool IsKeyValid(long key, int timeoutSeconds)
    {
        try
        {
            int seconds = (int)(key / multiplier);
            int difference = Math.Abs(GetSecondCount() - seconds);
            return (difference < timeoutSeconds);
        }
        catch
        {
            return false;
        }
    }
}
