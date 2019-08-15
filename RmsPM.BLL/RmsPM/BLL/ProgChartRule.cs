namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public class ProgChartRule
    {
        public static object GetMaxDate(object[] objs)
        {
            object obj2 = null;
            foreach (object obj3 in objs)
            {
                try
                {
                    if ((obj2 == null) || (obj2 == DBNull.Value))
                    {
                        obj2 = obj3;
                    }
                    else if (((obj3 != null) && (obj3 != DBNull.Value)) && (DateTime.Parse(obj3.ToString()) > DateTime.Parse(obj2.ToString())))
                    {
                        obj2 = obj3;
                    }
                }
                catch
                {
                }
            }
            return obj2;
        }

        public static object GetMinDate(object[] objs)
        {
            object obj2 = null;
            foreach (object obj3 in objs)
            {
                try
                {
                    if ((obj2 == null) || (obj2 == DBNull.Value))
                    {
                        obj2 = obj3;
                    }
                    else if (((obj3 != null) && (obj3 != DBNull.Value)) && (DateTime.Parse(obj3.ToString()) < DateTime.Parse(obj2.ToString())))
                    {
                        obj2 = obj3;
                    }
                }
                catch
                {
                }
            }
            return obj2;
        }

        public static int GetMonthBetween(object obj1, object obj2)
        {
            if (((obj1 != DBNull.Value) && (obj2 != DBNull.Value)) && ((((obj1 != null) && (obj2 != null)) && (obj1 != DBNull.Value)) && (obj2 != DBNull.Value)))
            {
                DateTime time = DateTime.Parse(obj1.ToString());
                DateTime time2 = DateTime.Parse(obj2.ToString());
                return (((time2.Year - time.Year) * 12) + (time2.Month - time.Month));
            }
            return -1;
        }

        public static DataTable GroupByYear(object dMin, object dMax)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable("XYear");
                table.Columns.Add(new DataColumn("sno", typeof(int)));
                table.Columns.Add(new DataColumn("year", typeof(int)));
                table.Columns.Add(new DataColumn("MonthCount", typeof(int)));
                int num = GetMonthBetween(dMin, dMax) + 1;
                if (num > 0)
                {
                    DateTime time = DateTime.Parse(dMin.ToString());
                    for (int i = 0; i < num; i++)
                    {
                        DataRow row;
                        int year = time.AddMonths(i).Year;
                        DataRow[] rowArray = table.Select("year=" + year.ToString());
                        if (rowArray.Length == 0)
                        {
                            row = table.NewRow();
                            row["sno"] = i + 1;
                            row["Year"] = year;
                            row["MonthCount"] = 1;
                            table.Rows.Add(row);
                        }
                        else
                        {
                            row = rowArray[0];
                            row["MonthCount"] = int.Parse(row["MonthCount"].ToString()) + 1;
                        }
                    }
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static int ToColSpan(int c)
        {
            int num = c;
            if (num <= 0)
            {
                num = 1;
            }
            return num;
        }
    }
}

