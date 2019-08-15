namespace RmsOA.MODEL
{
    using System;

    public class YF_AssetDrawModel
    {
        private DateTime _BackTime;
        private int _Code;
        private DateTime _DrawDate;
        private string _DrawPerson;
        private string _DrawUnit;
        private int _ManageCode;
        private string _Status;
        private string _Unit;
        private string _UserCode;

        public DateTime BackTime
        {
            get
            {
                return this._BackTime;
            }
            set
            {
                this._BackTime = value;
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

        public DateTime DrawDate
        {
            get
            {
                return this._DrawDate;
            }
            set
            {
                this._DrawDate = value;
            }
        }

        public string DrawPerson
        {
            get
            {
                return this._DrawPerson;
            }
            set
            {
                this._DrawPerson = value;
            }
        }

        public string DrawUnit
        {
            get
            {
                return this._DrawUnit;
            }
            set
            {
                this._DrawUnit = value;
            }
        }

        public int ManageCode
        {
            get
            {
                return this._ManageCode;
            }
            set
            {
                this._ManageCode = value;
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
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

