namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class ContractBillQueryModel : QueryBaseModel
    {
        public decimal BillMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" BillMoney=@BillMoney ");
                    base.InsertParameter("@BillMoney", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string BillNoEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" BillNo=@BillNo ");
                    base.InsertParameter("@BillNo", SqlDbType.VarChar, 50, value);
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

        public string ContractCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ContractCode=@ContractCode ");
                    base.InsertParameter("@ContractCode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ProjectCodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ProjectCode=@ProjectCode ");
                    base.InsertParameter("@ProjectCode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

