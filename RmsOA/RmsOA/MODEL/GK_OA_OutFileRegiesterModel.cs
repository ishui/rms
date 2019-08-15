namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_OutFileRegiesterModel
    {
        private int _Code;
        private string _Detail;
        private string _FileCode;
        private string _FileNumeber;
        private DateTime _RegiesterDate;
        private string _Remark;
        private string _UnitCode;
        private string _UserCode;

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

        public string Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                this._Detail = value;
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

        public string FileNumeber
        {
            get
            {
                return this._FileNumeber;
            }
            set
            {
                this._FileNumeber = value;
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

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
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

