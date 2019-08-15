namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_ArchivesCopyQueryModel : QueryBaseModel
    {
        public string ArchivesTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ArchivesType=@ArchivesType ");
                    base.InsertParameter("@ArchivesType", SqlDbType.VarChar, 50, value);
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

        public string FileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileCode=@FileCode ");
                    base.InsertParameter("@FileCode", SqlDbType.VarChar, 50, "%" + value + "%");
                }
            }
        }

        public string FileNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileName=@FileName ");
                    base.InsertParameter("@FileName", SqlDbType.VarChar, 50, "%" + value + "%");
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

        public DateTime ReturnDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ReturnDate<=@ReturnDateEnd ");
                    base.InsertParameter("@ReturnDateEnd", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime ReturnDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ReturnDate>=@ReturnDateStart");
                    base.InsertParameter("@ReturnDateStart", SqlDbType.DateTime, 8, value);
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

        public string UsePersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UsePerson=@UsePerson ");
                    base.InsertParameter("@UsePerson", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

