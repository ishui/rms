namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_WorkLogQueryModel : QueryBaseModel
    {
        public int CodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Code=@Code ");
                    base.InsertParameter("@Code", SqlDbType.Int, 4, value);
                }
            }
        }

        public string ContextEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Context=@Context ");
                    base.InsertParameter("@Context", SqlDbType.VarChar, 0x10, value);
                }
            }
        }

        public DateTime DayWritedEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" DayWrited=@DayWrited ");
                    base.InsertParameter("@DayWrited", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string MoodEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Mood=@Mood ");
                    base.InsertParameter("@Mood", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string UserIdEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserId=@UserId ");
                    base.InsertParameter("@UserId", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string WaiterEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Waiter=@Waiter ");
                    base.InsertParameter("@Waiter", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string WeatherEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Weather=@Weather ");
                    base.InsertParameter("@Weather", SqlDbType.VarChar, 100, value);
                }
            }
        }
    }
}

