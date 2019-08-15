namespace TiannuoPM.MODEL
{
    using System;

    public class V_MaterialInventoryIOModel
    {
        private decimal _InQty;
        private DateTime _IODate;
        private string _IOType;
        private string _IOTypeName;
        private int _MaterialCode;
        private int _MaterialIOCode;
        private int _MaterialIODtlCode;
        private string _MaterialIOID;
        private string _MaterialName;
        private decimal _OutQty;
        private string _ProjectCode;
        private string _Spec;
        private string _Unit;

        public decimal InQty
        {
            get
            {
                return this._InQty;
            }
            set
            {
                this._InQty = value;
            }
        }

        public DateTime IODate
        {
            get
            {
                return this._IODate;
            }
            set
            {
                this._IODate = value;
            }
        }

        public string IOType
        {
            get
            {
                return this._IOType;
            }
            set
            {
                this._IOType = value;
            }
        }

        public string IOTypeName
        {
            get
            {
                return this._IOTypeName;
            }
            set
            {
                this._IOTypeName = value;
            }
        }

        public int MaterialCode
        {
            get
            {
                return this._MaterialCode;
            }
            set
            {
                this._MaterialCode = value;
            }
        }

        public int MaterialIOCode
        {
            get
            {
                return this._MaterialIOCode;
            }
            set
            {
                this._MaterialIOCode = value;
            }
        }

        public int MaterialIODtlCode
        {
            get
            {
                return this._MaterialIODtlCode;
            }
            set
            {
                this._MaterialIODtlCode = value;
            }
        }

        public string MaterialIOID
        {
            get
            {
                return this._MaterialIOID;
            }
            set
            {
                this._MaterialIOID = value;
            }
        }

        public string MaterialName
        {
            get
            {
                return this._MaterialName;
            }
            set
            {
                this._MaterialName = value;
            }
        }

        public decimal OutQty
        {
            get
            {
                return this._OutQty;
            }
            set
            {
                this._OutQty = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string Spec
        {
            get
            {
                return this._Spec;
            }
            set
            {
                this._Spec = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

