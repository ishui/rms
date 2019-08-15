namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_OA_LingYongMenonyQueryModel : QueryBaseModel
    {
        public DateTime ApplayDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" ApplayDate=@ApplayDate ");
                    base.InsertParameter("@ApplayDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public int AuditingEqual
        {
            set
            {
                if (value != 0)
                {
                    if (value == 7)
                    {
                        base.QueryConditionStrAdd(" Auditing in (4,5,6)");
                    }
                    else
                    {
                        base.QueryConditionStrAdd(" Auditing in (" + value + ")");
                    }
                }
            }
        }

        public string BaiEqual
        {
            set
            {
                if (value != null)
                {
                    if (value.IndexOf(',') > 0)
                    {
                        base.QueryConditionStrAdd(" Bai =''or Bai='" + value.Substring(0, value.Length - 1) + "'");
                    }
                    else
                    {
                        base.QueryConditionStrAdd(" Bai=@Bai ");
                        base.InsertParameter("@Bai", SqlDbType.VarChar, 50, value);
                    }
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

        public string CustomerEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Customer=@Customer ");
                    base.InsertParameter("@Customer", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string FenEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Fen=@Fen ");
                    base.InsertParameter("@Fen", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string JiaoEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Jiao=@Jiao ");
                    base.InsertParameter("@Jiao", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string QianEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Qian=@Qian ");
                    base.InsertParameter("@Qian", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime QianShouDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" QianShouDate=@QianShouDate ");
                    base.InsertParameter("@QianShouDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string ShiEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Shi=@Shi ");
                    base.InsertParameter("@Shi", SqlDbType.VarChar, 50, value);
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

        public string UseWayEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" UseWay=@UseWay ");
                    base.InsertParameter("@UseWay", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string YuanEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Yuan=@Yuan ");
                    base.InsertParameter("@Yuan", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

