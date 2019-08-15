namespace RmsOA.MODEL
{
    using System;

    public class GK_OAPersonWorkModel
    {
        private string _BeginDate;
        private string _Certifier;
        private int _Code;
        private string _Duty;
        private string _EndDate;
        private string _JobPost;
        private string _PersonID;
        private string _WorkUnit;

        public string BeginDate
        {
            get
            {
                return this._BeginDate;
            }
            set
            {
                this._BeginDate = value;
            }
        }

        public string Certifier
        {
            get
            {
                return this._Certifier;
            }
            set
            {
                this._Certifier = value;
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

        public string Duty
        {
            get
            {
                return this._Duty;
            }
            set
            {
                this._Duty = value;
            }
        }

        public string EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public string JobPost
        {
            get
            {
                return this._JobPost;
            }
            set
            {
                this._JobPost = value;
            }
        }

        public string PersonID
        {
            get
            {
                return this._PersonID;
            }
            set
            {
                this._PersonID = value;
            }
        }

        public string WorkUnit
        {
            get
            {
                return this._WorkUnit;
            }
            set
            {
                this._WorkUnit = value;
            }
        }
    }
}

