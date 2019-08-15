namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_OA_WorkConcertModel
    {
        private int _Auditing;
        private int _Code;
        private string _GetUnitCode;
        private string _GetUserCode;
        private DateTime _SendDate;
        private string _SendUnitCode;
        private string _SendUserCode;

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

        public string GetUnitCode
        {
            get
            {
                return this._GetUnitCode;
            }
            set
            {
                this._GetUnitCode = value;
            }
        }

        public string GetUserCode
        {
            get
            {
                return this._GetUserCode;
            }
            set
            {
                this._GetUserCode = value;
            }
        }

        public DateTime SendDate
        {
            get
            {
                return this._SendDate;
            }
            set
            {
                this._SendDate = value;
            }
        }

        public string SendUnitCode
        {
            get
            {
                return this._SendUnitCode;
            }
            set
            {
                this._SendUnitCode = value;
            }
        }

        public string SendUserCode
        {
            get
            {
                return this._SendUserCode;
            }
            set
            {
                this._SendUserCode = value;
            }
        }
    }
}

