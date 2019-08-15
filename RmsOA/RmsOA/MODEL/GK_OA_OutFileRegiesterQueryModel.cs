namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_OutFileRegiesterQueryModel : QueryBaseModel
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

        public string FileNumeberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileNumeber=@FileNumeber ");
                    base.InsertParameter("@FileNumeber", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime RegiesterDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegiesterDate<=@RegiesterDateEnd ");
                    base.InsertParameter("@RegiesterDateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime RegiesterDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegiesterDate>=@RegiesterDateStart ");
                    base.InsertParameter("@RegiesterDateStart", SqlDbType.DateTime, 8, value);
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

        public string UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserCode=@UserCode ");
                    base.InsertParameter("@UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

