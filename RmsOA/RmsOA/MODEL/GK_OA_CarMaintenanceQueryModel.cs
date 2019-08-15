namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_CarMaintenanceQueryModel : QueryBaseModel
    {
        public string Car_codeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Car_code=@Car_code ");
                    base.InsertParameter("@Car_code", SqlDbType.VarChar, 4, value);
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

        public DateTime MDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" MDate=@MDate ");
                    base.InsertParameter("@MDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public decimal MilEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" Mil=@Mil ");
                    base.InsertParameter("@Mil", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public decimal MPriceEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" MPrice=@MPrice ");
                    base.InsertParameter("@MPrice", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string MValueEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MValue=@MValue ");
                    base.InsertParameter("@MValue", SqlDbType.VarChar, 0x3e8, value);
                }
            }
        }
    }
}

