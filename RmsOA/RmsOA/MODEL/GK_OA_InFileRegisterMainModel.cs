namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_InFileRegisterMainModel
    {
        private int _Code;
        private string _Field1;
        private string _FileCode;
        private DateTime _InFileDate;
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

        public string Field1
        {
            get
            {
                return this._Field1;
            }
            set
            {
                this._Field1 = value;
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

        public DateTime InFileDate
        {
            get
            {
                return this._InFileDate;
            }
            set
            {
                this._InFileDate = value;
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

