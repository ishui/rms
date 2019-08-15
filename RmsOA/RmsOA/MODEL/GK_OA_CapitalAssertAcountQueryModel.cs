namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_CapitalAssertAcountQueryModel : QueryBaseModel
    {
        public int BuyCountEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" BuyCount=@BuyCount ");
                    base.InsertParameter("@BuyCount", SqlDbType.Int, 4, value);
                }
            }
        }

        public DateTime BuydateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Buydate=@Buydate ");
                    base.InsertParameter("@Buydate", SqlDbType.DateTime, 8, value);
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
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime EndDayEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Buydate <= @BuydateEnd ");
                    base.InsertParameter("@BuydateEnd", SqlDbType.DateTime, 8, value);
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

        public string PlaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Place=@Place ");
                    base.InsertParameter("@Place", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public decimal PriceEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" Price=@Price ");
                    base.InsertParameter("@Price", SqlDbType.Decimal, 9, value);
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

        public string RemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark=@Remark ");
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 800, value);
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

        public DateTime StartDayEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Buydate >= @BuydateStart ");
                    base.InsertParameter("@BuydateStart", SqlDbType.DateTime, 8, value);
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

        public string TransferRecordEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TransferRecord=@TransferRecord ");
                    base.InsertParameter("@TransferRecord", SqlDbType.VarChar, 0x7d0, value);
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
                    base.InsertParameter("@Type", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Unit=@Unit ");
                    base.InsertParameter("@Unit", SqlDbType.VarChar, 10, value);
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

