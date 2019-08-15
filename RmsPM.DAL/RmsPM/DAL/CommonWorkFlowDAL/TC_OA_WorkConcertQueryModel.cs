namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_WorkConcertQueryModel : QueryBaseModel
    {
        public int AuditingEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Auditing=@Auditing ");
                    base.InsertParameter("@Auditing", SqlDbType.Int, 4, value);
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

        public string GetUnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GetUnitCode=@GetUnitCode ");
                    base.InsertParameter("@GetUnitCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string GetUserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GetUserCode=@GetUserCode ");
                    base.InsertParameter("@GetUserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime SendDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SendDate=@SendDate ");
                    base.InsertParameter("@SendDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string SendUnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SendUnitCode=@SendUnitCode ");
                    base.InsertParameter("@SendUnitCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SendUserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SendUserCode=@SendUserCode ");
                    base.InsertParameter("@SendUserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

