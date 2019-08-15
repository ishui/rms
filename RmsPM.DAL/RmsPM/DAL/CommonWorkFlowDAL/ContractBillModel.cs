namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class ContractBillModel
    {
        private decimal _BillMoney;
        private string _BillNo;
        private int _Code;
        private string _ContractCode;
        private string _ProjectCode;

        public decimal BillMoney
        {
            get
            {
                return this._BillMoney;
            }
            set
            {
                this._BillMoney = value;
            }
        }

        public string BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                this._BillNo = value;
            }
        }

        public int Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                this._Code = value;
            }
        }

        public string ContractCode
        {
            get
            {
                return this._ContractCode;
            }
            set
            {
                this._ContractCode = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }
    }
}

