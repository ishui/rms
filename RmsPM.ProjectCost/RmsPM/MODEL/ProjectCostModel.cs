namespace TiannuoPM.MODEL
{
    using System;

    public class ProjectCostModel
    {
        private decimal _Area;
        private string _GroupCode;
        private DateTime _InputDate;
        private string _InputPerson;
        private decimal _Money;
        private decimal _Price;
        private int _ProjectCostCode;
        private string _ProjectName;
        private decimal _Qty;
        private string _Remark;
        private string _Unit;

        public decimal Area
        {
            get
            {
                return this._Area;
            }
            set
            {
                this._Area = value;
            }
        }

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

        public decimal Money
        {
            get
            {
                return this._Money;
            }
            set
            {
                this._Money = value;
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

        public int ProjectCostCode
        {
            get
            {
                return this._ProjectCostCode;
            }
            set
            {
                this._ProjectCostCode = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this._ProjectName;
            }
            set
            {
                this._ProjectName = value;
            }
        }

        public decimal Qty
        {
            get
            {
                return this._Qty;
            }
            set
            {
                this._Qty = value;
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

