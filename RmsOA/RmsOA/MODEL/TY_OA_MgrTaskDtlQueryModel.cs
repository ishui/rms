namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class TY_OA_MgrTaskDtlQueryModel : QueryBaseModel
    {
        public string AssistpersonsEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Assistpersons=@Assistpersons ");
                    base.InsertParameter("@Assistpersons", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string BakEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Bak=@Bak ");
                    base.InsertParameter("@Bak", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime DeadLineEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" DeadLine=@DeadLine ");
                    base.InsertParameter("@DeadLine", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string IsfinishEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Isfinish=@Isfinish ");
                    base.InsertParameter("@Isfinish", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ManagerRevertEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ManagerRevert=@ManagerRevert ");
                    base.InsertParameter("@ManagerRevert", SqlDbType.VarChar, 0x3e8, value);
                }
            }
        }

        public int MgrCodeIDEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" MgrCodeID=@MgrCodeID ");
                    base.InsertParameter("@MgrCodeID", SqlDbType.Int, 4, value);
                }
            }
        }

        public int MgrDtlCodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" MgrDtlCode=@MgrDtlCode ");
                    base.InsertParameter("@MgrDtlCode", SqlDbType.Int, 4, value);
                }
            }
        }

        public string MgrDtlInfoEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MgrDtlInfo like '%' + @MgrDtlInfo + '%'");
                    base.InsertParameter("@MgrDtlInfo", SqlDbType.VarChar, 800, value);
                }
            }
        }

        public string ResponsePersonEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ResponsePerson=@ResponsePerson ");
                    base.InsertParameter("@ResponsePerson", SqlDbType.VarChar, 50, value);
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

        public string TrancRevertEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TrancRevert=@TrancRevert ");
                    base.InsertParameter("@TrancRevert", SqlDbType.VarChar, 0x3e8, value);
                }
            }
        }
    }
}

