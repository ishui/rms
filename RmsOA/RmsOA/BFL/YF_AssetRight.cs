namespace RmsOA.BFL
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    public struct YF_AssetRight
    {
        public const string Add = "33040101";
        public const string Edit = "33040102";
        public const string Delete = "33040103";
    }
}

