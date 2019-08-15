namespace RmsOA.MODEL
{
    using System;

    public class ConferenceUserListModel
    {
        private int _Code;
        private int _ConferenceCode;
        private string _State;
        private string _Type;
        private string _UserCode;
        private string _UserName;

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

        public int ConferenceCode
        {
            get
            {
                return this._ConferenceCode;
            }
            set
            {
                this._ConferenceCode = value;
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

