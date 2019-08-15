namespace RmsOA.MODEL
{
    using System;

    public class YF_AssetMainApplyModel
    {
        private string _ApplyDate;
        private int _Code;
        private string _Dept;
        private int _ManageCode;
        private string _Reason;
        private string _Remark;
        private string _Status;
        private string _UserCode;

        public string ApplyDate
        {
            get
            {
                return this._ApplyDate;
            }
            set
            {
                this._ApplyDate = value;
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

        public string Dept
        {
            get
            {
                return this._Dept;
            }
            set
            {
                this._Dept = value;
            }
        }

        public int ManageCode
        {
            get
            {
                return this._ManageCode;
            }
            set
            {
                this._ManageCode = value;
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

