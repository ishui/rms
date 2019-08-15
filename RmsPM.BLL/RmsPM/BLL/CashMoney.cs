namespace RmsPM.BLL
{
    using System;

    public class CashMoney
    {
        private string _ExchangeRate;
        private string _MoneyCash;
        private string _MoneyType;
        private string _RMBTypeCash;

        public string ExchangeRate
        {
            get
            {
                return this._ExchangeRate;
            }
            set
            {
                this._ExchangeRate = value;
            }
        }

        public string MoneyCash
        {
            get
            {
                return this._MoneyCash;
            }
            set
            {
                this._MoneyCash = value;
            }
        }

        public string MoneyType
        {
            get
            {
                return this._MoneyType;
            }
            set
            {
                this._MoneyType = value;
            }
        }

        public string RMBTypeCash
        {
            get
            {
                return this._RMBTypeCash;
            }
            set
            {
                this._RMBTypeCash = value;
            }
        }
    }
}

