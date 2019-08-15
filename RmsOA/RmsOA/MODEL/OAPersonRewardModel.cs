namespace RmsOA.MODEL
{
    using System;

    public class OAPersonRewardModel
    {
        private string _cause;
        private int _Code;
        private string _content;
        private string _dj_date;
        private string _personid;
        private string _remark;

        public string cause
        {
            get
            {
                return this._cause;
            }
            set
            {
                this._cause = value;
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

        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public string dj_date
        {
            get
            {
                return this._dj_date;
            }
            set
            {
                this._dj_date = value;
            }
        }

        public string personid
        {
            get
            {
                return this._personid;
            }
            set
            {
                this._personid = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
    }
}

