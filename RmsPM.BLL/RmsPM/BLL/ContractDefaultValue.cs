namespace RmsPM.BLL
{
    using System;

    public class ContractDefaultValue
    {
        private string _BiddingCode;
        private BiddingMoney[] _BiddingMoneys;
        private decimal _ContractMoney;
        private string _ContractName;
        private string _ContractNumber;
        private string _ContractRemark;
        private string _ContractType;
        private bool _Mostly;
        private string _ObligateMoney;
        private string _ProjectCode;
        private string _SupplierCode;
        private string _UnitCode;

        public string BiddingCode
        {
            get
            {
                return this._BiddingCode;
            }
            set
            {
                this._BiddingCode = value;
            }
        }

        public BiddingMoney[] BiddingMoneys
        {
            get
            {
                return this._BiddingMoneys;
            }
            set
            {
                this._BiddingMoneys = value;
            }
        }

        public decimal ContractMoney
        {
            get
            {
                return this._ContractMoney;
            }
            set
            {
                this._ContractMoney = value;
            }
        }

        public string ContractName
        {
            get
            {
                return this._ContractName;
            }
            set
            {
                this._ContractName = value;
            }
        }

        public string ContractNumber
        {
            get
            {
                return this._ContractNumber;
            }
            set
            {
                this._ContractNumber = value;
            }
        }

        public string ContractRemark
        {
            get
            {
                return this._ContractRemark;
            }
            set
            {
                this._ContractRemark = value;
            }
        }

        public string ContractType
        {
            get
            {
                return this._ContractType;
            }
            set
            {
                this._ContractType = value;
            }
        }

        public bool Mostly
        {
            get
            {
                return this._Mostly;
            }
            set
            {
                this._Mostly = value;
            }
        }

        public string ObligateMoney
        {
            get
            {
                return this._ObligateMoney;
            }
            set
            {
                this._ObligateMoney = value;
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

        public string SupplierCode
        {
            get
            {
                return this._SupplierCode;
            }
            set
            {
                this._SupplierCode = value;
            }
        }

        public string UnitCode
        {
            get
            {
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }
    }
}

