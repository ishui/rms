namespace RmsOA.BFL
{
    using System;

    public class RoomModelQuery
    {
        private string chaterMember;
        private int meetCode;
        private string message;
        private int roomCode;
        private string roomName;
        private string timeAge;
        private string topic;

        public string ChaterMember
        {
            get
            {
                return this.chaterMember;
            }
            set
            {
                this.chaterMember = value;
            }
        }

        public int MeetCode
        {
            get
            {
                return this.meetCode;
            }
            set
            {
                this.meetCode = value;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        public int RoomCode
        {
            get
            {
                return this.roomCode;
            }
            set
            {
                this.roomCode = value;
            }
        }

        public string RoomName
        {
            get
            {
                return this.roomName;
            }
            set
            {
                this.roomName = value;
            }
        }

        public string TimeAge
        {
            get
            {
                return this.timeAge;
            }
            set
            {
                this.timeAge = value;
            }
        }

        public string Topic
        {
            get
            {
                return this.topic;
            }
            set
            {
                this.topic = value;
            }
        }
    }
}

