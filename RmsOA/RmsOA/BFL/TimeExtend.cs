namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;

    public static class TimeExtend
    {
        public static string GetChineseWeekName(DateTime dt)
        {
            string text = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "周日";

                case DayOfWeek.Monday:
                    return "周一";

                case DayOfWeek.Tuesday:
                    return "周二";

                case DayOfWeek.Wednesday:
                    return "周三";

                case DayOfWeek.Thursday:
                    return "周四";

                case DayOfWeek.Friday:
                    return "周五";

                case DayOfWeek.Saturday:
                    return "周六";
            }
            return text;
        }

        public static DateTime GetWeekBeginDate(DateTime dt)
        {
            int num = int.Parse(dt.DayOfWeek.ToString("d"));
            if (num != 0)
            {
                return dt.Date.AddDays((double) (1 - num));
            }
            return dt.Date.AddDays(-6);
        }

        public static int GetWeekOFYear(DateTime dt)
        {
            DayOfWeek dayOfWeek = GetYearFirstDay(dt).DayOfWeek;
            int num = 0;
            if (dayOfWeek.Equals(DayOfWeek.Sunday))
            {
                num = 7;
            }
            else
            {
                num = int.Parse(dayOfWeek.ToString("d"));
            }
            return ((((dt.DayOfYear - 2) + num) / 7) + 1);
        }

        public static DateTime GetYearFirstDay(DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        public static Dictionary<string, DateTime> WeekBeginAddEndDay(DateTime dt)
        {
            DateTime time;
            DateTime date;
            int num = int.Parse(dt.DayOfWeek.ToString("d"));
            if (num != 0)
            {
                time = dt.Date.AddDays((double) (1 - num));
                date = dt.Date.AddDays((double) (7 - num));
            }
            else
            {
                time = dt.Date.AddDays(-6);
                date = dt.Date;
            }
            Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();
            dictionary.Add("WeekBegin", time);
            dictionary.Add("WeekEnd", date);
            return dictionary;
        }
    }
}

