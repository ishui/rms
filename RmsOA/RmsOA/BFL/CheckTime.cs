namespace RmsOA.BFL
{
    using System;

    public class CheckTime
    {
        protected DateTime checkTime;

        public DateTime ChangeTime(DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            dt = new DateTime(year, month, 1);
            return dt;
        }

        public DateTime CheckMonth
        {
            get
            {
                return this.checkTime;
            }
        }
    }
}

