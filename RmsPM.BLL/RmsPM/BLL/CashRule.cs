namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public class CashRule
    {
        private object _CashMessageCode;
        private string _CashMessageType = null;
        private string _CashMessageTypeCode = null;
        private int _count = -1;
        private DataTable _DetailCost = null;
        private DataSet _DetailDataSet = null;
        private object _TempMoneyValue;
        private DataTable _TotalCost = null;
        private object _TotalMoney = -1;

        private DataTable GetDetailCost()
        {
            this.GetTotalCost();
            Cash_Detail detail = new Cash_Detail();
            detail.Cash_MessageCode = this._CashMessageCode.ToString();
            this._DetailCost = detail.GetCash_Details();
            return this._DetailCost;
        }

        private DataTable GetTotalCost()
        {
            this.IsBool();
            Cash_Message message = new Cash_Message();
            message.CashMessageType = this._CashMessageType;
            message.CashMessageTypeCode = this._CashMessageTypeCode;
            DataTable table = message.GetCash_Messages();
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            this._CashMessageCode = table.Rows[0]["CashMessageCode"];
            this._TotalMoney = table.Rows[0]["CashMessageCashTotal"];
            this._TempMoneyValue = table.Rows[0]["CashMessageTemporaryMoney"];
            this._TotalCost = table;
            return table;
        }

        private void IsBool()
        {
            if ((this._CashMessageType == null) || (this._CashMessageTypeCode == null))
            {
                throw new Exception("对不起,条件不足");
            }
        }

        public string CashMessageType
        {
            get
            {
                return this._CashMessageType;
            }
            set
            {
                this._CashMessageType = value;
            }
        }

        public string CashMessageTypeCode
        {
            get
            {
                return this._CashMessageTypeCode;
            }
            set
            {
                this._CashMessageTypeCode = value;
            }
        }

        public int Count
        {
            get
            {
                if (this._TotalMoney.ToString() == "-1")
                {
                    this.GetDetailCost();
                }
                if (this._count == -1)
                {
                    this._count = this._DetailCost.Rows.Count;
                }
                return this._count;
            }
        }

        public DataTable DetailCostTable
        {
            get
            {
                if (this._DetailCost == null)
                {
                    this.GetDetailCost();
                }
                return this._DetailCost;
            }
        }

        public DataSet DetailDataSet
        {
            get
            {
                if (this._DetailDataSet == null)
                {
                    this._DetailDataSet = new DataSet();
                    this._DetailDataSet.Tables.Add(this.DetailCostTable);
                }
                return this._DetailDataSet;
            }
        }

        public string TempMoneyText
        {
            get
            {
                if (this._TotalMoney.ToString() == "-1")
                {
                    this.GetDetailCost();
                }
                return this._TempMoneyValue.ToString();
            }
        }

        public decimal TempMoneyValue
        {
            get
            {
                if (this._TotalMoney.ToString() == "-1")
                {
                    this.GetDetailCost();
                }
                return Convert.ToDecimal(this._TempMoneyValue);
            }
        }

        public DataTable TotalCostTable
        {
            get
            {
                return this._TotalCost;
            }
        }

        public string TotalMoneyText
        {
            get
            {
                if (this._TotalMoney.ToString() == "-1")
                {
                    this.GetDetailCost();
                }
                return this._TotalMoney.ToString();
            }
        }

        public decimal TotalMoneyValue
        {
            get
            {
                if (this._TotalMoney.ToString() == "-1")
                {
                    this.GetDetailCost();
                }
                return Convert.ToDecimal(this._TotalMoney);
            }
        }
    }
}

