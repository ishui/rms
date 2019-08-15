namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_UnusualQueryModel : QueryBaseModel
    {
        public DateTime ApplayDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplayDate=@ApplayDate ");
                    base.InsertParameter("@ApplayDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

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

        public string CoustomNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CoustomName=@CoustomName ");
                    base.InsertParameter("@CoustomName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string CoustomTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CoustomType=@CoustomType ");
                    base.InsertParameter("@CoustomType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string EmailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Email=@Email ");
                    base.InsertParameter("@Email", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProjectCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProjectCode=@ProjectCode ");
                    base.InsertParameter("@ProjectCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProjectNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProjectName=@ProjectName ");
                    base.InsertParameter("@ProjectName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ReasonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Reason=@Reason ");
                    base.InsertParameter("@Reason", SqlDbType.VarChar, 500, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string suggestEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" suggest=@suggest ");
                    base.InsertParameter("@suggest", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string TelephoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Telephone=@Telephone ");
                    base.InsertParameter("@Telephone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserCode=@UserCode ");
                    base.InsertParameter("@UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

