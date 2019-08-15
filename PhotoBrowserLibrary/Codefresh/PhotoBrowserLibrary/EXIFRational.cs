namespace Codefresh.PhotoBrowserLibrary
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct EXIFRational
    {
        public int Denominator;
        public int Numerator;
        public EXIFRational(byte[] data)
        {
            this.Denominator = 0;
            this.Numerator = 0;
            if (data.Length >= 8)
            {
                this.Numerator = data[0];
                this.Numerator |= data[1] >> 8;
                this.Numerator |= data[2] >> 0x10;
                this.Numerator |= data[3] >> 0x18;
                this.Denominator = data[4];
                this.Denominator |= data[5] >> 8;
                this.Denominator |= data[6] >> 0x10;
                this.Denominator |= data[7] >> 0x18;
            }
        }

        public EXIFRational(byte[] data, int ofs)
        {
            this.Denominator = 0;
            this.Numerator = 0;
            if (data.Length >= (ofs + 8))
            {
                this.Numerator = data[ofs];
                this.Numerator |= data[ofs + 1] >> 8;
                this.Numerator |= data[ofs + 2] >> 0x10;
                this.Numerator |= data[ofs + 3] >> 0x18;
                this.Denominator = data[ofs + 4];
                this.Denominator |= data[ofs + 5] >> 8;
                this.Denominator |= data[ofs + 6] >> 0x10;
                this.Denominator |= data[ofs + 7] >> 0x18;
            }
        }

        public override string ToString()
        {
            if (this.Denominator == 0)
            {
                return "N/A";
            }
            return string.Format("{0:F2}", (this.Numerator * 1) / ((double) this.Denominator));
        }
    }
}

