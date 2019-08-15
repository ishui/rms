namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CostBudgetPageRule
    {
        public static string m_ClassTdApportion = "bal";
        public static string m_ClassTdBalance = "bal";
        public static string m_ClassTdCBS = "cbs";
        public static string m_ClassTdContract = "cont";
        public static string m_ClassTdNoContract = "cont";
        public static bool m_IsRoundWanMoney = true;

        public static void AddColumnTargetHis(DataTable tbDtl, EntityData entityTargetHis)
        {
            try
            {
                if (entityTargetHis != null)
                {
                    DataView view = new DataView(entityTargetHis.CurrentTable, "", "VerID desc", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        string text = ConvertRule.ToString(view2.Row["VerID"]);
                        if (text != "")
                        {
                            string name = "BudgetMoneyHis_" + text;
                            if (!tbDtl.Columns.Contains(name))
                            {
                                tbDtl.Columns.Add(name);
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

        public static void AddMonthField(DataTable tb, string aStartY, string aEndY, string FieldName)
        {
            try
            {
                string val = aStartY;
                string text2 = aEndY;
                if ((val != "") && (text2 == ""))
                {
                    text2 = val;
                }
                if ((text2 != "") && (val == ""))
                {
                    val = text2;
                }
                if ((val != "") && (text2 != ""))
                {
                    int num = ConvertRule.ToInt(val);
                    int num2 = ConvertRule.ToInt(text2);
                    for (int i = num; i <= num2; i++)
                    {
                        for (int j = 0; j <= 12; j++)
                        {
                            string text3 = ConvertRule.FormatYYYYMM(i, j);
                            tb.Columns.Add(FieldName + text3, typeof(decimal));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string[] BuildArrayFieldByMonth(string StartY, string EndY, string FieldPre)
        {
            string[] textArray;
            try
            {
                textArray = BuildArrayFieldByMonth(StartY, EndY, 0, 12, FieldPre);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return textArray;
        }

        public static string[] BuildArrayFieldByMonth(string StartY, string EndY, int StartM, int EndM, string FieldPre)
        {
            string[] textArray2;
            try
            {
                string[] textArray = new string[0];
                int num = ConvertRule.ToInt(StartY);
                int num2 = ConvertRule.ToInt(EndY);
                int num3 = (EndM - StartM) + 1;
                if (num > 0)
                {
                    int num4 = ((num2 - num) + 1) * num3;
                    if (num4 > 0)
                    {
                        textArray = new string[num4];
                        int index = -1;
                        for (int i = num; i <= num2; i++)
                        {
                            for (int j = StartM; j <= EndM; j++)
                            {
                                index++;
                                string text = ConvertRule.FormatYYYYMM(i, j);
                                textArray[index] = FieldPre + text;
                            }
                        }
                    }
                }
                textArray2 = textArray;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return textArray2;
        }

        public static string[] BuildArrayFieldByYm(string StartYm, string EndYm, string FieldPre)
        {
            string[] textArray2;
            try
            {
                string[] textArray = new string[0];
                if (StartYm != "")
                {
                    int monthsBetween = StringRule.GetMonthsBetween(StartYm, EndYm);
                    textArray = new string[monthsBetween];
                    int index = -1;
                    for (int i = 0; i < monthsBetween; i++)
                    {
                        index++;
                        string text = StringRule.YmAddMonths(StartYm, i);
                        textArray[index] = FieldPre + text;
                    }
                }
                textArray2 = textArray;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return textArray2;
        }

        public static void CalcPercent(DataRow dr, DataRow drArea)
        {
            try
            {
                if (dr.Table.Columns.Contains("ContractPayPercent"))
                {
                    if (ConvertRule.ToDecimal(dr["ContractTotalMoney"]) == 0M)
                    {
                        dr["ContractPayPercent"] = DBNull.Value;
                    }
                    else
                    {
                        dr["ContractPayPercent"] = MathRule.Round((ConvertRule.ToDecimal(dr["ContractPay"]) / ConvertRule.ToDecimal(dr["ContractTotalMoney"])) * 100M, 0);
                    }
                }
                if (dr.Table.Columns.Contains("BuildingPrice"))
                {
                    if ((drArea != null) && (ConvertRule.ToDecimal(drArea["BuildingArea"]) != 0M))
                    {
                        dr["BuildingPrice"] = MathRule.Round(ConvertRule.ToDecimal(dr["ContractTotalMoney"]) / ConvertRule.ToDecimal(drArea["BuildingArea"]), 2);
                    }
                    else
                    {
                        dr["BuildingPrice"] = DBNull.Value;
                    }
                }
                if (dr.Table.Columns.Contains("HousePrice"))
                {
                    if ((drArea != null) && (ConvertRule.ToDecimal(drArea["HouseCount"]) != 0M))
                    {
                        dr["HousePrice"] = MathRule.Round(ConvertRule.ToDecimal(dr["ContractTotalMoney"]) / ConvertRule.ToDecimal(drArea["HouseCount"]), 2);
                    }
                    else
                    {
                        dr["HousePrice"] = DBNull.Value;
                    }
                }
                if (dr.Table.Columns.Contains("BudgetPrice"))
                {
                    if ((drArea != null) && (ConvertRule.ToDecimal(drArea["BuildingArea"]) != 0M))
                    {
                        dr["BudgetPrice"] = MathRule.Round(ConvertRule.ToDecimal(dr["BudgetMoney"]) / ConvertRule.ToDecimal(drArea["BuildingArea"]), 2);
                    }
                    else
                    {
                        dr["BudgetPrice"] = DBNull.Value;
                    }
                }
                if (dr.Table.Columns.Contains("ContractOriginalPrice"))
                {
                    if ((drArea != null) && (ConvertRule.ToDecimal(drArea["BuildingArea"]) != 0M))
                    {
                        dr["ContractOriginalPrice"] = MathRule.Round(ConvertRule.ToDecimal(dr["ContractMoney"]) / ConvertRule.ToDecimal(drArea["BuildingArea"]), 2);
                    }
                    else
                    {
                        dr["ContractOriginalPrice"] = DBNull.Value;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void ClearFieldValue(DataRow[] drs, string[] arrField)
        {
            try
            {
                foreach (DataRow row in drs)
                {
                    foreach (string text in arrField)
                    {
                        row[text] = DBNull.Value;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void ClearFieldValue(DataTable tb, string[] arrField)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    foreach (string text in arrField)
                    {
                        row[text] = DBNull.Value;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CollapseAll(DataTable tbDtl)
        {
            try
            {
                foreach (DataRow row in tbDtl.Rows)
                {
                    row["IsExpand"] = 0;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CostBudgetDtlCalcAllRow(DataTable tb, m_DynamicRowType RowType, DataRow drArea, int iStartY, int iEndY)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    CostBudgetDtlCalcField(row, RowType, drArea, iStartY, iEndY);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CostBudgetDtlCalcField(DataRow dr, m_DynamicRowType RowType, DataRow drArea, int iStartY, int iEndY)
        {
            try
            {
                dr["ContractTotalMoney"] = (ConvertRule.ToDecimal(dr["ContractMoney"]) + ConvertRule.ToDecimal(dr["ContractChangeMoney"])) + ConvertRule.ToDecimal(dr["ContractApplyMoney"]);
                if ((RowType == m_DynamicRowType.CBS) && dr.Table.Columns.Contains("ContractBudgetBalance"))
                {
                    dr["ContractBudgetBalance"] = ConvertRule.ToDecimal(dr["ContractTotalMoney"]) - ConvertRule.ToDecimal(dr["BudgetMoney"]);
                }
                dr["ContractPayBalance"] = ConvertRule.ToDecimal(dr["ContractTotalMoney"]) - ConvertRule.ToDecimal(dr["ContractPay"]);
                dr["ContractPayRealBalance"] = ConvertRule.ToDecimal(dr["ContractPay"]) - ConvertRule.ToDecimal(dr["ContractPayReal"]);
                CalcPercent(dr, drArea);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable CreateDynamicHtmlTable(string StartY, string EndY, DataTable tbDtl)
        {
            DataTable table2;
            try
            {
                DataTable tb = tbDtl.Clone();
                tb.TableName = "Html";
                tb.Columns.Add("ClassTd");
                tb.Columns.Add("RecordType");
                tb.Columns.Add("SupplierNameHtml");
                tb.Columns.Add("ContractIDHtml");
                tb.Columns.Add("ContractNameHtml");
                AddMonthField(tb, StartY, EndY, "BudgetMoney_");
                AddMonthField(tb, StartY, EndY, "ContractMoney_");
                tb.Columns.Add("PlanDataHtml");
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void ExpandDeep(DataTable tbDtl, int Deep)
        {
            try
            {
                DataRow[] rowArray = tbDtl.Select("Deep < " + Deep.ToString());
                foreach (DataRow row in rowArray)
                {
                    row["IsExpand"] = 1;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void ExpandRoot(DataTable tbDtl)
        {
            try
            {
                DataRow[] rowArray = tbDtl.Select("Deep = 1");
                if (rowArray.Length == 1)
                {
                    rowArray[0]["IsExpand"] = 1;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FillCostBudgetDtlCBSData(DataRow drDtl, DataRow drCBS)
        {
            try
            {
                drDtl["CostCode"] = drCBS["CostCode"];
                drDtl["CostName"] = drCBS["CostName"];
                drDtl["SortID"] = drCBS["SortID"];
                drDtl["ParentCode"] = drCBS["ParentCode"];
                drDtl["Deep"] = drCBS["Deep"];
                drDtl["FullCode"] = drCBS["FullCode"];
                drDtl["MeasurementUnit"] = drCBS["MeasurementUnit"];
                if (drDtl.Table.Columns.Contains("ChildCount") && drCBS.Table.Columns.Contains("ChildCount"))
                {
                    drDtl["ChildCount"] = drCBS["ChildCount"];
                    drDtl["IsLeafCBS"] = ConvertRule.ToInt(drDtl["ChildCount"]) <= 0;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FillCostBudgetDtlCBSData(DataRow drDtl, DataTable tbCBS)
        {
            try
            {
                DataRow[] rowArray = tbCBS.Select("CostCode = '" + drDtl["CostCode"] + "'");
                if (rowArray.Length > 0)
                {
                    FillCostBudgetDtlCBSData(drDtl, rowArray[0]);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GenerateCostBudgetPlanDataHtml(DataRow drDtl, int iStartY, int iEndY, string FieldName)
        {
            string text;
            try
            {
                text = GenerateCostBudgetPlanDataHtml(drDtl, iStartY, iEndY, FieldName, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string GenerateCostBudgetPlanDataHtml(DataRow drDtl, int iStartY, int iEndY, string FieldName, string ClassTd)
        {
            string text6;
            try
            {
                string text = "";
                if ((iStartY == 0) && (iEndY == 0))
                {
                    return "";
                }
                for (int i = iStartY; i <= iEndY; i++)
                {
                    for (int j = 0; j <= 12; j++)
                    {
                        string text2 = ConvertRule.FormatYYYYMM(i, j);
                        string text3 = FieldName + text2;
                        string moneyShowString = GetMoneyShowString(drDtl[text3]);
                        string wanDecimalShowHint = GetWanDecimalShowHint(drDtl[text3]);
                        int expand = GetExpandByYear(i.ToString(), iStartY.ToString());
                        text = text + string.Format("<td align=right nowrap id='YearData_{0}' title='{1}'", text2, wanDecimalShowHint);
                        if (ClassTd != "")
                        {
                            text = text + string.Format(" class='{0}'", ClassTd);
                        }
                        if (j != 0)
                        {
                            text = text + string.Format(" style='display:{0}'", GetStyleDisplayByExpand(expand));
                        }
                        text = text + ">" + moneyShowString + "</td>";
                    }
                }
                text6 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text6;
        }

        public static string GenerateCostBudgetPlanDataHtml(DataRow drDtl, int iStartY, int iEndY, string[] arrFieldName, string[] arrClassTd, string[] arrFieldDesc)
        {
            string text8;
            try
            {
                int objYear;
                int objMonth;
                string text3;
                string text4;
                string text = "";
                if ((iStartY == 0) && (iEndY == 0))
                {
                    return "";
                }
                bool[] flagArray = new bool[arrFieldName.Length];
                int index = -1;
                foreach (string text2 in arrFieldName)
                {
                    index++;
                    flagArray[index] = false;
                    for (objYear = iStartY; objYear <= iEndY; objYear++)
                    {
                        for (objMonth = 0; objMonth <= 12; objMonth++)
                        {
                            text3 = ConvertRule.FormatYYYYMM(objYear, objMonth);
                            text4 = text2 + text3;
                            if (GetMoneyShowString(drDtl[text4]) != "")
                            {
                                flagArray[index] = true;
                            }
                        }
                    }
                }
                for (objYear = iStartY; objYear <= iEndY; objYear++)
                {
                    int expand = GetExpandByYear(objYear.ToString(), iStartY.ToString());
                    for (objMonth = 0; objMonth <= 12; objMonth++)
                    {
                        text3 = ConvertRule.FormatYYYYMM(objYear, objMonth);
                        text = text + string.Format("<td align=right nowrap id='YearData_{0}'", text3);
                        if (objMonth != 0)
                        {
                            text = text + string.Format(" style='display:{0}'", GetStyleDisplayByExpand(expand));
                        }
                        text = text + ">";
                        int num5 = -1;
                        foreach (string text2 in arrFieldName)
                        {
                            num5++;
                            string text6 = arrClassTd[num5];
                            text4 = text2 + text3;
                            if ((num5 > 0) && flagArray[num5 - 1])
                            {
                                text = text + "<br>";
                            }
                            string moneyShowString = GetMoneyShowString(drDtl[text4]);
                            string wanDecimalShowHint = GetWanDecimalShowHint(drDtl[text4]);
                            if (wanDecimalShowHint != "")
                            {
                                wanDecimalShowHint = arrFieldDesc[num5] + " " + wanDecimalShowHint;
                            }
                            if ((moneyShowString != "") && (wanDecimalShowHint != ""))
                            {
                                text = text + "<span";
                                if (text6 != "")
                                {
                                    text = text + string.Format(" class='{0}'", text6);
                                }
                                if (wanDecimalShowHint != "")
                                {
                                    text = text + string.Format(" title='{0}'", wanDecimalShowHint);
                                }
                                text = text + ">" + moneyShowString + "</span>";
                            }
                            else if (flagArray[num5])
                            {
                                text = text + "&nbsp;";
                            }
                        }
                        text = text + "</td>";
                    }
                }
                text8 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text8;
        }

        public static void GenerateCostBudgetPlanTitleHtml(DataTable tbDtl, int iStartY, int iEndY, ref string html_title1, ref string html_title2)
        {
            try
            {
                html_title1 = "";
                html_title2 = "";
                if ((iStartY != 0) || (iEndY != 0))
                {
                    for (int i = iStartY; i <= iEndY; i++)
                    {
                        for (int j = 0; j <= 12; j++)
                        {
                            string text2;
                            int num4;
                            string text = ConvertRule.FormatYYYYMM(i, j);
                            int expand = GetExpandByYear(i.ToString(), iStartY.ToString());
                            if (expand == 0)
                            {
                                text2 = "展开到月度";
                                num4 = 1;
                            }
                            else
                            {
                                text2 = "折叠到年度";
                                num4 = 13;
                            }
                            if (j == 0)
                            {
                                html_title1 = html_title1 + string.Format("<th colspan={3} align=center nowrap id='YearTitle_{0}'><a href='#' onclick='javascript:YearExpand(this);return false;' expand={1} title='{2}' key='{0}'>{0}</a></th>", new object[] { i, expand, text2, num4 });
                                html_title2 = html_title2 + string.Format("<th align=center nowrap id='YearTitle_{0}'>年度</th>", text);
                            }
                            else
                            {
                                html_title2 = html_title2 + string.Format("<th align=center nowrap id='YearTitle_{0}' style='display:{2}'>{1}</th>", text, j, GetStyleDisplayByExpand(expand));
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

        public static DataSet GenerateEmptyCostBudgetDtl(string StartY, string EndY, EntityData entityTargetHis)
        {
            DataSet set2;
            try
            {
                DataSet set = new DataSet();
                EntityData costBudgetDtlByCode = CostBudgetDAO.GetCostBudgetDtlByCode("");
                DataTable tbDtl = costBudgetDtlByCode.CurrentTable.Clone();
                AddColumnTargetHis(tbDtl, entityTargetHis);
                tbDtl.Columns.Add("BudgetMoneyHisHtml");
                tbDtl.Columns.Add("BudgetMoneyHtml");
                tbDtl.Columns.Add("BudgetValidMoney", typeof(decimal));
                tbDtl.Columns.Add("DescriptionHtml");
                tbDtl.Columns.Add("CostName");
                tbDtl.Columns.Add("SortID");
                tbDtl.Columns.Add("Deep", typeof(int));
                tbDtl.Columns.Add("ParentCode");
                tbDtl.Columns.Add("FullCode");
                tbDtl.Columns.Add("ChildCount", typeof(int));
                tbDtl.Columns.Add("IsLeafCBS", typeof(bool));
                tbDtl.Columns.Add("MeasurementUnit");
                costBudgetDtlByCode.Dispose();
                set.Tables.Add(tbDtl);
                tbDtl.Columns.Add("BudgetChangeMoney", typeof(decimal));
                tbDtl.Columns.Add("BudgetChangeDescription");
                tbDtl.Columns.Add("BalanceContractMoney", typeof(decimal));
                tbDtl.Columns.Add("ContractCode");
                tbDtl.Columns.Add("ContractID");
                tbDtl.Columns.Add("ContractName");
                tbDtl.Columns.Add("SupplierCode");
                tbDtl.Columns.Add("SupplierName");
                tbDtl.Columns.Add("ContractTotalMoney", typeof(decimal));
                tbDtl.Columns.Add("ContractMoney", typeof(decimal));
                tbDtl.Columns.Add("ContractChangeMoney", typeof(decimal));
                tbDtl.Columns.Add("ContractApplyMoney", typeof(decimal));
                tbDtl.Columns.Add("ContractAccountMoney", typeof(decimal));
                tbDtl.Columns.Add("ContractBudgetBalance", typeof(decimal));
                tbDtl.Columns.Add("ContractPay", typeof(decimal));
                tbDtl.Columns.Add("ContractPayReal", typeof(decimal));
                tbDtl.Columns.Add("ContractPayBalance", typeof(decimal));
                tbDtl.Columns.Add("ContractPayRealBalance", typeof(decimal));
                tbDtl.Columns.Add("ContractPayPercent", typeof(decimal));
                tbDtl.Columns.Add("BuildingPrice", typeof(decimal));
                tbDtl.Columns.Add("HousePrice", typeof(decimal));
                tbDtl.Columns.Add("BudgetPrice", typeof(decimal));
                tbDtl.Columns.Add("ContractOriginalPrice", typeof(decimal));
                DataTable table = new DataTable("Month");
                set.Tables.Add(table);
                table.Columns.Add("CostCode");
                table.Columns.Add("FullCode");
                table.Columns.Add("RecordType");
                table.Columns.Add("IYear", typeof(int));
                table.Columns.Add("IMonth", typeof(int));
                table.Columns.Add("Money", typeof(decimal));
                DataTable table3 = new DataTable("Contract");
                set.Tables.Add(table3);
                table3.Columns.Add("ContractCode");
                table3.Columns.Add("AllContractCode");
                table3.Columns.Add("RecordType");
                table3.Columns.Add("ContractID");
                table3.Columns.Add("ContractName");
                table3.Columns.Add("SupplierCode");
                table3.Columns.Add("SupplierName");
                table3.Columns.Add("Description");
                table3.Columns.Add("DescriptionHtml");
                table3.Columns.Add("CostCode");
                table3.Columns.Add("FullCode");
                table3.Columns.Add("BudgetMoney", typeof(decimal));
                table3.Columns.Add("ContractTotalMoney", typeof(decimal));
                table3.Columns.Add("ContractMoney", typeof(decimal));
                table3.Columns.Add("ContractChangeMoney", typeof(decimal));
                table3.Columns.Add("ContractApplyMoney", typeof(decimal));
                table3.Columns.Add("ContractAccountMoney", typeof(decimal));
                table3.Columns.Add("ContractBudgetBalance", typeof(decimal));
                table3.Columns.Add("ContractPay", typeof(decimal));
                table3.Columns.Add("ContractPayReal", typeof(decimal));
                table3.Columns.Add("ContractPayBalance", typeof(decimal));
                table3.Columns.Add("ContractPayRealBalance", typeof(decimal));
                table3.Columns.Add("ContractPayPercent", typeof(decimal));
                table3.Columns.Add("BuildingPrice", typeof(decimal));
                table3.Columns.Add("HousePrice", typeof(decimal));
                table3.Columns.Add("BudgetPrice", typeof(decimal));
                table3.Columns.Add("ContractOriginalPrice", typeof(decimal));
                DataTable table4 = table3.Clone();
                table4.TableName = "NoContract";
                set.Tables.Add(table4);
                DataTable table5 = table3.Clone();
                table5.TableName = "Balance";
                set.Tables.Add(table5);
                DataTable table6 = new DataTable("ContractMonth");
                set.Tables.Add(table6);
                table6.Columns.Add("ContractCode");
                table6.Columns.Add("CostCode");
                table6.Columns.Add("FullCode");
                table6.Columns.Add("RecordType");
                table6.Columns.Add("IYear", typeof(int));
                table6.Columns.Add("IMonth", typeof(int));
                table6.Columns.Add("Money", typeof(decimal));
                DataTable table7 = CostBudgetDAO.GetCostBudgetContractByCode("").CurrentTable.Clone();
                set.Tables.Add(table7);
                set2 = set;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataTable GenerateEmptyCostTargetDtl(string StartY, string EndY, EntityData entityTargetHis)
        {
            DataTable table2;
            try
            {
                EntityData costBudgetDtlByCode = CostBudgetDAO.GetCostBudgetDtlByCode("");
                DataTable tbDtl = costBudgetDtlByCode.CurrentTable.Clone();
                AddColumnTargetHis(tbDtl, entityTargetHis);
                tbDtl.Columns.Add("BudgetMoneyHisHtml");
                tbDtl.Columns.Add("BudgetMoneyHtml");
                tbDtl.Columns.Add("BudgetValidMoney", typeof(decimal));
                tbDtl.Columns.Add("DescriptionHtml");
                tbDtl.Columns.Add("CostName");
                tbDtl.Columns.Add("SortID");
                tbDtl.Columns.Add("Deep", typeof(int));
                tbDtl.Columns.Add("ParentCode");
                tbDtl.Columns.Add("FullCode");
                tbDtl.Columns.Add("ChildCount", typeof(int));
                tbDtl.Columns.Add("IsLeafCBS", typeof(bool));
                tbDtl.Columns.Add("MeasurementUnit");
                AddMonthField(tbDtl, StartY, EndY, "BudgetMoney_");
                tbDtl.Columns.Add("PlanDataHtml");
                costBudgetDtlByCode.Dispose();
                table2 = tbDtl;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetBuildingHref(string BuildingCode, string BuildingName)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingCode != "") || (BuildingName != ""))
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewBuilding('{0}');return false;\">{1}</a>", BuildingCode, BuildingName);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractBudgetBalanceRemindStyle(object objContractBudgetMoney)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToDecimal(objContractBudgetMoney) > 0M)
                {
                    text = "color:#FF0000;";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractHref(object objContractCode, object objContractName)
        {
            string text4;
            try
            {
                string text = "";
                string text2 = ConvertRule.ToString(objContractCode);
                string text3 = ConvertRule.ToString(objContractName);
                if ((text2 != "") || (text3 != ""))
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractInfo('{0}');return false;\">{1}</a>", text2, text3);
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static string GetContractPayHref(object Money, object CostCode, object ContractCode, object IsContract)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractPay('{1}', '{2}', '{3}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(ContractCode), ConvertRule.ToString(IsContract) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractPayHref(object Money, object CostCode, object ContractCode, object IsContract, object PBSType, object PBSCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractPBSPay('{1}', '{2}', '{3}', '{4}', '{5}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(ContractCode), ConvertRule.ToString(IsContract), ConvertRule.ToString(PBSType), ConvertRule.ToString(PBSCode) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractPayRealBalanceHref(object Money, object CostCode, object ContractCode, object IsContract)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractPayRealBalance('{1}', '{2}', '{3}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(ContractCode), ConvertRule.ToString(IsContract) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractPayRealBalanceHref(object Money, object CostCode, object ContractCode, object IsContract, object PBSType, object PBSCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractPBSPayRealBalance('{1}', '{2}', '{3}', '{4}', '{5}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(ContractCode), ConvertRule.ToString(IsContract), ConvertRule.ToString(PBSType), ConvertRule.ToString(PBSCode) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractPayRealHref(object Money, object CostCode, object ContractCode, object IsContract, object PayoutDateBegin, object PayoutDateEnd)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractPayReal('{1}', '{2}', '{3}', '{4}', '{5}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(ContractCode), ConvertRule.ToString(IsContract), ConvertRule.ToString(PayoutDateBegin), ConvertRule.ToString(PayoutDateEnd) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractPayRealHref(object Money, object CostCode, object ContractCode, object IsContract, object PayoutDateBegin, object PayoutDateEnd, object PBSType, object PBSCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewContractPBSPayReal('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(ContractCode), ConvertRule.ToString(IsContract), ConvertRule.ToString(PayoutDateBegin), ConvertRule.ToString(PayoutDateEnd), ConvertRule.ToString(PBSType), ConvertRule.ToString(PBSCode) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int GetExpandByYear(string y, string default_expand_year)
        {
            int num2;
            try
            {
                int num = 0;
                if (y == default_expand_year)
                {
                    num = 1;
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetMoneyShowString(object money)
        {
            string text;
            try
            {
                text = GetMoneyShowString(money, m_MoneyUnit.yuan, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string GetMoneyShowString(object money, m_MoneyUnit unit)
        {
            string text;
            try
            {
                text = GetMoneyShowString(money, unit, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string GetMoneyShowString(object money, m_MoneyUnit unit, string MoneyType)
        {
            string text2;
            try
            {
                string wanDecimalShowString = "";
                switch (unit)
                {
                    case m_MoneyUnit.yuan:
                        wanDecimalShowString = StringRule.BuildShowNumberString(money, "#,##0");
                        break;

                    case m_MoneyUnit.fen:
                        wanDecimalShowString = StringRule.BuildShowNumberString(money, "#,##0.00");
                        break;

                    default:
                        if (MoneyType.ToLower() == "price")
                        {
                            wanDecimalShowString = MathRule.GetWanDecimalShowString(money);
                        }
                        else if (m_IsRoundWanMoney)
                        {
                            wanDecimalShowString = StringRule.BuildMoneyWanFormatString(ConvertRule.ToDecimal(money), -1, 0);
                        }
                        else
                        {
                            wanDecimalShowString = MathRule.GetWanDecimalShowString(money);
                        }
                        break;
                }
                text2 = wanDecimalShowString;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static m_MoneyUnit GetMoneyUnit(HtmlSelect sltMoneyUnit)
        {
            m_MoneyUnit wan;
            try
            {
                switch (sltMoneyUnit.Value)
                {
                    case "yuan":
                        return m_MoneyUnit.yuan;

                    case "fen":
                        return m_MoneyUnit.fen;
                }
                wan = m_MoneyUnit.wan;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return wan;
        }

        public static string GetSpan(object objText, object objClass)
        {
            string text4;
            try
            {
                string text = "";
                string text2 = ConvertRule.ToString(objText);
                string text3 = ConvertRule.ToString(objClass);
                if (text2 == "")
                {
                    text = "";
                }
                else
                {
                    text = "<span";
                    if (text3 != "")
                    {
                        text = text + " class='" + text3 + "'";
                    }
                    text = (text + ">") + text2 + "</span>";
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static string GetStyleDisplayByExpand(int expand)
        {
            string text2;
            try
            {
                string text;
                if (expand == 0)
                {
                    text = "none";
                }
                else
                {
                    text = "";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSupplierHref(string SupplierCode, string SupplierName)
        {
            string text2;
            try
            {
                string text = "";
                if ((SupplierCode != "") || (SupplierName != ""))
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewSupplierInfo('{0}');return false;\">{1}</a>", SupplierCode, SupplierName);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetTd(object objText, object objClass)
        {
            string text4;
            try
            {
                string text = "";
                string text2 = ConvertRule.ToString(objText);
                string text3 = ConvertRule.ToString(objClass);
                if (text2 == "")
                {
                    text = "<td></td>";
                }
                else
                {
                    text = "<td";
                    if (text3 != "")
                    {
                        text = text + " class='" + text3 + "'";
                    }
                    text = (text + ">") + text2 + "</td>";
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static string GetTd(object objText1, object objText2, object objClass)
        {
            return GetTd(ConvertRule.ToString(objText1) + ConvertRule.ToString(objText2), objClass);
        }

        public static string GetTd2(object objText, object objClass, object objTitle)
        {
            return GetTd2(objText, "", objClass, objTitle, "");
        }

        public static string GetTd2(object objText1, object objText2, object objClass, object objTitle)
        {
            return GetTd2(objText1, objText2, objClass, objTitle, "");
        }

        public static string GetTd2(object objText1, object objText2, object objClass, object objTitle, object objAttributes)
        {
            string text6;
            try
            {
                string text = "";
                string text2 = ConvertRule.ToString(objText1) + ConvertRule.ToString(objText2);
                string text3 = ConvertRule.ToString(objClass);
                string text4 = ConvertRule.ToString(objTitle);
                string text5 = ConvertRule.ToString(objAttributes);
                if (text2 == "")
                {
                    text = "<td></td>";
                }
                else
                {
                    text = "<td";
                    if (text5 != "")
                    {
                        text = text + " " + text5;
                    }
                    if (text3 != "")
                    {
                        text = text + " class='" + text3 + "'";
                    }
                    if (text4 != "")
                    {
                        text = text + " title='" + text4 + "'";
                    }
                    text = (text + ">") + text2 + "</td>";
                }
                text6 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text6;
        }

        public static string GetTitle(object title)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(title) != "")
                {
                    text = "title='" + title + "'";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetWanDecimalShowHint(object money)
        {
            string text2;
            try
            {
                string decimalShowString = "";
                if (m_IsRoundWanMoney)
                {
                    decimalShowString = MathRule.GetDecimalShowString(money);
                    if (decimalShowString != "")
                    {
                        decimalShowString = decimalShowString + "元";
                    }
                }
                text2 = decimalShowString;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetWanDecimalShowString(object money)
        {
            string text2;
            try
            {
                string wanDecimalShowString = "";
                if (m_IsRoundWanMoney)
                {
                    wanDecimalShowString = StringRule.BuildMoneyWanFormatString(ConvertRule.ToDecimal(money), -1, 0);
                }
                else
                {
                    wanDecimalShowString = MathRule.GetWanDecimalShowString(money);
                }
                text2 = wanDecimalShowString;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void RemoveColumnTargetHis(DataTable tbDtl)
        {
            try
            {
                for (int i = tbDtl.Columns.Count - 1; i >= 0; i--)
                {
                    DataColumn column = tbDtl.Columns[i];
                    if (column.ColumnName.StartsWith("BudgetMoneyHis_"))
                    {
                        tbDtl.Columns.Remove(column);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetColumnTargetHis(DataTable tbDtl, EntityData entityTargetHis)
        {
            try
            {
                if (entityTargetHis == null)
                {
                    RemoveColumnTargetHis(tbDtl);
                }
                else
                {
                    AddColumnTargetHis(tbDtl, entityTargetHis);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetHtmlControlMoney(HtmlControl obj, object money, m_MoneyUnit unit, string MoneyType)
        {
            try
            {
                string text = GetMoneyShowString(money, unit, MoneyType);
                string wanDecimalShowHint = GetWanDecimalShowHint(money);
                if (obj is HtmlGenericControl)
                {
                    ((HtmlGenericControl) obj).InnerHtml = text;
                    if (m_IsRoundWanMoney)
                    {
                        ((HtmlGenericControl) obj).Attributes["title"] = wanDecimalShowHint;
                    }
                }
                else if (obj is HtmlTableCell)
                {
                    ((HtmlTableCell) obj).InnerHtml = text;
                    if (m_IsRoundWanMoney)
                    {
                        ((HtmlTableCell) obj).Attributes["title"] = wanDecimalShowHint;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public enum m_DynamicRowType
        {
            CBS,
            Contract,
            NoContract,
            Balance
        }

        public enum m_MoneyUnit
        {
            wan,
            yuan,
            fen
        }
    }
}

