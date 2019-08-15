namespace TiannuoPM.MODEL
{
    using System;
    using System.Data;

    public class V_MaterialInventoryIOQueryModel : QueryBaseModel
    {
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

        public string IODateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" IODate>=@IODateRange1 ");
                    base.InsertParameter("@IODateRange1", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string IODateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" IODate<@IODateRange2 + 1 ");
                    base.InsertParameter("@IODateRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string IOTypeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" IOType=@IOType ");
                    base.InsertParameter("@IOType", SqlDbType.VarChar, 1, value);
                }
            }
        }

        public string IOTypeNameEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" IOTypeName=@IOTypeName ");
                    base.InsertParameter("@IOTypeName", SqlDbType.VarChar, 8, value);
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

        public string MaterialIOCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialIOCode=@MaterialIOCode ");
                    base.InsertParameter("@MaterialIOCode", SqlDbType.Int, 0, value);
                }
            }
        }

        public string MaterialIODtlCodeEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialIODtlCode=@MaterialIODtlCode ");
                    base.InsertParameter("@MaterialIODtlCode", SqlDbType.Int, 0, value);
                }
            }
        }

        public string MaterialIOIDEqual
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" MaterialIOID=@MaterialIOID ");
                    base.InsertParameter("@MaterialIOID", SqlDbType.VarChar, 50, value);
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

