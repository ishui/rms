namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_MakeMarksQueryModel : QueryBaseModel
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

        public string MarkTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MarkType=@MarkType ");
                    base.InsertParameter("@MarkType", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime RegisterDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegisterDate<=@RegisterDateEnd ");
                    base.InsertParameter("@RegisterDateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime RegisterDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegisterDate>=@RegisterDateStart ");
                    base.InsertParameter("@RegisterDateStart", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string RegisterPersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" RegisterPerson=@RegisterPerson ");
                    base.InsertParameter("@RegisterPerson", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TitleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Title Like @Title ");
                    base.InsertParameter("@Title", SqlDbType.VarChar, 50, "%" + value + "%");
                }
            }
        }

        public string UnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UnitCode=@UnitCode ");
                    base.InsertParameter("@UnitCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

