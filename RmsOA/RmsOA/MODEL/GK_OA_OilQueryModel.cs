namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_OilQueryModel : QueryBaseModel
    {
        public string Car_idLike
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Car_id like '%'+ @Car_id +'%' ");
                    base.InsertParameter("@Car_id", SqlDbType.VarChar, 50, value);
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

        public decimal FactMilEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" FactMil=@FactMil ");
                    base.InsertParameter("@FactMil", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public DateTime GetDateEndEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" GetDate<=@GetDate1 ");
                    base.InsertParameter("@GetDate1", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime GetDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" GetDate=@GetDate ");
                    base.InsertParameter("@GetDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime GetDateStartEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" GetDate>=@GetDate ");
                    base.InsertParameter("@GetDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string GetManEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" GetMan=@GetMan ");
                    base.InsertParameter("@GetMan", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public decimal GetNumEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" GetNum=@GetNum ");
                    base.InsertParameter("@GetNum", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string IndexNumLike
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" IndexNum like '%'+ @IndexNum +'%' ");
                    base.InsertParameter("@IndexNum", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

