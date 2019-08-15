namespace RmsOA.MODEL
{
    using System;

    public class OAPersonContractModel
    {
        private int _Code;
        private string _ContractCode;
        private string _EndDate;
        private string _PersonID;
        private string _RegisterDate;
        private string _Remark;
        private string _StartDate;
        private string _StationCode;

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

        public string EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public string PersonID
        {
            get
            {
                return this._PersonID;
            }
            set
            {
                this._PersonID = value;
            }
        }

        public string RegisterDate
        {
            get
            {
                return this._RegisterDate;
            }
            set
            {
                this._RegisterDate = value;
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

        public string StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
            }
        }

        public string StationCode
        {
            get
            {
                return this._StationCode;
            }
            set
            {
                this._StationCode = value;
            }
        }
    }
}

