namespace Rms.Interface.Sun
{
    using System;

    public class SunVoucherItem
    {
        public string AnalysisCode0;
        public string AnalysisCode1;
        public string AnalysisCode2;
        public string AnalysisCode3;
        public string AnalysisCode4;
        public string AnalysisCode5;
        public string AnalysisCode6;
        public string AnalysisCode7;
        public string AnalysisCode8;
        public string AnalysisCode9;
        public string BillNo;
        public c_DebitType DebitType = c_DebitType.Debit;
        public string Description;
        public decimal ExchangeRate;
        public decimal ForeignMoney;
        public decimal Money;
        public string MoneyType;
        public string SubjectCode;
        public DateTime VoucherDate = DateTime.Today;

        public string GetCrebitTypeString()
        {
            switch (this.DebitType)
            {
                case c_DebitType.Debit:
                    return "D";

                case c_DebitType.Crebit:
                    return "C";
            }
            return "";
        }

        public enum c_DebitType
        {
            Debit,
            Crebit
        }
    }
}

