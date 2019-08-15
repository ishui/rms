namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_OA_BigGoodsDetailModel
    {
        private int _Code;
        private string _GoodName;
        private int _IsSubmit;
        private string _MastCode;
        private decimal _Numeber;
        private string _Type;
        private DateTime _UseDate;
        private string _UseWay;

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

        public string GoodName
        {
            get
            {
                return this._GoodName;
            }
            set
            {
                this._GoodName = value;
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

        public decimal Numeber
        {
            get
            {
                return this._Numeber;
            }
            set
            {
                this._Numeber = value;
            }
        }

        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        public DateTime UseDate
        {
            get
            {
                return this._UseDate;
            }
            set
            {
                this._UseDate = value;
            }
        }

        public string UseWay
        {
            get
            {
                return this._UseWay;
            }
            set
            {
                this._UseWay = value;
            }
        }
    }
}

