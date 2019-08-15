namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_OA_PurchasePriceDetailModel
    {
        private int _Code;
        private string _ConcertTelephone;
        private string _ConcertUser;
        private string _Detail;
        private int _IsSubmit;
        private string _MastCode;
        private decimal _Price;
        private string _SupplyName;

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

        public string ConcertTelephone
        {
            get
            {
                return this._ConcertTelephone;
            }
            set
            {
                this._ConcertTelephone = value;
            }
        }

        public string ConcertUser
        {
            get
            {
                return this._ConcertUser;
            }
            set
            {
                this._ConcertUser = value;
            }
        }

        public string Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                this._Detail = value;
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

        public string SupplyName
        {
            get
            {
                return this._SupplyName;
            }
            set
            {
                this._SupplyName = value;
            }
        }
    }
}

