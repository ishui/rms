namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TC_OA_BigGoodsModel
    {
        private DateTime _ApplayDate;
        private int _Auditing;
        private int _Code;
        private string _GoodsCode;
        private string _Name;
        private string _Type;
        private string _UnitCode;
        private string _UserCode;
        private string _UseWay;

        public DateTime ApplayDate
        {
            get
            {
                return this._ApplayDate;
            }
            set
            {
                this._ApplayDate = value;
            }
        }

        public int Auditing
        {
            get
            {
                return this._Auditing;
            }
            set
            {
                this._Auditing = value;
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

        public string GoodsCode
        {
            get
            {
                return this._GoodsCode;
            }
            set
            {
                this._GoodsCode = value;
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

        public string Type
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

        public string UseWay
        {
            get
            {
                return this._UseWay;
            }
            set
            {
                this._UseWay = value;
            }
        }
    }
}

