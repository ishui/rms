namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class CachetManageQueryModel : QueryBaseModel
    {
        public string CachetNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CachetName=@CachetName ");
                    base.InsertParameter("@CachetName", SqlDbType.VarChar, 500, value);
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

        public string DeptEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Dept=@Dept ");
                    base.InsertParameter("@Dept", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime EndDataEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" EndData=@EndData ");
                    base.InsertParameter("@EndData", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime StartDataEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" StartData=@StartData ");
                    base.InsertParameter("@StartData", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string StateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" State=@State ");
                    base.InsertParameter("@State", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string TypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Type=@Type ");
                    base.InsertParameter("@Type", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string UserNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UserName=@UserName ");
                    base.InsertParameter("@UserName", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

