namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public sealed class MathRule
    {
        private static decimal MaxI = 0.0001M;

        private MathRule()
        {
        }

        public static bool CheckDecimalEqual(decimal p0, decimal p1)
        {
            bool flag = false;
            if (Math.Abs((decimal) (p0 - p1)) < MaxI)
            {
                flag = true;
            }
            return flag;
        }

        public static bool CheckDecimalStringEqual(string p0, string p1)
        {
            bool flag = false;
            try
            {
                if ((p0 == "") && (p1 == ""))
                {
                    return flag;
                }
                decimal num = decimal.Parse(p0);
                decimal num2 = decimal.Parse(p1);
                if (Math.Abs((decimal) (num - num2)) < MaxI)
                {
                    flag = true;
                }
            }
            catch
            {
            }
            return flag;
        }

        public static string GetDecimalNoPointShowString(object val)
        {
            if ((val == DBNull.Value) || (val == null))
            {
                return "";
            }
            decimal num = 0M;
            try
            {
                num = decimal.Parse(val.ToString());
            }
            catch
            {
            }
            if (CheckDecimalEqual(num, 0M))
            {
                return "";
            }
            return num.ToString("N0");
        }

        public static string GetDecimalShowString(object val)
        {
            return GetDecimalShowString(val, "");
        }

        public static string GetDecimalShowString(DataRowView dr, string columnName)
        {
            if (dr[columnName] == DBNull.Value)
            {
                return "";
            }
            decimal num = (decimal) dr[columnName];
            if (CheckDecimalEqual(num, 0M))
            {
                return "";
            }
            return StringRule.BuildShowNumberString(num);
        }

        public static string GetDecimalShowString(object val, string format)
        {
            return GetDecimalShowString(val, format, false);
        }

        public static string GetDecimalShowString(object val, string format, bool ShowZero)
        {
            if ((val == DBNull.Value) || (val == null))
            {
                return "";
            }
            decimal num = 0M;
            try
            {
                num = decimal.Parse(val.ToString());
            }
            catch
            {
            }
            if (!(!CheckDecimalEqual(num, 0M) || ShowZero))
            {
                return "";
            }
            return StringRule.BuildShowNumberString(num, format, ShowZero);
        }

        public static string GetIntShowString(object val)
        {
            if ((val == DBNull.Value) || (val == null))
            {
                return "";
            }
            int num = 0;
            try
            {
                num = int.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
            if (num == 0)
            {
                return "";
            }
            return num.ToString("N0");
        }

        public static int GetMaxInt(DataTable dt, string columnName)
        {
            if (!dt.Columns.Contains(columnName))
            {
                throw new ApplicationException("该表中不包含：" + columnName);
            }
            int num = 0;
            int num2 = 0;
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (!dt.Rows[i].IsNull(columnName))
                {
                    num2 = (int) dt.Rows[i][columnName];
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
            }
            return num;
        }

        public static string GetWanDecimalShowString(object val)
        {
            return GetWanDecimalShowString(val, -1);
        }

        public static string GetWanDecimalShowString(DataRow dr, string columnName)
        {
            if (dr[columnName] == DBNull.Value)
            {
                return "";
            }
            decimal num = (decimal) dr[columnName];
            if (CheckDecimalEqual(num, 0M))
            {
                return "";
            }
            return StringRule.BuildMoneyWanFormatString(num);
        }

        public static string GetWanDecimalShowString(DataRowView dr, string columnName)
        {
            if (dr[columnName] == DBNull.Value)
            {
                return "";
            }
            decimal num = (decimal) dr[columnName];
            if (CheckDecimalEqual(num, 0M))
            {
                return "";
            }
            return StringRule.BuildMoneyWanFormatString(num);
        }

        public static string GetWanDecimalShowString(object val, int decimals)
        {
            if ((val == DBNull.Value) || (val == null))
            {
                return "";
            }
            decimal num = 0M;
            try
            {
                num = decimal.Parse(val.ToString());
            }
            catch
            {
            }
            if (CheckDecimalEqual(num, 0M))
            {
                return "";
            }
            return StringRule.BuildMoneyWanFormatString(num, 0, decimals);
        }

        public static bool IsNum(string str)
        {
            try
            {
                double.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static decimal Round(decimal d)
        {
            decimal num;
            try
            {
                num = Round(d, 0);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static decimal Round(decimal d, int decimals)
        {
            decimal num4;
            try
            {
                decimal num2 = (decimal) Math.Pow(10, (double) decimals);
                double num3 = (d >= 0M) ? 0.5 : -0.5;
                decimal num = ((long) ((d * num2) + ((decimal) num3))) / num2;
                num4 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num4;
        }

        public static decimal[] SumColumn(DataRow[] drs, string[] arrColumnName)
        {
            int index;
            int length = drs.Length;
            int num2 = arrColumnName.Length;
            decimal[] numArray = new decimal[num2];
            for (index = 0; index < num2; index++)
            {
                numArray[index] = 0M;
            }
            for (int i = 0; i < length; i++)
            {
                decimal num5 = 0M;
                if (drs[i].RowState != DataRowState.Deleted)
                {
                    for (index = 0; index < num2; index++)
                    {
                        string columnName = arrColumnName[index];
                        if (!drs[i].IsNull(columnName))
                        {
                            try
                            {
                                num5 = decimal.Parse(drs[i][columnName].ToString());
                            }
                            catch
                            {
                                num5 = 0M;
                            }
                            numArray[index] += num5;
                        }
                    }
                }
            }
            return numArray;
        }

        public static decimal[] SumColumn(DataTable dt, string[] arrColumnName)
        {
            foreach (string text in arrColumnName)
            {
                if (!dt.Columns.Contains(text))
                {
                    throw new ApplicationException(string.Format("该数据表中没有包含数据项“{0}”", text));
                }
            }
            return SumColumn(dt.Select(""), arrColumnName);
        }

        public static decimal SumColumn(DataRow[] drs, string columnName)
        {
            int length = drs.Length;
            decimal num2 = 0M;
            for (int i = 0; i < length; i++)
            {
                decimal num4 = 0M;
                if ((drs[i].RowState != DataRowState.Deleted) && !drs[i].IsNull(columnName))
                {
                    try
                    {
                        num4 = decimal.Parse(drs[i][columnName].ToString());
                    }
                    catch
                    {
                        num4 = 0M;
                    }
                    num2 += num4;
                }
            }
            return num2;
        }

        public static decimal SumColumn(DataTable dt, string columnName)
        {
            if (!dt.Columns.Contains(columnName))
            {
                throw new ApplicationException("该数据表中没有包含这个数据项");
            }
            return SumColumn(dt.Select(""), columnName);
        }

        public static decimal[] SumColumn(DataTable dt, string[] arrColumnName, string filter)
        {
            foreach (string text in arrColumnName)
            {
                if (!dt.Columns.Contains(text))
                {
                    throw new ApplicationException(string.Format("该数据表中没有包含数据项“{0}”", text));
                }
            }
            return SumColumn(dt.Select(filter), arrColumnName);
        }

        public static decimal SumColumn(DataTable dt, string columnName, string filter)
        {
            if (!dt.Columns.Contains(columnName))
            {
                throw new ApplicationException("该数据表中没有包含这个数据项");
            }
            return SumColumn(dt.Select(filter), columnName);
        }

        public static int SumIntColumn(DataTable dt, string columnName)
        {
            if (!dt.Columns.Contains(columnName))
            {
                throw new ApplicationException("该数据表中没有包含这个数据项");
            }
            return SumIntColumn(dt.Select(""), columnName);
        }

        public static int SumIntColumn(DataRow[] drs, string columnName)
        {
            int length = drs.Length;
            int num2 = 0;
            for (int i = 0; i < length; i++)
            {
                int num4 = 0;
                if ((drs[i].RowState != DataRowState.Deleted) && !drs[i].IsNull(columnName))
                {
                    num4 = (int) drs[i][columnName];
                    num2 += num4;
                }
            }
            return num2;
        }

        public static int[] SumIntColumn(DataRow[] drs, string[] arrColumnName)
        {
            int index;
            int length = drs.Length;
            int num2 = arrColumnName.Length;
            int[] numArray = new int[num2];
            for (index = 0; index < num2; index++)
            {
                numArray[index] = 0;
            }
            for (int i = 0; i < length; i++)
            {
                int num5 = 0;
                if (drs[i].RowState != DataRowState.Deleted)
                {
                    for (index = 0; index < num2; index++)
                    {
                        string columnName = arrColumnName[index];
                        if (!drs[i].IsNull(columnName))
                        {
                            num5 = (int) drs[i][columnName];
                            numArray[index] += num5;
                        }
                    }
                }
            }
            return numArray;
        }

        public static int[] SumIntColumn(DataTable dt, string[] arrColumnName)
        {
            foreach (string text in arrColumnName)
            {
                if (!dt.Columns.Contains(text))
                {
                    throw new ApplicationException(string.Format("该数据表中没有包含数据项“{0}”", text));
                }
            }
            return SumIntColumn(dt.Select(""), arrColumnName);
        }

        public static int[] SumIntColumn(DataTable dt, string[] arrColumnName, string filter)
        {
            foreach (string text in arrColumnName)
            {
                if (!dt.Columns.Contains(text))
                {
                    throw new ApplicationException(string.Format("该数据表中没有包含数据项“{0}”", text));
                }
            }
            return SumIntColumn(dt.Select(filter), arrColumnName);
        }

        public static int SumIntColumn(DataTable dt, string columnName, string filter)
        {
            if (!dt.Columns.Contains(columnName))
            {
                throw new ApplicationException("该数据表中没有包含这个数据项");
            }
            return SumIntColumn(dt.Select(filter), columnName);
        }
    }
}

