namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_ManpowerNeedQueryModel : QueryBaseModel
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

        public string EducationEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Education=@Education ");
                    base.InsertParameter("@Education", SqlDbType.VarChar, 50, value);
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

        public string PersonNumberEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PersonNumber=@PersonNumber ");
                    base.InsertParameter("@PersonNumber", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime RegistDateEqualEnd
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegistDate<=@RegistDate ");
                    base.InsertParameter("@RegistDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime RegistDateEqualStart
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" RegistDate>=@RegistDate ");
                    base.InsertParameter("@RegistDate", SqlDbType.DateTime, 8, value);
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

        public string SeniorityEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Seniority=@Seniority ");
                    base.InsertParameter("@Seniority", SqlDbType.VarChar, 50, value);
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

        public string TreatmentEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Treatment=@Treatment ");
                    base.InsertParameter("@Treatment", SqlDbType.VarChar, 50, value);
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

        public string YearOfWorkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" YearOfWork=@YearOfWork ");
                    base.InsertParameter("@YearOfWork", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

