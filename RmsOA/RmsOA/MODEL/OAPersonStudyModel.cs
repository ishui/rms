namespace RmsOA.MODEL
{
    using System;

    public class OAPersonStudyModel
    {
        private string _BEGIN_DATE;
        private string _Certifier;
        private int _Code;
        private string _DEGREE;
        private string _END_DATE;
        private string _LETTER_NAME;
        private string _personid;
        private string _SCHOOL_NAME;
        private string _SPECIALITY;

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

        public string DEGREE
        {
            get
            {
                return this._DEGREE;
            }
            set
            {
                this._DEGREE = value;
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

        public string LETTER_NAME
        {
            get
            {
                return this._LETTER_NAME;
            }
            set
            {
                this._LETTER_NAME = value;
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

        public string SCHOOL_NAME
        {
            get
            {
                return this._SCHOOL_NAME;
            }
            set
            {
                this._SCHOOL_NAME = value;
            }
        }

        public string SPECIALITY
        {
            get
            {
                return this._SPECIALITY;
            }
            set
            {
                this._SPECIALITY = value;
            }
        }
    }
}

