namespace TiannuoPM.MODEL
{
    using System;

    [Serializable]
    public class MaterialOutDtlModel
    {
        private string _ContractCode;
        private string _GroupCode;
        private string _GroupFullID;
        private string _GroupName;
        private string _GroupSortID;
        private string _InContractCode;
        private DateTime _InDate;
        private decimal _InPrice;
        private int _MaterialCode;
        private int _MaterialInCode;
        private int _MaterialInDtlCode;
        private string _MaterialInID;
        private string _MaterialName;
        private int _MaterialOutCode;
        private int _MaterialOutDtlCode;
        private string _MaterialOutID;
        private DateTime _OutDate;
        private decimal _OutMoney;
        private decimal _OutPrice;
        private decimal _OutQty;
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

        public string InContractCode
        {
            get
            {
                return this._InContractCode;
            }
            set
            {
                this._InContractCode = value;
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

        public int MaterialOutCode
        {
            get
            {
                return this._MaterialOutCode;
            }
            set
            {
                this._MaterialOutCode = value;
            }
        }

        public int MaterialOutDtlCode
        {
            get
            {
                return this._MaterialOutDtlCode;
            }
            set
            {
                this._MaterialOutDtlCode = value;
            }
        }

        public string MaterialOutID
        {
            get
            {
                return this._MaterialOutID;
            }
            set
            {
                this._MaterialOutID = value;
            }
        }

        public DateTime OutDate
        {
            get
            {
                return this._OutDate;
            }
            set
            {
                this._OutDate = value;
            }
        }

        public decimal OutMoney
        {
            get
            {
                return this._OutMoney;
            }
            set
            {
                this._OutMoney = value;
            }
        }

        public decimal OutPrice
        {
            get
            {
                return this._OutPrice;
            }
            set
            {
                this._OutPrice = value;
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

