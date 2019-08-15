namespace RmsPM.BLL
{
    using System;
    using System.Web;

    public sealed class StringRule
    {
        private static string[] cstr = new string[] { "0", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

        private StringRule()
        {
        }

        public static string AddUnit(object text, string unit)
        {
            string text3;
            try
            {
                string text2 = ConvertRule.ToString(text);
                if (text2.Length > 0)
                {
                    text2 = text2 + unit;
                }
                text3 = text2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string BuildGeneralNumberString(decimal num)
        {
            if (MathRule.CheckDecimalEqual(0M, num))
            {
                return "";
            }
            return num.ToString("F");
        }

        public static string BuildGeneralNumberString(object oNum)
        {
            if (oNum == DBNull.Value)
            {
                return "";
            }
            decimal num = Convert.ToDecimal(oNum);
            if (MathRule.CheckDecimalEqual(0M, num))
            {
                return "";
            }
            return num.ToString("F");
        }

        public static string BuildMoneyWanFormatString(decimal num)
        {
            return BuildMoneyWanFormatString(num, 0);
        }

        public static string BuildMoneyWanFormatString(object oNum)
        {
            if (oNum == DBNull.Value)
            {
                return "";
            }
            if (oNum == null)
            {
                return "";
            }
            return BuildMoneyWanFormatString(Convert.ToDecimal(oNum));
        }

        public static string BuildMoneyWanFormatString(decimal num, int flag)
        {
            return BuildMoneyWanFormatString(num, flag, -1);
        }

        public static string BuildMoneyWanFormatString(decimal num, int flag, int decimals)
        {
            decimal d = num / 10000M;
            if (decimals >= 0)
            {
                if (d < 1M)
                {
                    d = MathRule.Round(d, decimals);
                }
                else
                {
                    d = MathRule.Round(d, decimals);
                }
            }
            if (flag == 0)
            {
                if (MathRule.CheckDecimalEqual(0M, d))
                {
                    return "";
                }
            }
            else if ((flag == -1) && MathRule.CheckDecimalEqual(0M, num))
            {
                return "";
            }
            return d.ToString("#,##0.####");
        }

        public static string BuildShowNumberString(decimal num)
        {
            return BuildShowNumberString(num, "");
        }

        public static string BuildShowNumberString(object oNum)
        {
            return BuildShowNumberString(ConvertRule.ToDecimal(oNum));
        }

        public static string BuildShowNumberString(decimal num, string format)
        {
            return BuildShowNumberString(num, format, false);
        }

        public static string BuildShowNumberString(object oNum, string format)
        {
            return BuildShowNumberString(ConvertRule.ToDecimal(oNum), format);
        }

        public static string BuildShowNumberString(decimal num, string format, bool ShowZero)
        {
            if (!(!MathRule.CheckDecimalEqual(0M, num) || ShowZero))
            {
                return "";
            }
            if (format == "")
            {
                return num.ToString("N");
            }
            return num.ToString(format);
        }

        public static string BuildShowPercentString(object oNum)
        {
            return BuildShowPercentString(ConvertRule.ToDecimal(oNum), "");
        }

        public static string BuildShowPercentString(decimal num, string format)
        {
            if (MathRule.CheckDecimalEqual(0M, num))
            {
                return "";
            }
            if (format == "")
            {
                return (num.ToString("N") + "%");
            }
            return (num.ToString(format) + "%");
        }

        public static string BuildShowPercentString(object oNum, string format)
        {
            return BuildShowPercentString(ConvertRule.ToDecimal(oNum), format);
        }

        public static string ConvertCapitalization(string str)
        {
            string text3;
            try
            {
                int length = str.Length;
                string text2 = "";
                for (int i = 1; i <= length; i++)
                {
                    string s = str.Substring(length - i, 1);
                    text2 = cstr[int.Parse(s)] + text2;
                }
                if (str.Length == 2)
                {
                    text2 = text2.Substring(0, text2.Length - 1) + "十" + text2.Substring(text2.Length - 1, 1);
                    if (str.StartsWith("1"))
                    {
                        text2 = text2.Substring(1, text2.Length - 1);
                    }
                }
                if (str == "10")
                {
                    text2 = "十";
                }
                text3 = text2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string ConvertMoneyToCapitalization(decimal Money)
        {
            return ConvertMoneyToCapitalization(Money.ToString());
        }

        public static string ConvertMoneyToCapitalization(int Money)
        {
            return ConvertMoneyToCapitalization(Money.ToString());
        }

        public static string ConvertMoneyToCapitalization(float Money)
        {
            return ConvertMoneyToCapitalization(Money.ToString());
        }

        public static string ConvertMoneyToCapitalization(string Money)
        {
            try
            {
                if (decimal.Parse(Money) > 0M)
                {
                    string text3;
                    string s;
                    string text10;
                    string[] textArray = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
                    string[] textArray2 = new string[] { "", "萬", "億" };
                    string[] textArray3 = new string[] { "", "拾", "佰", "仟" };
                    string[] textArray4 = new string[] { "圆", "角", "分" };
                    char[] separator = ".".ToCharArray();
                    string[] textArray5 = Money.Split(separator);
                    string text2 = textArray5[0];
                    switch (textArray5[1].Length)
                    {
                        case 0:
                            text3 = "";
                            break;

                        case 1:
                        case 2:
                            text3 = textArray5[1];
                            break;

                        default:
                            if (int.Parse(textArray5[1].Substring(2, 1)) >= 5)
                            {
                                text3 = (int.Parse(textArray5[1].Substring(0, 2)) + 1).ToString();
                            }
                            else
                            {
                                text3 = textArray5[1].Substring(0, 2);
                            }
                            break;
                    }
                    string text4 = "";
                    for (int i = 0; i < 3; i++)
                    {
                        int length = (text2.Length > ((i + 1) * 4)) ? 4 : (text2.Length - (4 * i));
                        if (length <= 0)
                        {
                            break;
                        }
                        string text5 = text2.Substring((text2.Length - (4 * i)) - length, length);
                        string text6 = "";
                        for (int j = 0; j < length; j++)
                        {
                            s = text5.Substring((length - j) - 1, 1);
                            text10 = s;
                            if ((text10 != null) && (text10 == "0"))
                            {
                                if (text6 != "")
                                {
                                    text6 = textArray[0] + text6;
                                }
                            }
                            else
                            {
                                text6 = textArray[int.Parse(s)] + textArray3[j] + text6;
                            }
                            text6.Replace("零零", "零");
                            text6.Replace("壹拾", "拾");
                        }
                        if (text6 != "")
                        {
                            text4 = text6 + textArray2[i] + text4;
                        }
                        else if (text4 != "")
                        {
                            text10 = text4.Substring(0, 1);
                            if ((text10 == null) || (text10 != "零"))
                            {
                                text4 = textArray[0] + text4;
                            }
                        }
                        text4.Replace("零零", "零");
                    }
                    if (text4.Substring(0, 1) == "零")
                    {
                        text4 = text4.Substring(1, text4.Length - 1);
                    }
                    text4 = text4 + textArray4[0];
                    string text8 = "";
                    for (int k = 0; k < text3.Length; k++)
                    {
                        s = text3.Substring(k, 1);
                        text10 = s;
                        if ((text10 == null) || (text10 != "0"))
                        {
                            text8 = text8 + textArray[int.Parse(s)] + textArray4[k + 1];
                        }
                    }
                    return (text4 + text8);
                }
                if (decimal.Parse(Money) == 0M)
                {
                    return "零圆";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        public static string CutRepeat(string strTmp)
        {
            if (strTmp.Length < 1)
            {
                return strTmp;
            }
            string text = "";
            string[] textArray = strTmp.Split(new char[] { ',' });
            for (int i = 0; i < textArray.Length; i++)
            {
                if (textArray[i].Length >= 1)
                {
                    bool flag = true;
                    for (int j = i + 1; j < textArray.Length; j++)
                    {
                        if (textArray[i] == textArray[j])
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        text = text + "," + textArray[i];
                    }
                }
            }
            if (text.Length < 1)
            {
                return "";
            }
            return text.Substring(1);
        }

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

        public static string FormartInput(string strInput)
        {
            strInput = strInput.Replace("\n", "<br>");
            strInput = HttpUtility.HtmlEncode(strInput);
            return strInput;
        }

        public static string FormartOutput(string strOutput)
        {
            strOutput = HttpUtility.HtmlDecode(strOutput);
            return strOutput;
        }

        public static string GetDateRangeDesc(string BeginDate, string EndDate)
        {
            string text2;
            try
            {
                string text = "";
                if ((BeginDate != "") && (EndDate != ""))
                {
                    text = string.Format("{0} 至 {1}", BeginDate, EndDate);
                }
                else if ((BeginDate != "") && (EndDate == ""))
                {
                    text = string.Format("{0} 至今", BeginDate);
                }
                else if ((BeginDate == "") && (EndDate != ""))
                {
                    text = string.Format("{0}以前", EndDate);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int GetMonthsBetween(string BeginYm, string EndYm)
        {
            int num3;
            try
            {
                int num = 0;
                if ((BeginYm != "") && (EndYm != ""))
                {
                    DateTime time = new DateTime(int.Parse(BeginYm.Substring(0, 4)), int.Parse(BeginYm.Substring(4, 2)), 1);
                    int num2 = int.Parse(EndYm);
                    while (int.Parse(time.ToString("yyyyMM")) <= num2)
                    {
                        num++;
                        time = time.AddMonths(1);
                    }
                }
                num3 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num3;
        }

        public static string GetNumberRangeDesc(string BeginNumber, string EndNumber)
        {
            string text2;
            try
            {
                string text = "";
                if ((BeginNumber != "") && (EndNumber != ""))
                {
                    text = string.Format("{0} 至 {1}", BeginNumber, EndNumber);
                }
                else if ((BeginNumber != "") && (EndNumber == ""))
                {
                    text = string.Format("大于等于{0}", BeginNumber);
                }
                else if ((BeginNumber == "") && (EndNumber != ""))
                {
                    text = string.Format("小于等于{0}", EndNumber);
                }
                text2 = text;
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
                char[] chArray = s.ToCharArray();
                foreach (char ch in chArray)
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

        public static string ShowDate(DateTime pm_Date)
        {
            return pm_Date.Date.ToString("yyyy-MM-dd");
        }

        public static string ShowDate(object pm_Date)
        {
            try
            {
                if (((DateTime) pm_Date) != DateTime.MinValue)
                {
                    DateTime time = (DateTime) pm_Date;
                    return time.ToString("yyyy-MM-dd");
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public static string ShowDate(object pm_Date, string format)
        {
            try
            {
                if (((DateTime) pm_Date) != DateTime.MinValue)
                {
                    DateTime time = (DateTime) pm_Date;
                    return time.ToString(format);
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public static string TruncateString(object longObject, int keepLength)
        {
            if (longObject == DBNull.Value)
            {
                return "";
            }
            return TruncateString((string) longObject, keepLength);
        }

        public static string TruncateString(string longString, int keepLength)
        {
            if (longString.Length <= keepLength)
            {
                return longString;
            }
            return (longString.Substring(0, keepLength) + "...");
        }

        public static string TruncText(object text, int len)
        {
            string text2 = ConvertRule.ToString(text);
            if ((text2.Length > 0) && (text2.Length > len))
            {
                text2 = text2.Substring(0, len - 1) + "...";
            }
            return text2;
        }

        public static string YmAddMonths(string BeginYm, int MonthCount)
        {
            string text2;
            try
            {
                string text = "";
                if (BeginYm != "")
                {
                    DateTime time = new DateTime(int.Parse(BeginYm.Substring(0, 4)), int.Parse(BeginYm.Substring(4, 2)), 1);
                    text = time.AddMonths(MonthCount).ToString("yyyyMM");
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }
    }
}

