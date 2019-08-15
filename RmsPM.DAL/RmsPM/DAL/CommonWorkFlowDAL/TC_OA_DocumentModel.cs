namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_OA_DocumentModel
    {
        private DateTime _ApplayDate;
        private int _Auditing;
        private int _Code;
        private string _DocumentCode;
        private string _DocumentName;
        private string _UnitCode;
        private string _UserCode;

        public DateTime ApplayDate
        {
            get
            {
                return this._ApplayDate;
            }
            set
            {
                this._ApplayDate = value;
            }
        }

        public int Auditing
        {
            get
            {
                return this._Auditing;
            }
            set
            {
                this._Auditing = value;
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

        public string DocumentCode
        {
            get
            {
                return this._DocumentCode;
            }
            set
            {
                this._DocumentCode = value;
            }
        }

        public string DocumentName
        {
            get
            {
                return this._DocumentName;
            }
            set
            {
                this._DocumentName = value;
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

        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }
    }
}

