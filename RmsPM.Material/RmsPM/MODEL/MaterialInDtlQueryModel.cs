namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;
    using RmsPM.DAL.QueryStrategy;

    public class MaterialInDtlQueryModel : QueryBaseModel
    {
        public string AccessRange
        {
            set
            {
                if (value != null)
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString("150101", textArray[1], textArray[2], "MaterialInDtl", SystemClassDescription.GetItemKeyColumnName("Material"), SystemClassDescription.GetItemTypeColumnName("Material"), SystemClassDescription.GetItemCreateUserColumnName("Material"));
                    base.QueryConditionStrAdd(conditionStr);
                }
            }
        }

        public string AccessRangeIn
        {
            set
            {
                if (value != null)
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString("150301", textArray[1], textArray[2], "MaterialInDtl", SystemClassDescription.GetItemKeyColumnName("MaterialIn"), SystemClassDescription.GetItemTypeColumnName("MaterialIn"), SystemClassDescription.GetItemCreateUserColumnName("MaterialIn"));
                    base.QueryConditionStrAdd(conditionStr);
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

        public string GroupFullIDEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" GroupFullID=@GroupFullID ");
                    base.InsertParameter("@GroupFullID", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string GroupNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" GroupName=@GroupName ");
                    base.InsertParameter("@GroupName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string GroupSortIDEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" GroupSortID=@GroupSortID ");
                    base.InsertParameter("@GroupSortID", SqlDbType.VarChar, 50, value);
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

        public string InGroupCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InGroupCode in ( select GroupCode from SystemGroup where FullID  like (select FullID from SystemGroup where GroupCode = @GroupCode) + '%'  ) ");
                    base.InsertParameter("@InGroupCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string InGroupNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InGroupName=@InGroupName ");
                    base.InsertParameter("@InGroupName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string InMoneyEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InMoney=@InMoney ");
                    base.InsertParameter("@InMoney", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InPriceEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InPrice=@InPrice ");
                    base.InsertParameter("@InPrice", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InQtyEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InQty=@InQty ");
                    base.InsertParameter("@InQty", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InvMoneyEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InvMoney=@InvMoney ");
                    base.InsertParameter("@InvMoney", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InvQtyEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InvQty=@InvQty ");
                    base.InsertParameter("@InvQty", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string MaterialCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialCode=@MaterialCode ");
                    base.InsertParameter("@MaterialCode", SqlDbType.Int, 0, value);
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

        public string MaterialInDtlCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialInDtlCode=@MaterialInDtlCode ");
                    base.InsertParameter("@MaterialInDtlCode", SqlDbType.Int, 0, value);
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

        public string MaterialNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialName=@MaterialName ");
                    base.InsertParameter("@MaterialName", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string OutQtyEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutQty=@OutQty ");
                    base.InsertParameter("@OutQty", SqlDbType.Decimal, 0, value);
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

        public string SpecEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Spec=@Spec ");
                    base.InsertParameter("@Spec", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string UnitEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" Unit=@Unit ");
                    base.InsertParameter("@Unit", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

