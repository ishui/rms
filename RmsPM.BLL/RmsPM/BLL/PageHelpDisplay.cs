namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class PageHelpDisplay
    {
        public static string ChangeMessageForDisplay(string message, string formatStr, char splitChar)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            string[] textArray = message.Split(new char[] { splitChar });
            foreach (string text in textArray)
            {
                builder.Append(string.Format("{0}{1}", text, formatStr));
            }
            int startIndex = builder.Length - formatStr.Length;
            return builder.Remove(startIndex, formatStr.Length).ToString();
        }

        public static List<string> GetYearRangeForSelect(int? startYear, int? endYear)
        {
            int? nullable;
            int? nullable2;
            int? nullable3;
            int? nullable4;
            List<string> list = new List<string>();
            if (!startYear.HasValue && !endYear.HasValue)
            {
                for (int i = 0x7d0; i <= 0x7e4; i++)
                {
                    list.Add(string.Format("{0}", i));
                }
                return list;
            }
            if (startYear.HasValue)
            {
                if (endYear.HasValue)
                {
                    nullable = startYear;
                    while (true)
                    {
                        nullable2 = nullable;
                        nullable3 = endYear;
                        if ((nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault()) || !(nullable2.HasValue & nullable3.HasValue))
                        {
                            return list;
                        }
                        list.Add(string.Format("{0}", nullable));
                        nullable += 1;
                    }
                }
                nullable = startYear;
                while (true)
                {
                    nullable2 = nullable;
                    nullable3 = startYear;
                    nullable3 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 20) : ((int?) (nullable4 = null));
                    if ((nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault()) || !(nullable2.HasValue & nullable3.HasValue))
                    {
                        return list;
                    }
                    list.Add(string.Format("{0}", nullable));
                    nullable += 1;
                }
            }
            nullable = endYear;
        Label_00B2:
            nullable2 = nullable;
            nullable3 = endYear;
            nullable3 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() - 20) : ((int?) (nullable4 = null));
            if ((nullable2.GetValueOrDefault() <= nullable3.GetValueOrDefault()) && (nullable2.HasValue & nullable3.HasValue))
            {
                list.Add(string.Format("{0}", nullable));
                nullable -= 1;
                goto Label_00B2;
            }
            return list;
        }
    }
}

