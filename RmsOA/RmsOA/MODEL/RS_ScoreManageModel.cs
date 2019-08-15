namespace RmsOA.MODEL
{
    using System;

    public class RS_ScoreManageModel
    {
        private int _Code;
        private string _DeptCode;
        private DateTime _MarkDate;
        private string _Marker;
        private string _Status;
        private int _Type;

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

        public string DeptCode
        {
            get
            {
                return this._DeptCode;
            }
            set
            {
                this._DeptCode = value;
            }
        }

        public DateTime MarkDate
        {
            get
            {
                return this._MarkDate;
            }
            set
            {
                this._MarkDate = value;
            }
        }

        public string Marker
        {
            get
            {
                return this._Marker;
            }
            set
            {
                this._Marker = value;
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

        public int Type
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

