using System;

namespace MyLrcMaker.Extension
{
    internal static class TimeSpanExtension
    {
        public static string ToLrcFormat(this TimeSpan timeSpan)
        {
            return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
        }
    }
}