namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_MakeMarksModel
    {
        private int _Code;
        private string _MarkType;
        private DateTime _RegisterDate;
        private string _RegisterPerson;
        private string _Title;
        private string _UnitCode;

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

        public string MarkType
        {
            get
            {
                return this._MarkType;
            }
            set
            {
                this._MarkType = value;
            }
        }

        public DateTime RegisterDate
        {
            get
            {
                return this._RegisterDate;
            }
            set
            {
                this._RegisterDate = value;
            }
        }

        public string RegisterPerson
        {
            get
            {
                return this._RegisterPerson;
            }
            set
            {
                this._RegisterPerson = value;
            }
        }

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public string UnitCode
        {
            get
            {
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }
    }
}

