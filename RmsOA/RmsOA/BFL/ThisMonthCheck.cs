namespace RmsOA.BFL
{
    using System;

    public class ThisMonthCheck : CheckTime
    {
        public ThisMonthCheck()
        {
            base.checkTime = base.ChangeTime(DateTime.Now);
        }
    }
}

