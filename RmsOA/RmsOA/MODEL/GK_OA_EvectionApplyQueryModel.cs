namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_EvectionApplyQueryModel : QueryBaseModel
    {
        public DateTime ApplyDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplyDate=@ApplyDate ");
                    base.InsertParameter("@ApplyDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime ApplyEndDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplyDate <= @ApplyEndDate ");
                    base.InsertParameter("@ApplyEndDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ApplyerEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Applyer=@Applyer ");
                    base.InsertParameter("@Applyer", SqlDbType.VarChar, 20, value);
                }
            }
        }

        public DateTime ApplyStartDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplyDate >= @ApplyStartDate ");
                    base.InsertParameter("@ApplyStartDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public decimal BudgetMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" BudgetMoney=@BudgetMoney ");
                    base.InsertParameter("@BudgetMoney", SqlDbType.Decimal, 9, value);
                }
            }
        }

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

        public string DeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Dept=@Dept ");
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public DateTime EndDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" EndDate=@EndDate ");
                    base.InsertParameter("@EndDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string LiveLevelEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" LiveLevel=@LiveLevel ");
                    base.InsertParameter("@LiveLevel", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string QualityNOEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" QualityNO=@QualityNO ");
                    base.InsertParameter("@QualityNO", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string ReasonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Reason=@Reason ");
                    base.InsertParameter("@Reason", SqlDbType.VarChar, 0x7d0, value);
                }
            }
        }

        public string RountEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Rount=@Rount ");
                    base.InsertParameter("@Rount", SqlDbType.VarChar, 200, value);
                }
            }
        }

        public string SNRuleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SNRule=@SNRule ");
                    base.InsertParameter("@SNRule", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime StartDataEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartData=@StartData ");
                    base.InsertParameter("@StartData", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Status in (" + value + ")");
                }
            }
        }

        public int UserCountEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" UserCount=@UserCount ");
                    base.InsertParameter("@UserCount", SqlDbType.Int, 4, value);
                }
            }
        }

        public string UsersEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Users=@Users ");
                    base.InsertParameter("@Users", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string VehicleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Vehicle=@Vehicle ");
                    base.InsertParameter("@Vehicle", SqlDbType.VarChar, 200, value);
                }
            }
        }
    }
}

