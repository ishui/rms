namespace RmsOA.MODEL
{
    using System;

    public class DeskTopTypeModel
    {
        private int _Code;
        private int _ControldID;
        private string _DeskType;

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

        public int ControldID
        {
            get
            {
                return this._ControldID;
            }
            set
            {
                this._ControldID = value;
            }
        }

        public string DeskType
        {
            get
            {
                return this._DeskType;
            }
            set
            {
                this._DeskType = value;
            }
        }
    }
}

