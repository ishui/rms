namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class DeskTopTypeQueryModel : QueryBaseModel
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

        public int ControldIDEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" ControldID=@ControldID ");
                    base.InsertParameter("@ControldID", SqlDbType.Int, 4, value);
                }
            }
        }

        public string DeskTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DeskType=@DeskType ");
                    base.InsertParameter("@DeskType", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

