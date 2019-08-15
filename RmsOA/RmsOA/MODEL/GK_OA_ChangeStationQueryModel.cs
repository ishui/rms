namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_ChangeStationQueryModel : QueryBaseModel
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

        public string ConditionEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Condition=@Condition ");
                    base.InsertParameter("@Condition", SqlDbType.VarChar, 500, value);
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

        public string NewStationEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NewStation=@NewStation ");
                    base.InsertParameter("@NewStation", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string OldStationEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OldStation=@OldStation ");
                    base.InsertParameter("@OldStation", SqlDbType.VarChar, 50, value);
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
    }
}

