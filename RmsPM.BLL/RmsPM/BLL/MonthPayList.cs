namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;

    public class MonthPayList
    {
        private string m_CostBudgetSetCode = "";
        private string m_DateTitleHtml1 = "";
        private string m_DateTitleHtml2 = "";
        private DataSet m_ds = null;
        private string m_EndYm = "";
        private e_PayType m_PayType = e_PayType.payment;
        private string m_ProjectCode = "";
        private string m_StartYm = "";

        public MonthPayList(string a_ProjectCode, string a_CostBudgetSetCode)
        {
            this.m_ProjectCode = a_ProjectCode;
            this.m_CostBudgetSetCode = a_CostBudgetSetCode;
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
                    tb.Columns.Add("PayoutMoneyYm_" + text, typeof(decimal));
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
                table.Columns.Add("PayoutMoney", typeof(decimal));
                table.Columns.Add("PlanDataHtml");
                this.m_ds.Tables.Add(table);
                DataTable table2 = new DataTable("Payout");
                table2.Columns.Add("CostCode");
                table2.Columns.Add("FullCode");
                table2.Columns.Add("PayoutMoney", typeof(decimal));
                this.m_ds.Tables.Add(table2);
                DataTable table3 = new DataTable("PayoutYm");
                table3.Columns.Add("CostCode");
                table3.Columns.Add("FullCode");
                table3.Columns.Add("PayoutYm");
                table3.Columns.Add("PayoutMoney", typeof(decimal));
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

        public void Generate()
        {
            try
            {
                if (this.m_PayType == e_PayType.payout)
                {
                    this.SetPayout();
                }
                else
                {
                    this.SetPayment();
                }
                DataView view = new DataView(CostBudgetRule.GetAllCBSBySet(this.ProjectCode, this.CostBudgetSetCode).CurrentTable, "", "SortID, Deep", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow row = view2.Row;
                    DataRow row2 = this.tb.NewRow();
                    string text = row["CostCode"].ToString();
                    row2["DtlCode"] = row["CostCode"];
                    row2["CostCode"] = row["CostCode"];
                    row2["CostName"] = row["CostName"];
                    row2["SortID"] = row["SortID"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    row2["FullCode"] = row["FullCode"];
                    row2["ChildCount"] = row["ChildCount"];
                    row2["IsLeafCBS"] = ConvertRule.ToInt(row2["ChildCount"]) <= 0;
                    this.tb.Rows.Add(row2);
                }
                DropDateRangeColumn(this.tb);
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    AddDateRangeColumn(this.tb, this.StartYm, this.EndYm);
                }
                this.ReCalcByRelation();
                this.RefreshDateRange();
            }
            catch (Exception exception)
            {
                throw exception;
            }
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

        public static string GetPayoutHref(object Money, object CostCode, object PayoutDateBegin, object PayoutDateEnd)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToString(Money) != "")
                {
                    text = string.Format("<a href=\"#\" onclick=\"ViewPayout('{1}', '{2}', '{3}');return false;\">{0}</a>", new object[] { ConvertRule.ToString(Money), ConvertRule.ToString(CostCode), ConvertRule.ToString(PayoutDateBegin), ConvertRule.ToString(PayoutDateEnd) });
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private void ReCalcByRelation()
        {
            try
            {
                DataView view = new DataView(this.tb, "", "", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow row = view2.Row;
                    if (ConvertRule.ToString(row["FullCode"]) != "")
                    {
                        DataRow[] drs = this.tbPayout.Select("FullCode like '" + ConvertRule.ToString(row["FullCode"]) + "%'");
                        string[] arrColumnName = new string[] { "PayoutMoney" };
                        decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                        int index = -1;
                        foreach (string text in arrColumnName)
                        {
                            index++;
                            row[text] = numArray[index];
                        }
                        if ((this.StartYm != "") && (this.EndYm != ""))
                        {
                            int monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                            for (int i = 0; i < monthsBetween; i++)
                            {
                                string text2 = StringRule.YmAddMonths(this.StartYm, i);
                                decimal num4 = MathRule.SumColumn(this.tbPayoutYm.Select("FullCode like '" + ConvertRule.ToString(row["FullCode"]) + "%' and PayoutYm = '" + text2 + "'"), "PayoutMoney");
                                row["PayoutMoneyYm_" + text2] = num4;
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

        public void RefreshDateRange()
        {
            try
            {
                this.ResetDateTitleHtml();
                CostBudgetPageRule.ClearFieldValue(this.tb, new string[] { "PlanDataHtml" });
                if ((this.StartYm != "") && (this.EndYm != ""))
                {
                    foreach (DataRow row in this.tb.Rows)
                    {
                        string costCode = ConvertRule.ToString(row["CostCode"]);
                        string text2 = "";
                        int monthsBetween = StringRule.GetMonthsBetween(this.StartYm, this.EndYm);
                        for (int i = 0; i < monthsBetween; i++)
                        {
                            string text3 = StringRule.YmAddMonths(this.StartYm, i);
                            string s = string.Format("{0}-{1}-01", text3.Substring(0, 4), text3.Substring(4, 2));
                            string payoutDateEnd = DateTime.Parse(s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                            string text6 = "PayoutMoneyYm_" + text3;
                            string text8 = GetPayoutHref(StringRule.BuildShowNumberString(row[text6]), costCode, s, payoutDateEnd);
                            if (text8 == "")
                            {
                                text2 = text2 + string.Format("<td>{0}</td>", text8);
                            }
                            else
                            {
                                text2 = text2 + string.Format("<td align=right nowrap>{0}</td>", text8);
                            }
                        }
                        row["PlanDataHtml"] = text2;
                    }
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

        private void SetPayment()
        {
            try
            {
                this.tbPayout.Rows.Clear();
                this.tbPayout.AcceptChanges();
                this.tbPayoutYm.Rows.Clear();
                this.tbPayoutYm.AcceptChanges();
                QueryAgent agent = new QueryAgent();
                try
                {
                    DataRow row2;
                    string queryString = "select a.CostCode, c.FullCode, sum(isnull(a.ItemMoney, 0)) as PayoutMoney from PaymentItem a inner join Payment b on b.PaymentCode = a.PaymentCode left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode where b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status in (1,2) group by a.CostCode, c.FullCode";
                    DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        row2 = this.tbPayout.NewRow();
                        row2["CostCode"] = row["CostCode"];
                        row2["FullCode"] = row["FullCode"];
                        row2["PayoutMoney"] = row["PayoutMoney"];
                        this.tbPayout.Rows.Add(row2);
                    }
                    if ((this.StartYm != "") && (this.EndYm != ""))
                    {
                        queryString = "select a.CostCode, c.FullCode, convert(varchar(6), b.PayDate, 112) as PayoutYm, sum(isnull(a.ItemMoney, 0)) as PayoutMoney from PaymentItem a inner join Payment b on b.PaymentCode = a.PaymentCode left join CBS c on c.CostCode = a.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = a.CostBudgetSetCode where b.ProjectCode = '" + this.ProjectCode + "' and a.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and convert(varchar(6), b.PayDate, 112) between '" + this.StartYm + "' and '" + this.EndYm + "' and b.Status in (1,2) group by a.CostCode, c.FullCode, convert(varchar(6), b.PayDate, 112)";
                        table = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row in table.Rows)
                        {
                            row2 = this.tbPayoutYm.NewRow();
                            row2["CostCode"] = row["CostCode"];
                            row2["FullCode"] = row["FullCode"];
                            row2["PayoutYm"] = row["PayoutYm"];
                            row2["PayoutMoney"] = row["PayoutMoney"];
                            this.tbPayoutYm.Rows.Add(row2);
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

        private void SetPayout()
        {
            try
            {
                this.tbPayout.Rows.Clear();
                this.tbPayout.AcceptChanges();
                this.tbPayoutYm.Rows.Clear();
                this.tbPayoutYm.AcceptChanges();
                QueryAgent agent = new QueryAgent();
                try
                {
                    DataRow row2;
                    string queryString = "select mi.CostCode, c.FullCode, sum(isnull(a.PayoutMoney, 0)) as PayoutMoney from PayoutItem a inner join Payout b on b.PayoutCode = a.PayoutCode, PaymentItem mi left join CBS c on c.CostCode = mi.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = mi.CostBudgetSetCode where a.PaymentItemCode = mi.PaymentItemCode and b.ProjectCode = '" + this.ProjectCode + "' and mi.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and b.Status in (1,2) group by mi.CostCode, c.FullCode";
                    DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        row2 = this.tbPayout.NewRow();
                        row2["CostCode"] = row["CostCode"];
                        row2["FullCode"] = row["FullCode"];
                        row2["PayoutMoney"] = row["PayoutMoney"];
                        this.tbPayout.Rows.Add(row2);
                    }
                    if ((this.StartYm != "") && (this.EndYm != ""))
                    {
                        queryString = "select mi.CostCode, c.FullCode, convert(varchar(6), b.PayoutDate, 112) as PayoutYm, sum(isnull(a.PayoutMoney, 0)) as PayoutMoney from PayoutItem a inner join Payout b on b.PayoutCode = a.PayoutCode, PaymentItem mi left join CBS c on c.CostCode = mi.CostCode left join CostBudgetSet cb on cb.CostBudgetSetCode = mi.CostBudgetSetCode where a.PaymentItemCode = mi.PaymentItemCode and b.ProjectCode = '" + this.ProjectCode + "' and mi.CostBudgetSetCode = '" + this.CostBudgetSetCode + "' and convert(varchar(6), b.PayoutDate, 112) between '" + this.StartYm + "' and '" + this.EndYm + "' and b.Status in (1,2) group by mi.CostCode, c.FullCode, convert(varchar(6), b.PayoutDate, 112)";
                        table = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row in table.Rows)
                        {
                            row2 = this.tbPayoutYm.NewRow();
                            row2["CostCode"] = row["CostCode"];
                            row2["FullCode"] = row["FullCode"];
                            row2["PayoutYm"] = row["PayoutYm"];
                            row2["PayoutMoney"] = row["PayoutMoney"];
                            this.tbPayoutYm.Rows.Add(row2);
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

        public string CostBudgetSetCode
        {
            get
            {
                return this.m_CostBudgetSetCode;
            }
            set
            {
                this.m_CostBudgetSetCode = value;
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

        public e_PayType PayType
        {
            get
            {
                return this.m_PayType;
            }
            set
            {
                this.m_PayType = value;
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

        public DataTable tbPayout
        {
            get
            {
                return this.m_ds.Tables["Payout"];
            }
        }

        public DataTable tbPayoutYm
        {
            get
            {
                return this.m_ds.Tables["PayoutYm"];
            }
        }

        public enum e_PayType
        {
            payment,
            payout
        }
    }
}

