namespace Rms.ORMap
{
    using System;

    public class FormatStringSegment
    {
        public int CurrentID = 1;
        public char FillChar = '0';
        public bool IsFillEmptyChar = false;
        public int Lenth = 1;
        public string StaticString = "";
        public FormatStringSegmentType Type = FormatStringSegmentType.StaticString;
    }
}

