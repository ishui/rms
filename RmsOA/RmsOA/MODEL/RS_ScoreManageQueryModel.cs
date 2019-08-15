namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class RS_ScoreManageQueryModel : QueryBaseModel
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

        public string DeptCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" DeptCode=@DeptCode ");
                    base.InsertParameter("@DeptCode", SqlDbType.VarChar, 10, value);
                }
            }
        }

        public DateTime MarkDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" MarkDate=@MarkDate ");
                    base.InsertParameter("@MarkDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string MarkerEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Marker=@Marker ");
                    base.InsertParameter("@Marker", SqlDbType.VarChar, 10, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Status=@Status ");
                    base.InsertParameter("@Status", SqlDbType.VarChar, 2, value);
                }
            }
        }

        public int TypeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.Int, 4, value);
                }
            }
        }
    }
}

