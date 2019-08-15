namespace RmsOA.MODEL
{
    using System;

    public class RS_EmployScoreModel
    {
        private int _Code;
        private int _ManageCode;
        private int _Score;
        private string _UserCode;

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

        public int Score
        {
            get
            {
                return this._Score;
            }
            set
            {
                this._Score = value;
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

