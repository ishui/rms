namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_CarMaintenanceModel
    {
        private string _Car_code;
        private int _Code;
        private DateTime _MDate;
        private decimal _Mil;
        private decimal _MPrice;
        private string _MValue;

        public string Car_code
        {
            get
            {
                return this._Car_code;
            }
            set
            {
                this._Car_code = value;
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

        public DateTime MDate
        {
            get
            {
                return this._MDate;
            }
            set
            {
                this._MDate = value;
            }
        }

        public decimal Mil
        {
            get
            {
                return this._Mil;
            }
            set
            {
                this._Mil = value;
            }
        }

        public decimal MPrice
        {
            get
            {
                return this._MPrice;
            }
            set
            {
                this._MPrice = value;
            }
        }

        public string MValue
        {
            get
            {
                return this._MValue;
            }
            set
            {
                this._MValue = value;
            }
        }
    }
}

