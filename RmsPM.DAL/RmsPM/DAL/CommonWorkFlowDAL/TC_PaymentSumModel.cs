namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_PaymentSumModel
    {
        private DateTime _AuditDateTime;
        private int _Code;
        private string _CollectBillCode;
        private string _SendUnitCode;
        private string _Status;
        private DateTime _SumDateTime;
        private string _SumUserCode;

        public DateTime AuditDateTime
        {
            get
            {
                return this._AuditDateTime;
            }
            set
            {
                this._AuditDateTime = value;
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

        public string CollectBillCode
        {
            get
            {
                return this._CollectBillCode;
            }
            set
            {
                this._CollectBillCode = value;
            }
        }

        public string SendUnitCode
        {
            get
            {
                return this._SendUnitCode;
            }
            set
            {
                this._SendUnitCode = value;
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        public DateTime SumDateTime
        {
            get
            {
                return this._SumDateTime;
            }
            set
            {
                this._SumDateTime = value;
            }
        }

        public string SumUserCode
        {
            get
            {
                return this._SumUserCode;
            }
            set
            {
                this._SumUserCode = value;
            }
        }
    }
}

