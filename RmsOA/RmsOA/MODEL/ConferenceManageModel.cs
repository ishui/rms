namespace RmsOA.MODEL
{
    using System;

    public class ConferenceManageModel
    {
        private string _ChaterMember;
        private int _Code;
        private string _Dept;
        private DateTime _EndTime;
        private string _Place;
        private string _Remark;
        private DateTime _StartTime;
        private string _State;
        private string _Topic;
        private string _Type;

        public string ChaterMember
        {
            get
            {
                return this._ChaterMember;
            }
            set
            {
                this._ChaterMember = value;
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

        public string Dept
        {
            get
            {
                return this._Dept;
            }
            set
            {
                this._Dept = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                this._EndTime = value;
            }
        }

        public string Place
        {
            get
            {
                return this._Place;
            }
            set
            {
                this._Place = value;
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

        public DateTime StartTime
        {
            get
            {
                return this._StartTime;
            }
            set
            {
                this._StartTime = value;
            }
        }

        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Topic
        {
            get
            {
                return this._Topic;
            }
            set
            {
                this._Topic = value;
            }
        }

        public string Type
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
    }
}

