namespace RmsOA.MODEL
{
    using System;

    public class CachetManageModel
    {
        private string _CachetName;
        private int _Code;
        private string _Dept;
        private DateTime _EndData;
        private DateTime _StartData;
        private string _State;
        private string _Type;
        private string _UserName;

        public string CachetName
        {
            get
            {
                return this._CachetName;
            }
            set
            {
                this._CachetName = value;
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

        public DateTime EndData
        {
            get
            {
                return this._EndData;
            }
            set
            {
                this._EndData = value;
            }
        }

        public DateTime StartData
        {
            get
            {
                return this._StartData;
            }
            set
            {
                this._StartData = value;
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

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
    }
}

