namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class GK_OA_OutFileSignQueryModel : QueryBaseModel
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

        public string FileTitleEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" FileTitle=@FileTitle ");
                    base.InsertParameter("@FileTitle", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string HG_UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" HG_UserCode=@HG_UserCode ");
                    base.InsertParameter("@HG_UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string JB_UnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" JB_UnitCode=@JB_UnitCode ");
                    base.InsertParameter("@JB_UnitCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string JB_UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" JB_UserCode=@JB_UserCode ");
                    base.InsertParameter("@JB_UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NB_UnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NB_UnitCode=@NB_UnitCode ");
                    base.InsertParameter("@NB_UnitCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string NB_UserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" NB_UserCode=@NB_UserCode ");
                    base.InsertParameter("@NB_UserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string OutFileCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" OutFileCode=@OutFileCode ");
                    base.InsertParameter("@OutFileCode", SqlDbType.VarChar, 50, value);
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
                    base.QueryConditionStrAdd(" RegisterDate>=@RegisterDateStart");
                    base.InsertParameter("@RegisterDateStart", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string SecretEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Secret=@Secret ");
                    base.InsertParameter("@Secret", SqlDbType.VarChar, 50, value);
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

        public string UrgentEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Urgent=@Urgent ");
                    base.InsertParameter("@Urgent", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

