namespace RmsDM.MODEL
{
    using System;
    using System.Data;

    public class WorkFlowProcedureQueryModel : QueryBaseModel
    {
        public int ActivityEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Activity=@Activity ");
                    base.InsertParameter("@Activity", SqlDbType.Int, 4, value);
                }
            }
        }

        public string ApplicationInfoPathEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ApplicationInfoPath=@ApplicationInfoPath ");
                    base.InsertParameter("@ApplicationInfoPath", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string ApplicationPathEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ApplicationPath=@ApplicationPath ");
                    base.InsertParameter("@ApplicationPath", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public DateTime CreateDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" CreateDate=@CreateDate ");
                    base.InsertParameter("@CreateDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string CreateUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CreateUser=@CreateUser ");
                    base.InsertParameter("@CreateUser", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string CreatorEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Creator=@Creator ");
                    base.InsertParameter("@Creator", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DescriptionEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Description=@Description ");
                    base.InsertParameter("@Description", SqlDbType.VarChar, 160, value);
                }
            }
        }

        public DateTime ModifyDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ModifyDate=@ModifyDate ");
                    base.InsertParameter("@ModifyDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ModifyUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ModifyUser=@ModifyUser ");
                    base.InsertParameter("@ModifyUser", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProcedureCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProcedureCode=@ProcedureCode ");
                    base.InsertParameter("@ProcedureCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProcedureNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProcedureName=@ProcedureName ");
                    base.InsertParameter("@ProcedureName", SqlDbType.VarChar, 80, value);
                }
            }
        }

        public string ProcedureRemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProcedureRemark=@ProcedureRemark ");
                    base.InsertParameter("@ProcedureRemark", SqlDbType.VarChar, 0x7d0, value);
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

        public string RemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark=@Remark ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SysTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SysType=@SysType ");
                    base.InsertParameter("@SysType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public int TypeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.Int, 4, value);
                }
            }
        }

        public string VersionDescriptionEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" VersionDescription=@VersionDescription ");
                    base.InsertParameter("@VersionDescription", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public decimal VersionNumberEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" VersionNumber=@VersionNumber ");
                    base.InsertParameter("@VersionNumber", SqlDbType.Decimal, 9, value);
                }
            }
        }
    }
}

