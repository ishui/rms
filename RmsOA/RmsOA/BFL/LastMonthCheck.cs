namespace RmsOA.BFL
{
    using System;

    public class LastMonthCheck : CheckTime
    {
        public LastMonthCheck()
        {
            base.checkTime = base.ChangeTime(DateTime.Now.AddMonths(-1));
        }
    }
}

