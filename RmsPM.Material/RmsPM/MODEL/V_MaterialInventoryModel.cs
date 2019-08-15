namespace TiannuoPM.MODEL
{
    using System;

    public class V_MaterialInventoryModel
    {
        private string _GroupCode;
        private string _GroupFullID;
        private string _GroupName;
        private string _GroupSortID;
        private decimal _InMoney;
        private decimal _InQty;
        private decimal _InvQty;
        private int _MaterialCode;
        private string _MaterialName;
        private decimal _OutQty;
        private string _ProjectCode;
        private string _ProjectName;
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

        public string GroupFullID
        {
            get
            {
                return this._GroupFullID;
            }
            set
            {
                this._GroupFullID = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                this._GroupName = value;
            }
        }

        public string GroupSortID
        {
            get
            {
                return this._GroupSortID;
            }
            set
            {
                this._GroupSortID = value;
            }
        }

        public decimal InMoney
        {
            get
            {
                return this._InMoney;
            }
            set
            {
                this._InMoney = value;
            }
        }

        public decimal InQty
        {
            get
            {
                return this._InQty;
            }
            set
            {
                this._InQty = value;
            }
        }

        public decimal InvQty
        {
            get
            {
                return this._InvQty;
            }
            set
            {
                this._InvQty = value;
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

        public decimal OutQty
        {
            get
            {
                return this._OutQty;
            }
            set
            {
                this._OutQty = value;
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

