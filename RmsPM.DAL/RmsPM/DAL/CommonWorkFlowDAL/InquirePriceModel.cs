namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class InquirePriceModel
    {
        private int _Aduiting;
        private string _Field1;
        private string _InquireObject;
        private int _InquirePriceCode;
        private string _ProjectName;
        private string _Requirement;

        public int Aduiting
        {
            get
            {
                return this._Aduiting;
            }
            set
            {
                this._Aduiting = value;
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

        public string InquireObject
        {
            get
            {
                return this._InquireObject;
            }
            set
            {
                this._InquireObject = value;
            }
        }

        public int InquirePriceCode
        {
            get
            {
                return this._InquirePriceCode;
            }
            set
            {
                this._InquirePriceCode = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this._ProjectName;
            }
            set
            {
                this._ProjectName = value;
            }
        }

        public string Requirement
        {
            get
            {
                return this._Requirement;
            }
            set
            {
                this._Requirement = value;
            }
        }
    }
}

