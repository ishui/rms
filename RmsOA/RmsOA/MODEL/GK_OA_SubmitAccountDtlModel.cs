namespace RmsOA.MODEL
{
    using System;

    [Serializable]
    public class GK_OA_SubmitAccountDtlModel
    {
        private int _Code;
        private string _MastCode;
        private DateTime _Month;
        private decimal _RealityCost;
        private decimal _RemainCost;
        private string _Remark;
        private decimal _StandardCost;
        private decimal _SumCost;

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

        public string MastCode
        {
            get
            {
                return this._MastCode;
            }
            set
            {
                this._MastCode = value;
            }
        }

        public DateTime Month
        {
            get
            {
                return this._Month;
            }
            set
            {
                this._Month = value;
            }
        }

        public decimal RealityCost
        {
            get
            {
                return this._RealityCost;
            }
            set
            {
                this._RealityCost = value;
            }
        }

        public decimal RemainCost
        {
            get
            {
                return this._RemainCost;
            }
            set
            {
                this._RemainCost = value;
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

        public decimal StandardCost
        {
            get
            {
                return this._StandardCost;
            }
            set
            {
                this._StandardCost = value;
            }
        }

        public decimal SumCost
        {
            get
            {
                return this._SumCost;
            }
            set
            {
                this._SumCost = value;
            }
        }
    }
}

