namespace RmsPM.BLL
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    public struct QuarterMessage
    {
        public static readonly string Spring;
        public static readonly string Summer;
        public static readonly string Autumn;
        public static readonly string Winter;
        static QuarterMessage()
        {
            Spring = "第<br/>一<br/>季<br/>度";
            Summer = "第<br/>二<br/>季<br/>度";
            Autumn = "第<br/>三<br/>季<br/>度";
            Winter = "第<br/>四<br/>季<br/>度";
        }
    }
}

