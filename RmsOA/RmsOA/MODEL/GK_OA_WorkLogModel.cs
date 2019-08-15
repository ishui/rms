namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_WorkLogModel
    {
        private int _Code;
        private string _Context;
        private DateTime _DayWrited;
        private string _Mood;
        private string _UserId;
        private string _Waiter;
        private string _Weather;

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

        public string Context
        {
            get
            {
                return this._Context;
            }
            set
            {
                this._Context = value;
            }
        }

        public DateTime DayWrited
        {
            get
            {
                return this._DayWrited;
            }
            set
            {
                this._DayWrited = value;
            }
        }

        public string Mood
        {
            get
            {
                return this._Mood;
            }
            set
            {
                this._Mood = value;
            }
        }

        public string UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }

        public string Waiter
        {
            get
            {
                return this._Waiter;
            }
            set
            {
                this._Waiter = value;
            }
        }

        public string Weather
        {
            get
            {
                return this._Weather;
            }
            set
            {
                this._Weather = value;
            }
        }
    }
}

