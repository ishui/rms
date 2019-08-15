namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;

    public class MaterialOutDtlQueryModel : QueryBaseModel
    {
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

        public string InContractCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InContractCode=@InContractCode ");
                    base.InsertParameter("@InContractCode", SqlDbType.VarChar, 50, value);
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

        public string MaterialOutDtlCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialOutDtlCode=@MaterialOutDtlCode ");
                    base.InsertParameter("@MaterialOutDtlCode", SqlDbType.Int, 0, value);
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

        public string OutMoneyEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutMoney=@OutMoney ");
                    base.InsertParameter("@OutMoney", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string OutPriceEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutPrice=@OutPrice ");
                    base.InsertParameter("@OutPrice", SqlDbType.Decimal, 0, value);
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

