namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_LingYongMoneyDetailQueryModel : QueryBaseModel
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

        public int IsSubmitEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" IsSubmit = @IsSubmit ");
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

        public string NameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Name=@Name ");
                    base.InsertParameter("@Name", SqlDbType.VarChar, 50, value);
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
                    base.InsertParameter("@Number", SqlDbType.VarChar, 50, value);
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

        public decimal TotalMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" TotalMoney=@TotalMoney ");
                    base.InsertParameter("@TotalMoney", SqlDbType.Decimal, 9, value);
                }
            }
        }
    }
}

