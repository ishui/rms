namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_MeetSummaryQueryModel : QueryBaseModel
    {
        public string AttendPersonsEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AttendPersons=@AttendPersons ");
                    base.InsertParameter("@AttendPersons", SqlDbType.VarChar, 800, value);
                }
            }
        }

        public string CharterMemberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CharterMember=@CharterMember ");
                    base.InsertParameter("@CharterMember", SqlDbType.VarChar, 50, value);
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

        public string CodeRuleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CodeRule=@CodeRule ");
                    base.InsertParameter("@CodeRule", SqlDbType.VarChar, 200, value);
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

        public string DeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Dept=@Dept ");
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime EndTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubmitTime <= @EndTime ");
                    base.InsertParameter("@EndTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime MeetEndTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" MeetEndTime=@MeetEndTime ");
                    base.InsertParameter("@MeetEndTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime MeetStartTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" MeetStartTime=@MeetStartTime ");
                    base.InsertParameter("@MeetStartTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string OtherContextEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OtherContext=@OtherContext ");
                    base.InsertParameter("@OtherContext", SqlDbType.VarChar, 0x1f40, value);
                }
            }
        }

        public string OtherPersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OtherPerson=@OtherPerson ");
                    base.InsertParameter("@OtherPerson", SqlDbType.VarChar, 500, value);
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
                    base.InsertParameter("@Place", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string PreLeaveEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PreLeave=@PreLeave ");
                    base.InsertParameter("@PreLeave", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string RecoderEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Recoder=@Recoder ");
                    base.InsertParameter("@Recoder", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SendStatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SendStatus=@SendStatus ");
                    base.InsertParameter("@SendStatus", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SmallTitleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SmallTitle=@SmallTitle ");
                    base.InsertParameter("@SmallTitle", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SortCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SortCode=@SortCode ");
                    base.InsertParameter("@SortCode", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public DateTime StartTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubmitTime >= @StartTime ");
                    base.InsertParameter("@StartTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    base.QueryConditionStrAdd(" Status IN (@Status) ");
                    base.InsertParameter("@Status", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string SubmiterEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Submiter=@Submiter ");
                    base.InsertParameter("@Submiter", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime SubmitTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SubmitTime=@SubmitTime ");
                    base.InsertParameter("@SubmitTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string TitleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Title LIKE %@Title%");
                    base.InsertParameter("@Title", SqlDbType.VarChar, 200, value);
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
                    base.InsertParameter("@Type", SqlDbType.VarChar, 200, value);
                }
            }
        }
    }
}

