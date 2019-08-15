namespace RmsOA.MODEL
{
    using System;

    public class CachetTypeManageModel
    {
        private string _CachetName;
        private int _Code;
        private string _Type;

        public string CachetName
        {
            get
            {
                return this._CachetName;
            }
            set
            {
                this._CachetName = value;
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
    }
}

