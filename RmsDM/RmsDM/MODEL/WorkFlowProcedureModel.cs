namespace RmsDM.MODEL
{
    using System;

    public class WorkFlowProcedureModel
    {
        private int _Activity;
        private string _ApplicationInfoPath;
        private string _ApplicationPath;
        private DateTime _CreateDate;
        private string _CreateUser;
        private string _Creator;
        private string _Description;
        private DateTime _ModifyDate;
        private string _ModifyUser;
        private string _ProcedureCode;
        private string _ProcedureName;
        private string _ProcedureRemark;
        private string _ProjectCode;
        private string _Remark;
        private string _SysType;
        private int _Type;
        private string _VersionDescription;
        private decimal _VersionNumber;

        public int Activity
        {
            get
            {
                return this._Activity;
            }
            set
            {
                this._Activity = value;
            }
        }

        public string ApplicationInfoPath
        {
            get
            {
                return this._ApplicationInfoPath;
            }
            set
            {
                this._ApplicationInfoPath = value;
            }
        }

        public string ApplicationPath
        {
            get
            {
                return this._ApplicationPath;
            }
            set
            {
                this._ApplicationPath = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
            }
        }

        public string CreateUser
        {
            get
            {
                return this._CreateUser;
            }
            set
            {
                this._CreateUser = value;
            }
        }

        public string Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        public DateTime ModifyDate
        {
            get
            {
                return this._ModifyDate;
            }
            set
            {
                this._ModifyDate = value;
            }
        }

        public string ModifyUser
        {
            get
            {
                return this._ModifyUser;
            }
            set
            {
                this._ModifyUser = value;
            }
        }

        public string ProcedureCode
        {
            get
            {
                return this._ProcedureCode;
            }
            set
            {
                this._ProcedureCode = value;
            }
        }

        public string ProcedureName
        {
            get
            {
                return this._ProcedureName;
            }
            set
            {
                this._ProcedureName = value;
            }
        }

        public string ProcedureRemark
        {
            get
            {
                return this._ProcedureRemark;
            }
            set
            {
                this._ProcedureRemark = value;
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

        public string SysType
        {
            get
            {
                return this._SysType;
            }
            set
            {
                this._SysType = value;
            }
        }

        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        public string VersionDescription
        {
            get
            {
                return this._VersionDescription;
            }
            set
            {
                this._VersionDescription = value;
            }
        }

        public decimal VersionNumber
        {
            get
            {
                return this._VersionNumber;
            }
            set
            {
                this._VersionNumber = value;
            }
        }
    }
}

