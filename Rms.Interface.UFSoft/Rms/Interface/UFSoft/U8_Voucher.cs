namespace Rms.Interface.UFSoft
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    public class U8_Voucher
    {
        private string g_strFirstLine = "";
        public ArrayList Items = new ArrayList();

        public U8_Voucher(VoucherType m_Type, string m_strVersion)
        {
            switch (m_Type)
            {
                case VoucherType.未引入过文本:
                    this.g_strFirstLine = "填制凭证," + m_strVersion;
                    break;

                case VoucherType.输出文本:
                    this.g_strFirstLine = "凭证输出," + m_strVersion;
                    break;

                case VoucherType.已引入过文本:
                    this.g_strFirstLine = "凭证已引入," + m_strVersion;
                    break;
            }
        }

        public void SaveAs(string m_strFilename)
        {
            StreamWriter writer = new StreamWriter(m_strFilename, false, Encoding.Default);
            writer.WriteLine(this.g_strFirstLine);
            foreach (object obj2 in this.Items)
            {
                U8_VoucherItem item = (U8_VoucherItem) obj2;
                string text = item.Field01 + "," + item.Field02 + "," + item.Field03 + "," + item.Field04 + "," + item.Field05 + "," + item.Field06 + "," + item.Field07 + "," + item.Field08 + "," + item.Field09 + "," + item.Field10 + "," + item.Field11 + "," + item.Field12 + "," + item.Field13 + "," + item.Field14 + "," + item.Field15 + "," + item.Field16 + "," + item.Field17 + "," + item.Field18 + "," + item.Field19 + "," + item.Field20 + "," + item.Field21 + "," + item.Field22 + "," + item.Field23 + "," + item.Field24 + "," + item.Field25 + "," + item.Field26 + "," + item.Field27 + "," + item.Field28 + "," + item.Field29 + "," + item.Field30 + "," + item.Field31 + "," + item.Field32 + "," + item.Field33 + "," + item.Field34 + "," + item.Field35 + "," + item.Field36 + "," + item.Field37 + "," + item.Field38 + "," + (item.Field39 ? "1" : "0") + "," + (item.Field40 ? "1" : "0") + "," + (item.Field41 ? "1" : "0") + "," + (item.Field42 ? "1" : "0") + "," + (item.Field43 ? "1" : "0") + "," + item.Field44 + "," + (item.Field45 ? "1" : "0") + "," + (item.Field46 ? "1" : "0") + "," + (item.Field47 ? "1" : "0") + "," + (item.Field48 ? "1" : "0");
                writer.WriteLine(text);
            }
            writer.Close();
        }
    }
}

