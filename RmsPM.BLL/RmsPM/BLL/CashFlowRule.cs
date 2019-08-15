namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CashFlowRule
    {
        
        
        public static CashFlowSource m_SourceFact = new CashFlowSource("Fact", "实际已批", Color.SpringGreen);
        public static CashFlowSource m_SourceFactI = new CashFlowSource("FactI", "实际收入", Color.Blue);
        public static CashFlowSource m_SourceFactO = new CashFlowSource("FactO", "实际支出", Color.Blue);
        public static CashFlowSource m_SourceFactPay = new CashFlowSource("FactPay", "实际已付", Color.Blue);
        public static CashFlowSource m_SourcePlan = new CashFlowSource("Plan", "计划基准", Color.Red);
        public static CashFlowSource m_SourcePlanI = new CashFlowSource("PlanI", "计划收入", Color.Red);
        public static CashFlowSource m_SourcePlanO = new CashFlowSource("PlanO", "计划支出", Color.Red);

        private static CashFlowSource[] m_arrSource = new CashFlowSource[] { m_SourcePlan, m_SourceFact, m_SourceFactPay, m_SourcePlanI, m_SourceFactI, m_SourcePlanO, m_SourceFactO };
        
        private static decimal CalcPercent(object objVal1, object objVal2, int dec)
        {
            decimal num4;
            try
            {
                decimal num = 0M;
                decimal num2 = ConvertRule.ToDecimal(objVal1);
                decimal num3 = ConvertRule.ToDecimal(objVal2);
                if (num3 != 0M)
                {
                    num = Math.Round((decimal) ((num2 / num3) * 100M), dec);
                }
                num4 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num4;
        }

        private static void CalcRptCostListPercent(DataTable tb)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    int dec = 2;
                    decimal num2 = ConvertRule.ToDecimal(row["CurrAct"]) - ConvertRule.ToDecimal(row["CurrPlan"]);
                    row["CurrPercent"] = CalcPercent(num2, row["CurrPlan"], dec);
                    num2 = ConvertRule.ToDecimal(row["CurrYAct"]) - ConvertRule.ToDecimal(row["CurrYPlan"]);
                    row["CurrYPercent"] = CalcPercent(num2, row["CurrYPlan"], dec);
                    num2 = ConvertRule.ToDecimal(row["ProjectAct"]) - ConvertRule.ToDecimal(row["ProjectPlan"]);
                    row["ProjectPercent"] = CalcPercent(num2, row["ProjectPlan"], dec);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CalcRptSalListPercent(DataTable tb)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    int dec = 2;
                    decimal num2 = ConvertRule.ToDecimal(row["CurrAct"]) - ConvertRule.ToDecimal(row["CurrPlan"]);
                    row["CurrPercent"] = CalcPercent(num2, row["CurrPlan"], dec);
                    num2 = ConvertRule.ToDecimal(row["CurrYAct"]) - ConvertRule.ToDecimal(row["CurrYPlan"]);
                    row["CurrYPercent"] = CalcPercent(num2, row["CurrYPlan"], dec);
                    num2 = ConvertRule.ToDecimal(row["ProjectAct"]) - ConvertRule.ToDecimal(row["ProjectPlan"]);
                    row["ProjectPercent"] = CalcPercent(num2, row["ProjectPlan"], dec);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CashFlowTotalAddRow(DataTable tb, CashFlowSource source, string ProjectCode, int y_begin, int y_end, string MonthType)
        {
            try
            {
                DataRow drDst = tb.NewRow();
                drDst["id"] = source.Id;
                drDst["name"] = source.Desc;
                DataTable tbSrc = GenerateCashFlowList(ProjectCode, y_begin, y_end, MonthType, source.Id).Tables["CashFlowTotal"];
                ConvertRule.DataRowCopy(tbSrc.Rows[0], drDst, tbSrc, tb);
                drDst["html"] = string.Format("<span style=\"BORDER: black 1px outset; FONT-SIZE: 1px; WIDTH: 12px; HEIGHT: 12px; BACKGROUND-COLOR: {0}\"></span>", source.Color.Name);
                tb.Rows.Add(drDst);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CashFlowTotalAddRow(DataTable tb, CashFlowSource source, string ProjectCode, string BeginDate, string EndDate, string MonthType)
        {
            try
            {
                DataRow drDst = tb.NewRow();
                drDst["id"] = source.Id;
                drDst["name"] = source.Desc;
                DataTable tbSrc = GenerateCashFlowList(ProjectCode, BeginDate, EndDate, MonthType, source.Id).Tables["CashFlowTotal"];
                ConvertRule.DataRowCopy(tbSrc.Rows[0], drDst, tbSrc, tb);
                drDst["html"] = string.Format("<span style=\"BORDER: black 1px outset; FONT-SIZE: 1px; WIDTH: 12px; HEIGHT: 12px; BACKGROUND-COLOR: {0}\"></span>", source.Color.Name);
                tb.Rows.Add(drDst);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static DataTable CreateCashFlowInTable(int y_begin, int y_end, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable("CashFlowI");
                tb.Columns.Add("sno");
                tb.Columns.Add(new DataColumn("PBSTypeCode", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeName", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeSortID", typeof(int)));
                tb.Columns.Add(new DataColumn("PBSTypeFullID", typeof(string)));
                tb.Columns.Add(new DataColumn("Deep", typeof(int)));
                tb.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                CreateColumnByMonthType(tb, y_begin, y_end, MonthType);
                tb.Columns.Add("TotalMoney", typeof(decimal));
                tb.Columns.Add("MoneyHtml");
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static DataTable CreateCashFlowInTable(string BeginDate, string EndDate, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable("CashFlowI");
                tb.Columns.Add("sno");
                tb.Columns.Add(new DataColumn("PBSTypeCode", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeName", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeSortID", typeof(int)));
                tb.Columns.Add(new DataColumn("PBSTypeFullID", typeof(string)));
                tb.Columns.Add(new DataColumn("Deep", typeof(int)));
                tb.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                CreateColumnByMonthType(tb, BeginDate, EndDate, MonthType);
                tb.Columns.Add("TotalMoney", typeof(decimal));
                tb.Columns.Add("MoneyHtml");
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static DataTable CreateCashFlowOutTable(int y_begin, int y_end, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable("CashFlowO");
                tb.Columns.Add("sno");
                tb.Columns.Add(new DataColumn("CostCode", typeof(string)));
                tb.Columns.Add(new DataColumn("CostName", typeof(string)));
                tb.Columns.Add(new DataColumn("CostSortID", typeof(string)));
                tb.Columns.Add(new DataColumn("CostFullID", typeof(string)));
                tb.Columns.Add(new DataColumn("Deep", typeof(int)));
                tb.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                CreateColumnByMonthType(tb, y_begin, y_end, MonthType);
                tb.Columns.Add("TotalMoney", typeof(decimal));
                tb.Columns.Add("MoneyHtml");
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static DataTable CreateCashFlowOutTable(string BeginDate, string EndDate, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable("CashFlowO");
                tb.Columns.Add("sno");
                tb.Columns.Add(new DataColumn("CostCode", typeof(string)));
                tb.Columns.Add(new DataColumn("CostName", typeof(string)));
                tb.Columns.Add(new DataColumn("CostSortID", typeof(string)));
                tb.Columns.Add(new DataColumn("CostFullID", typeof(string)));
                tb.Columns.Add(new DataColumn("Deep", typeof(int)));
                tb.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                CreateColumnByMonthType(tb, BeginDate, EndDate, MonthType);
                tb.Columns.Add("TotalMoney", typeof(decimal));
                tb.Columns.Add("MoneyHtml");
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static DataTable CreateCashFlowTotalTable(int y_begin, int y_end, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("id");
                tb.Columns.Add("name");
                tb.Columns.Add("html");
                tb.Columns.Add("MoneyHtml");
                CreateColumnByMonthType(tb, y_begin, y_end, MonthType);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static DataTable CreateCashFlowTotalTable(string BeginDate, string EndDate, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("id");
                tb.Columns.Add("name");
                tb.Columns.Add("html");
                tb.Columns.Add("MoneyHtml");
                CreateColumnByMonthType(tb, BeginDate, EndDate, MonthType);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static void CreateColumnByDay(DataTable tb, string BeginDate, string EndDate)
        {
            try
            {
                string beginYmd = BeginDate.Replace("-", "");
                string endYmd = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginYmd.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endYmd.Substring(0, 4));
                int num3 = ConvertRule.ToInt(beginYmd.Substring(0, 6));
                int monthCount = GetMonthCount(num3.ToString(), ConvertRule.ToInt(endYmd.Substring(0, 6)).ToString());
                string currentYm = num3.ToString();
                for (int i = 0; i < monthCount; i++)
                {
                    int y = ConvertRule.ToInt(currentYm.Substring(0, 4));
                    int m = ConvertRule.ToInt(currentYm.Substring(4, 2));
                    int iBeginD = 0;
                    int iEndD = 0;
                    GetCurrentDayRange(currentYm, beginYmd, endYmd, ref iBeginD, ref iEndD);
                    for (int j = iBeginD; j <= iEndD; j++)
                    {
                        string columnName = GetFieldName(y, m, j);
                        tb.Columns.Add(columnName, typeof(decimal));
                    }
                    currentYm = GetNextMonth(currentYm, 1);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CreateColumnByMonth(DataTable tb, int y_begin, int y_end)
        {
            try
            {
                for (int i = y_begin; i <= y_end; i++)
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        string columnName = i.ToString().Substring(2) + "-" + j.ToString();
                        tb.Columns.Add(columnName, typeof(decimal));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CreateColumnByMonth(DataTable tb, string BeginDate, string EndDate)
        {
            try
            {
                string beginDate = BeginDate.Replace("-", "");
                string endDate = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endDate.Substring(0, 4));
                for (int i = num; i <= num2; i++)
                {
                    int iBeginM = 0;
                    int iEndM = 0;
                    GetCurrentYearMonthRange(i, beginDate, endDate, ref iBeginM, ref iEndM);
                    for (int j = iBeginM; j <= iEndM; j++)
                    {
                        string columnName = i.ToString().Substring(2) + "-" + j.ToString();
                        tb.Columns.Add(columnName, typeof(decimal));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CreateColumnByMonthType(DataTable tb, int y_begin, int y_end, string MonthType)
        {
            try
            {
                switch (MonthType.ToLower())
                {
                    case "q":
                        CreateColumnByQuoater(tb, y_begin, y_end);
                        break;

                    case "m":
                        goto Label_0033;
                }
                return;
            Label_0033:
                CreateColumnByMonth(tb, y_begin, y_end);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CreateColumnByMonthType(DataTable tb, string BeginDate, string EndDate, string MonthType)
        {
            try
            {
                string text = MonthType.ToLower();
                if (text != null)
                {
                    if (text != "q")
                    {
                        if (text == "m")
                        {
                            goto Label_0040;
                        }
                        if (text == "d")
                        {
                            goto Label_004B;
                        }
                    }
                    else
                    {
                        CreateColumnByQuoater(tb, BeginDate, EndDate);
                    }
                }
                return;
            Label_0040:
                CreateColumnByMonth(tb, BeginDate, EndDate);
                return;
            Label_004B:
                CreateColumnByDay(tb, BeginDate, EndDate);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CreateColumnByQuoater(DataTable tb, int y_begin, int y_end)
        {
            try
            {
                for (int i = y_begin; i <= y_end; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        string columnName = i.ToString().Substring(2) + "-" + j.ToString();
                        tb.Columns.Add(columnName, typeof(decimal));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void CreateColumnByQuoater(DataTable tb, string BeginDate, string EndDate)
        {
            try
            {
                string beginDate = BeginDate.Replace("-", "");
                string endDate = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endDate.Substring(0, 4));
                for (int i = num; i <= num2; i++)
                {
                    int iBeginQ = 0;
                    int iEndQ = 0;
                    GetCurrentYearQuarterRange(i, beginDate, endDate, ref iBeginQ, ref iEndQ);
                    for (int j = iBeginQ; j <= iEndQ; j++)
                    {
                        string columnName = i.ToString().Substring(2) + "-" + j.ToString();
                        tb.Columns.Add(columnName, typeof(decimal));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static DataTable CreateRptCostListTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("sno", typeof(int)));
                table.Columns.Add(new DataColumn("ItemName", typeof(string)));
                table.Columns.Add(new DataColumn("FieldName", typeof(string)));
                table.Columns.Add(new DataColumn("Unit", typeof(string)));
                table.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
                table.Columns.Add(new DataColumn("FieldType", typeof(string)));
                table.Columns.Add(new DataColumn("CostCode", typeof(string)));
                table.Columns.Add(new DataColumn("CostName", typeof(string)));
                table.Columns.Add(new DataColumn("CostSortID", typeof(string)));
                table.Columns.Add(new DataColumn("CostFullID", typeof(string)));
                table.Columns.Add(new DataColumn("Deep", typeof(int)));
                table.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                table.Columns.Add(new DataColumn("CurrAct", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrPlan", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrPercent", typeof(decimal)));
                table.Columns.Add(new DataColumn("AfterPlanM1", typeof(decimal)));
                table.Columns.Add(new DataColumn("AfterPlanM3", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrYAct", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrYPlan", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrYPercent", typeof(decimal)));
                table.Columns.Add(new DataColumn("ProjectAct", typeof(decimal)));
                table.Columns.Add(new DataColumn("ProjectPlan", typeof(decimal)));
                table.Columns.Add(new DataColumn("ProjectPercent", typeof(decimal)));
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static DataTable CreateRptSalListTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("sno", typeof(int)));
                table.Columns.Add(new DataColumn("ItemName", typeof(string)));
                table.Columns.Add(new DataColumn("FieldName", typeof(string)));
                table.Columns.Add(new DataColumn("Unit", typeof(string)));
                table.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
                table.Columns.Add(new DataColumn("FieldType", typeof(string)));
                table.Columns.Add(new DataColumn("PBSTypeCode", typeof(string)));
                table.Columns.Add(new DataColumn("PBSTypeName", typeof(string)));
                table.Columns.Add(new DataColumn("PBSTypeSortID", typeof(int)));
                table.Columns.Add(new DataColumn("PBSTypeFullID", typeof(string)));
                table.Columns.Add(new DataColumn("Deep", typeof(int)));
                table.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                table.Columns.Add(new DataColumn("CurrAct", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrPlan", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrPercent", typeof(decimal)));
                table.Columns.Add(new DataColumn("AfterPlanM1", typeof(decimal)));
                table.Columns.Add(new DataColumn("AfterPlanM3", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrYAct", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrYPlan", typeof(decimal)));
                table.Columns.Add(new DataColumn("CurrYPercent", typeof(decimal)));
                table.Columns.Add(new DataColumn("ProjectAct", typeof(decimal)));
                table.Columns.Add(new DataColumn("ProjectPlan", typeof(decimal)));
                table.Columns.Add(new DataColumn("ProjectPercent", typeof(decimal)));
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static void FillCashFlowInList(DataTable tb, string ProjectCode, string ColName, DataTable tbPBSType, DataRow[] drsDtl)
        {
            try
            {
                foreach (DataRow row2 in tbPBSType.Rows)
                {
                    DataRow row;
                    string text = ConvertRule.ToString(row2["PBSTypeCode"]);
                    string text2 = ConvertRule.ToString(row2["ParentCode"]);
                    int num = ConvertRule.ToInt(row2["Deep"]);
                    DataRow[] rowArray = tb.Select("PBSTypeCode='" + text + "'");
                    if (rowArray.Length == 0)
                    {
                        row = tb.NewRow();
                        row["PBSTypeCode"] = text;
                        row["PBSTypeName"] = row2["PBSTypeName"];
                        row["PBSTypeSortID"] = row2["SortID"];
                        row["PBSTypeFullID"] = row2["FullID"];
                        row["Deep"] = row2["Deep"];
                        row["ParentCode"] = row2["ParentCode"];
                        tb.Rows.Add(row);
                    }
                    else
                    {
                        row = rowArray[0];
                    }
                    decimal num2 = 0M;
                    foreach (DataRow row3 in drsDtl)
                    {
                        if (ConvertRule.ToString(row3["PBSTypeCode"]) == text)
                        {
                            num2 += ConvertRule.ToDecimal(row3["Money"]);
                        }
                    }
                    row[ColName] = ConvertRule.ToDecimal(row[ColName]) + num2;
                    if (num2 != 0M)
                    {
                        int num3 = num;
                        string text3 = text2;
                        while (num3 > 0)
                        {
                            DataRow[] rowArray2 = tb.Select(string.Format("PBSTypeCode = '{0}'", text3));
                            if (rowArray2.Length > 0)
                            {
                                rowArray2[0][ColName] = ConvertRule.ToDecimal(rowArray2[0][ColName]) + num2;
                                num3 = ConvertRule.ToInt(rowArray2[0]["Deep"]);
                                text3 = ConvertRule.ToString(rowArray2[0]["ParentCode"]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FillCashFlowMoneyHtml(DataRow dr, int y_begin, int y_end, string MonthType, string Format)
        {
            try
            {
                string text = "";
                DataTable table = dr.Table;
                int monthCountByType = GetMonthCountByType(MonthType);
                for (int i = y_begin; i <= y_end; i++)
                {
                    for (int j = 1; j <= monthCountByType; j++)
                    {
                        string fieldName = GetFieldName(i, j);
                        decimal val = ConvertRule.ToDecimal(dr[fieldName]);
                        text = text + string.Format("<td align=\"right\" nowrap>{0}</td>", MathRule.GetDecimalShowString(val, Format));
                    }
                }
                if (table.Columns.Contains("MoneyHtml"))
                {
                    dr["MoneyHtml"] = text;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FillCashFlowMoneyHtml(DataRow dr, string BeginDate, string EndDate, string MonthType, string Format)
        {
            try
            {
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(text.Substring(0, 4));
                int num2 = ConvertRule.ToInt(text2.Substring(0, 4));
                string text3 = "";
                DataTable table = dr.Table;
                foreach (DataColumn column in table.Columns)
                {
                    string decimalShowString;
                    if (column.ColumnName.IndexOf("-") <= 0)
                    {
                        goto Label_02D9;
                    }
                    decimal val = ConvertRule.ToDecimal(dr[column.ColumnName]);
                    string text4 = "";
                    if (table.Columns.Contains("id"))
                    {
                        text4 = dr["id"].ToString();
                    }
                    string s = "";
                    string text6 = "";
                    string text10 = MonthType.ToLower();
                    if (text10 != null)
                    {
                        if (text10 != "q")
                        {
                            if (text10 == "m")
                            {
                                goto Label_01DC;
                            }
                            if (text10 == "d")
                            {
                                goto Label_0229;
                            }
                        }
                        else
                        {
                            string[] textArray = column.ColumnName.Split(new char[] { "-"[0] });
                            int num4 = ConvertRule.ToInt(textArray[1]);
                            int num5 = 1 + ((num4 - 1) * 3);
                            s = "20" + textArray[0] + "-" + num5.ToString() + "-01";
                            text6 = DateTime.Parse(s).AddMonths(3).AddDays(-1).ToString("yyyy-MM-dd");
                        }
                    }
                    goto Label_0238;
                Label_01DC:
                    s = "20" + column.ColumnName + "-01";
                    text6 = DateTime.Parse(s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                    goto Label_0238;
                Label_0229:
                    s = column.ColumnName;
                    text6 = s;
                Label_0238:
                    decimalShowString = MathRule.GetDecimalShowString(val, Format);
                    string text8 = decimalShowString;
                    if (text4.StartsWith("Fact") && (decimalShowString != ""))
                    {
                        text8 = string.Format("<a href=\"#\" onclick=\"OpenMoney('{1}', '{2}', '{3}', '{4}', '{5}');return false;\">{0}</a>", new object[] { decimalShowString, text4, column.ColumnName, MonthType, s, text6 });
                    }
                    string text9 = val.ToString();
                    text3 = text3 + string.Format("<td align=\"right\" nowrap title=\"{1}\">{0}</td>", text8, text9);
                Label_02D9:;
                }
                if (table.Columns.Contains("MoneyHtml"))
                {
                    dr["MoneyHtml"] = text3;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FillCashFlowMoneyHtml(DataTable tb, int y_begin, int y_end, string MonthType, string Format)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    FillCashFlowMoneyHtml(row, y_begin, y_end, MonthType, Format);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FillCashFlowMoneyHtml(DataTable tb, string BeginDate, string EndDate, string MonthType, string Format)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    FillCashFlowMoneyHtml(row, BeginDate, EndDate, MonthType, Format);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillCashFlowOutList(DataTable tb, string ProjectCode, string ColName, DataTable tbCbs, DataRow[] drsDtl)
        {
            try
            {
                foreach (DataRow row2 in tbCbs.Rows)
                {
                    DataRow row;
                    string text = ConvertRule.ToString(row2["CostCode"]);
                    string text2 = ConvertRule.ToString(row2["ParentCode"]);
                    int num = ConvertRule.ToInt(row2["Deep"]);
                    DataRow[] rowArray = tb.Select("CostCode='" + text + "'");
                    if (rowArray.Length == 0)
                    {
                        row = tb.NewRow();
                        row["CostCode"] = text;
                        row["CostName"] = row2["CostName"];
                        row["CostSortID"] = row2["SortID"];
                        row["CostFullID"] = row2["FullCode"];
                        row["Deep"] = row2["Deep"];
                        row["ParentCode"] = row2["ParentCode"];
                        tb.Rows.Add(row);
                    }
                    else
                    {
                        row = rowArray[0];
                    }
                    decimal num2 = 0M;
                    foreach (DataRow row3 in drsDtl)
                    {
                        if (ConvertRule.ToString(row3["CostCode"]) == text)
                        {
                            num2 += ConvertRule.ToDecimal(row3["Money"]);
                        }
                    }
                    row[ColName] = ConvertRule.ToDecimal(row[ColName]) + num2;
                    if (num2 != 0M)
                    {
                        int num3 = num;
                        string text3 = text2;
                        while (num3 > 0)
                        {
                            DataRow[] rowArray2 = tb.Select(string.Format("CostCode = '{0}'", text3));
                            if (rowArray2.Length > 0)
                            {
                                rowArray2[0][ColName] = ConvertRule.ToDecimal(rowArray2[0][ColName]) + num2;
                                num3 = ConvertRule.ToInt(rowArray2[0]["Deep"]);
                                text3 = ConvertRule.ToString(rowArray2[0]["ParentCode"]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillCashFlowTotalMoney(DataRow dr, int y_begin, int y_end, string MonthType)
        {
            try
            {
                decimal num = 0M;
                string text = "";
                DataTable table = dr.Table;
                int monthCountByType = GetMonthCountByType(MonthType);
                for (int i = y_begin; i <= y_end; i++)
                {
                    for (int j = 1; j <= monthCountByType; j++)
                    {
                        string fieldName = GetFieldName(i, j);
                        decimal objVal = ConvertRule.ToDecimal(dr[fieldName]);
                        num += objVal;
                        text = text + string.Format("<td align=\"right\" nowrap>{0}</td>", FormatSalListValue(objVal, ""));
                    }
                }
                if (table.Columns.Contains("TotalMoney"))
                {
                    dr["TotalMoney"] = num;
                }
                if (table.Columns.Contains("MoneyHtml"))
                {
                    dr["MoneyHtml"] = text;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillCashFlowTotalMoney(DataRow dr, string BeginDate, string EndDate, string MonthType)
        {
            try
            {
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(text.Substring(0, 4));
                int num2 = ConvertRule.ToInt(text2.Substring(0, 4));
                decimal num3 = 0M;
                string text3 = "";
                DataTable table = dr.Table;
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName.IndexOf("-") > 0)
                    {
                        decimal objVal = ConvertRule.ToDecimal(dr[column.ColumnName]);
                        num3 += objVal;
                        text3 = text3 + string.Format("<td align=\"right\" nowrap>{0}</td>", FormatSalListValue(objVal, ""));
                    }
                }
                if (table.Columns.Contains("TotalMoney"))
                {
                    dr["TotalMoney"] = num3;
                }
                if (table.Columns.Contains("MoneyHtml"))
                {
                    dr["MoneyHtml"] = text3;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillCashFlowTotalMoney(DataTable tb, int y_begin, int y_end, string MonthType)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    FillCashFlowTotalMoney(row, y_begin, y_end, MonthType);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillCashFlowTotalMoney(DataTable tb, string BeginDate, string EndDate, string MonthType)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    FillCashFlowTotalMoney(row, BeginDate, EndDate, MonthType);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillRptCostList(DataTable tb, string ProjectCode, string ColName, DataTable tbCbs, DataTable tbDtl)
        {
            try
            {
                string[] textArray = new string[] { "成本" };
                string[] textArray2 = new string[] { "万元" };
                string[] textArray3 = new string[] { "decimal" };
                string[] textArray4 = new string[] { "Money" };
                int num = 0;
                for (int i = 0; i < textArray.Length; i++)
                {
                    num++;
                    string text = textArray[i];
                    string text2 = textArray4[i];
                    string text3 = textArray2[i];
                    string text4 = textArray3[i];
                    string text5 = text;
                    if (text3 != "")
                    {
                        text5 = text5 + "<br>(" + text3 + ")";
                    }
                    foreach (DataRow row2 in tbCbs.Rows)
                    {
                        DataRow row;
                        string text6 = ConvertRule.ToString(row2["CostCode"]);
                        string text7 = ConvertRule.ToString(row2["ParentCode"]);
                        int num3 = ConvertRule.ToInt(row2["Deep"]);
                        DataRow[] rowArray = tb.Select("FieldName='" + text2 + "' and CostCode='" + text6 + "'");
                        if (rowArray.Length == 0)
                        {
                            row = tb.NewRow();
                            row["sno"] = num;
                            row["ItemName"] = text;
                            row["FieldName"] = text2;
                            row["Unit"] = text3;
                            row["FieldType"] = text4;
                            row["ItemDesc"] = text5;
                            row["CostCode"] = text6;
                            row["CostName"] = row2["CostName"];
                            row["CostSortID"] = row2["SortID"];
                            row["CostFullID"] = row2["FullCode"];
                            row["Deep"] = row2["Deep"];
                            row["ParentCode"] = row2["ParentCode"];
                            tb.Rows.Add(row);
                        }
                        else
                        {
                            row = rowArray[0];
                        }
                        string text8 = row["ItemName"].ToString();
                        string text9 = row["FieldType"].ToString();
                        decimal val = 0M;
                        DataRow[] rowArray2 = tbDtl.Select("CostCode='" + text6 + "'");
                        if (rowArray2.Length > 0)
                        {
                            foreach (DataRow row3 in rowArray2)
                            {
                                val += ConvertRule.ToDecimal(row3[text2]);
                            }
                        }
                        if (text9 == "int")
                        {
                            row[ColName] = ConvertRule.ToInt(row[ColName]) + ConvertRule.ToInt(val);
                        }
                        else
                        {
                            row[ColName] = ConvertRule.ToDecimal(row[ColName]) + val;
                        }
                        if (val != 0M)
                        {
                            DataRow[] rowArray3;
                            int num5 = num3;
                            for (string text10 = text7; num5 > 0; text10 = ConvertRule.ToString(rowArray3[0]["ParentCode"]))
                            {
                                rowArray3 = tb.Select(string.Format("FieldName = '{0}' and CostCode = '{1}'", text2, text10));
                                if (rowArray3.Length <= 0)
                                {
                                    break;
                                }
                                if (text9 == "int")
                                {
                                    rowArray3[0][ColName] = ConvertRule.ToInt(rowArray3[0][ColName]) + ConvertRule.ToInt(val);
                                }
                                else
                                {
                                    rowArray3[0][ColName] = ConvertRule.ToDecimal(rowArray3[0][ColName]) + val;
                                }
                                num5 = ConvertRule.ToInt(rowArray3[0]["Deep"]);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void FillRptSalList(DataTable tb, string ProjectCode, string ColName, DataTable tbPBSType, DataTable tbDtl)
        {
            try
            {
                string[] textArray = new string[] { "套数", "面积", "均价", "销售额", "回款额" };
                string[] textArray2 = new string[] { "", "平米", "元/平米", "万元", "万元" };
                string[] textArray3 = new string[] { "int", "decimal", "decimal", "decimal", "decimal" };
                string[] textArray4 = new string[] { "HouseCount", "HouseArea", "Price", "Money", "RcvMoney" };
                int num = 0;
                for (int i = 0; i < textArray.Length; i++)
                {
                    num++;
                    string text = textArray[i];
                    string text2 = textArray4[i];
                    string text3 = textArray2[i];
                    string text4 = textArray3[i];
                    string text5 = text;
                    if (text3 != "")
                    {
                        text5 = text5 + "<br>(" + text3 + ")";
                    }
                    foreach (DataRow row2 in tbPBSType.Rows)
                    {
                        DataRow row;
                        string text6 = ConvertRule.ToString(row2["PBSTypeCode"]);
                        string text7 = ConvertRule.ToString(row2["ParentCode"]);
                        int num3 = ConvertRule.ToInt(row2["Deep"]);
                        DataRow[] rowArray = tb.Select("FieldName='" + text2 + "' and PBSTypeCode='" + text6 + "'");
                        if (rowArray.Length == 0)
                        {
                            row = tb.NewRow();
                            row["sno"] = num;
                            row["ItemName"] = text;
                            row["FieldName"] = text2;
                            row["Unit"] = text3;
                            row["FieldType"] = text4;
                            row["ItemDesc"] = text5;
                            row["PBSTypeCode"] = text6;
                            row["PBSTypeName"] = row2["PBSTypeName"];
                            row["PBSTypeSortID"] = row2["SortID"];
                            row["PBSTypeFullID"] = row2["FullID"];
                            row["Deep"] = row2["Deep"];
                            row["ParentCode"] = row2["ParentCode"];
                            tb.Rows.Add(row);
                        }
                        else
                        {
                            row = rowArray[0];
                        }
                        string text8 = row["ItemName"].ToString();
                        string text9 = row["FieldType"].ToString();
                        decimal val = 0M;
                        DataRow[] rowArray2 = tbDtl.Select("PBSTypeCode='" + text6 + "'");
                        if (rowArray2.Length > 0)
                        {
                            foreach (DataRow row3 in rowArray2)
                            {
                                val += ConvertRule.ToDecimal(row3[text2]);
                            }
                        }
                        if (text9 == "int")
                        {
                            row[ColName] = ConvertRule.ToInt(row[ColName]) + ConvertRule.ToInt(val);
                        }
                        else
                        {
                            row[ColName] = ConvertRule.ToDecimal(row[ColName]) + val;
                        }
                        if (val != 0M)
                        {
                            DataRow[] rowArray3;
                            int num5 = num3;
                            for (string text10 = text7; num5 > 0; text10 = ConvertRule.ToString(rowArray3[0]["ParentCode"]))
                            {
                                rowArray3 = tb.Select(string.Format("FieldName = '{0}' and PBSTypeCode = '{1}'", text2, text10));
                                if (rowArray3.Length <= 0)
                                {
                                    break;
                                }
                                if (text9 == "int")
                                {
                                    rowArray3[0][ColName] = ConvertRule.ToInt(rowArray3[0][ColName]) + ConvertRule.ToInt(val);
                                }
                                else
                                {
                                    rowArray3[0][ColName] = ConvertRule.ToDecimal(rowArray3[0][ColName]) + val;
                                }
                                num5 = ConvertRule.ToInt(rowArray3[0]["Deep"]);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static string FormatD(int d)
        {
            string text;
            try
            {
                text = d.ToString().PadLeft(2, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        private static string FormatM(int m)
        {
            string text;
            try
            {
                text = m.ToString().PadLeft(2, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string FormatMoney(object objVal)
        {
            string text = "";
            try
            {
                decimal num = ConvertRule.ToDecimal(objVal);
                if (num != 0M)
                {
                    text = num.ToString();
                }
            }
            catch
            {
            }
            return text;
        }

        public static string FormatMoneyHtml(object objVal)
        {
            string text = "";
            try
            {
                decimal num = ConvertRule.ToDecimal(objVal);
                if (num != 0M)
                {
                    text = num.ToString();
                }
            }
            catch
            {
            }
            if (text == "")
            {
                text = "&nbsp;";
            }
            return text;
        }

        public static string FormatSalListValue(object objVal, string FieldName)
        {
            string text2;
            try
            {
                string intShowString = "";
                if (FieldName.ToUpper() == "HOUSECOUNT")
                {
                    intShowString = MathRule.GetIntShowString(objVal);
                }
                else
                {
                    intShowString = MathRule.GetDecimalShowString(objVal);
                }
                text2 = intShowString;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string FormatY(int y)
        {
            string text;
            try
            {
                text = y.ToString().PadLeft(4, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static DataSet GenerateCashFlowList(string ProjectCode, int y_begin, int y_end, string MonthType, string Source)
        {
            DataSet set2;
            try
            {
                DataSet set = new DataSet();
                DataTable table = GenerateRptCashFlowInList(ProjectCode, y_begin, y_end, MonthType, Source);
                set.Tables.Add(table);
                DataTable table2 = GenerateRptCashFlowOutList(ProjectCode, y_begin, y_end, MonthType, Source);
                set.Tables.Add(table2);
                DataView view = new DataView(table, "Deep = 0", "", DataViewRowState.CurrentRows);
                DataRow drI = view[0].Row;
                DataView view2 = new DataView(table2, "Deep = 0", "", DataViewRowState.CurrentRows);
                DataRow drO = view2[0].Row;
                DataTable table3 = GenerateCashFlowTotalList(drI, drO, y_begin, y_end, MonthType);
                set.Tables.Add(table3);
                set2 = set;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataSet GenerateCashFlowList(string ProjectCode, string BeginDate, string EndDate, string MonthType, string Source)
        {
            DataSet set2;
            try
            {
                DataSet set = new DataSet();
                DataTable table = GenerateRptCashFlowInList(ProjectCode, BeginDate, EndDate, MonthType, Source);
                set.Tables.Add(table);
                DataTable table2 = GenerateRptCashFlowOutList(ProjectCode, BeginDate, EndDate, MonthType, Source);
                set.Tables.Add(table2);
                DataView view = new DataView(table, "Deep = 0", "", DataViewRowState.CurrentRows);
                DataRow drI = null;
                if (view.Count > 0)
                {
                    drI = view[0].Row;
                }
                DataView view2 = new DataView(table2, "Deep = 0", "", DataViewRowState.CurrentRows);
                DataRow drO = null;
                if (view2.Count > 0)
                {
                    drO = view2[0].Row;
                }
                DataTable table3 = GenerateCashFlowTotalList(drI, drO, BeginDate, EndDate, MonthType);
                set.Tables.Add(table3);
                set2 = set;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataTable GenerateCashFlowTotalList(DataRow drI, DataRow drO, int y_begin, int y_end, string MonthType)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable("CashFlowTotal");
                tb.Columns.Add("sno", typeof(int));
                CreateColumnByMonthType(tb, y_begin, y_end, MonthType);
                tb.Columns.Add("TotalMoney", typeof(decimal));
                tb.Columns.Add("MoneyHtml");
                DataRow row = tb.NewRow();
                row["sno"] = 1;
                tb.Rows.Add(row);
                int monthCountByType = GetMonthCountByType(MonthType);
                for (int i = y_begin; i <= y_end; i++)
                {
                    for (int j = 1; j <= monthCountByType; j++)
                    {
                        string fieldName = GetFieldName(i, j);
                        decimal num4 = 0M;
                        decimal num5 = 0M;
                        if (drI != null)
                        {
                            num4 = ConvertRule.ToDecimal(drI[fieldName]);
                        }
                        if (drO != null)
                        {
                            num5 = ConvertRule.ToDecimal(drO[fieldName]);
                        }
                        decimal d = num4 - num5;
                        if (drI == null)
                        {
                            d = decimal.Negate(d);
                            //d = decimal.op_UnaryNegation(d);
                        }
                        row[fieldName] = d;
                    }
                }
                FillCashFlowTotalMoney(tb, y_begin, y_end, MonthType);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GenerateCashFlowTotalList(DataRow drI, DataRow drO, string BeginDate, string EndDate, string MonthType)
        {
            DataTable table2;
            try
            {
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(text.Substring(0, 4));
                int num2 = ConvertRule.ToInt(text2.Substring(0, 4));
                DataTable tb = new DataTable("CashFlowTotal");
                tb.Columns.Add("sno", typeof(int));
                CreateColumnByMonthType(tb, BeginDate, EndDate, MonthType);
                tb.Columns.Add("TotalMoney", typeof(decimal));
                tb.Columns.Add("MoneyHtml");
                DataRow row = tb.NewRow();
                row["sno"] = 1;
                tb.Rows.Add(row);
                foreach (DataColumn column in tb.Columns)
                {
                    if (column.ColumnName.IndexOf("-") > 0)
                    {
                        decimal num3 = 0M;
                        decimal num4 = 0M;
                        if (drI != null)
                        {
                            num3 = ConvertRule.ToDecimal(drI[column.ColumnName]);
                        }
                        if (drO != null)
                        {
                            num4 = ConvertRule.ToDecimal(drO[column.ColumnName]);
                        }
                        decimal d = num3 - num4;
                        if (drI == null)
                        {
                            d = decimal.Negate(d);
                            //d = decimal.op_UnaryNegation(d);
                        }
                        row[column.ColumnName] = d;
                    }
                }
                FillCashFlowTotalMoney(tb, BeginDate, EndDate, MonthType);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GenerateDateTitleHtml(string StartDate, string EndDate, ref string html_title1, ref string html_title2, ref int DayCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                DayCount = 0;
                string beginYmd = StartDate.Replace("-", "");
                string endYmd = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginYmd.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endYmd.Substring(0, 4));
                int num3 = ConvertRule.ToInt(beginYmd.Substring(0, 6));
                int monthCount = GetMonthCount(num3.ToString(), ConvertRule.ToInt(endYmd.Substring(0, 6)).ToString());
                string currentYm = num3.ToString();
                for (int i = 0; i < monthCount; i++)
                {
                    int iBeginD = 0;
                    int iEndD = 0;
                    GetCurrentDayRange(currentYm, beginYmd, endYmd, ref iBeginD, ref iEndD);
                    html_title1 = html_title1 + string.Format("<th colspan={1} align=center nowrap>{0}</th>", currentYm.Substring(0, 4) + "-" + currentYm.Substring(4, 2), (iEndD - iBeginD) + 1);
                    for (int j = iBeginD; j <= iEndD; j++)
                    {
                        DayCount++;
                        html_title2 = html_title2 + string.Format("<th align=center nowrap>{0}</th>", j);
                    }
                    currentYm = GetNextMonth(currentYm, 1);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GenerateRptCashFlowInList(string ProjectCode, int y_begin, int y_end, string MonthType, string Source)
        {
            DataTable table4;
            try
            {
                DataTable tb = CreateCashFlowInTable(y_begin, y_end, MonthType);
                DataTable tbPBSType = GetSalPBSType(true);
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString;
                    DataTable table3 = null;
                    string text4 = Source;
                    if (text4 != null)
                    {
                        if (text4 != "Plan")
                        {
                            if ((text4 == "Fact") || (text4 == "FactPay"))
                            {
                                goto Label_00BD;
                            }
                            if (text4 != "PlanI")
                            {
                                if (text4 == "FactI")
                                {
                                    goto Label_00BD;
                                }
                                goto Label_0114;
                            }
                        }
                        queryString = "select * from RptFinIn where 1 = 1 and 1 = 2";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + string.Format(" and ProjectCode = '{0}'", ProjectCode);
                        }
                        table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    }
                    goto Label_0114;
                Label_00BD:
                    queryString = "select Year(a.PayDate) as IYear, Month(a.PayDate) as IMonth, '' as PBSTypeCode, sum(isnull(a.PayMoney, 0)) as Money from SalPay a where 1 = 1";
                    if (ProjectCode != "")
                    {
                        queryString = queryString + " and ProjectCode = '" + ProjectCode + "'";
                    }
                    queryString = queryString + " and a.PayDate is not null group by Year(a.PayDate), Month(a.PayDate)";
                    table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                Label_0114:
                    if (table3 != null)
                    {
                        for (int i = y_begin; i <= y_end; i++)
                        {
                            string colName;
                            DataRow[] drsDtl;
                            switch (MonthType.ToLower())
                            {
                                case "q":
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        colName = GetFieldName(i, j);
                                        string monthInQuoater = GetMonthInQuoater(j);
                                        drsDtl = table3.Select(string.Format("IYear = {0} and IMonth in ({1})", i, monthInQuoater));
                                        FillCashFlowInList(tb, ProjectCode, colName, tbPBSType, drsDtl);
                                    }
                                    break;

                                case "m":
                                    for (int k = 1; k <= 12; k++)
                                    {
                                        colName = GetFieldName(i, k);
                                        drsDtl = table3.Select(string.Format("IYear = {0} and IMonth = {1}", i, k));
                                        FillCashFlowInList(tb, ProjectCode, colName, tbPBSType, drsDtl);
                                    }
                                    goto Label_020D;
                            }
                        Label_020D:;
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                FillCashFlowTotalMoney(tb, y_begin, y_end, MonthType);
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GenerateRptCashFlowInList(string ProjectCode, string BeginDate, string EndDate, string MonthType, string Source)
        {
            DataTable table4;
            try
            {
                string beginDate = BeginDate.Replace("-", "");
                string endDate = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endDate.Substring(0, 4));
                DataTable tb = CreateCashFlowInTable(BeginDate, EndDate, MonthType);
                DataTable tbPBSType = GetSalPBSType(true);
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString;
                    DataTable table3 = null;
                    string text6 = Source;
                    if (text6 != null)
                    {
                        if (text6 != "Plan")
                        {
                            if ((text6 == "Fact") || (text6 == "FactPay"))
                            {
                                goto Label_0157;
                            }
                            if (text6 != "PlanI")
                            {
                                if (text6 == "FactI")
                                {
                                    goto Label_0157;
                                }
                                goto Label_0217;
                            }
                        }
                        if (MonthType.ToLower() == "d")
                        {
                            queryString = "select cast(null as int) as IDay, * from RptFinIn where 1 = 1 and 1 = 2";
                            if (ProjectCode != "")
                            {
                                queryString = queryString + " and ProjectCode = '" + ProjectCode + "'";
                            }
                        }
                        else
                        {
                            queryString = "select * from RptFinIn where 1 = 1";
                            if (ProjectCode != "")
                            {
                                queryString = queryString + " and ProjectCode = '" + ProjectCode + "'";
                            }
                        }
                        table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    }
                    goto Label_0217;
                Label_0157:
                    if (MonthType.ToLower() == "d")
                    {
                        queryString = "select Year(a.PayDate) as IYear, Month(a.PayDate) as IMonth, Day(a.PayDate) as IDay, '' as PBSTypeCode, sum(isnull(a.PayMoney, 0)) as Money from SalPay a where 1 = 1";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.PayDate is not null group by Year(a.PayDate), Month(a.PayDate), Day(a.PayDate)";
                    }
                    else
                    {
                        queryString = "select Year(a.PayDate) as IYear, Month(a.PayDate) as IMonth, '' as PBSTypeCode, sum(isnull(a.PayMoney, 0)) as Money from SalPay a where 1 = 1";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.PayDate is not null group by Year(a.PayDate), Month(a.PayDate)";
                    }
                    table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                Label_0217:
                    if (table3 != null)
                    {
                        int num3;
                        int num4;
                        DataRow[] drsDtl;
                        if (MonthType.ToLower() == "d")
                        {
                            foreach (DataColumn column in tb.Columns)
                            {
                                if (column.ColumnName.IndexOf("-") > 0)
                                {
                                    num3 = ConvertRule.ToInt(column.ColumnName.Substring(0, 4));
                                    num4 = ConvertRule.ToInt(column.ColumnName.Substring(5, 2));
                                    int num5 = ConvertRule.ToInt(column.ColumnName.Substring(8, 2));
                                    drsDtl = table3.Select(string.Format("IYear = {0} and IMonth = {1} and IDay = {2}", num3, num4, num5));
                                    FillCashFlowInList(tb, ProjectCode, column.ColumnName, tbPBSType, drsDtl);
                                }
                            }
                        }
                        else
                        {
                            for (num3 = num; num3 <= num2; num3++)
                            {
                                string colName;
                                switch (MonthType.ToLower())
                                {
                                    case "q":
                                    {
                                        int iBeginQ = 0;
                                        int iEndQ = 0;
                                        GetCurrentYearQuarterRange(num3, beginDate, endDate, ref iBeginQ, ref iEndQ);
                                        for (int i = iBeginQ; i <= iEndQ; i++)
                                        {
                                            colName = GetFieldName(num3, i);
                                            string monthInQuoater = GetMonthInQuoater(i);
                                            drsDtl = table3.Select(string.Format("IYear = {0} and IMonth in ({1})", num3, monthInQuoater));
                                            FillCashFlowInList(tb, ProjectCode, colName, tbPBSType, drsDtl);
                                        }
                                        break;
                                    }
                                    case "m":
                                    {
                                        int iBeginM = 0;
                                        int iEndM = 0;
                                        GetCurrentYearMonthRange(num3, beginDate, endDate, ref iBeginM, ref iEndM);
                                        for (num4 = iBeginM; num4 <= iEndM; num4++)
                                        {
                                            colName = GetFieldName(num3, num4);
                                            drsDtl = table3.Select(string.Format("IYear = {0} and IMonth = {1}", num3, num4));
                                            FillCashFlowInList(tb, ProjectCode, colName, tbPBSType, drsDtl);
                                        }
                                        goto Label_0451;
                                    }
                                }
                            Label_0451:;
                            }
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                FillCashFlowTotalMoney(tb, BeginDate, EndDate, MonthType);
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GenerateRptCashFlowOutList(string ProjectCode, int y_begin, int y_end, string MonthType, string Source)
        {
            DataTable table4;
            try
            {
                DataTable tb = CreateCashFlowOutTable(y_begin, y_end, MonthType);
                DataTable tbCbs = GetCBS(ProjectCode, true);
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString;
                    DataTable table3 = null;
                    string text4 = Source;
                    if (text4 != null)
                    {
                        if (text4 != "Plan")
                        {
                            if (text4 == "Fact")
                            {
                                goto Label_00D0;
                            }
                            if (text4 == "FactPay")
                            {
                                goto Label_011E;
                            }
                            if (text4 != "PlanO")
                            {
                                if (text4 == "FactO")
                                {
                                    goto Label_011E;
                                }
                                goto Label_0173;
                            }
                        }
                        queryString = "select Year(c.PlanningPayDate) as IYear, Month(c.PlanningPayDate) as IMonth, b.CostCode, sum(isnull(c.Money, 0)) as Money from Contract a, ContractCost b, ContractCostPlan c where a.ContractCode = b.ContractCode and b.ContractCostCode = c.ContractCostCode and a.Status in (0, 2)";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.ContractDate is not null group by Year(c.PlanningPayDate), Month(c.PlanningPayDate), b.CostCode";
                        table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    }
                    goto Label_0173;
                Label_00D0:
                    queryString = "select Year(a.PayDate) as IYear, Month(a.PayDate) as IMonth, b.CostCode, sum(isnull(b.ItemMoney, 0)) as Money from Payment a, PaymentItem b where a.PaymentCode = b.PaymentCode and a.Status in (1, 2)";
                    if (ProjectCode != "")
                    {
                        queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                    }
                    queryString = queryString + " and a.PayDate is not null group by Year(a.PayDate), Month(a.PayDate), b.CostCode";
                    table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    goto Label_0173;
                Label_011E:
                    queryString = "select Year(a.PayoutDate) as IYear, Month(a.PayoutDate) as IMonth, pi.CostCode, sum(isnull(b.PayoutMoney, 0)) as Money from Payout a, PayoutItem b, PaymentItem pi where a.PayoutCode = b.PayoutCode and b.PaymentItemCode = pi.PaymentItemCode";
                    if (ProjectCode != "")
                    {
                        queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                    }
                    queryString = queryString + " and a.PayoutDate is not null and a.Status in (1, 2) group by Year(a.PayoutDate), Month(a.PayoutDate), pi.CostCode";
                    table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                Label_0173:
                    if (table3 != null)
                    {
                        for (int i = y_begin; i <= y_end; i++)
                        {
                            string colName;
                            DataRow[] drsDtl;
                            switch (MonthType.ToLower())
                            {
                                case "q":
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        colName = GetFieldName(i, j);
                                        string monthInQuoater = GetMonthInQuoater(j);
                                        drsDtl = table3.Select(string.Format("IYear = {0} and IMonth in ({1})", i, monthInQuoater));
                                        FillCashFlowOutList(tb, ProjectCode, colName, tbCbs, drsDtl);
                                    }
                                    break;

                                case "m":
                                    for (int k = 1; k <= 12; k++)
                                    {
                                        colName = GetFieldName(i, k);
                                        drsDtl = table3.Select(string.Format("IYear = {0} and IMonth = {1}", i, k));
                                        FillCashFlowOutList(tb, ProjectCode, colName, tbCbs, drsDtl);
                                    }
                                    goto Label_026C;
                            }
                        Label_026C:;
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                FillCashFlowTotalMoney(tb, y_begin, y_end, MonthType);
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GenerateRptCashFlowOutList(string ProjectCode, string BeginDate, string EndDate, string MonthType, string Source)
        {
            DataTable table4;
            try
            {
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(text.Substring(0, 4));
                int num2 = ConvertRule.ToInt(text2.Substring(0, 4));
                DataTable tb = CreateCashFlowOutTable(BeginDate, EndDate, MonthType);
                DataTable tbCbs = GetEmptyCBS(true);
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString;
                    DataTable table3 = null;
                    string text6 = Source;
                    if (text6 != null)
                    {
                        if (text6 != "Plan")
                        {
                            if (text6 == "Fact")
                            {
                                goto Label_0173;
                            }
                            if (text6 == "FactPay")
                            {
                                goto Label_0231;
                            }
                            if (text6 != "PlanO")
                            {
                                if (text6 == "FactO")
                                {
                                    goto Label_0231;
                                }
                                goto Label_02F6;
                            }
                        }
                        if (MonthType.ToLower() == "d")
                        {
                            queryString = "select Year(c.PlanningPayDate) as IYear, Month(c.PlanningPayDate) as IMonth, Day(c.PlanningPayDate) as IDay, '' as CostCode, sum(isnull(c.Money, 0)) as Money from Contract a, ContractCost b, ContractCostPlan c where a.ContractCode = b.ContractCode and b.ContractCostCode = c.ContractCostCode and a.Status in (0, 2)";
                            if (ProjectCode != "")
                            {
                                queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                            }
                            queryString = queryString + " and a.ContractDate is not null group by Year(c.PlanningPayDate), Month(c.PlanningPayDate), Day(c.PlanningPayDate), b.CostCode";
                        }
                        else
                        {
                            queryString = "select Year(c.PlanningPayDate) as IYear, Month(c.PlanningPayDate) as IMonth, '' as CostCode, sum(isnull(c.Money, 0)) as Money from Contract a, ContractCost b, ContractCostPlan c where a.ContractCode = b.ContractCode and b.ContractCostCode = c.ContractCostCode and a.Status in (0, 2)";
                            if (ProjectCode != "")
                            {
                                queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                            }
                            queryString = queryString + " and a.ContractDate is not null group by Year(c.PlanningPayDate), Month(c.PlanningPayDate), b.CostCode";
                        }
                        table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    }
                    goto Label_02F6;
                Label_0173:
                    if (MonthType.ToLower() == "d")
                    {
                        queryString = "select Year(a.PayDate) as IYear, Month(a.PayDate) as IMonth, Day(a.PayDate) as IDay, '' as CostCode, sum(isnull(b.ItemMoney, 0)) as Money from Payment a, PaymentItem b where a.PaymentCode = b.PaymentCode and a.Status in (1, 2)";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.PayDate is not null group by Year(a.PayDate), Month(a.PayDate), Day(a.PayDate), b.CostCode";
                    }
                    else
                    {
                        queryString = "select Year(a.PayDate) as IYear, Month(a.PayDate) as IMonth, '' as CostCode, sum(isnull(b.ItemMoney, 0)) as Money from Payment a, PaymentItem b where a.PaymentCode = b.PaymentCode and a.Status in (1, 2)";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.PayDate is not null group by Year(a.PayDate), Month(a.PayDate), b.CostCode";
                    }
                    queryString = string.Format(queryString, ProjectCode);
                    table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    goto Label_02F6;
                Label_0231:
                    if (MonthType.ToLower() == "d")
                    {
                        queryString = "select Year(a.PayoutDate) as IYear, Month(a.PayoutDate) as IMonth, Day(a.PayoutDate) as IDay, '' as CostCode, sum(isnull(b.PayoutMoney, 0)) as Money from Payout a, PayoutItem b, PaymentItem pi where a.PayoutCode = b.PayoutCode and b.PaymentItemCode = pi.PaymentItemCode";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.PayoutDate is not null and a.Status in (1, 2) group by Year(a.PayoutDate), Month(a.PayoutDate), Day(a.PayoutDate), pi.CostCode";
                    }
                    else
                    {
                        queryString = "select Year(a.PayoutDate) as IYear, Month(a.PayoutDate) as IMonth, '' as CostCode, sum(isnull(b.PayoutMoney, 0)) as Money from Payout a, PayoutItem b, PaymentItem pi where a.PayoutCode = b.PayoutCode and b.PaymentItemCode = pi.PaymentItemCode";
                        if (ProjectCode != "")
                        {
                            queryString = queryString + " and a.ProjectCode = '" + ProjectCode + "'";
                        }
                        queryString = queryString + " and a.PayoutDate is not null and a.Status in (1, 2) group by Year(a.PayoutDate), Month(a.PayoutDate), pi.CostCode";
                    }
                    queryString = string.Format(queryString, ProjectCode);
                    table3 = agent.ExecSqlForDataSet(queryString).Tables[0];
                Label_02F6:
                    if (table3 != null)
                    {
                        int num3;
                        int num4;
                        DataRow[] drsDtl;
                        if (MonthType.ToLower() == "d")
                        {
                            foreach (DataColumn column in tb.Columns)
                            {
                                if (column.ColumnName.IndexOf("-") > 0)
                                {
                                    num3 = ConvertRule.ToInt(column.ColumnName.Substring(0, 4));
                                    num4 = ConvertRule.ToInt(column.ColumnName.Substring(5, 2));
                                    int num5 = ConvertRule.ToInt(column.ColumnName.Substring(8, 2));
                                    drsDtl = table3.Select(string.Format("IYear = {0} and IMonth = {1} and IDay = {2}", num3, num4, num5));
                                    FillCashFlowOutList(tb, ProjectCode, column.ColumnName, tbCbs, drsDtl);
                                }
                            }
                        }
                        else
                        {
                            for (num3 = num; num3 <= num2; num3++)
                            {
                                string name;
                                switch (MonthType.ToLower())
                                {
                                    case "q":
                                        for (int i = 1; i <= 4; i++)
                                        {
                                            name = GetFieldName(num3, i);
                                            if (tb.Columns.Contains(name))
                                            {
                                                string monthInQuoater = GetMonthInQuoater(i);
                                                drsDtl = table3.Select(string.Format("IYear = {0} and IMonth in ({1})", num3, monthInQuoater));
                                                FillCashFlowOutList(tb, ProjectCode, name, tbCbs, drsDtl);
                                            }
                                        }
                                        break;

                                    case "m":
                                        for (num4 = 1; num4 <= 12; num4++)
                                        {
                                            name = GetFieldName(num3, num4);
                                            if (tb.Columns.Contains(name))
                                            {
                                                drsDtl = table3.Select(string.Format("IYear = {0} and IMonth = {1}", num3, num4));
                                                FillCashFlowOutList(tb, ProjectCode, name, tbCbs, drsDtl);
                                            }
                                        }
                                        goto Label_0531;
                                }
                            Label_0531:;
                            }
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                FillCashFlowTotalMoney(tb, BeginDate, EndDate, MonthType);
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GenerateRptCostList(string ProjectCode, int IYear, int IMonth)
        {
            DataTable table4;
            try
            {
                DataTable tb = CreateRptCostListTable();
                DataTable tbCbs = GetCBS(ProjectCode, true);
                QueryAgent agent = new QueryAgent();
                try
                {
                    string text2 = "select * from RptFinOut where 1 = 1";
                    if (ProjectCode != "")
                    {
                        text2 = text2 + string.Format(" and ProjectCode = '{0}'", ProjectCode);
                    }
                    string queryString = string.Format(text2 + "' and IYear = {0} and IMonth = {1} and VerID = -1", IYear, IMonth);
                    DataTable tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "CurrAct", tbCbs, tbDtl);
                    queryString = string.Format(text2 + " and IYear = {0} and IMonth = {1} and VerID = 0", IYear, IMonth);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "CurrPlan", tbCbs, tbDtl);
                    int y = IYear;
                    int m = IMonth;
                    GetNextMonth(ref y, ref m, 1);
                    queryString = string.Format(text2 + " and IYear = {0} and IMonth = {1} and VerID = 0", y, m);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "AfterPlanM1", tbCbs, tbDtl);
                    int num3 = IYear;
                    int num4 = IMonth;
                    GetNextMonth(ref num3, ref num4, 3);
                    queryString = string.Format(text2 + " and cast(IYear as varchar) + right('00' + cast(IMonth as varchar), 2) between '{0}' and '{1}' and VerID = 0", UnionYm(IYear, IMonth), UnionYm(num3, num4));
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "AfterPlanM3", tbCbs, tbDtl);
                    queryString = string.Format(text2 + " and IYear = {0} and VerID = -1", IYear);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "CurrYAct", tbCbs, tbDtl);
                    queryString = string.Format(text2 + " and IYear = {0} and VerID = 0", IYear);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "CurrYPlan", tbCbs, tbDtl);
                    queryString = text2 + " and VerID = -1";
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "ProjectAct", tbCbs, tbDtl);
                    queryString = text2 + " and VerID = 0";
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptCostList(tb, ProjectCode, "ProjectPlan", tbCbs, tbDtl);
                }
                finally
                {
                    agent.Dispose();
                }
                CalcRptCostListPercent(tb);
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GenerateRptCostListCashFlow(string ProjectCode, int IYear, int IMonth)
        {
            DataTable table2;
            try
            {
                DataTable tb = CreateRptCostListTable();
                QueryAgent agent = new QueryAgent();
                try
                {
                    DataRow row;
                    int index;
                    string[] textArray = new string[] { "现金流入", "现金流出" };
                    string[] textArray2 = new string[] { "RptFinIn", "RptFinOut" };
                    string[] textArray3 = new string[] { "Money", "Money" };
                    int num = 0;
                    for (index = 0; index < textArray.Length; index++)
                    {
                        num++;
                        string text = textArray[index];
                        string text2 = textArray2[index];
                        string text3 = textArray3[index];
                        row = tb.NewRow();
                        row["sno"] = num;
                        row["ItemName"] = text;
                        row["ItemDesc"] = text;
                        row["Deep"] = 1;
                        tb.Rows.Add(row);
                        string queryString = string.Format("select sum(isnull({4}, 0)) as Money from {3} where ProjectCode = '{0}' and IYear = {1} and IMonth = {2} and VerID = -1", new object[] { ProjectCode, IYear, IMonth, text2, text3 });
                        decimal num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["CurrAct"] = num3;
                        queryString = string.Format("select sum(isnull({4}, 0)) as Money from {3} where ProjectCode = '{0}' and IYear = {1} and IMonth = {2} and VerID = 0", new object[] { ProjectCode, IYear, IMonth, text2, text3 });
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["CurrPlan"] = num3;
                        int y = IYear;
                        int m = IMonth;
                        GetNextMonth(ref y, ref m, 1);
                        queryString = string.Format("select sum(isnull({4}, 0)) as Money from {3} where ProjectCode = '{0}' and IYear = {1} and IMonth = {2} and VerID = 0", new object[] { ProjectCode, y, m, text2, text3 });
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["AfterPlanM1"] = num3;
                        int num6 = IYear;
                        int num7 = IMonth;
                        GetNextMonth(ref num6, ref num7, 3);
                        queryString = string.Format("select sum(isnull({4}, 0)) as Money from {3} where ProjectCode = '{0}' and cast(IYear as varchar) + right('00' + cast(IMonth as varchar), 2) between '{1}' and '{2}' and VerID = 0", new object[] { ProjectCode, UnionYm(IYear, IMonth), UnionYm(num6, num7), text2, text3 });
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["AfterPlanM3"] = num3;
                        queryString = string.Format("select sum(isnull({3}, 0)) as Money from {2} where ProjectCode = '{0}' and IYear = {1} and VerID = -1", new object[] { ProjectCode, IYear, text2, text3 });
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["CurrYAct"] = num3;
                        queryString = string.Format("select sum(isnull({3}, 0)) as Money from {2} where ProjectCode = '{0}' and IYear = {1} and VerID = 0", new object[] { ProjectCode, IYear, text2, text3 });
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["CurrYPlan"] = num3;
                        queryString = string.Format("select sum(isnull({2}, 0)) as Money from {1} where ProjectCode = '{0}' and VerID = -1", ProjectCode, text2, text3);
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["ProjectAct"] = num3;
                        queryString = string.Format("select sum(isnull({2}, 0)) as Money from {1} where ProjectCode = '{0}' and VerID = 0", ProjectCode, text2, text3);
                        num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        row["ProjectPlan"] = num3;
                    }
                    string[] textArray4 = new string[] { "CurrAct", "CurrPlan", "AfterPlanM1", "AfterPlanM3", "CurrYAct", "CurrYPlan", "ProjectAct", "ProjectPlan" };
                    row = tb.NewRow();
                    row["sno"] = 0;
                    row["ItemName"] = "现金净流量";
                    row["ItemDesc"] = "现金净流量(万元)";
                    row["Deep"] = 0;
                    DataRow row2 = tb.Rows[0];
                    DataRow row3 = tb.Rows[1];
                    for (index = 0; index < textArray4.Length; index++)
                    {
                        row[textArray4[index]] = ConvertRule.ToDecimal(row2[textArray4[index]]) - ConvertRule.ToDecimal(row3[textArray4[index]]);
                    }
                    tb.Rows.InsertAt(row, 0);
                }
                finally
                {
                    agent.Dispose();
                }
                CalcRptCostListPercent(tb);
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GenerateRptSalList(string ProjectCode, int IYear, int IMonth)
        {
            DataTable table4;
            try
            {
                DataTable tb = CreateRptSalListTable();
                DataTable tbPBSType = GetSalPBSType(true);
                QueryAgent agent = new QueryAgent();
                try
                {
                    string text2 = "select * from RptFinIn where 1 = 1";
                    if (ProjectCode != "")
                    {
                        text2 = text2 + string.Format(" and ProjectCode = '{0}'", ProjectCode);
                    }
                    string queryString = string.Format(text2 + " and IYear = {0} and IMonth = {1} and VerID = -1", IYear, IMonth);
                    DataTable tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "CurrAct", tbPBSType, tbDtl);
                    queryString = string.Format(text2 + " and IYear = {0} and IMonth = {1} and VerID = 0", IYear, IMonth);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "CurrPlan", tbPBSType, tbDtl);
                    int y = IYear;
                    int m = IMonth;
                    GetNextMonth(ref y, ref m, 1);
                    queryString = string.Format(text2 + " and IYear = {0} and IMonth = {1} and VerID = 0", y, m);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "AfterPlanM1", tbPBSType, tbDtl);
                    int num3 = IYear;
                    int num4 = IMonth;
                    GetNextMonth(ref num3, ref num4, 3);
                    queryString = string.Format(text2 + " and cast(IYear as varchar) + right('00' + cast(IMonth as varchar), 2) between '{0}' and '{1}' and VerID = 0", UnionYm(IYear, IMonth), UnionYm(num3, num4));
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "AfterPlanM3", tbPBSType, tbDtl);
                    queryString = string.Format(text2 + " and IYear = {0} and VerID = -1", IYear);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "CurrYAct", tbPBSType, tbDtl);
                    queryString = string.Format(text2 + " and IYear = {0} and VerID = 0", IYear);
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "CurrYPlan", tbPBSType, tbDtl);
                    queryString = text2 + " and VerID = -1";
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "ProjectAct", tbPBSType, tbDtl);
                    queryString = text2 + " and VerID = 0";
                    tbDtl = agent.ExecSqlForDataSet(queryString).Tables[0];
                    FillRptSalList(tb, ProjectCode, "ProjectPlan", tbPBSType, tbDtl);
                }
                finally
                {
                    agent.Dispose();
                }
                CalcRptSalListPercent(tb);
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static void GenerateYearMonthTitleHtml(int StartY, int EndY, ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                MonthCount = 0;
                for (int i = StartY; i <= EndY; i++)
                {
                    html_title1 = html_title1 + string.Format("<th colspan=12 align=center nowrap>{0}年</th>", i);
                    for (int j = 1; j <= 12; j++)
                    {
                        MonthCount++;
                        html_title2 = html_title2 + string.Format("<th align=center nowrap>{0}月</th>", j);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GenerateYearMonthTitleHtml(string StartDate, string EndDate, ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                MonthCount = 0;
                string beginDate = StartDate.Replace("-", "");
                string endDate = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endDate.Substring(0, 4));
                for (int i = num; i <= num2; i++)
                {
                    int iBeginM = 0;
                    int iEndM = 0;
                    GetCurrentYearMonthRange(i, beginDate, endDate, ref iBeginM, ref iEndM);
                    html_title1 = html_title1 + string.Format("<th colspan={1} align=center nowrap>{0}年</th>", i, (iEndM - iBeginM) + 1);
                    for (int j = iBeginM; j <= iEndM; j++)
                    {
                        MonthCount++;
                        html_title2 = html_title2 + string.Format("<th align=center nowrap>{0}月</th>", j);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GenerateYearQuarterTitleHtml(int StartY, int EndY, ref string html_title1, ref string html_title2, ref int QuarterCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                QuarterCount = 0;
                for (int i = StartY; i <= EndY; i++)
                {
                    html_title1 = html_title1 + string.Format("<th colspan=4 align=center nowrap>{0}年</th>", i);
                    for (int j = 1; j <= 4; j++)
                    {
                        QuarterCount++;
                        html_title2 = html_title2 + string.Format("<th align=center nowrap>{0}季度</th>", j);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GenerateYearQuarterTitleHtml(string StartDate, string EndDate, ref string html_title1, ref string html_title2, ref int QuarterCount)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                QuarterCount = 0;
                string beginDate = StartDate.Replace("-", "");
                string endDate = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(beginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(endDate.Substring(0, 4));
                for (int i = num; i <= num2; i++)
                {
                    int iBeginQ = 0;
                    int iEndQ = 0;
                    GetCurrentYearQuarterRange(i, beginDate, endDate, ref iBeginQ, ref iEndQ);
                    html_title1 = html_title1 + string.Format("<th colspan={1} align=center nowrap>{0}年</th>", i, (iEndQ - iBeginQ) + 1);
                    for (int j = iBeginQ; j <= iEndQ; j++)
                    {
                        QuarterCount++;
                        html_title2 = html_title2 + string.Format("<th align=center nowrap>{0}季度</th>", j);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GenerateYearTitleHtml(string MonthType, int StartY, int EndY, ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                switch (MonthType.ToLower())
                {
                    case "q":
                        GenerateYearQuarterTitleHtml(StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);
                        break;

                    case "m":
                        goto Label_0037;
                }
                return;
            Label_0037:
                GenerateYearMonthTitleHtml(StartY, EndY, ref html_title1, ref html_title2, ref MonthCount);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GenerateYearTitleHtml(string MonthType, string StartDate, string EndDate, ref string html_title1, ref string html_title2, ref int MonthCount)
        {
            try
            {
                string text = MonthType.ToLower();
                if (text != null)
                {
                    if (text != "q")
                    {
                        if (text == "m")
                        {
                            goto Label_0044;
                        }
                        if (text == "d")
                        {
                            goto Label_0053;
                        }
                    }
                    else
                    {
                        GenerateYearQuarterTitleHtml(StartDate, EndDate, ref html_title1, ref html_title2, ref MonthCount);
                    }
                }
                return;
            Label_0044:
                GenerateYearMonthTitleHtml(StartDate, EndDate, ref html_title1, ref html_title2, ref MonthCount);
                return;
            Label_0053:
                GenerateDateTitleHtml(StartDate, EndDate, ref html_title1, ref html_title2, ref MonthCount);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GetAllDiscountRate()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("DiscountRate", typeof(decimal));
                table.Columns.Add("Name");
                table.Columns.Add("Scale", typeof(decimal));
                table.Rows.Add(new object[] { 1, 0.1, "", 0.0241 });
                table.Rows.Add(new object[] { 1, 0.15, "", 0.0356 });
                table.Rows.Add(new object[] { 1, 0.2, "", 0.0466 });
                table.Rows.Add(new object[] { 1, 0.25, "", 0.0574 });
                table.Rows.Add(new object[] { 1, 0.3, "", 0.0678 });
                table.Rows.Add(new object[] { 1, 0.35, "", 0.0779 });
                table.Rows.Add(new object[] { 1, 0.4, "", 0.0878 });
                foreach (DataRow row in table.Rows)
                {
                    row["Name"] = ((ConvertRule.ToDecimal(row["DiscountRate"]) * 100M)).ToString("0.##") + "%贴现率";
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static CashFlowSource GetCashFlowSourceById(string id)
        {
            try
            {
                foreach (CashFlowSource source in m_arrSource)
                {
                    if (source.Id == id)
                    {
                        return source;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return null;
        }

        public static void GetCashFlowStartEnd(string ProjectCode, ref int StartY, ref int EndY)
        {
            try
            {
                StartY = 0;
                EndY = 0;
                StartY = DateTime.Today.Year;
                EndY = StartY;
                if ((StartY == 0) && (EndY == 0))
                {
                    StartY = DateTime.Today.Year;
                    EndY = StartY;
                }
                else if ((StartY == 0) && (EndY > 0))
                {
                    StartY = EndY;
                }
                else if ((StartY > 0) && (EndY == 0))
                {
                    EndY = StartY;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GetCashFlowTotal(string ProjectCode, int y_begin, int y_end, string MonthType, int IsSum)
        {
            DataTable tb = CreateCashFlowTotalTable(y_begin, y_end, MonthType);
            CashFlowTotalAddRow(tb, m_SourcePlan, ProjectCode, y_begin, y_end, MonthType);
            CashFlowTotalAddRow(tb, m_SourceFact, ProjectCode, y_begin, y_end, MonthType);
            CashFlowTotalAddRow(tb, m_SourceFactPay, ProjectCode, y_begin, y_end, MonthType);
            if (IsSum == 1)
            {
                int monthCountByType = GetMonthCountByType(MonthType);
                foreach (DataRow row in tb.Rows)
                {
                    decimal num2 = 0M;
                    for (int i = y_begin; i <= y_end; i++)
                    {
                        for (int j = 1; j <= monthCountByType; j++)
                        {
                            string fieldName = GetFieldName(i, j);
                            decimal num5 = ConvertRule.ToDecimal(row[fieldName]);
                            num2 += num5;
                            row[fieldName] = num2;
                        }
                    }
                }
            }
            FillCashFlowTotalMoney(tb, y_begin, y_end, MonthType);
            return tb;
        }

        public static DataTable GetCashFlowTotal(string ProjectCode, string BeginDate, string EndDate, string MonthType, int IsSum, string Sources)
        {
            string text = BeginDate.Replace("-", "");
            string text2 = EndDate.Replace("-", "");
            int num = ConvertRule.ToInt(BeginDate.Substring(0, 4));
            int num2 = ConvertRule.ToInt(EndDate.Substring(0, 4));
            DataTable tb = CreateCashFlowTotalTable(BeginDate, EndDate, MonthType);
            string[] textArray = Sources.Split(new char[] { ","[0] });
            foreach (string text3 in textArray)
            {
                switch (text3)
                {
                    case "Plan":
                        CashFlowTotalAddRow(tb, m_SourcePlan, ProjectCode, BeginDate, EndDate, MonthType);
                        break;

                    case "Fact":
                        CashFlowTotalAddRow(tb, m_SourceFact, ProjectCode, BeginDate, EndDate, MonthType);
                        break;

                    case "FactPay":
                        CashFlowTotalAddRow(tb, m_SourceFactPay, ProjectCode, BeginDate, EndDate, MonthType);
                        break;

                    case "PlanI":
                        CashFlowTotalAddRow(tb, m_SourcePlanI, ProjectCode, BeginDate, EndDate, MonthType);
                        break;

                    case "FactI":
                        CashFlowTotalAddRow(tb, m_SourceFactI, ProjectCode, BeginDate, EndDate, MonthType);
                        break;

                    case "PlanO":
                        CashFlowTotalAddRow(tb, m_SourcePlanO, ProjectCode, BeginDate, EndDate, MonthType);
                        break;

                    case "FactO":
                        CashFlowTotalAddRow(tb, m_SourceFactO, ProjectCode, BeginDate, EndDate, MonthType);
                        break;
                }
            }
            if (IsSum == 1)
            {
                int monthCountByType = GetMonthCountByType(MonthType);
                foreach (DataRow row in tb.Rows)
                {
                    decimal num4 = 0M;
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.IndexOf("-") > 0)
                        {
                            decimal num5 = ConvertRule.ToDecimal(row[column.ColumnName]);
                            num4 += num5;
                            row[column.ColumnName] = num4;
                        }
                    }
                }
            }
            FillCashFlowTotalMoney(tb, BeginDate, EndDate, MonthType);
            return tb;
        }

        public static DataTable GetCBS(string ProjectCode, bool IsAddAll)
        {
            DataTable table2;
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select * from cbs where ProjectCode = '{0}' order by FullCode", ProjectCode);
                    DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                    if (IsAddAll)
                    {
                        DataRow row = table.NewRow();
                        row["CostCode"] = "";
                        row["CostName"] = "所有";
                        row["Deep"] = 0;
                        row["ParentCode"] = "";
                        table.Rows.InsertAt(row, 0);
                    }
                    table2 = table;
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GetCurrentDayRange(string CurrentYm, string BeginYmd, string EndYmd, ref int iBeginD, ref int iEndD)
        {
            try
            {
                iBeginD = 1;
                iEndD = 0x1f;
                string text = BeginYmd.Substring(0, 6);
                string text2 = EndYmd.Substring(0, 6);
                if (CurrentYm == text)
                {
                    iBeginD = ConvertRule.ToInt(BeginYmd.Substring(6, 2));
                }
                if (CurrentYm == text2)
                {
                    iEndD = ConvertRule.ToInt(EndYmd.Substring(6, 2));
                }
                else
                {
                    iEndD = DateTime.Parse(CurrentYm.Substring(0, 4) + "-" + CurrentYm.Substring(4, 2) + "-01").AddMonths(1).AddDays(-1).Day;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GetCurrentYearMonthRange(int CurrentY, string BeginDate, string EndDate, ref int iBeginM, ref int iEndM)
        {
            try
            {
                iBeginM = 1;
                iEndM = 12;
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(BeginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(EndDate.Substring(0, 4));
                if (CurrentY == num)
                {
                    iBeginM = ConvertRule.ToInt(text.Substring(4, 2));
                }
                if (CurrentY == num2)
                {
                    iEndM = ConvertRule.ToInt(text2.Substring(4, 2));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GetCurrentYearQuarterRange(int CurrentY, string BeginDate, string EndDate, ref int iBeginQ, ref int iEndQ)
        {
            try
            {
                iBeginQ = 1;
                iEndQ = 4;
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(BeginDate.Substring(0, 4));
                int num2 = ConvertRule.ToInt(EndDate.Substring(0, 4));
                if (CurrentY == num)
                {
                    iBeginQ = GetQuarterByMonth(ConvertRule.ToInt(text.Substring(4, 2)));
                }
                if (CurrentY == num2)
                {
                    iEndQ = GetQuarterByMonth(ConvertRule.ToInt(text2.Substring(4, 2)));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static decimal GetDiscountRateScale(decimal DiscountRate)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                DataRow[] rowArray = GetAllDiscountRate().Select("DiscountRate=" + DiscountRate.ToString());
                if (rowArray.Length > 0)
                {
                    num = ConvertRule.ToDecimal(rowArray[0]["Scale"]);
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static DataTable GetEmptyCBS(bool IsAddAll)
        {
            DataTable table2;
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = "select * from cbs where 1 = 2 order by FullCode";
                    DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                    if (IsAddAll)
                    {
                        DataRow row = table.NewRow();
                        row["CostCode"] = "";
                        row["CostName"] = "所有";
                        row["Deep"] = 0;
                        row["ParentCode"] = "";
                        table.Rows.InsertAt(row, 0);
                    }
                    table2 = table;
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetFieldName(int IYear, int IQuarter)
        {
            string text2;
            try
            {
                text2 = IYear.ToString().Substring(2) + "-" + IQuarter.ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetFieldName(int y, int m, int d)
        {
            string text2;
            try
            {
                text2 = FormatY(y) + "-" + FormatM(m) + "-" + FormatD(d);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetIncomeTotal(string ProjectCode)
        {
            int num = 0x7d2;
            int num2 = 0x7d6;
            DataTable table = CreateCashFlowTotalTable(num, num2, "q");
            table.Rows.Add(new object[] { 
                "1", "实际发生", "<span style=\"BORDER: black 1px outset; FONT-SIZE: 1px; WIDTH: 12px; HEIGHT: 12px; BACKGROUND-COLOR: #0000ff\"></span>", 0x2b, 0x25, 0x23, 0x26, 0x18, 30, 0x21, 0x23, 0x2b, 0x3f, 70, 0x4a, 0x4d, 
                0x45, 0x38, 0x30, 0x37, 0x37, 0x37, 0x37
             });
            table.Rows.Add(new object[] { 
                "2", "基准收益率", "<span style=\"BORDER: black 1px outset; FONT-SIZE: 1px; WIDTH: 12px; HEIGHT: 12px; BACKGROUND-COLOR: red\"></span>", 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 
                0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b, 0x2b
             });
            table.Rows.Add(new object[] { 
                "3", "计划调整", "<span style=\"BORDER: black 1px outset; FONT-SIZE: 1px; WIDTH: 12px; HEIGHT: 12px; BACKGROUND-COLOR: 76d769\"></span>", 0x2b, 0x25, 0x23, 0x26, 0x18, 30, 0x21, 0x23, 0x2b, 0x3f, 70, 0x53, 0x57, 
                0x4d, 0x41, 0x38, 0x40, 0x40, 0x40, 0x40
             });
            return table;
        }

        public static int GetMonthCount(string BeginYm, string EndYm)
        {
            int num7;
            try
            {
                int num = 0;
                int num2 = ConvertRule.ToInt(BeginYm.Substring(0, 4));
                int num3 = ConvertRule.ToInt(EndYm.Substring(0, 4));
                for (int i = num2; i <= num3; i++)
                {
                    int iBeginM = 0;
                    int iEndM = 0;
                    GetCurrentYearMonthRange(i, BeginYm + "01", EndYm + "01", ref iBeginM, ref iEndM);
                    num += (iEndM - iBeginM) + 1;
                }
                num7 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num7;
        }

        public static int GetMonthCountByType(string MonthType)
        {
            try
            {
                switch (MonthType.ToLower())
                {
                    case "q":
                        return 4;

                    case "m":
                        return 12;

                    case "d":
                        return 0x1f;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return 0;
        }

        public static string GetMonthInQuoater(int q)
        {
            string text2;
            try
            {
                string text = "";
                switch (q)
                {
                    case 1:
                        text = "1,2,3";
                        break;

                    case 2:
                        text = "4,5,6";
                        break;

                    case 3:
                        text = "7,8,9";
                        break;

                    case 4:
                        text = "10,11,12";
                        break;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetMonthTypeName(string MonthType)
        {
            try
            {
                switch (MonthType.ToLower())
                {
                    case "q":
                        return "季度";

                    case "m":
                        return "月度";

                    case "d":
                        return "每日";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        public static DataTable GetNetCashFlowTotal(string ProjectCode, int start_y, int end_y, string MonthType, decimal DiscountRate, int IsSum)
        {
            DataTable table2;
            try
            {
                int iYear;
                int iQuarter;
                string fieldName;
                decimal d = GetDiscountRateScale(DiscountRate);
                DataTable table = GetCashFlowTotal(ProjectCode, start_y, end_y, MonthType, 0);
                int monthCountByType = GetMonthCountByType(MonthType);
                foreach (DataRow row in table.Rows)
                {
                    int num3 = -1;
                    for (iYear = start_y; iYear <= end_y; iYear++)
                    {
                        for (iQuarter = 1; iQuarter <= monthCountByType; iQuarter++)
                        {
                            num3++;
                            fieldName = GetFieldName(iYear, iQuarter);
                            if (row[fieldName] == DBNull.Value)
                            {
                                row[fieldName] = DBNull.Value;
                            }
                            else
                            {
                                decimal num7 = Math.Round((decimal) (ConvertRule.ToDecimal(row[fieldName]) * ((decimal) Math.Pow((double) (++d), (double) -num3))), 2);
                                row[fieldName] = num7;
                            }
                        }
                    }
                }
                if (IsSum == 1)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        decimal num8 = 0M;
                        for (iYear = start_y; iYear <= end_y; iYear++)
                        {
                            for (iQuarter = 1; iQuarter <= monthCountByType; iQuarter++)
                            {
                                fieldName = GetFieldName(iYear, iQuarter);
                                decimal num6 = ConvertRule.ToDecimal(row[fieldName]);
                                num8 += num6;
                                row[fieldName] = num8;
                            }
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

        private static string GetNextMonth(string ym, int mNext)
        {
            string text2;
            try
            {
                int y = ConvertRule.ToInt(ym.Substring(0, 4));
                int m = ConvertRule.ToInt(ym.Substring(4, 2));
                GetNextMonth(ref y, ref m, mNext);
                text2 = UnionYm(y, m);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static void GetNextMonth(ref int y, ref int m, int mNext)
        {
            try
            {
                for (int i = 0; i < mNext; i++)
                {
                    m++;
                    if (m == 13)
                    {
                        m = 1;
                        y++;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int GetQuarterByMonth(int m)
        {
            try
            {
                if (m <= 3)
                {
                    return 1;
                }
                if (m <= 6)
                {
                    return 2;
                }
                if (m <= 9)
                {
                    return 3;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return 4;
        }

        public static DataTable GetSalPBSType(bool IsAddAll)
        {
            DataTable table2;
            try
            {
                EntityData pBSTypeByProject = PBSDAO.GetPBSTypeByProject("0");
                DataTable currentTable = pBSTypeByProject.CurrentTable;
                if (IsAddAll)
                {
                    DataRow row = currentTable.NewRow();
                    row["PBSTypeCode"] = "";
                    row["PBSTypeName"] = "所有";
                    row["Deep"] = 0;
                    row["ParentCode"] = "";
                    currentTable.Rows.InsertAt(row, 0);
                }
                pBSTypeByProject.Dispose();
                table2 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void LoadDiscountRateSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                DataTable allDiscountRate = GetAllDiscountRate();
                slt.Items.Clear();
                foreach (DataRow row in allDiscountRate.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["Name"]), ConvertRule.ToString(row["DiscountRate"])));
                }
                if (slt.Items.Count > 0)
                {
                    slt.Items[0].Selected = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MoneyMulti(DataTable tb, decimal scale, int dec, int start_y, int end_y, string MonthType)
        {
            try
            {
                int monthCountByType = GetMonthCountByType(MonthType);
                foreach (DataRow row in tb.Rows)
                {
                    for (int i = start_y; i <= end_y; i++)
                    {
                        for (int j = 1; j <= monthCountByType; j++)
                        {
                            string fieldName = GetFieldName(i, j);
                            decimal num4 = MathRule.Round(ConvertRule.ToDecimal(row[fieldName]) * scale, dec);
                            row[fieldName] = num4;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MoneyMulti(DataTable tb, decimal scale, int dec, string BeginDate, string EndDate, string MonthType)
        {
            try
            {
                string text = BeginDate.Replace("-", "");
                string text2 = EndDate.Replace("-", "");
                int num = ConvertRule.ToInt(text.Substring(0, 4));
                int num2 = ConvertRule.ToInt(text2.Substring(0, 4));
                int monthCountByType = GetMonthCountByType(MonthType);
                foreach (DataRow row in tb.Rows)
                {
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (column.ColumnName.IndexOf("-") > 0)
                        {
                            decimal num4 = MathRule.Round(ConvertRule.ToDecimal(row[column.ColumnName]) * scale, dec);
                            row[column.ColumnName] = num4;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static string UnionYm(int y, int m)
        {
            string text2;
            try
            {
                text2 = y.ToString().PadLeft(4, "0"[0]) + m.ToString().PadLeft(2, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void YuanToMil(DataTable tb, int start_y, int end_y, string MonthType)
        {
            try
            {
                MoneyMulti(tb, 0.000001M, 0, start_y, end_y, MonthType);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void YuanToMil(DataTable tb, string BeginDate, string EndDate, string MonthType, int dec)
        {
            try
            {
                MoneyMulti(tb, 0.000001M, dec, BeginDate, EndDate, MonthType);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void YuanToWan(DataTable tb, int start_y, int end_y, string MonthType)
        {
            try
            {
                MoneyMulti(tb, 0.0001M, 0, start_y, end_y, MonthType);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

