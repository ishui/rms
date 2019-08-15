namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class ConferenceManageQueryModel : QueryBaseModel
    {
        public string ChaterMemberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ChaterMember=@ChaterMember ");
                    base.InsertParameter("@ChaterMember", SqlDbType.VarChar, 50, value);
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

        public string DeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Dept=@Dept ");
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime EndTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" EndTime=@EndTime ");
                    base.InsertParameter("@EndTime", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public string PlaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Place=@Place ");
                    base.InsertParameter("@Place", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string RemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark=@Remark ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public DateTime StartTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartTime=@StartTime ");
                    base.InsertParameter("@StartTime", SqlDbType.DateTime, 4, value);
                }
            }
        }

        public string StateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" State=@State ");
                    base.InsertParameter("@State", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TopicEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Topic=@Topic ");
                    base.InsertParameter("@Topic", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string TypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime WeekEndTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartTime <= @WeekEndTime ");
                    base.InsertParameter("@WeekEndTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime WeekStartTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartTime>=@WeekStartTime ");
                    base.InsertParameter("@WeekStartTime", SqlDbType.DateTime, 8, value);
                }
            }
        }
    }
}

