namespace TiannuoPM.MODEL
{
    using System;

    [Serializable]
    public class MaterialInDtlModel
    {
        private string _ContractCode;
        private string _GroupCode;
        private string _GroupFullID;
        private string _GroupName;
        private string _GroupSortID;
        private DateTime _InDate;
        private string _InGroupCode;
        private string _InGroupName;
        private decimal _InMoney;
        private decimal _InPrice;
        private decimal _InQty;
        private decimal _InvMoney;
        private decimal _InvQty;
        private int _MaterialCode;
        private int _MaterialInCode;
        private int _MaterialInDtlCode;
        private string _MaterialInID;
        private string _MaterialName;
        private decimal _OutQty;
        private string _ProjectCode;
        private string _Spec;
        private string _Unit;

        public string ContractCode
        {
            get
            {
                return this._ContractCode;
            }
            set
            {
                this._ContractCode = value;
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

        public DateTime InDate
        {
            get
            {
                return this._InDate;
            }
            set
            {
                this._InDate = value;
            }
        }

        public string InGroupCode
        {
            get
            {
                return this._InGroupCode;
            }
            set
            {
                this._InGroupCode = value;
            }
        }

        public string InGroupName
        {
            get
            {
                return this._InGroupName;
            }
            set
            {
                this._InGroupName = value;
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

        public decimal InPrice
        {
            get
            {
                return this._InPrice;
            }
            set
            {
                this._InPrice = value;
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

        public decimal InvMoney
        {
            get
            {
                return this._InvMoney;
            }
            set
            {
                this._InvMoney = value;
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

        public int MaterialInCode
        {
            get
            {
                return this._MaterialInCode;
            }
            set
            {
                this._MaterialInCode = value;
            }
        }

        public int MaterialInDtlCode
        {
            get
            {
                return this._MaterialInDtlCode;
            }
            set
            {
                this._MaterialInDtlCode = value;
            }
        }

        public string MaterialInID
        {
            get
            {
                return this._MaterialInID;
            }
            set
            {
                this._MaterialInID = value;
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

