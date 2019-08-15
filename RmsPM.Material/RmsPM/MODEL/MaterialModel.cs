namespace TiannuoPM.MODEL
{
    using System;

    public class MaterialModel
    {
        private string _GroupCode;
        private DateTime _InputDate;
        private string _InputPerson;
        private int _MaterialCode;
        private string _MaterialName;
        private string _Remark;
        private string _Spec;
        private decimal _StandardPrice;
        private string _Unit;

        public string GroupCode
        {
            get
            {
                return this._GroupCode;
            }
            set
            {
                this._GroupCode = value;
            }
        }

        public DateTime InputDate
        {
            get
            {
                return this._InputDate;
            }
            set
            {
                this._InputDate = value;
            }
        }

        public string InputPerson
        {
            get
            {
                return this._InputPerson;
            }
            set
            {
                this._InputPerson = value;
            }
        }

        public int MaterialCode
        {
            get
            {
                return this._MaterialCode;
            }
            set
            {
                this._MaterialCode = value;
            }
        }

        public string MaterialName
        {
            get
            {
                return this._MaterialName;
            }
            set
            {
                this._MaterialName = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string Spec
        {
            get
            {
                return this._Spec;
            }
            set
            {
                this._Spec = value;
            }
        }

        public decimal StandardPrice
        {
            get
            {
                return this._StandardPrice;
            }
            set
            {
                this._StandardPrice = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

