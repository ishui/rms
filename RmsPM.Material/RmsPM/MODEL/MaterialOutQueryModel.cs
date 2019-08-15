namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;
    using RmsPM.DAL.QueryStrategy;

    public class MaterialOutQueryModel : QueryBaseModel
    {
        public string AccessRange
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString(textArray[0], textArray[1], textArray[2], SystemClassDescription.GetItemTableName("MaterialOut"), SystemClassDescription.GetItemKeyColumnName("MaterialOut"), SystemClassDescription.GetItemTypeColumnName("MaterialOut"), SystemClassDescription.GetItemCreateUserColumnName("MaterialOut"));
                    base.QueryConditionStrAdd(conditionStr);
                }
            }
        }

        public string CheckDateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" CheckDate>=@CheckDateRange1 ");
                    base.InsertParameter("@CheckDateRange1", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string CheckDateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" CheckDate<@CheckDateRange2 + 1 ");
                    base.InsertParameter("@CheckDateRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string CheckPersonEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" CheckPerson=@CheckPerson ");
                    base.InsertParameter("@CheckPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ContractCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ContractCode=@ContractCode ");
                    base.InsertParameter("@ContractCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ContractIDEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ContractID=@ContractID ");
                    base.InsertParameter("@ContractID", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ContractNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ContractName=@ContractName ");
                    base.InsertParameter("@ContractName", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string GroupCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" GroupCode in ( select GroupCode from SystemGroup where FullID  like (select FullID from SystemGroup where GroupCode = @GroupCode) + '%'  ) ");
                    base.InsertParameter("@GroupCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string InputDateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InputDate>=@InputDateRange1 ");
                    base.InsertParameter("@InputDateRange1", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string InputDateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InputDate<@InputDateRange2 + 1 ");
                    base.InsertParameter("@InputDateRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string InputPersonEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InputPerson=@InputPerson ");
                    base.InsertParameter("@InputPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MaterialOutCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialOutCode=@MaterialOutCode ");
                    base.InsertParameter("@MaterialOutCode", SqlDbType.Int, 0, value);
                }
            }
        }

        public string MaterialOutIDEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialOutID=@MaterialOutID ");
                    base.InsertParameter("@MaterialOutID", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string OutDateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutDate>=@OutDateRange1 ");
                    base.InsertParameter("@OutDateRange1", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string OutDateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutDate<@OutDateRange2 + 1 ");
                    base.InsertParameter("@OutDateRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string OutPersonEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutPerson=@OutPerson ");
                    base.InsertParameter("@OutPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProjectCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
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
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Remark=@Remark ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 800, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Status=@Status ");
                    base.InsertParameter("@Status", SqlDbType.Int, 0, value);
                }
            }
        }
    }
}

