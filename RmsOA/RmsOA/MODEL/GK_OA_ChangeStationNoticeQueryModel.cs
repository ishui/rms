namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_ChangeStationNoticeQueryModel : QueryBaseModel
    {
        public DateTime ChangeStationDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ChangeStationDate=@ChangeStationDate ");
                    base.InsertParameter("@ChangeStationDate", SqlDbType.DateTime, 8, value);
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

        public string NewStationLeavelEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NewStationLeavel=@NewStationLeavel ");
                    base.InsertParameter("@NewStationLeavel", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NewUnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NewUnitCode=@NewUnitCode ");
                    base.InsertParameter("@NewUnitCode", SqlDbType.VarChar, 50, value);
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

        public string OldStationLeavelEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OldStationLeavel=@OldStationLeavel ");
                    base.InsertParameter("@OldStationLeavel", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string OldUnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OldUnitCode=@OldUnitCode ");
                    base.InsertParameter("@OldUnitCode", SqlDbType.VarChar, 50, value);
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

        public string ReasonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Reason=@Reason ");
                    base.InsertParameter("@Reason", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string Remark1Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark1=@Remark1 ");
                    base.InsertParameter("@Remark1", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string Remark2Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark2=@Remark2 ");
                    base.InsertParameter("@Remark2", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string Remark3Equal
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Remark3=@Remark3 ");
                    base.InsertParameter("@Remark3", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string SexEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Sex=@Sex ");
                    base.InsertParameter("@Sex", SqlDbType.VarChar, 50, value);
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
    }
}

