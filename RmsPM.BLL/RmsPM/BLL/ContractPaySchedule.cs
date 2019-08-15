namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class ContractPaySchedule
    {
        private string m_DateTitleHtml1 = "";
        private string m_DateTitleHtml2 = "";
        private DataSet m_ds = null;
        private string m_EndYm = "";
        private string m_PBSCode = "";
        private string m_PBSType = "";
        private string m_ProjectCode = "";
        private string m_StartYm = "";

        public ContractPaySchedule(string a_ProjectCode)
        {
            this.m_ProjectCode = a_ProjectCode;
            this.CreateTable();
        }

        private void AddApportion(string ContractCode, string CostCode, string PBSCode, string RecordType, string MoneyField, decimal Money)
        {
            try
            {
                DataRow row;
                DataRow[] rowArray = this.tbApportion.Select("ContractCode = '" + ContractCode + "' and CostCode='" + CostCode + "' and PBSCode = '" + PBSCode + "'");
                if (rowArray.Length == 0)
                {
                    row = this.tbApportion.NewRow();
                    row["RecordType"] = RecordType;
                    row["ContractCode"] = ContractCode;
                    row["CostCode"] = CostCode;
                    row["PBSCode"] = PBSCode;
                    this.tbApportion.Rows.Add(row);
                }
                else
                {
                    row = rowArray[0];
                }
                row[MoneyField] = ConvertRule.ToDecimal(row[MoneyField]) + Money;
                CostBudgetPageRule.CostBudgetDtlCalcField(row, CostBudgetPageRule.m_DynamicRowType.Contract, null, 0, 0);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void AddDateRangeColumn(DataTable tb, string StartYm, string EndYm)
        {
            try
            {
                int monthsBetween = StringRule.GetMonthsBetween(StartYm, EndYm);
                for (int i = 0; i < monthsBetween; i++)
                {
                    string text = StringRule.YmAddMonths(StartYm, i);
                    tb.Columns.Add("PayoutMoneyYm_" + text, typeof(decimal));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void AddPayout(DataTable tbSrc, string RecordType)
        {
            try
            {
                DataRow[] drsSrc = tbSrc.Select("");
                this.AddPayout(drsSrc, RecordType);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void AddPayout(DataRow[] drsSrc, string RecordType)
        {
            try
            {
                foreach (DataRow row in drsSrc)
                {
                    DataRow row2;
                    string text = ConvertRule.ToString(row["ContractCode"]);
                    string text2 = ConvertRule.ToString(row["CostCode"]);
                    string text3 = ConvertRule.ToString(row["PayoutCode"]);
                    string text4 = ConvertRule.ToString(row["CostBudgetSetCode"]);
                    string text5 = ConvertRule.ToString(row["PBSType"]);
                    string text6 = ConvertRule.ToString(row["PBSCode"]);
                    DataRow[] rowArray = this.tbPayout.Select("ContractCode = '" + text + "' and CostCode='" + text2 + "' and PayoutCode = '" + text3 + "' and CostBudgetSetCode = '" + text4 + "' and PBSType = '" + text5 + "' and PBSCode = '" + text6 + "'");
                    if (rowArray.Length == 0)
                    {
                        row2 = this.tbPayout.NewRow();
                        row2["RecordType"] = RecordType;
                        row2["ContractCode"] = text;
                        row2["PayoutCode"] = text3;
                        row2["CostCode"] = text2;
                        row2["FullCode"] = row["FullCode"];
                        row2["CostBudgetSetCode"] = text4;
                        row2["PBSType"] = text5;
                        row2["PBSCode"] = text6;
                        row2["PayoutDate"] = row["PayoutDate"];
                        row2["PayoutYm"] = ConvertRule.ToDateString(row2["PayoutDate"], "yyyyMM");
                        this.tbPayout.Rows.Add(row2);
                    }
                    else
                    {
                        row2 = rowArray[0];
                    }
                    row2["PayoutMoney"] = ConvertRule.ToDecimal(row2["PayoutMoney"]) + ConvertRule.ToDecimal(row["Money"]);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void AddRelationContractRow(DataTable tbContract, DataTable tbSrc, string RecordType, string MoneyField)
        {
            try
            {
                foreach (DataRow row in tbSrc.Rows)
                {
                    DataRow row2;
                    string contractCode = ConvertRule.ToString(row["ContractCode"]);
                    string costCode = ConvertRule.ToString(row["CostCode"]);
                    DataRow[] rowArray = tbContract.Select("ContractCode = '" + contractCode + "' and CostCode='" + costCode + "'");
                    if (rowArray.Length == 0)
                    {
                        row2 = tbContract.NewRow();
                        row2["RecordType"] = RecordType;
                        row2["ContractCode"] = contractCode;
                        row2["ContractID"] = row["ContractID"];
                        row2["ContractName"] = row["ContractName"];
                        row2["SupplierName"] = row["SupplierName"];
                        row2["SupplierCode"] = row["SupplierCode"];
                        row2["CostCode"] = costCode;
                        row2["FullCode"] = row["FullCode"];
                        tbContract.Rows.Add(row2);
                    }
                    else
                    {
                        row2 = rowArray[0];
                    }
                    string text3 = ConvertRule.ToString(row2["AllContractCode"]);
                    if (text3.IndexOf("'" + contractCode + "'") < 0)
                    {
                        if (text3 != "")
                        {
                            text3 = text3 + ",";
                        }
                        text3 = text3 + "'" + contractCode + "'";
                    }
                    row2["AllContractCode"] = text3;
                    if (tbSrc.Columns.Contains("ContractCostCode"))
                    {
                        string text4 = ConvertRule.ToString(row["ContractCostCode"]);
                        if (text4 != "")
                        {
                            string text5 = ConvertRule.ToString(row2["AllocateCodes"]);
                            if (text5.IndexOf("'" + text4 + "'") < 0)
                            {
                                if (text5 != "")
                                {
                                    text5 = text5 + ",";
                                }
                                text5 = text5 + "'" + text4 + "'";
                            }
                            row2["AllocateCodes"] = text5;
                        }
                    }
                    row2[MoneyField] = ConvertRule.ToDecimal(row2[MoneyField]) + ConvertRule.ToDecimal(row["Money"]);
                    CostBudgetPageRule.CostBudgetDtlCalcField(row2, CostBudgetPageRule.m_DynamicRowType.Contract, null, 0, 0);
                    this.DoApportion(contractCode, costCode, ConvertRule.ToString(row["CostBudgetSetCode"]), RecordType, MoneyField, ConvertRule.ToDecimal(row["Money"]));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void CreateTable()
        {
            try
            {
                this.m_ds = new DataSet();
                DataTable table = new DataTable("Dtl");
                table.Columns.Add("DtlCode");
                table.Columns.Add("IsExpand", typeof(int));
                table.Columns.Add("RecordType");
                table.Columns.Add("ClassTd");
                table.Columns.Add("CostCode");
                table.Columns.Add("CostName");
                table.Columns.Add("SortID");
                table.Columns.Add("Deep", typeof(int));
                table.Columns.Add("ParentCode");
                table.Columns.Add("FullCode");
                table.Columns.Add("ChildCount", typeof(int));
                table.Columns.Add("IsLeafCBS", typeof(bool));
                table.Columns.Add("ContractCode");
                table.Columns.Add("AllContractCode");
                table.Columns.Add("ContractID");
                table.Columns.Add("ContractName");
                table.Columns.Add("SupplierCode");
                table.Columns.Add("SupplierName");
                table.Columns.Add("AllocateCodes");
                table.Columns.Add("PBSCode");
                table.Columns.Add("ContractTotalMoney", typeof(decimal));
                table.Columns.Add("ContractMoney", typeof(decimal));
                table.Columns.Add("ContractChangeMoney", typeof(decimal));
                table.Columns.Add("ContractApplyMoney", typeof(decimal));
                table.Columns.Add("ContractPay", typeof(decimal));
                table.Columns.Add("ContractPayReal", typeof(decimal));
                table.Columns.Add("ContractPayBalance", typeof(decimal));
                table.Columns.Add("ContractPayRealBalance", typeof(decimal));
                table.Columns.Add("ContractPayPercent", typeof(decimal));
                table.Columns.Add("PlanDataHtml");
                this.m_ds.Tables.Add(table);
                DataTable table2 = table.Clone();
                table2.TableName = "Contract";
                this.m_ds.Tables.Add(table2);
                DataTable table3 = new DataTable("Payout");
                table3.Columns.Add("ContractCode");
                table3.Columns.Add("CostCode");
                table3.Columns.Add("FullCode");
                table3.Columns.Add("PayoutCode");
                table3.Columns.Add("PayoutDate", typeof(DateTime));
                table3.Columns.Add("PayoutYm");
                table3.Columns.Add("PayoutMoney", typeof(decimal));
                table3.Columns.Add("RecordType");
                table3.Columns.Add("CostBudgetSetCode");
                table3.Columns.Add("PBSType");
                table3.Columns.Add("PBSCode");
                this.m_ds.Tables.Add(table3);
                DataTable table4 = new DataTable("CostBudgetSet");
                table4.Columns.Add("CostBudgetSetCode");
                table4.Columns.Add("CostBudgetSetName");
                this.m_ds.Tables.Add(table4);
                DataTable table5 = table.Clone();
                table5.TableName = "Apportion";
                this.m_ds.Tables.Add(table5);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void DoApportion(string ContractCode, string CostCode, string CostBudgetSetCode, string RecordType, string MoneyField, decimal TotalMoney)
        {
            try
            {
                this.AddApportion(ContractCode, CostCode, CostBudgetSetCode, RecordType, MoneyField, TotalMoney);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void DropDateRangeColumn(DataTable tb)
        {
            try
            {
                for (int i = tb.Columns.Count - 1; i >= 0; i--)
                {
                    DataColumn column = tb.Columns[i];
                    if (column.ColumnName.StartsWith("PayoutMoneyYm_"))
                    {
                        tb.Columns.Remove(column);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void FillContractChangeCostCode(DataTable tbTemp)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                foreach (DataRow row in tbTemp.Rows)
                {
                    if (ConvertRule.ToString(row["ContractCostCode"]) == "")
                    {
                        string text = ConvertRule.ToString(row["ContractCode"]);
                        string text2 = ConvertRule.ToString(row["CostCode"]);
                        string text3 = ConvertRule.ToString(row["CostBudgetSetCode"]);
                        string text4 = "";
                        string queryString = string.Format("select ContractCostCode from ContractCost where ContractCode = '{0}' and CostCode = '{1}' and CostBudgetSetCode = '{2}'", text, text2, text3);
                        DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row2 in table.Rows)
                        {
                            text4 = text4 + ((text4 == "") ? "" : ",");
                            text4 = text4 + ConvertRule.ToString(row2["ContractCostCode"]);
                        }
                        row["ContractCostCode"] = text4;
                    }
                }
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Generate()
        {
            try
            {
                this.InitCostBudgetSet();
                this.SetRelationContract();
                this.SetRelationNoContract();
                DataView view = new DataView(CBSDAO.GetCBSByProject(this.ProjectCode).CurrentTable, "", "SortID, Deep", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow row = view2.Row;
                    DataRow row2 = this.tb.NewRow();
                    string text = row["CostCode"].ToString();
                    row2["DtlCode"] = row["CostCode"];
                    row2["RecordType"] = "";
                    row2["CostCode"] = row["CostCode"];
                    row2["CostName"] = row["CostName"];
                    row2["SortID"] = row["SortID"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    row2["FullCode"] = row["FullCode"];
                    row2["ChildCount"] = row["ChildCount"];
                    row2["IsLeafCBS"] = ConvertRule.ToInt(row2["ChildCount"]) <= 0;
                    this.tb.Rows.Add(row2);
                    DataRow[] rowArray = this.tbContract.Select("CostCode = '" + text + "'");
                    foreach (DataRow row3 in rowArray)
                    {
                        string text2 = row3["ContractCode"].ToString();
                        DataRow drDst = this.tb.NewRow();
                        ConvertRule.DataRowCopy(row3, drDst, this.tbContract, this.tb);
                        drDst["DtlCode"] = "C_" + text + ":" + text2;
                        drDst["RecordType"] = row3["RecordType"];
                        drDst["Deep"] = row2["Deep"];
                        drDst["ParentCode"] = row2["ParentCode"];
                        drDst["ChildCount"] = 0;
                        drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdContract;
                        this.tb.Rows.Add(drDst);
                        DataRow[] rowArray2 = this.tbApportion.Select("ContractCode = '" + text2 + "' and CostCode = '" + text + "'");
                        foreach (DataRow row5 in rowArray2)
                        {
                            string costBudgetSetCode = ConvertRule.ToString(row5["PBSCode"]);
                            drDst = this.tb.NewRow();
                            ConvertRule.DataRowCopy(row5, drDst, this.tbApportion, this.tb);
                            drDst["DtlCode"] = "A_" + text + ":" + text2 + "@" + costBudgetSetCode;
                            drDst["RecordType"] = "Apportion";
                            drDst["PBSCode"] = costBudgetSetCode;
                            drDst["Deep"] = row2["Deep"];
                            drDst["ParentCode"] = row2["ParentCode"];
                            drDst["ChildCount"] = 0;
                            drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdApportion;
                            drDst["ContractName"] = "　　" + this.GetCostBudgetSetName(costBudgetSetCode);
                            this.tb.Rows.Add(drDst);
                        }
                    }
                }
                this.ReCalcByRelation("", "");
                this.RefreshDateRange();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetContractHref(object objRecordType, object objContractCode, object objContractName)
        {
            string text3;
            try
            {
                string contractHref = "";
                if (ConvertRule.ToString(objRecordType) != "Apportion")
                {
                    contractHref = CostBudgetPageRule.GetContractHref(objContractCode, objContractName);
                }
                else
                {
                    contractHref = ConvertRule.ToString(objContractName);
                }
                text3 = contractHref;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetContractPayHref(object objRecordType, object Money, object CostCode, object ContractCode, object IsContract, object PBSType, object PBSCode)
        {
            string text3;
            try
            {
                string text = "";
                if (ConvertRule.ToString(objRecordType) != "Apportion")
                {
                    text = CostBudgetPageRule.GetContractPayHref(Money, CostCode, ContractCode, IsContract, PBSType, PBSCode);
                }
                else
                {
                    text = ConvertRule.ToString(Money);
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetContractPayRealBalanceHref(object objRecordType, object Money, object CostCode, object ContractCode, object IsContract, object PBSType, object PBSCode)
        {
            string text3;
            try
            {
                string text = "";
                if (ConvertRule.ToString(objRecordType) != "Apportion")
                {
                    text = CostBudgetPageRule.GetContractPayRealBalanceHref(Money, CostCode, ContractCode, IsContract, PBSType, PBSCode);
                }
                else
                {
                    text = ConvertRule.ToString(Money);
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetContractPayRealHref(object objRecordType, object Money, object CostCode, object ContractCode, object IsContract, object PayoutDateBegin, object PayoutDateEnd, object PBSType, object PBSCode)
        {
            string text3;
            try
            {
                string text = "";
                if (ConvertRule.ToString(objRecordType) != "Apportion")
                {
                    text = CostBudgetPageRule.GetContractPayRealHref(Money, CostCode, ContractCode, IsContract, PayoutDateBegin, PayoutDateEnd, PBSType, PBSCode);
                }
                else
                {
                    text = ConvertRule.ToString(Money);
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        private string GetCostBudgetSetName(string CostBudgetSetCode)
        {
            string text2;
            try
            {
                string text = "";
                DataRow[] rowArray = this.tbCostBudgetSet.Select("CostBudgetSetCode = '" + CostBudgetSetCode + "'");
                if (rowArray.Length > 0)
                {
                    text = ConvertRule.ToString(rowArray[0]["CostBudgetSetName"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private void GetCurrentYearMonthRange(int CurrentY, int BeginY, int EndY, ref int iBeginM, ref int iEndM)
        {
            try
            {
                iBeginM = 1;
                iEndM = 12;
                if (CurrentY == BeginY)
                {
                    iBeginM = ConvertRule.ToInt(this.StartYm.Substring(4, 2));
                }
                if (CurrentY == EndY)
                {
                    iEndM = ConvertRule.ToInt(this.EndYm.Substring(4, 2));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void InitCostBudgetSet()
        {
            try
            {
                this.tbCostBudgetSet.Rows.Clear();
                this.tbCostBudgetSet.AcceptChanges();
                EntityData costBudgetSetByProjectCode = CostBudgetDAO.GetCostBudgetSetByProjectCode(this.ProjectCode);
                foreach (DataRow row2 in costBudgetSetByProjectCode.CurrentTable.Rows)
                {
                    DataRow row = this.tbCostBudgetSet.NewRow();
                    row["CostBudgetSetCode"] = row2["CostBudgetSetCode"];
                    row["CostBudgetSetName"] = row2["CostBudgetSetName"];
                    this.tbCostBudgetSet.Rows.Add(row);
                }
                costBudgetSetByProjectCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void ReCalcByRelation(string AStartYm, string AEndYm)
        {
            try
            {
                DataView view = new DataView(this.tb, "RecordType = ''", "", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow dr = view2.Row;
                    if (ConvertRule.ToString(dr["FullCode"]) != "")
                    {
                        DataRow[] drs = this.tbContract.Select("FullCode like '" + ConvertRule.ToString(dr["FullCode"]) + "%'");
                        string[] textArray = new string[] { "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractPay", "ContractPayReal" };
                        string[] textArray2 = CostBudgetPageRule.BuildArrayFieldByYm(AStartYm, AEndYm, "PayoutMoneyYm_");
                        string[] arrColumnName = ConvertRule.ArrayConcat(textArray, textArray2);
                        decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                        int index = -1;
                        foreach (string text in arrColumnName)
                        {
                            index++;
                            dr[text] = numArray[index];
                        }
                        CostBudgetPageRule.CostBudgetDtlCalcField(dr, CostBudgetPageRule.m_DynamicRowType.Contract, null, 0, 0);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshDateRange()
        {
            try
            {
                this.ResetDateTitleHtml();
                this.ResetContractDateRange();
                CostBudgetPageRule.ClearFieldValue(this.tb, new string[] { "PlanDataHtml" });
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    foreach (DataRow row in this.tb.Rows)
                    {
                        string text = "";
                        string text2 = row["RecordType"].ToString();
                        string contractCode = ConvertRule.ToString(row["ContractCode"]);
                        string costCode = ConvertRule.ToString(row["CostCode"]);
                        string text5 = ConvertRule.ToString(row["FullCode"]);
                        string pBSCode = ConvertRule.ToString(row["PBSCode"]);
                        int monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                        for (int i = 0; i < monthsBetween; i++)
                        {
                            string text12;
                            string text7 = StringRule.YmAddMonths(this.StartYm, i);
                            string s = string.Format("{0}-{1}-01", text7.Substring(0, 4), text7.Substring(4, 2));
                            string payoutDateEnd = DateTime.Parse(s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                            string text10 = "PayoutMoneyYm_" + text7;
                            string money = StringRule.BuildShowNumberString(row[text10]);
                            if (pBSCode == "")
                            {
                                text12 = CostBudgetPageRule.GetContractPayRealHref(money, costCode, contractCode, "", s, payoutDateEnd, this.PBSType, pBSCode);
                            }
                            else
                            {
                                text12 = money;
                            }
                            if (text12 == "")
                            {
                                text = text + string.Format("<td>{0}</td>", text12);
                            }
                            else
                            {
                                text = text + string.Format("<td align=right nowrap>{0}</td>", text12);
                            }
                        }
                        row["PlanDataHtml"] = text;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void ResetContractDateRange()
        {
            try
            {
                DropDateRangeColumn(this.tbContract);
                DropDateRangeColumn(this.tb);
                DropDateRangeColumn(this.tbApportion);
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    string contractCode;
                    string costCode;
                    string text3;
                    string name;
                    DataRow row3;
                    DataRow[] rowArray2;
                    int monthsBetween;
                    int monthCount;
                    AddDateRangeColumn(this.tbContract, this.StartYm, this.EndYm);
                    AddDateRangeColumn(this.tb, this.StartYm, this.EndYm);
                    AddDateRangeColumn(this.tbApportion, this.StartYm, this.EndYm);
                    foreach (DataRow row in this.tbContract.Rows)
                    {
                        contractCode = ConvertRule.ToString(row["ContractCode"]);
                        costCode = ConvertRule.ToString(row["CostCode"]);
                        DataRow[] rowArray = this.tbPayout.Select("ContractCode = '" + contractCode + "' and CostCode='" + costCode + "'");
                        foreach (DataRow row2 in rowArray)
                        {
                            text3 = ConvertRule.ToString(row2["PayoutYm"]);
                            name = "PayoutMoneyYm_" + text3;
                            if (this.tbContract.Columns.Contains(name))
                            {
                                decimal totalMoney = ConvertRule.ToDecimal(row2["PayoutMoney"]);
                                row[name] = ConvertRule.ToDecimal(row[name]) + totalMoney;
                                this.DoApportion(contractCode, costCode, ConvertRule.ToString(row2["CostBudgetSetCode"]), ConvertRule.ToString(row["RecordType"]), name, totalMoney);
                            }
                        }
                    }
                    foreach (DataRow row in this.tbContract.Rows)
                    {
                        contractCode = ConvertRule.ToString(row["ContractCode"]);
                        costCode = ConvertRule.ToString(row["CostCode"]);
                        row3 = null;
                        rowArray2 = this.tb.Select("ContractCode = '" + contractCode + "' and CostCode = '" + costCode + "' and RecordType <> '' and RecordType <> 'Apportion'");
                        if (rowArray2.Length > 0)
                        {
                            row3 = rowArray2[0];
                        }
                        if (row3 != null)
                        {
                            monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                            for (monthCount = 0; monthCount < monthsBetween; monthCount++)
                            {
                                text3 = StringRule.YmAddMonths(this.StartYm, monthCount);
                                name = "PayoutMoneyYm_" + text3;
                                row3[name] = row[name];
                            }
                        }
                    }
                    foreach (DataRow row4 in this.tbApportion.Rows)
                    {
                        contractCode = ConvertRule.ToString(row4["ContractCode"]);
                        costCode = ConvertRule.ToString(row4["CostCode"]);
                        string text5 = ConvertRule.ToString(row4["PBSCode"]);
                        row3 = null;
                        rowArray2 = this.tb.Select("ContractCode = '" + contractCode + "' and CostCode = '" + costCode + "' and PBSCode = '" + text5 + "' and RecordType <> ''");
                        if (rowArray2.Length > 0)
                        {
                            row3 = rowArray2[0];
                        }
                        if (row3 != null)
                        {
                            monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                            for (monthCount = 0; monthCount < monthsBetween; monthCount++)
                            {
                                text3 = StringRule.YmAddMonths(this.StartYm, monthCount);
                                name = "PayoutMoneyYm_" + text3;
                                row3[name] = row4[name];
                            }
                        }
                    }
                    this.ReCalcByRelation(this.StartYm, this.EndYm);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ResetDateTitleHtml()
        {
            try
            {
                this.m_DateTitleHtml1 = "";
                this.m_DateTitleHtml2 = "";
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    int beginY = int.Parse(this.StartYm.Substring(0, 4));
                    int endY = int.Parse(this.EndYm.Substring(0, 4));
                    for (int i = beginY; i <= endY; i++)
                    {
                        int iBeginM = 0;
                        int iEndM = 0;
                        this.GetCurrentYearMonthRange(i, beginY, endY, ref iBeginM, ref iEndM);
                        this.m_DateTitleHtml1 = this.m_DateTitleHtml1 + string.Format("<th colspan={1} align=center nowrap>{0}年</th>", i, (iEndM - iBeginM) + 1);
                        for (int j = iBeginM; j <= iEndM; j++)
                        {
                            this.m_DateTitleHtml2 = this.m_DateTitleHtml2 + string.Format("<th align=center nowrap>{0}月</th>", j);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetRelationContract()
        {
            try
            {
                int index;
                DataRow[] rowArray = this.tbContract.Select("RecordType = 'Contract'");
                int length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbContract.Rows.Remove(rowArray[index]);
                }
                rowArray = this.tbPayout.Select("RecordType = 'Contract'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbPayout.Rows.Remove(rowArray[index]);
                }
                rowArray = this.tbApportion.Select("RecordType = 'Contract'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbApportion.Rows.Remove(rowArray[index]);
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string text6;
                    string queryString = "select c.FullCode, s.SupplierName, cb.PBSType, cb.PBSCode, a.*, b.* from ContractCost a left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '" + this.ProjectCode + "' and b.Status in (0, 2) and not exists (select * from ContractChange g where g.ContractCode = b.ContractCode)";
                    if ((this.PBSType != "") || (this.PBSCode != ""))
                    {
                        text6 = queryString;
                        queryString = text6 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                    }
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractMoney");
                    queryString = "select c.FullCode, a.Money as Money, s.SupplierName, cb.PBSType, cb.PBSCode, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '" + this.ProjectCode + "' and b.Status in (0, 2, 4) and a.ContractChangeCode = (select min(ContractChangeCode) from ContractChange g where g.ContractCode = b.ContractCode) and exists (select * from ContractChange g where g.ContractCode = b.ContractCode)";
                    if ((this.PBSType != "") || (this.PBSCode != ""))
                    {
                        text6 = queryString;
                        queryString = text6 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                    }
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.FillContractChangeCostCode(tbSrc);
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractMoney");
                    queryString = "select c.FullCode, a.ChangeMoney as Money, s.SupplierName, cb.PBSType, cb.PBSCode, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode, ContractChange g where a.ContractCode = b.ContractCode and b.ContractCode = g.ContractCode and a.ContractChangeCode = g.ContractChangeCode and b.ProjectCode = '" + this.ProjectCode + "' and b.Status not in (3) and g.Status in (0) and a.ChangeMoney <> 0";
                    if ((this.PBSType != "") || (this.PBSCode != ""))
                    {
                        text6 = queryString;
                        queryString = text6 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                    }
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.FillContractChangeCostCode(tbSrc);
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractChangeMoney");
                    foreach (DataRow row in this.tbContract.Rows)
                    {
                        string contractCode = ConvertRule.ToString(row["ContractCode"]);
                        string costCode = ConvertRule.ToString(row["CostCode"]);
                        string text4 = ConvertRule.ToString(row["AllContractCode"]);
                        string text5 = ConvertRule.ToString(row["AllocateCodes"]);
                        if ((text4 != "") && (text5 != ""))
                        {
                            queryString = "select a.CostBudgetSetCode, cb.PBSType, cb.PBSCode, sum(isnull(a.ItemMoney, 0)) as Money from PaymentItem a left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '" + this.ProjectCode + "' and b.Status in (1, 2) and b.ContractCode in (" + text4 + ") and a.AllocateCode in (" + text5 + ")";
                            if ((this.PBSType != "") || (this.PBSCode != ""))
                            {
                                text6 = queryString;
                                queryString = text6 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                            }
                            queryString = queryString + " group by a.CostBudgetSetCode, cb.PBSType, cb.PBSCode";
                            tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                            foreach (DataRow row2 in tbSrc.Rows)
                            {
                                row["ContractPay"] = ConvertRule.ToDecimal(row["ContractPay"]) + ConvertRule.ToDecimal(row2["Money"]);
                                this.DoApportion(contractCode, costCode, ConvertRule.ToString(row2["CostBudgetSetCode"]), ConvertRule.ToString(row["RecordType"]), "ContractPay", ConvertRule.ToDecimal(row2["Money"]));
                            }
                            queryString = "select a.PayoutCode, b.PayoutDate, a.PayoutMoney as Money, '" + contractCode + "' as ContractCode, mi.CostCode, c.FullCode, mi.CostBudgetSetCode, cb.PBSType, cb.PBSCode from PayoutItem a inner join Payout b on b.PayoutCode = a.PayoutCode, PaymentItem mi left join CBS c on c.CostCode = mi.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = mi.CostBudgetSetCode, Payment m where a.PaymentItemCode = mi.PaymentItemCode and mi.PaymentCode = m.PaymentCode and m.ProjectCode = '" + this.ProjectCode + "' and m.ContractCode in (" + text4 + ") and mi.AllocateCode in (" + text5 + ")";
                            if (PaymentRule.IsPayoutMoneyIncludeNotCheck == 0)
                            {
                                queryString = queryString + " and b.Status in (1)";
                            }
                            if ((this.PBSType != "") || (this.PBSCode != ""))
                            {
                                text6 = queryString;
                                queryString = text6 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                            }
                            tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                            this.AddPayout(tbSrc, "Contract");
                            foreach (DataRow row2 in tbSrc.Rows)
                            {
                                row["ContractPayReal"] = ConvertRule.ToDecimal(row["ContractPayReal"]) + ConvertRule.ToDecimal(row2["Money"]);
                                this.DoApportion(contractCode, costCode, ConvertRule.ToString(row2["CostBudgetSetCode"]), ConvertRule.ToString(row["RecordType"]), "ContractPayReal", ConvertRule.ToDecimal(row2["Money"]));
                            }
                            CostBudgetPageRule.CostBudgetDtlCalcField(row, CostBudgetPageRule.m_DynamicRowType.Contract, null, 0, 0);
                        }
                    }
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
        }

        private void SetRelationNoContract()
        {
            try
            {
                int index;
                DataRow[] rowArray = this.tbContract.Select("RecordType = 'NoContract'");
                int length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbContract.Rows.Remove(rowArray[index]);
                }
                rowArray = this.tbPayout.Select("RecordType = 'NoContract'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbPayout.Rows.Remove(rowArray[index]);
                }
                rowArray = this.tbApportion.Select("RecordType = 'NoContract'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbApportion.Rows.Remove(rowArray[index]);
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string text4;
                    string queryString = "select 'Payment_' + a.PaymentCode as ContractCode, '' as ContractID, '非合同请款' + b.PaymentID as ContractName, a.CostCode, c.FullCode, b.SupplyCode as SupplierCode, b.SupplyName as SupplierName, a.ItemMoney as Money, a.CostBudgetSetCode, cb.PBSType, cb.PBSCode from PaymentItem a left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '" + this.ProjectCode + "' and isnull(b.IsContract, 0) = 0";
                    if ((this.PBSType != "") || (this.PBSCode != ""))
                    {
                        text4 = queryString;
                        queryString = text4 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                    }
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "NoContract", "ContractMoney");
                    queryString = "select 'Payment_' + a.PaymentCode as ContractCode, '' as ContractID, '非合同请款' + b.PaymentID as ContractName, a.CostCode, c.FullCode, b.SupplyCode as SupplierCode, b.SupplyName as SupplierName, a.ItemMoney as Money, a.CostBudgetSetCode, cb.PBSType, cb.PBSCode from PaymentItem a left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '" + this.ProjectCode + "' and b.Status in (1, 2) and isnull(b.IsContract, 0) = 0";
                    if ((this.PBSType != "") || (this.PBSCode != ""))
                    {
                        text4 = queryString;
                        queryString = text4 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                    }
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "NoContract", "ContractPay");
                    queryString = "select mi.CostCode, c.FullCode, 'Payment_' + m.PaymentCode as ContractCode, a.PayoutCode, b.PayoutDate, a.PayoutMoney as Money, mi.CostBudgetSetCode, cb.PBSType, cb.PBSCode from PayoutItem a inner join Payout b on b.PayoutCode = a.PayoutCode, PaymentItem mi left join CBS c on c.CostCode = mi.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = mi.CostBudgetSetCode, Payment m where a.PaymentItemCode = mi.PaymentItemCode and mi.PaymentCode = m.PaymentCode and m.ProjectCode = '" + this.ProjectCode + "' and isnull(m.IsContract, 0) = 0";
                    if ((this.PBSType != "") || (this.PBSCode != ""))
                    {
                        text4 = queryString;
                        queryString = text4 + " and cb.PBSType = '" + this.PBSType + "' and cb.PBSCode = '" + this.PBSCode + "'";
                    }
                    if (PaymentRule.IsPayoutMoneyIncludeNotCheck == 0)
                    {
                        queryString = queryString + " and b.Status in (1)";
                    }
                    DataTable table2 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    DataView view = new DataView(this.tbContract, "RecordType = 'NoContract'", "", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        DataRow row = view2.Row;
                        string costCode = ConvertRule.ToString(row["CostCode"]);
                        string contractCode = ConvertRule.ToString(row["ContractCode"]);
                        DataRow[] drsSrc = table2.Select("CostCode = '" + costCode + "' and ContractCode = '" + contractCode + "'");
                        this.AddPayout(drsSrc, "Contract");
                        foreach (DataRow row2 in drsSrc)
                        {
                            row["ContractPayReal"] = ConvertRule.ToDecimal(row["ContractPayReal"]) + ConvertRule.ToDecimal(row2["Money"]);
                            this.DoApportion(contractCode, costCode, ConvertRule.ToString(row2["CostBudgetSetCode"]), ConvertRule.ToString(row["RecordType"]), "ContractPayReal", ConvertRule.ToDecimal(row2["Money"]));
                        }
                    }
                    CostBudgetPageRule.CostBudgetDtlCalcAllRow(this.tbContract, CostBudgetPageRule.m_DynamicRowType.Contract, null, 0, 0);
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
        }

        public string DateTitleHtml1
        {
            get
            {
                return this.m_DateTitleHtml1;
            }
        }

        public string DateTitleHtml2
        {
            get
            {
                return this.m_DateTitleHtml2;
            }
        }

        public DataSet ds
        {
            get
            {
                return this.m_ds;
            }
        }

        public string EndYm
        {
            get
            {
                return this.m_EndYm;
            }
            set
            {
                this.m_EndYm = value;
            }
        }

        public string PBSCode
        {
            get
            {
                return this.m_PBSCode;
            }
            set
            {
                this.m_PBSCode = value;
            }
        }

        public string PBSType
        {
            get
            {
                return this.m_PBSType;
            }
            set
            {
                this.m_PBSType = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this.m_ProjectCode;
            }
        }

        public string StartYm
        {
            get
            {
                return this.m_StartYm;
            }
            set
            {
                this.m_StartYm = value;
            }
        }

        public DataTable tb
        {
            get
            {
                return this.m_ds.Tables["Dtl"];
            }
        }

        public DataTable tbApportion
        {
            get
            {
                return this.m_ds.Tables["Apportion"];
            }
        }

        public DataTable tbContract
        {
            get
            {
                return this.m_ds.Tables["Contract"];
            }
        }

        public DataTable tbCostBudgetSet
        {
            get
            {
                return this.m_ds.Tables["CostBudgetSet"];
            }
        }

        public DataTable tbPayout
        {
            get
            {
                return this.m_ds.Tables["Payout"];
            }
        }
    }
}

