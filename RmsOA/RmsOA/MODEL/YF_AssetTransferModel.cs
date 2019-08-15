namespace RmsOA.MODEL
{
    using System;

    public class YF_AssetTransferModel
    {
        private string _Applyer;
        private int _Code;
        private int _ManageCode;
        private string _PostDept;
        private string _PreDept;
        private string _Status;
        private string _TransferTime;
        private string _UnitCode;

        public string Applyer
        {
            get
            {
                return this._Applyer;
            }
            set
            {
                this._Applyer = value;
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

        public string PostDept
        {
            get
            {
                return this._PostDept;
            }
            set
            {
                this._PostDept = value;
            }
        }

        public string PreDept
        {
            get
            {
                return this._PreDept;
            }
            set
            {
                this._PreDept = value;
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

        public string TransferTime
        {
            get
            {
                return this._TransferTime;
            }
            set
            {
                this._TransferTime = value;
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

