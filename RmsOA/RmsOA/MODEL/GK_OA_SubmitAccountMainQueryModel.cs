namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_SubmitAccountMainQueryModel : QueryBaseModel
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

        public string DutiesEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Duties=@Duties ");
                    base.InsertParameter("@Duties", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileCode=@FileCode ");
                    base.InsertParameter("@FileCode", SqlDbType.VarChar, 50, value);
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

        public DateTime RegiesterDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegiesterDate=@RegiesterDate ");
                    base.InsertParameter("@RegiesterDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string SystemCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SystemCode=@SystemCode ");
                    base.InsertParameter("@SystemCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

