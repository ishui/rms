namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_OfficialSealRegiesterQueryModel : QueryBaseModel
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

        public string CollectCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CollectCode=@CollectCode ");
                    base.InsertParameter("@CollectCode", SqlDbType.VarChar, 50, value);
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

        public string Field1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Field1=@Field1 ");
                    base.InsertParameter("@Field1", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime RegiesterDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegiesterDate<=@RegiesterDate ");
                    base.InsertParameter("@RegiesterDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime RegiesterDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegiesterDate>=@RegiesterDate ");
                    base.InsertParameter("@RegiesterDate", SqlDbType.DateTime, 8, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 500, value);
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

