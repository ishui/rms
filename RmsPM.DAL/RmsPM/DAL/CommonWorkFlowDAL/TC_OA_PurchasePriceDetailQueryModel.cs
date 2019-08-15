namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_PurchasePriceDetailQueryModel : QueryBaseModel
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

        public string ConcertTelephoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ConcertTelephone=@ConcertTelephone ");
                    base.InsertParameter("@ConcertTelephone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ConcertUserEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ConcertUser=@ConcertUser ");
                    base.InsertParameter("@ConcertUser", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string DetailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Detail=@Detail ");
                    base.InsertParameter("@Detail", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public int IsSubmitEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" IsSubmit=@IsSubmit ");
                    base.InsertParameter("@IsSubmit", SqlDbType.Int, 4, value);
                }
            }
        }

        public string MastCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MastCode in (" + value + ")");
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

        public string SupplyNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SupplyName=@SupplyName ");
                    base.InsertParameter("@SupplyName", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

