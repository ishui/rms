namespace RmsPM.BLL
{
    using System;

    public class BiddingMoney
    {
        private CashMoney[] _CashMoneys;
        private string _CostBudgetSetCode;
        private string _CostCode;
        private string _PBSCode;
        private string _PBSType;
        private decimal _SumCashMoney;

        public CashMoney[] CashMoneys
        {
            get
            {
                return this._CashMoneys;
            }
            set
            {
                this._CashMoneys = value;
            }
        }

        public string CostBudgetSetCode
        {
            get
            {
                return this._CostBudgetSetCode;
            }
            set
            {
                this._CostBudgetSetCode = value;
            }
        }

        public string CostCode
        {
            get
            {
                return this._CostCode;
            }
            set
            {
                this._CostCode = value;
            }
        }

        public string PBSCode
        {
            get
            {
                return this._PBSCode;
            }
            set
            {
                this._PBSCode = value;
            }
        }

        public string PBSType
        {
            get
            {
                return this._PBSType;
            }
            set
            {
                this._PBSType = value;
            }
        }

        public decimal SumCashMoney
        {
            get
            {
                return this._SumCashMoney;
            }
            set
            {
                this._SumCashMoney = value;
            }
        }
    }
}

