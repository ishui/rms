namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ColumnMessage
    {
        public string _TableColumn;
        public string _ColumnValue;
    }
}

