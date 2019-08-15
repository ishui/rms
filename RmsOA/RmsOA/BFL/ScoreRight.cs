namespace RmsOA.BFL
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    public struct ScoreRight
    {
        public static string ViewStat;
        public static string ScoreForEmploy
        {
            get
            {
                return "3901";
            }
        }
        public static string ScoreForManager
        {
            get
            {
                return "3902";
            }
        }
        public static string ScoreForDept
        {
            get
            {
                return "3903";
            }
        }
        static ScoreRight()
        {
            ViewStat = "3904";
        }
    }
}

