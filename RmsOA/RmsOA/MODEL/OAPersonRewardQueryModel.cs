namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class OAPersonRewardQueryModel : QueryBaseModel
    {
        public string causeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" cause=@cause ");
                    base.InsertParameter("@cause", SqlDbType.VarChar, 500, value);
                }
            }
        }

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

        public string contentEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" content=@content ");
                    base.InsertParameter("@content", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public DateTime dj_dateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" dj_date=@dj_date ");
                    base.InsertParameter("@dj_date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string personidEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" personid=@personid ");
                    base.InsertParameter("@personid", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string remarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" remark=@remark ");
                    base.InsertParameter("@remark", SqlDbType.VarChar, 500, value);
                }
            }
        }
    }
}

