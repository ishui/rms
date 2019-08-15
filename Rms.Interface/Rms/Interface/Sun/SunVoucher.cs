namespace Rms.Interface.Sun
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    public class SunVoucher
    {
        public ArrayList Items = new ArrayList();

        public static string FillLeftB(string s1, int len, char c)
        {
            string text2;
            try
            {
                string s = s1;
                int num = LenB(s);
                if (num > len)
                {
                    s = LeftB(s, len);
                }
                else
                {
                    for (int i = 1; i <= (len - num); i++)
                    {
                        s = c.ToString() + s;
                    }
                }
                text2 = s;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string FillRightB(string s1, int len, char c)
        {
            string text2;
            try
            {
                string s = s1;
                int num = LenB(s);
                if (num > len)
                {
                    s = LeftB(s, len);
                }
                else
                {
                    for (int i = 1; i <= (len - num); i++)
                    {
                        s = s + c.ToString();
                    }
                }
                text2 = s;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string GetIntString(int d, int len)
        {
            string text2;
            try
            {
                text2 = FillLeftB(d.ToString(), len, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string GetNumberString(decimal d, int len, int dec)
        {
            string text2;
            try
            {
                d = (decimal) Math.Round((double) (((double) d) * Math.Pow(10, (double) dec)));
                text2 = FillLeftB(d.ToString(), len, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSpace(int len)
        {
            string text;
            try
            {
                text = GetString("", len);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        private static string GetString(object obj, int len)
        {
            string text2;
            try
            {
                string text = "";
                if (obj != null)
                {
                    try
                    {
                        text = (string) obj;
                    }
                    catch
                    {
                    }
                }
                text2 = FillRightB(text, len, " "[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string LeftB(string s1, int len)
        {
            string text2;
            try
            {
                string text = "";
                char[] chArray = s1.ToCharArray();
                int num = 0;
                foreach (char ch in chArray)
                {
                    int num2 = 0;
                    if (ch > '\x00ff')
                    {
                        num2 = 2;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    if ((num + num2) > len)
                    {
                        break;
                    }
                    text = text + ch;
                    num += num2;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int LenB(string s)
        {
            int num2;
            try
            {
                int num = 0;
                foreach (char ch in s.ToCharArray())
                {
                    if (ch > '\x00ff')
                    {
                        num += 2;
                    }
                    else
                    {
                        num++;
                    }
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public void SaveAs(string m_strFilename)
        {
            try
            {
                StreamWriter writer = new StreamWriter(m_strFilename, false, Encoding.Default);
                int d = 0;
                foreach (object obj2 in this.Items)
                {
                    d++;
                    SunVoucherItem item = (SunVoucherItem) obj2;
                    string text = "";
                    text = text + GetString(item.SubjectCode, 10) + GetSpace(5) + item.VoucherDate.Year.ToString().PadLeft(4, "0"[0]) + item.VoucherDate.Month.ToString().PadLeft(3, "0"[0]) + GetString(item.VoucherDate.ToString("yyyyMMdd"), 8) + GetSpace(2) + "M" + GetSpace(7) + GetIntString(d, 5) + GetSpace(2) + GetNumberString(item.Money, 0x12, 3) + GetString(item.GetCrebitTypeString(), 1) + GetSpace(1) + GetString("ACTUL", 5) + GetSpace(5) + GetString(item.BillNo, 10) + GetSpace(5) + GetString(item.Description, 0x19) + GetSpace(0x45) + GetString(item.MoneyType, 5) + GetNumberString(item.ExchangeRate, 0x12, 9) + GetNumberString(item.ForeignMoney, 0x12, 3) + GetSpace(14) + GetString(item.AnalysisCode0, 15) + GetString(item.AnalysisCode1, 15) + GetString(item.AnalysisCode2, 15) + GetString(item.AnalysisCode3, 15) + GetString(item.AnalysisCode4, 15) + GetString(item.AnalysisCode5, 15) + GetString(item.AnalysisCode6, 15) + GetString(item.AnalysisCode7, 15) + GetString(item.AnalysisCode8, 15) + GetString(item.AnalysisCode9, 15) + GetSpace(0x79);
                    writer.WriteLine(text);
                }
                writer.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

