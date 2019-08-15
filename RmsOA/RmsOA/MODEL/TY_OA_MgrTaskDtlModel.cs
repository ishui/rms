namespace RmsOA.MODEL
{
    using System;

    [Serializable]
    public class TY_OA_MgrTaskDtlModel
    {
        private string _Assistpersons;
        private string _Bak;
        private DateTime _DeadLine;
        private string _Isfinish;
        private string _ManagerRevert;
        private int _MgrCodeID;
        private int _MgrDtlCode;
        private string _MgrDtlInfo;
        private string _ResponsePerson;
        private string _State;
        private string _TrancRevert;

        public string Assistpersons
        {
            get
            {
                return this._Assistpersons;
            }
            set
            {
                this._Assistpersons = value;
            }
        }

        public string Bak
        {
            get
            {
                return this._Bak;
            }
            set
            {
                this._Bak = value;
            }
        }

        public DateTime DeadLine
        {
            get
            {
                return this._DeadLine;
            }
            set
            {
                this._DeadLine = value;
            }
        }

        public string Isfinish
        {
            get
            {
                return this._Isfinish;
            }
            set
            {
                this._Isfinish = value;
            }
        }

        public string ManagerRevert
        {
            get
            {
                return this._ManagerRevert;
            }
            set
            {
                this._ManagerRevert = value;
            }
        }

        public int MgrCodeID
        {
            get
            {
                return this._MgrCodeID;
            }
            set
            {
                this._MgrCodeID = value;
            }
        }

        public int MgrDtlCode
        {
            get
            {
                return this._MgrDtlCode;
            }
            set
            {
                this._MgrDtlCode = value;
            }
        }

        public string MgrDtlInfo
        {
            get
            {
                return this._MgrDtlInfo;
            }
            set
            {
                this._MgrDtlInfo = value;
            }
        }

        public string ResponsePerson
        {
            get
            {
                return this._ResponsePerson;
            }
            set
            {
                this._ResponsePerson = value;
            }
        }

        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string TrancRevert
        {
            get
            {
                return this._TrancRevert;
            }
            set
            {
                this._TrancRevert = value;
            }
        }
    }
}

