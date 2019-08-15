namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;
    using System.Data;

    public class TCheckPaymentQueryModel : QueryBaseModel
    {
        public string AcceptAccountsEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AcceptAccounts=@AcceptAccounts ");
                    base.InsertParameter("@AcceptAccounts", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string AcceptBankEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AcceptBank=@AcceptBank ");
                    base.InsertParameter("@AcceptBank", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public decimal AcceptMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" AcceptMoney=@AcceptMoney ");
                    base.InsertParameter("@AcceptMoney", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string AcceptMoneyTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AcceptMoneyType=@AcceptMoneyType ");
                    base.InsertParameter("@AcceptMoneyType", SqlDbType.VarChar, 10, value);
                }
            }
        }

        public string AcceptUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" AcceptUnit=@AcceptUnit ");
                    base.InsertParameter("@AcceptUnit", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string FlagEqual
        {
            set
            {
                if (value != null)
                {
                    if (value.IndexOf(',') > 0)
                    {
                        base.QueryConditionStrAdd(" (Flag ='' or Flag is null or Flag='" + value.Substring(0, value.Length - 1) + "')");
                    }
                    else
                    {
                        base.QueryConditionStrAdd(" Flag=@Flag ");
                        base.InsertParameter("@Flag", SqlDbType.VarChar, 50, value);
                    }
                }
            }
        }

        public string PaymentAccountsEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentAccounts=@PaymentAccounts ");
                    base.InsertParameter("@PaymentAccounts", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string PaymentBankEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentBank=@PaymentBank ");
                    base.InsertParameter("@PaymentBank", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string PaymentCoditionEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentCodition=@PaymentCodition ");
                    base.InsertParameter("@PaymentCodition", SqlDbType.VarChar, 0xfa0, value);
                }
            }
        }

        public decimal PaymentMoneyEqual
        {
            set
            {
                if (value != 0M)
                {
                    base.QueryConditionStrAdd(" PaymentMoney=@PaymentMoney ");
                    base.InsertParameter("@PaymentMoney", SqlDbType.Decimal, 9, value);
                }
            }
        }

        public string PaymentMoneyTypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentMoneyType=@PaymentMoneyType ");
                    base.InsertParameter("@PaymentMoneyType", SqlDbType.VarChar, 10, value);
                }
            }
        }

        public string PaymentRemarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentRemark=@PaymentRemark ");
                    base.InsertParameter("@PaymentRemark", SqlDbType.VarChar, 0x1f40, value);
                }
            }
        }

        public string PaymentTicketDateEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentTicketDate=@PaymentTicketDate ");
                    base.InsertParameter("@PaymentTicketDate", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string PaymentTicketMarkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentTicketMark=@PaymentTicketMark ");
                    base.InsertParameter("@PaymentTicketMark", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string PaymentUnitEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" PaymentUnit=@PaymentUnit ");
                    base.InsertParameter("@PaymentUnit", SqlDbType.VarChar, 100, value);
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
                    base.InsertParameter("@Remark", SqlDbType.VarChar, 0xfa0, value);
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

        public string TCheckPaymentcodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" TCheckPaymentcode=@TCheckPaymentcode ");
                    base.InsertParameter("@TCheckPaymentcode", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

