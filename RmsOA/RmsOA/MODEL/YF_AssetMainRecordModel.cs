namespace RmsOA.MODEL
{
    using System;

    public class YF_AssetMainRecordModel
    {
        private string _ChangeDetail;
        private int _Code;
        private string _Content;
        private decimal _CostMoney;
        private int _CostTime;
        private int _FKCode;
        private DateTime _MainTime;
        private string _MainUser;
        private string _Result;
        private string _UserSign;

        public string ChangeDetail
        {
            get
            {
                return this._ChangeDetail;
            }
            set
            {
                this._ChangeDetail = value;
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

        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        public decimal CostMoney
        {
            get
            {
                return this._CostMoney;
            }
            set
            {
                this._CostMoney = value;
            }
        }

        public int CostTime
        {
            get
            {
                return this._CostTime;
            }
            set
            {
                this._CostTime = value;
            }
        }

        public int FKCode
        {
            get
            {
                return this._FKCode;
            }
            set
            {
                this._FKCode = value;
            }
        }

        public DateTime MainTime
        {
            get
            {
                return this._MainTime;
            }
            set
            {
                this._MainTime = value;
            }
        }

        public string MainUser
        {
            get
            {
                return this._MainUser;
            }
            set
            {
                this._MainUser = value;
            }
        }

        public string Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                this._Result = value;
            }
        }

        public string UserSign
        {
            get
            {
                return this._UserSign;
            }
            set
            {
                this._UserSign = value;
            }
        }
    }
}

