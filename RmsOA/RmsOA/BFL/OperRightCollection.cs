namespace RmsOA.BFL
{
    using System;
    using System.Runtime.InteropServices;

    public static class OperRightCollection
    {
        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct MeetRoomOper
        {
            public const string Add = "320701";
            public const string Edit = "320702";
            public const string Delete = "320703";
        }
    }
}

