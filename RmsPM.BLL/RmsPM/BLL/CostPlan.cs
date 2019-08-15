namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;

    public class CostPlan
    {
        private string m_CostBudgetCode = "";
        private string m_CostBudgetSetCode = "";
        private string m_DateTitleHtml1 = "";
        private string m_DateTitleHtml2 = "";
        private DataSet m_ds = null;
        private string m_EndYm = "";
        private string m_ProjectCode = "";
        private string m_StartYm = "";
        public bool ReadOnly = true;

        public CostPlan(string a_ProjectCode, string a_CostBudgetSetCode)
        {
            this.m_ProjectCode = a_ProjectCode;
            this.m_CostBudgetSetCode = a_CostBudgetSetCode;
            this.InitValidCostBudgetInfo();
            this.CreateTable();
        }

        private static void AddDateRangeColumn(DataTable tb, string StartYm, string EndYm)
        {
            try
            {
                int monthsBetween = StringRule.GetMonthsBetween(StartYm, EndYm);
                for (int i = 0; i < monthsBetween; i++)
                {
                    string text = StringRule.YmAddMonths(StartYm, i);
                    tb.Columns.Add("PlanMoneyYm_" + text, typeof(decimal));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void AddPlan(DataRow[] drsSrc, string RecordType)
        {
            try
            {
                foreach (DataRow row in drsSrc)
                {
                    DataRow row2;
                    string text = ConvertRule.ToString(row["ContractCode"]);
                    string text2 = ConvertRule.ToString(row["CostCode"]);
                    string text3 = ConvertRule.ToString(row["PlanYm"]);
                    DataRow[] rowArray = this.tbPlan.Select("ContractCode = '" + text + "' and CostCode='" + text2 + "' and PlanYm = '" + text3 + "'");
                    if (rowArray.Length == 0)
                    {
                        row2 = this.tbPlan.NewRow();
                        row2["RecordType"] = RecordType;
                        row2["ContractCode"] = text;
                        row2["CostCode"] = text2;
                        row2["FullCode"] = row["FullCode"];
                        row2["PlanYm"] = row["PlanYm"];
                        this.tbPlan.Rows.Add(row2);
                    }
                    else
                    {
                        row2 = rowArray[0];
                    }
                    row2["PlanMoney"] = ConvertRule.ToDecimal(row2["PlanMoney"]) + ConvertRule.ToDecimal(row["PlanMoney"]);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void AddPlan(DataTable tbSrc, string RecordType)
        {
            try
            {
                DataRow[] drsSrc = tbSrc.Select("");
                this.AddPlan(drsSrc, RecordType);
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
                    string text = ConvertRule.ToString(row["ContractCode"]);
                    DataRow[] rowArray = tbContract.Select("ContractCode = '" + text + "' and CostCode='" + ConvertRule.ToString(row["CostCode"]) + "'");
                    if (rowArray.Length == 0)
                    {
                        row2 = tbContract.NewRow();
                        row2["RecordType"] = RecordType;
                        row2["ContractCode"] = text;
                        row2["ContractID"] = row["ContractID"];
                        row2["ContractName"] = row["ContractName"];
                        if (tbSrc.Columns.Contains("SupplierCode"))
                        {
                            row2["SupplierName"] = row["SupplierName"];
                            row2["SupplierCode"] = row["SupplierCode"];
                        }
                        row2["CostCode"] = row["CostCode"];
                        row2["FullCode"] = row["FullCode"];
                        tbContract.Rows.Add(row2);
                    }
                    else
                    {
                        row2 = rowArray[0];
                    }
                    string text2 = ConvertRule.ToString(row2["AllContractCode"]);
                    if (text2.IndexOf("'" + text + "'") < 0)
                    {
                        if (text2 != "")
                        {
                            text2 = text2 + ",";
                        }
                        text2 = text2 + "'" + text + "'";
                    }
                    row2["AllContractCode"] = text2;
                    row2[MoneyField] = ConvertRule.ToDecimal(row2[MoneyField]) + ConvertRule.ToDecimal(row["Money"]);
                    CostBudgetPageRule.CostBudgetDtlCalcField(row2, CostBudgetPageRule.m_DynamicRowType.Contract, null, 0, 0);
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
                DataTable table3 = new DataTable("Plan");
                table3.Columns.Add("ContractCode");
                table3.Columns.Add("CostCode");
                table3.Columns.Add("FullCode");
                table3.Columns.Add("PlanYm");
                table3.Columns.Add("PlanMoney", typeof(decimal));
                table3.Columns.Add("RecordType");
                this.m_ds.Tables.Add(table3);
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
                    if (column.ColumnName.StartsWith("PlanMoneyYm_"))
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

        public void Generate()
        {
            try
            {
                this.SetRelationContract();
                this.SetRelationBidding();
                this.SetRelationNoContract();
                this.SetRelationBalance();
                DataView view = new DataView(CostBudgetRule.GetAllCBSBySet(this.ProjectCode, this.CostBudgetSetCode).CurrentTable, "", "SortID, Deep", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow drDst;
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
                    bool flag = false;
                    DataRow[] rowArray = this.tbContract.Select("CostCode = '" + text + "'");
                    foreach (DataRow row3 in rowArray)
                    {
                        drDst = this.tb.NewRow();
                        ConvertRule.DataRowCopy(row3, drDst, this.tbContract, this.tb);
                        drDst["DtlCode"] = "C_" + text + ":" + drDst["ContractCode"].ToString();
                        drDst["RecordType"] = row3["RecordType"];
                        drDst["Deep"] = row2["Deep"];
                        drDst["ParentCode"] = row2["ParentCode"];
                        drDst["ChildCount"] = 0;
                        string text2 = ConvertRule.ToString(row3["RecordType"]);
                        if ((text2 != null) && (text2 == "Balance"))
                        {
                            drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdBalance;
                            flag = true;
                        }
                        else
                        {
                            drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdContract;
                        }
                        this.tb.Rows.Add(drDst);
                    }
                    if (!this.ReadOnly && !((ConvertRule.ToInt(row2["ChildCount"]) != 0) || flag))
                    {
                        drDst = this.tb.NewRow();
                        drDst["ContractCode"] = "Balance_" + text;
                        drDst["ContractName"] = "预留金额";
                        drDst["CostCode"] = text;
                        drDst["FullCode"] = row2["FullCode"];
                        drDst["DtlCode"] = "C_" + text + ":" + drDst["ContractCode"].ToString();
                        drDst["RecordType"] = "Balance";
                        drDst["Deep"] = row2["Deep"];
                        drDst["ParentCode"] = row2["ParentCode"];
                        drDst["ChildCount"] = 0;
                        drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdBalance;
                        this.tb.Rows.Add(drDst);
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

        public static string GetContractHref(object objContractCode, object objContractName, object objRecordType)
        {
            string text3;
            try
            {
                string contractHref = "";
                if (ConvertRule.ToString(objRecordType) != "Balance")
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

        private void InitValidCostBudgetInfo()
        {
            try
            {
                EntityData data = CostBudgetRule.GetValidCostBudget(this.m_CostBudgetSetCode, 0, false);
                if (data.HasRecord())
                {
                    this.m_CostBudgetCode = data.GetString("CostBudgetCode");
                }
                data.Dispose();
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
                        string[] textArray2 = CostBudgetPageRule.BuildArrayFieldByYm(AStartYm, AEndYm, "PlanMoneyYm_");
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
                int num = -1;
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    foreach (DataRow row in this.tb.Rows)
                    {
                        num++;
                        string text = "";
                        string text2 = row["RecordType"].ToString();
                        string text3 = ConvertRule.ToString(row["ContractCode"]);
                        string text4 = ConvertRule.ToString(row["CostCode"]);
                        string text5 = ConvertRule.ToString(row["FullCode"]);
                        string text6 = ConvertRule.ToString(row["ParentCode"]);
                        string text7 = ConvertRule.ToString(row["ClassTd"]);
                        int monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                        for (int i = 0; i < monthsBetween; i++)
                        {
                            string text8 = StringRule.YmAddMonths(this.StartYm, i);
                            string text10 = DateTime.Parse(string.Format("{0}-{1}-01", text8.Substring(0, 4), text8.Substring(4, 2))).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                            string text11 = "PlanMoneyYm_" + text8;
                            string text12 = StringRule.BuildShowNumberString(row[text11]);
                            if (this.ReadOnly)
                            {
                                text = text + string.Format("<td align=right nowrap class=\"{1}\">{0}</td>", text12, text7);
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
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    string text;
                    string text2;
                    string text3;
                    string name;
                    AddDateRangeColumn(this.tbContract, this.StartYm, this.EndYm);
                    AddDateRangeColumn(this.tb, this.StartYm, this.EndYm);
                    int monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                    foreach (DataRow row in this.tbContract.Rows)
                    {
                        text = ConvertRule.ToString(row["ContractCode"]);
                        text2 = ConvertRule.ToString(row["CostCode"]);
                        DataRow[] rowArray = this.tbPlan.Select("ContractCode = '" + text + "' and CostCode='" + text2 + "'");
                        foreach (DataRow row2 in rowArray)
                        {
                            text3 = ConvertRule.ToString(row2["PlanYm"]);
                            name = "PlanMoneyYm_" + text3;
                            if (this.tbContract.Columns.Contains(name))
                            {
                                row[name] = ConvertRule.ToDecimal(row[name]) + ConvertRule.ToDecimal(row2["PlanMoney"]);
                            }
                        }
                    }
                    foreach (DataRow row in this.tbContract.Rows)
                    {
                        text = ConvertRule.ToString(row["ContractCode"]);
                        text2 = ConvertRule.ToString(row["CostCode"]);
                        DataRow row3 = null;
                        DataRow[] rowArray2 = this.tb.Select("ContractCode = '" + text + "' and CostCode = '" + text2 + "' and RecordType <> ''");
                        if (rowArray2.Length > 0)
                        {
                            row3 = rowArray2[0];
                        }
                        if (row3 != null)
                        {
                            for (int i = 0; i < monthsBetween; i++)
                            {
                                text3 = StringRule.YmAddMonths(this.StartYm, i);
                                name = "PlanMoneyYm_" + text3;
                                row3[name] = row[name];
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
                    int monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                    for (int i = 0; i < monthsBetween; i++)
                    {
                        string text = StringRule.YmAddMonths(this.StartYm, i);
                        this.m_DateTitleHtml2 = this.m_DateTitleHtml2 + string.Format("<th align=center nowrap>{0}年{1}月</th>", text.Substring(0, 4), text.Substring(4, 2));
                    }
                    this.m_DateTitleHtml1 = string.Format("<th colspan=\"{0}\">预计发生</th>", monthsBetween);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetRelationBalance()
        {
            try
            {
                int index;
                DataRow[] rowArray = this.tbContract.Select("RecordType = 'Balance'");
                int length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbContract.Rows.Remove(rowArray[index]);
                }
                rowArray = this.tbPlan.Select("RecordType = 'Balance'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbPlan.Rows.Remove(rowArray[index]);
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    DataTable tbSrc;
                    string queryString;
                    if (this.CostBudgetCode != "")
                    {
                        queryString = "select c.FullCode, a.CostCode, 'Balance' as RecordType, 'Balance_' + a.CostCode as ContractCode, '' as ContractID, '预留金额' as ContractName, a.BudgetMoney as Money from CostBudgetDtl a left join CBS c on c.CostCode = a.CostCode where a.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetCode = '" + this.CostBudgetCode + "' and not exists(select * from CBS CBSCount where CBSCount.ParentCode = a.CostCode)";
                        tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        this.AddRelationContractRow(this.tbContract, tbSrc, "Balance", "ContractMoney");
                    }
                    if (this.CostBudgetCode != "")
                    {
                        queryString = "select c.FullCode, a.CostCode, 'Balance' as RecordType, 'Balance_' + a.CostCode as ContractCode, right('0000' + cast(b.IYear as varchar), 4) + right('00' + cast(b.IMonth as varchar), 2) as PlanYm, b.BudgetMoney as PlanMoney from CostBudgetDtl a left join CBS c on c.CostCode = a.CostCode, CostBudgetMonth b where a.CostBudgetDtlCode = b.CostBudgetDtlCode and a.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetCode = '" + this.CostBudgetCode + "' and right('0000' + cast(b.IYear as varchar), 4) + right('00' + cast(b.IMonth as varchar), 2) between '" + this.StartYm + "' and '" + this.EndYm + "'";
                        tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        this.AddPlan(tbSrc, "");
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

        private void SetRelationBidding()
        {
            try
            {
                int index;
                DataRow[] rowArray = this.tbContract.Select("RecordType = 'Bidding'");
                int length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbContract.Rows.Remove(rowArray[index]);
                }
                rowArray = this.tbPlan.Select("RecordType = 'Bidding'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbPlan.Rows.Remove(rowArray[index]);
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = "select c.FullCode, 'Bidding' as RecordType, 'Bidding_' + b.BiddingCode + '#' + b.BiddingDtlCode as ContractCode, '' as ContractID, a.title + '(' + b.title + ')' as ContractName, b.* from Bidding a , BiddingDtl b left join CBS c on c.CostCode = b.CostCode where a.ProjectCode = '" + this.ProjectCode + "' and b.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and isnull(a.state, 0) = 0 and a.BiddingCode = b.BiddingCode";
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Bidding", "ContractMoney");
                    queryString = "select c.FullCode, 'Bidding' as RecordType, 'Bidding_' + b.BiddingCode + '#' + a.BiddingDtlCode as ContractCode, a.CostCode, convert(varchar(6), p.PlanningPayDate, 112) as PlanYm, sum(isnull(p.Money, 0)) as PlanMoney from BiddingDtl a left join CBS c on c.CostCode = a.CostCode, Bidding b, BiddingDtlPlan p where a.BiddingCode = b.BiddingCode and a.BiddingDtlCode = p.BiddingDtlCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and convert(varchar(6), p.PlanningPayDate, 112) between '" + this.StartYm + "' and '" + this.EndYm + "' group by c.FullCode, a.CostCode, 'Bidding_' + b.BiddingCode + '#' + a.BiddingDtlCode, convert(varchar(6), p.PlanningPayDate, 112)";
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddPlan(tbSrc, "");
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
                rowArray = this.tbPlan.Select("RecordType = 'Contract'");
                length = rowArray.Length;
                for (index = length - 1; index >= 0; index--)
                {
                    this.tbPlan.Rows.Remove(rowArray[index]);
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = "select c.FullCode, s.SupplierName, a.*, b.* from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status in (0, 2) and not exists (select * from ContractChange g where g.ContractCode = b.ContractCode)";
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractMoney");
                    queryString = "select c.FullCode, a.Money as Money, s.SupplierName, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status in (0, 2, 4) and a.ContractChangeCode = (select min(ContractChangeCode) from ContractChange g where g.ContractCode = b.ContractCode) and exists (select * from ContractChange g where g.ContractCode = b.ContractCode)";
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractMoney");
                    queryString = "select c.FullCode, a.ChangeMoney as Money, s.SupplierName, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode, ContractChange g where a.ContractCode = b.ContractCode and b.ContractCode = g.ContractCode and a.ContractChangeCode = g.ContractChangeCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status not in (3) and g.Status in (0) and a.ChangeMoney <> 0";
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractChangeMoney");
                    queryString = "select c.FullCode, s.SupplierName, a.*, b.* from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status in (1, 7)";
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractApplyMoney");
                    queryString = "select c.FullCode, s.SupplierName, a.ChangeMoney as Money, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode, ContractChange g where a.ContractCode = b.ContractCode and b.ContractCode = g.ContractCode and a.ContractChangeCode = g.ContractChangeCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status not in (3) and g.Status in (1, 2) and a.ChangeMoney <> 0";
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddRelationContractRow(this.tbContract, tbSrc, "Contract", "ContractApplyMoney");
                    queryString = "select c.FullCode, 'Contract' as RecordType, a.CostCode, a.ContractCode, convert(varchar(6), p.PlanningPayDate, 112) as PlanYm, sum(isnull(p.Money, 0)) as PlanMoney from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b, ContractCostPlan p where a.ContractCode = b.ContractCode and a.ContractCostCode = p.ContractCostCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and convert(varchar(6), p.PlanningPayDate, 112) between '" + this.StartYm + "' and '" + this.EndYm + "' group by c.FullCode, a.CostCode, a.ContractCode, convert(varchar(6), p.PlanningPayDate, 112)";
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.AddPlan(tbSrc, "");
                    foreach (DataRow row in this.tbContract.Rows)
                    {
                        string text2 = ConvertRule.ToString(row["ContractCode"]);
                        if (ConvertRule.ToString(row["AllContractCode"]) != "")
                        {
                            queryString = "select sum(isnull(a.ItemMoney, 0)) from PaymentItem a, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status in (1, 2) and b.ContractCode in (" + ConvertRule.ToString(row["AllContractCode"]) + ") and a.CostCode = '" + ConvertRule.ToString(row["CostCode"]) + "'";
                            row["ContractPay"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            queryString = "select a.PayoutCode, b.PayoutDate, a.PayoutMoney as Money, '" + text2 + "' as ContractCode, mi.CostCode, c.FullCode from PayoutItem a inner join Payout b on b.PayoutCode = a.PayoutCode, PaymentItem mi left join CBS c on c.CostCode = mi.CostCode, Payment m where a.PaymentItemCode = mi.PaymentItemCode and mi.PaymentCode = m.PaymentCode and m.ProjectCode = '" + this.ProjectCode + "' and mi.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and m.ContractCode in (" + ConvertRule.ToString(row["AllContractCode"]) + ") and mi.CostCode = '" + ConvertRule.ToString(row["CostCode"]) + "'";
                            tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                            row["ContractPayReal"] = MathRule.SumColumn(tbSrc, "Money");
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

        private void SetRelationNoContract()
        {
            try
            {
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string CostBudgetCode
        {
            get
            {
                return this.m_CostBudgetCode;
            }
        }

        public string CostBudgetSetCode
        {
            get
            {
                return this.m_CostBudgetSetCode;
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

        public DataTable tbContract
        {
            get
            {
                return this.m_ds.Tables["Contract"];
            }
        }

        public DataTable tbPlan
        {
            get
            {
                return this.m_ds.Tables["Plan"];
            }
        }
    }
}

