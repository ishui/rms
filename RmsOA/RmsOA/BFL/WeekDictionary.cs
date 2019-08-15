namespace RmsOA.BFL
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    public struct WeekDictionary
    {
        public const string WeekBegin = "WeekBegin";
        public const string WeekEnd = "WeekEnd";
        public const string Monday = "周一";
        public const string Tuesday = "周二";
        public const string Wednesday = "周三";
        public const string Thursday = "周四";
        public const string Friday = "周五";
        public const string Saturday = "周六";
        public const string Sunday = "周日";
    }
}

