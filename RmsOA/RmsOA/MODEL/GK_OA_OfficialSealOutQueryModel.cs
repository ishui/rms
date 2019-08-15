namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_OfficialSealOutQueryModel : QueryBaseModel
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

        public string Fieled1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Fieled1=@Fieled1 ");
                    base.InsertParameter("@Fieled1", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string OfficialSealNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OfficialSealName=@OfficialSealName ");
                    base.InsertParameter("@OfficialSealName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime OutDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" OutDate=@OutDate ");
                    base.InsertParameter("@OutDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string PersonCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PersonCode=@PersonCode ");
                    base.InsertParameter("@PersonCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ReasonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Reason=@Reason ");
                    base.InsertParameter("@Reason", SqlDbType.VarChar, 500, value);
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

        public DateTime ReturnDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ReturnDate=@ReturnDate ");
                    base.InsertParameter("@ReturnDate", SqlDbType.DateTime, 8, value);
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

