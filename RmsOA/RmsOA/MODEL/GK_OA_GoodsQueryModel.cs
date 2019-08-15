namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_GoodsQueryModel : QueryBaseModel
    {
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

        public string GoodsCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsCode=@GoodsCode ");
                    base.InsertParameter("@GoodsCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string GoodsNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsName=@GoodsName ");
                    base.InsertParameter("@GoodsName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string GoodsNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsNumber=@GoodsNumber ");
                    base.InsertParameter("@GoodsNumber", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string GoodsPartEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsPart=@GoodsPart ");
                    base.InsertParameter("@GoodsPart", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string GoodsPriceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsPrice=@GoodsPrice ");
                    base.InsertParameter("@GoodsPrice", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string GoodsTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodsType=@GoodsType ");
                    base.InsertParameter("@GoodsType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime InputDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InputDate<=@InputDateEnd ");
                    base.InsertParameter("@InputDateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime InputDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InputDate>=@InputDateStart ");
                    base.InsertParameter("@InputDateStart", SqlDbType.DateTime, 8, value);
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

        public string UsePersonCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UsePersonCode=@UsePersonCode ");
                    base.InsertParameter("@UsePersonCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

