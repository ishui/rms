namespace Codefresh.PhotoBrowserLibrary
{
    using System;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class EXIFPropertyItem
    {
        private byte[] data;
        private KnownEXIFIDCodes exifCode;
        private EXIFPropertyTypes exifType;
        private int id;
        private int len;
        private int type;

        public EXIFPropertyItem(PropertyItem prop)
        {
            if (Enum.IsDefined(this.exifCode.GetType(), prop.Id))
            {
                this.exifCode = (KnownEXIFIDCodes) prop.Id;
            }
            else
            {
                this.exifCode = KnownEXIFIDCodes.UnknownCode;
            }
            this.exifType = (EXIFPropertyTypes) prop.Type;
            this.data = prop.Value;
            this.type = prop.Type;
            this.id = prop.Id;
            this.len = prop.Len;
        }

        public KnownEXIFIDCodes EXIFCode
        {
            get
            {
                return this.exifCode;
            }
        }

        public EXIFPropertyTypes EXIFType
        {
            get
            {
                return this.exifType;
            }
        }

        public bool IsKnownCode
        {
            get
            {
                return (this.exifCode != KnownEXIFIDCodes.UnknownCode);
            }
        }

        public DateTime ParsedDate
        {
            get
            {
                DateTime time = new DateTime(0x76c, 1, 1, 0, 0, 0);
                string parsedString = this.ParsedString;
                if (parsedString.Length < 0x13)
                {
                    return time;
                }
                try
                {
                    int year = int.Parse(parsedString.Substring(0, 4));
                    int month = int.Parse(parsedString.Substring(5, 2));
                    int day = int.Parse(parsedString.Substring(8, 2));
                    int hour = int.Parse(parsedString.Substring(11, 2));
                    int minute = int.Parse(parsedString.Substring(14, 2));
                    return new DateTime(year, month, day, hour, minute, int.Parse(parsedString.Substring(0x11, 2)));
                }
                catch
                {
                    return time;
                }
            }
        }

        public EXIFRational ParsedRational
        {
            get
            {
                return new EXIFRational(this.data);
            }
        }

        public EXIFRational[] ParsedRationalArray
        {
            get
            {
                EXIFRational[] rationalArray = null;
                int num = this.data.Length / 8;
                if (num > 0)
                {
                    rationalArray = new EXIFRational[num];
                    for (int i = 0; i < num; i++)
                    {
                        rationalArray[i] = new EXIFRational(this.data, i * 8);
                    }
                }
                return rationalArray;
            }
        }

        public string ParsedString
        {
            get
            {
                string text = "";
                if (this.data.Length > 1)
                {
                    IntPtr ptr = Marshal.AllocHGlobal(this.data.Length);
                    int ofs = 0;
                    foreach (byte num2 in this.data)
                    {
                        Marshal.WriteByte(ptr, ofs, num2);
                        ofs++;
                    }
                    text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeHGlobal(ptr);
                }
                return text;
            }
        }

        public byte[] Value
        {
            get
            {
                return this.data;
            }
        }
    }
}

