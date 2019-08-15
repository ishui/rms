namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_InFileRegisterModel
    {
        private string _AuditingMailCode;
        private int _Code;
        private string _Field1;
        private string _FileNumber;
        private string _FileType;
        private string _InFileCode;
        private DateTime _InFileDate;
        private string _OriginalFileCode;
        private string _RegisterMainCode;
        private string _Remark;

        public string AuditingMailCode
        {
            get
            {
                return this._AuditingMailCode;
            }
            set
            {
                this._AuditingMailCode = value;
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

        public string Field1
        {
            get
            {
                return this._Field1;
            }
            set
            {
                this._Field1 = value;
            }
        }

        public string FileNumber
        {
            get
            {
                return this._FileNumber;
            }
            set
            {
                this._FileNumber = value;
            }
        }

        public string FileType
        {
            get
            {
                return this._FileType;
            }
            set
            {
                this._FileType = value;
            }
        }

        public string InFileCode
        {
            get
            {
                return this._InFileCode;
            }
            set
            {
                this._InFileCode = value;
            }
        }

        public DateTime InFileDate
        {
            get
            {
                return this._InFileDate;
            }
            set
            {
                this._InFileDate = value;
            }
        }

        public string OriginalFileCode
        {
            get
            {
                return this._OriginalFileCode;
            }
            set
            {
                this._OriginalFileCode = value;
            }
        }

        public string RegisterMainCode
        {
            get
            {
                return this._RegisterMainCode;
            }
            set
            {
                this._RegisterMainCode = value;
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
    }
}

