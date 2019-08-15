namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_SmallGoodsDetailQueryModel : QueryBaseModel
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

        public string GoodNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GoodName=@GoodName ");
                    base.InsertParameter("@GoodName", SqlDbType.VarChar, 50, value);
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

        public decimal NumeberEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" Numeber=@Numeber ");
                    base.InsertParameter("@Numeber", SqlDbType.Decimal, 9, value);
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

        public DateTime UseDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" UseDate=@UseDate ");
                    base.InsertParameter("@UseDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string UseWayEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UseWay=@UseWay ");
                    base.InsertParameter("@UseWay", SqlDbType.VarChar, 500, value);
                }
            }
        }
    }
}

