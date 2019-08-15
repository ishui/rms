namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_CarQueryModel : QueryBaseModel
    {
        public DateTime Buy_DateEndEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Buy_Date<=@Buy_DateEnd ");
                    base.InsertParameter("@Buy_DateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime Buy_DateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Buy_Date=@Buy_Date ");
                    base.InsertParameter("@Buy_Date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime Buy_DateStartEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" Buy_Date>=@Buy_DateStart ");
                    base.InsertParameter("@Buy_DateStart", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public int Car_CodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Car_Code=@Car_Code ");
                    base.InsertParameter("@Car_Code", SqlDbType.Int, 4, value);
                }
            }
        }

        public string Car_IdLike
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Car_Id like '%'+@Car_Id+'%' ");
                    base.InsertParameter("@Car_Id", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string Car_TypeLike
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Car_Type like '%'+@Car_Type+'%' ");
                    base.InsertParameter("@Car_Type", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string Chejia_IdEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Chejia_Id=@Chejia_Id ");
                    base.InsertParameter("@Chejia_Id", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string Fadongji_IdEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Fadongji_Id=@Fadongji_Id ");
                    base.InsertParameter("@Fadongji_Id", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string Index_NumLike
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Index_num like '%' = @Index_num like '%'");
                    base.InsertParameter("@Index_num", SqlDbType.VarChar, 50, "%" + value + "%");
                }
            }
        }
    }
}

