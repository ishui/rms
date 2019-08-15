namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_OfficialSealRegiesterModel
    {
        private int _Code;
        private string _CollectCode;
        private string _Detail;
        private string _Field1;
        private DateTime _RegiesterDate;
        private string _Remark;
        private string _Status;
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

        public string CollectCode
        {
            get
            {
                return this._CollectCode;
            }
            set
            {
                this._CollectCode = value;
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

