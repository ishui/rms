namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_SubmitAccountMainModel
    {
        private int _Code;
        private string _Duties;
        private string _FileCode;
        private string _Name;
        private DateTime _RegiesterDate;
        private string _SystemCode;

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

        public string Duties
        {
            get
            {
                return this._Duties;
            }
            set
            {
                this._Duties = value;
            }
        }

        public string FileCode
        {
            get
            {
                return this._FileCode;
            }
            set
            {
                this._FileCode = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public DateTime RegiesterDate
        {
            get
            {
                return this._RegiesterDate;
            }
            set
            {
                this._RegiesterDate = value;
            }
        }

        public string SystemCode
        {
            get
            {
                return this._SystemCode;
            }
            set
            {
                this._SystemCode = value;
            }
        }
    }
}

