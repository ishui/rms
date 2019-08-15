namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_OilModel
    {
        private string _Car_id;
        private int _Code;
        private decimal _FactMil;
        private decimal _FirstMil;
        private DateTime _GetDate;
        private string _GetMan;
        private decimal _GetNum;
        private string _IndexNum;
        private decimal _ThisMil;

        public string Car_id
        {
            get
            {
                return this._Car_id;
            }
            set
            {
                this._Car_id = value;
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

        public decimal FactMil
        {
            get
            {
                return this._FactMil;
            }
            set
            {
                this._FactMil = value;
            }
        }

        public decimal FirstMil
        {
            get
            {
                return this._FirstMil;
            }
            set
            {
                this._FirstMil = value;
            }
        }

        public DateTime GetDate
        {
            get
            {
                return this._GetDate;
            }
            set
            {
                this._GetDate = value;
            }
        }

        public string GetMan
        {
            get
            {
                return this._GetMan;
            }
            set
            {
                this._GetMan = value;
            }
        }

        public decimal GetNum
        {
            get
            {
                return this._GetNum;
            }
            set
            {
                this._GetNum = value;
            }
        }

        public string IndexNum
        {
            get
            {
                return this._IndexNum;
            }
            set
            {
                this._IndexNum = value;
            }
        }

        public decimal ThisMil
        {
            get
            {
                return this._ThisMil;
            }
            set
            {
                this._ThisMil = value;
            }
        }
    }
}

