namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_WorkEvaluationQueryModel : QueryBaseModel
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

        public string DecideEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Decide=@Decide ");
                    base.InsertParameter("@Decide", SqlDbType.VarChar, 50, value);
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

        public DateTime InComDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InComDate<=@InComDate ");
                    base.InsertParameter("@InComDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime InComDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" InComDate>=@InComDate ");
                    base.InsertParameter("@InComDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string MeritsAndDemeritsEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MeritsAndDemerits=@MeritsAndDemerits ");
                    base.InsertParameter("@MeritsAndDemerits", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string PersonNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PersonName=@PersonName ");
                    base.InsertParameter("@PersonName", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string StationCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" StationCode=@StationCode ");
                    base.InsertParameter("@StationCode", SqlDbType.VarChar, 50, value);
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

        public string WorkPperationEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" WorkPperation=@WorkPperation ");
                    base.InsertParameter("@WorkPperation", SqlDbType.VarChar, 500, value);
                }
            }
        }
    }
}

