namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;
    using RmsPM.DAL.QueryStrategy;

    public class V_MaterialInventoryQueryModel : QueryBaseModel
    {
        public string AccessRange
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    string[] textArray = value.Split(new char[] { "\n"[0] });
                    string conditionStr = AccessRanggeQuery.BuildAccessRangeString("150101", textArray[1], textArray[2], "V_MaterialInventory", SystemClassDescription.GetItemKeyColumnName("Material"), SystemClassDescription.GetItemTypeColumnName("Material"), SystemClassDescription.GetItemCreateUserColumnName("Material"));
                    base.QueryConditionStrAdd(conditionStr);
                }
            }
        }

        public string GroupCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" GroupCode=@GroupCode ");
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

        public string InMoneyRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InMoney>=@InMoneyRange1 ");
                    base.InsertParameter("@InMoneyRange1", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InMoneyRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InMoney<=@InMoneyRange2 ");
                    base.InsertParameter("@InMoneyRange2", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InQtyRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InQty>=@InQtyRange1 ");
                    base.InsertParameter("@InQtyRange1", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InQtyRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InQty<=@InQtyRange2 ");
                    base.InsertParameter("@InQtyRange2", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InvQtyRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InvQty>=@InvQtyRange1 ");
                    base.InsertParameter("@InvQtyRange1", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string InvQtyRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" InvQty<=@InvQtyRange2 ");
                    base.InsertParameter("@InvQtyRange2", SqlDbType.Decimal, 0, value);
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

        public string OutQtyRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutQty>=@OutQtyRange1 ");
                    base.InsertParameter("@OutQtyRange1", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string OutQtyRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" OutQty<=@OutQtyRange2 ");
                    base.InsertParameter("@OutQtyRange2", SqlDbType.Decimal, 0, value);
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

        public string ProjectNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" ProjectName=@ProjectName ");
                    base.InsertParameter("@ProjectName", SqlDbType.VarChar, 50, value);
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

        public string StandardPriceRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" StandardPrice>=@StandardPriceRange1 ");
                    base.InsertParameter("@StandardPriceRange1", SqlDbType.Decimal, 0, value);
                }
            }
        }

        public string StandardPriceRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" StandardPrice<=@StandardPriceRange2 ");
                    base.InsertParameter("@StandardPriceRange2", SqlDbType.Decimal, 0, value);
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

