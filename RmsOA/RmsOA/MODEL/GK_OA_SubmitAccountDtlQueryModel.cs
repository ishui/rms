namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_SubmitAccountDtlQueryModel : QueryBaseModel
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

        public string MastCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MastCode=@MastCode ");
                    base.InsertParameter("@MastCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MonthEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Month=@Month ");
                    base.InsertParameter("@Month", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public decimal RealityCostEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" RealityCost=@RealityCost ");
                    base.InsertParameter("@RealityCost", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public decimal RemainCostEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" RemainCost=@RemainCost ");
                    base.InsertParameter("@RemainCost", SqlDbType.Decimal, 9, value);
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

        public decimal StandardCostEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" StandardCost=@StandardCost ");
                    base.InsertParameter("@StandardCost", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public decimal SumCostEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" SumCost=@SumCost ");
                    base.InsertParameter("@SumCost", SqlDbType.Decimal, 9, value);
                }
            }
        }
    }
}

