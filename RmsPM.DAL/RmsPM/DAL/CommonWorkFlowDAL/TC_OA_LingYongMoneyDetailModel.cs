namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_OA_LingYongMoneyDetailModel
    {
        private int _Code;
        private int _IsSubmit;
        private string _MastCode;
        private string _Name;
        private string _Number;
        private decimal _Price;
        private decimal _TotalMoney;

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

        public int IsSubmit
        {
            get
            {
                return this._IsSubmit;
            }
            set
            {
                this._IsSubmit = value;
            }
        }

        public string MastCode
        {
            get
            {
                return this._MastCode;
            }
            set
            {
                this._MastCode = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        public decimal TotalMoney
        {
            get
            {
                return this._TotalMoney;
            }
            set
            {
                this._TotalMoney = value;
            }
        }
    }
}

