namespace RmsOA.BFL
{
    using System;

    public class MeetRoomBOX
    {
        private int roomCode;
        private string roomName;

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
    }
}

