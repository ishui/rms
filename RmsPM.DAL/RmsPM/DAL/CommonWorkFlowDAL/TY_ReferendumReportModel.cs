namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TY_ReferendumReportModel
    {
        private string _Audit;
        private string _Field1;
        private string _Number;
        private int _ReferendumReportCode;
        private DateTime _StartDate;
        private string _StartPerson;
        private string _TextContent;
        private string _TextTitle;

        public string Audit
        {
            get
            {
                return this._Audit;
            }
            set
            {
                this._Audit = value;
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

        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        public int ReferendumReportCode
        {
            get
            {
                return this._ReferendumReportCode;
            }
            set
            {
                this._ReferendumReportCode = value;
            }
        }

        public DateTime StartDate
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

        public string StartPerson
        {
            get
            {
                return this._StartPerson;
            }
            set
            {
                this._StartPerson = value;
            }
        }

        public string TextContent
        {
            get
            {
                return this._TextContent;
            }
            set
            {
                this._TextContent = value;
            }
        }

        public string TextTitle
        {
            get
            {
                return this._TextTitle;
            }
            set
            {
                this._TextTitle = value;
            }
        }
    }
}

