namespace RmsOA.MODEL
{
    using System;

    public class MeetRoomModel
    {
        private string _Capacity;
        private int _Code;
        private string _HardCondition;
        private string _Place;
        private string _Remark;
        private string _RoomName;

        public string Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {
                this._Capacity = value;
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

        public string HardCondition
        {
            get
            {
                return this._HardCondition;
            }
            set
            {
                this._HardCondition = value;
            }
        }

        public string Place
        {
            get
            {
                return this._Place;
            }
            set
            {
                this._Place = value;
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

        public string RoomName
        {
            get
            {
                return this._RoomName;
            }
            set
            {
                this._RoomName = value;
            }
        }
    }
}

