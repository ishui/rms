namespace TiannuoPM.MODEL
{
    using System;

    public class MaterialInModel
    {
        private DateTime _CheckDate;
        private string _CheckPerson;
        private string _ContractCode;
        private string _ContractID;
        private string _ContractName;
        private string _GroupCode;
        private DateTime _InDate;
        private string _InPerson;
        private DateTime _InputDate;
        private string _InputPerson;
        private int _MaterialInCode;
        private string _MaterialInID;
        private string _ProjectCode;
        private string _Remark;
        private int _Status;

        public DateTime CheckDate
        {
            get
            {
                return this._CheckDate;
            }
            set
            {
                this._CheckDate = value;
            }
        }

        public string CheckPerson
        {
            get
            {
                return this._CheckPerson;
            }
            set
            {
                this._CheckPerson = value;
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

        public string ContractID
        {
            get
            {
                return this._ContractID;
            }
            set
            {
                this._ContractID = value;
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

        public string InPerson
        {
            get
            {
                return this._InPerson;
            }
            set
            {
                this._InPerson = value;
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

        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
    }
}

