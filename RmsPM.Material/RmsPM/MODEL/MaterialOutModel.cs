namespace TiannuoPM.MODEL
{
    using System;

    public class MaterialOutModel
    {
        private DateTime _CheckDate;
        private string _CheckPerson;
        private string _ContractCode;
        private string _ContractID;
        private string _ContractName;
        private string _GroupCode;
        private DateTime _InputDate;
        private string _InputPerson;
        private int _MaterialOutCode;
        private string _MaterialOutID;
        private DateTime _OutDate;
        private string _OutPerson;
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

        public string OutPerson
        {
            get
            {
                return this._OutPerson;
            }
            set
            {
                this._OutPerson = value;
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

