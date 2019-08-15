namespace RmsOA.MODEL
{
    using System;

    public class TY_OA_MgrTaskModel
    {
        private int _Code;
        private DateTime _CreateDate;
        private string _CreateMan;
        private DateTime _DeadLine;
        private string _IsFinish;
        private string _MgrTaskID;
        private string _ReferLink;
        private string _State;
        private string _TaskDetail;
        private string _TaskName;
        private string _TaskTail;

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

        public string CreateMan
        {
            get
            {
                return this._CreateMan;
            }
            set
            {
                this._CreateMan = value;
            }
        }

        public DateTime DeadLine
        {
            get
            {
                return this._DeadLine;
            }
            set
            {
                this._DeadLine = value;
            }
        }

        public string IsFinish
        {
            get
            {
                return this._IsFinish;
            }
            set
            {
                this._IsFinish = value;
            }
        }

        public string MgrTaskID
        {
            get
            {
                return this._MgrTaskID;
            }
            set
            {
                this._MgrTaskID = value;
            }
        }

        public string ReferLink
        {
            get
            {
                return this._ReferLink;
            }
            set
            {
                this._ReferLink = value;
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

        public string TaskDetail
        {
            get
            {
                return this._TaskDetail;
            }
            set
            {
                this._TaskDetail = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this._TaskName = value;
            }
        }

        public string TaskTail
        {
            get
            {
                return this._TaskTail;
            }
            set
            {
                this._TaskTail = value;
            }
        }
    }
}

