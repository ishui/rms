namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TY_ReferendumReportQueryModel : QueryBaseModel
    {
        public string AuditEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Audit=@Audit ");
                    base.InsertParameter("@Audit", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string Field1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Field1=@Field1 ");
                    base.InsertParameter("@Field1", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Number=@Number ");
                    base.InsertParameter("@Number", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int ReferendumReportCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" ReferendumReportCode=@ReferendumReportCode ");
                    base.InsertParameter("@ReferendumReportCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public DateTime StartDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartDate=@StartDate ");
                    base.InsertParameter("@StartDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string StartPersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" StartPerson=@StartPerson ");
                    base.InsertParameter("@StartPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TextContentEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TextContent=@TextContent ");
                    base.InsertParameter("@TextContent", SqlDbType.VarChar, 800, value);
                }
            }
        }

        public string TextTitleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TextTitle=@TextTitle ");
                    base.InsertParameter("@TextTitle", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

