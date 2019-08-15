namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_ArchivesCopyModel
    {
        private string _ArchivesType;
        private int _Code;
        private string _FileCode;
        private string _FileName;
        private string _FileSystemCode;
        private string _Reason;
        private DateTime _RegiesterDate;
        private DateTime _ReturnDate;
        private string _Status;
        private string _SystemCode;
        private string _UnitCode;
        private string _UsePerson;

        public string ArchivesType
        {
            get
            {
                return this._ArchivesType;
            }
            set
            {
                this._ArchivesType = value;
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

        public string Reason
        {
            get
            {
                return this._Reason;
            }
            set
            {
                this._Reason = value;
            }
        }

        public DateTime RegiesterDate
        {
            get
            {
                return this._RegiesterDate;
            }
            set
            {
                this._RegiesterDate = value;
            }
        }

        public DateTime ReturnDate
        {
            get
            {
                return this._ReturnDate;
            }
            set
            {
                this._ReturnDate = value;
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

        public string UsePerson
        {
            get
            {
                return this._UsePerson;
            }
            set
            {
                this._UsePerson = value;
            }
        }
    }
}

