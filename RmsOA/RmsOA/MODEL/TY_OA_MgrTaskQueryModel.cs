namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class TY_OA_MgrTaskQueryModel : QueryBaseModel
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

        public DateTime CreateDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" CreateDate=@CreateDate ");
                    base.InsertParameter("@CreateDate", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string CreateDateRange1
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" CreateDate>=@CreateDateRange1 ");
                    base.InsertParameter("@CreateDateRange1", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string CreateDateRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" CreateDate<@CreateDateRange2 + 1 ");
                    base.InsertParameter("@CreateDateRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string CreateManEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CreateMan=@CreateMan ");
                    base.InsertParameter("@CreateMan", SqlDbType.VarChar, 50, value);
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

        public string DeadLineRange2
        {
            set
            {
                if ((value != null) && (value != ""))
                {
                    base.QueryConditionStrAdd(" DeadLine<@DeadLineRange2 + 1 ");
                    base.InsertParameter("@DeadLineRange2", SqlDbType.DateTime, 0, value);
                }
            }
        }

        public string IsFinishEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" IsFinish=@IsFinish ");
                    base.InsertParameter("@IsFinish", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string MgrTaskIDEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" MgrTaskID=@MgrTaskID ");
                    base.InsertParameter("@MgrTaskID", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ReferLinkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ReferLink=@ReferLink ");
                    base.InsertParameter("@ReferLink", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string StateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" State in (" + value + ")");
                }
            }
        }

        public string TaskDetailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TaskDetail like '%' + @TaskDetail + '%' ");
                    base.InsertParameter("@TaskDetail", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string TaskNameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TaskName like '%' + @TaskName + '%' ");
                    base.InsertParameter("@TaskName", SqlDbType.VarChar, 500, value);
                }
            }
        }

        public string TaskTailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TaskTail like '%' + @TaskTail + '%' ");
                    base.InsertParameter("@TaskTail", SqlDbType.VarChar, 0x3e8, value);
                }
            }
        }
    }
}

