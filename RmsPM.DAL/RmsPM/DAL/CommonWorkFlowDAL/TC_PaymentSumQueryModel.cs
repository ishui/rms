namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TC_PaymentSumQueryModel : QueryBaseModel
    {
        public DateTime AuditDateTimeEndEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" AuditDateTime<=@AuditDateTime ");
                    base.InsertParameter("@AuditDateTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime AuditDateTimeStartEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SumDateTime>=@SumDateTime ");
                    base.InsertParameter("@SumDateTime", SqlDbType.DateTime, 8, value);
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

        public string CollectBillCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" CollectBillCode=@CollectBillCode ");
                    base.InsertParameter("@CollectBillCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string SendUnitCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SendUnitCode=@SendUnitCode ");
                    base.InsertParameter("@SendUnitCode", SqlDbType.VarChar, 50, value);
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

        public DateTime SumDateTimeEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SumDateTime>=@SumDateTime ");
                    base.InsertParameter("@SumDateTime", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string SumUserCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SumUserCode=@SumUserCode ");
                    base.InsertParameter("@SumUserCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

