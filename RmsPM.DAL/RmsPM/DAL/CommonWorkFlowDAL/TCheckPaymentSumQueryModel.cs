﻿namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TCheckPaymentSumQueryModel : QueryBaseModel
    {
        public string AuditingEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Auditing=@Auditing ");
                    base.InsertParameter("@Auditing", SqlDbType.VarChar, 50, value);
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

        public string SendUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" SendUnit=@SendUnit ");
                    base.InsertParameter("@SendUnit", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime SumDateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" SumDate=@SumDate ");
                    base.InsertParameter("@SumDate", SqlDbType.DateTime, 8, value);
                }
            }
        }
    }
}

