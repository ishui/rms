namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class FlowUseSealQueryModel : QueryBaseModel
    {
        public DateTime ApplySealDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplySealDate=@ApplySealDate ");
                    base.InsertParameter("@ApplySealDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ArchivesCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ArchivesCode=@ArchivesCode ");
                    base.InsertParameter("@ArchivesCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string AuditingEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Auditing=@Auditing ");
                    base.InsertParameter("@Auditing", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DepartmentCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DepartmentCode=@DepartmentCode ");
                    base.InsertParameter("@DepartmentCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FileNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileName=@FileName ");
                    base.InsertParameter("@FileName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string KeptFlagEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" KeptFlag=@KeptFlag ");
                    base.InsertParameter("@KeptFlag", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime QianShouDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" QianShouDate=@QianShouDate ");
                    base.InsertParameter("@QianShouDate", SqlDbType.DateTime, 8, value);
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

        public string UnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UnitCode=@UnitCode ");
                    base.InsertParameter("@UnitCode", SqlDbType.VarChar, 50, value);
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

        public int UseSealCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" UseSealCode=@UseSealCode ");
                    base.InsertParameter("@UseSealCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public DateTime UseSealDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" UseSealDate=@UseSealDate ");
                    base.InsertParameter("@UseSealDate", SqlDbType.DateTime, 8, value);
                }
            }
        }
    }
}

