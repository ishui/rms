namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;
    using RmsPM.DAL.QueryStrategy;

    public class MaterialInQueryModel : QueryBaseModel
    {
        public string AccessRange
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString(textArray[0], textArray[1], textArray[2], SystemClassDescription.GetItemTableName("MaterialIn"), SystemClassDescription.GetItemKeyColumnName("MaterialIn"), SystemClassDescription.GetItemTypeColumnName("MaterialIn"), SystemClassDescription.GetItemCreateUserColumnName("MaterialIn"));
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

        public string InDateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InDate>=@InDateRange1 ");
                    base.InsertParameter("@InDateRange1", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string InDateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InDate<@InDateRange2 + 1 ");
                    base.InsertParameter("@InDateRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string InPersonEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InPerson=@InPerson ");
                    base.InsertParameter("@InPerson", SqlDbType.VarChar, 50, value);
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

        public string MaterialInCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialInCode=@MaterialInCode ");
                    base.InsertParameter("@MaterialInCode", SqlDbType.Int, 0, value);
                }
            }
        }

        public string MaterialInIDEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialInID=@MaterialInID ");
                    base.InsertParameter("@MaterialInID", SqlDbType.VarChar, 50, value);
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

