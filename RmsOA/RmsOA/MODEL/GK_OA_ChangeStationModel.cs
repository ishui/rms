namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_ChangeStationModel
    {
        private int _Code;
        private string _Condition;
        private string _Field1;
        private string _FileCode;
        private DateTime _InComDate;
        private string _NewStation;
        private string _OldStation;
        private string _Reason;
        private string _Remark;
        private string _Status;
        private string _SystemCode;
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

        public string Condition
        {
            get
            {
                return this._Condition;
            }
            set
            {
                this._Condition = value;
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

        public DateTime InComDate
        {
            get
            {
                return this._InComDate;
            }
            set
            {
                this._InComDate = value;
            }
        }

        public string NewStation
        {
            get
            {
                return this._NewStation;
            }
            set
            {
                this._NewStation = value;
            }
        }

        public string OldStation
        {
            get
            {
                return this._OldStation;
            }
            set
            {
                this._OldStation = value;
            }
        }

        public string Reason
        {
            get
            {
                return this._Reason;
            }
            set
            {
                this._Reason = value;
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

