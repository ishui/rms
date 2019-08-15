namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_FileChangeModel
    {
        private string _ChangeReason;
        private int _Code;
        private string _FileCode;
        private string _FileName;
        private string _FileSystemCode;
        private string _NewContext;
        private string _NewVersionNumber;
        private string _OldContext;
        private string _OldVersionNumber;
        private string _Status;
        private DateTime _SubmitDate;
        private string _SystemCode;
        private string _UnitCode;

        public string ChangeReason
        {
            get
            {
                return this._ChangeReason;
            }
            set
            {
                this._ChangeReason = value;
            }
        }

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

        public string FileCode
        {
            get
            {
                return this._FileCode;
            }
            set
            {
                this._FileCode = value;
            }
        }

        public string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;
            }
        }

        public string FileSystemCode
        {
            get
            {
                return this._FileSystemCode;
            }
            set
            {
                this._FileSystemCode = value;
            }
        }

        public string NewContext
        {
            get
            {
                return this._NewContext;
            }
            set
            {
                this._NewContext = value;
            }
        }

        public string NewVersionNumber
        {
            get
            {
                return this._NewVersionNumber;
            }
            set
            {
                this._NewVersionNumber = value;
            }
        }

        public string OldContext
        {
            get
            {
                return this._OldContext;
            }
            set
            {
                this._OldContext = value;
            }
        }

        public string OldVersionNumber
        {
            get
            {
                return this._OldVersionNumber;
            }
            set
            {
                this._OldVersionNumber = value;
            }
        }

        public string Status
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

        public DateTime SubmitDate
        {
            get
            {
                return this._SubmitDate;
            }
            set
            {
                this._SubmitDate = value;
            }
        }

        public string SystemCode
        {
            get
            {
                return this._SystemCode;
            }
            set
            {
                this._SystemCode = value;
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

