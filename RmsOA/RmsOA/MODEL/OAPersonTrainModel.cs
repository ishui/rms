namespace RmsOA.MODEL
{
    using System;

    public class OAPersonTrainModel
    {
        private string _BEGIN_DATE;
        private int _Code;
        private string _END_DATE;
        private string _personid;
        private string _TRAIN_CONTENT;
        private string _TRAIN_HOUR;
        private string _TRAIN_METHOD;

        public string BEGIN_DATE
        {
            get
            {
                return this._BEGIN_DATE;
            }
            set
            {
                this._BEGIN_DATE = value;
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

        public string END_DATE
        {
            get
            {
                return this._END_DATE;
            }
            set
            {
                this._END_DATE = value;
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

        public string TRAIN_CONTENT
        {
            get
            {
                return this._TRAIN_CONTENT;
            }
            set
            {
                this._TRAIN_CONTENT = value;
            }
        }

        public string TRAIN_HOUR
        {
            get
            {
                return this._TRAIN_HOUR;
            }
            set
            {
                this._TRAIN_HOUR = value;
            }
        }

        public string TRAIN_METHOD
        {
            get
            {
                return this._TRAIN_METHOD;
            }
            set
            {
                this._TRAIN_METHOD = value;
            }
        }
    }
}

