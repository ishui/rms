namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_EquipmentMaintainApplyQueryModel : QueryBaseModel
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
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public DateTime EndDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplyDate <= @EndDate ");
                    base.InsertParameter("@EndDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ModelNOEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ModelNO=@ModelNO ");
                    base.InsertParameter("@ModelNO", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string NameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Name=@Name ");
                    base.InsertParameter("@Name", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string NumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Number=@Number ");
                    base.InsertParameter("@Number", SqlDbType.VarChar, 100, value);
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

        public DateTime StartDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplyDate >= @StartDate ");
                    base.InsertParameter("@StartDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string StateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" State in (" + value + ")");
                }
            }
        }

        public string TypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string UserNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserName=@UserName ");
                    base.InsertParameter("@UserName", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

