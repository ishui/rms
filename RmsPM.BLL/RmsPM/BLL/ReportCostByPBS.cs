namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class ReportCostByPBS
    {
        private DataSet m_ds = null;
        private string m_EndDate = "";
        private string m_ProjectCode = "";
        private string m_StartDate = "";
        private string m_TitleHtml1 = "";
        private string m_TitleHtmlArea = "";
        private string m_TitleHtmlAreaPercent = "";
        private decimal m_TotalPBSArea = 0M;

        public ReportCostByPBS(string a_ProjectCode)
        {
            this.m_ProjectCode = a_ProjectCode;
            this.CreateTable();
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
                table.Columns.Add("Money", typeof(decimal));
                table.Columns.Add("MoneyHtml");
                this.m_ds.Tables.Add(table);
                DataTable table2 = new DataTable("Payment");
                table2.Columns.Add("CostCode");
                table2.Columns.Add("FullCode");
                table2.Columns.Add("PBSType");
                table2.Columns.Add("PBSCode");
                table2.Columns.Add("PaymentMoney", typeof(decimal));
                this.m_ds.Tables.Add(table2);
                DataTable table3 = new DataTable("PBS");
                table3.Columns.Add("PBSType");
                table3.Columns.Add("PBSCode");
                table3.Columns.Add("PBSName");
                table3.Columns.Add("Area", typeof(decimal));
                table3.Columns.Add("AreaPercent", typeof(decimal));
                this.m_ds.Tables.Add(table3);
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
                string costCode;
                this.tb.Rows.Clear();
                this.SetPBS();
                this.SetPayment();
                DataView view = new DataView(CBSDAO.GetCBSByProject(this.ProjectCode).CurrentTable, "", "SortID, Deep", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow row = view2.Row;
                    DataRow row2 = this.tb.NewRow();
                    costCode = row["CostCode"].ToString();
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
                }
                foreach (DataRow row3 in this.tb.Rows)
                {
                    costCode = ConvertRule.ToString(row3["CostCode"]);
                    string text2 = ConvertRule.ToString(row3["FullCode"]);
                    decimal num = 0M;
                    string text3 = "";
                    foreach (DataRow row4 in this.tbPBS.Rows)
                    {
                        string text4 = ConvertRule.ToString(row4["PBSType"]);
                        string text5 = ConvertRule.ToString(row4["PBSCode"]);
                        decimal money = 0M;
                        if (text2 != "")
                        {
                            string filterExpression = string.Format("FullCode like '{0}%' and isnull(PBSType, '') = '{1}' and isnull(PBSCode, '') = '{2}'", text2, text4, text5);
                            money = MathRule.SumColumn(this.tbPayment.Select(filterExpression), "PaymentMoney");
                        }
                        num += money;
                        text3 = text3 + string.Format("<td align=right nowrap title='{1}'>{0}</td>", CostBudgetPageRule.GetContractPayHref(CostBudgetPageRule.GetMoneyShowString(money, CostBudgetPageRule.m_MoneyUnit.yuan), costCode, "", "", text4, text5), CostBudgetPageRule.GetWanDecimalShowHint(money));
                    }
                    row3["Money"] = num;
                    row3["MoneyHtml"] = text3;
                }
                this.ResetTitleHtml();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void PBSApport()
        {
            try
            {
                CostApportion apportion = new CostApportion();
                apportion.RoundDec = 2;
                DataView view = new DataView(this.tbPBS, "PBSType = 'B'", "", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    apportion.SetArea(ConvertRule.ToString(view2.Row["PBSCode"]), ConvertRule.ToDecimal(view2.Row["Area"]));
                }
                apportion.SetTotalMoney("AreaPercent", 100M);
                apportion.DoApportion();
                this.m_TotalPBSArea = apportion.TotalArea;
                foreach (DataRowView view2 in view)
                {
                    DataRow[] rowArray = apportion.tbArea.Select("ID = '" + ConvertRule.ToString(view2.Row["PBSCode"]) + "'");
                    if (rowArray.Length > 0)
                    {
                        DataRow row = rowArray[0];
                        view2.Row["AreaPercent"] = row["AreaPercent"];
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ResetTitleHtml()
        {
            try
            {
                this.m_TitleHtml1 = "";
                this.m_TitleHtmlArea = "";
                this.m_TitleHtmlAreaPercent = "";
                foreach (DataRow row in this.tbPBS.Rows)
                {
                    if (ConvertRule.ToString(row["PBSType"]) == "B")
                    {
                        this.m_TitleHtml1 = this.m_TitleHtml1 + string.Format("<th align=center nowrap>{0}</th>", CostBudgetPageRule.GetBuildingHref(ConvertRule.ToString(row["PBSCode"]), ConvertRule.ToString(row["PBSName"])) + "&nbsp;");
                    }
                    else
                    {
                        this.m_TitleHtml1 = this.m_TitleHtml1 + string.Format("<th align=center nowrap>{0}</th>", ConvertRule.ToString(row["PBSName"]) + "&nbsp;");
                    }
                    this.m_TitleHtmlArea = this.m_TitleHtmlArea + string.Format("<th align=center nowrap style='border-top:0'>{0}</th>", StringRule.BuildShowNumberString(row["Area"], "#,##0.##") + "&nbsp;");
                    this.m_TitleHtmlAreaPercent = this.m_TitleHtmlAreaPercent + string.Format("<th align=center nowrap style='border-top:0'>{0}</th>", StringRule.BuildShowPercentString(row["AreaPercent"]) + "&nbsp;");
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
                this.tbPayment.Clear();
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = "select a.CostCode, c.FullCode, isnull(a.PBSType, '') as PBSType, isnull(a.PBSCode, '') as PBSCode, sum(isnull(a.ItemMoney, 0)) as PaymentMoney from PaymentItem a left join CBS c on c.CostCode = a.CostCode, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '{0}' and b.Status in (1, 2)";
                    if (this.StartDate != "")
                    {
                        queryString = queryString + string.Format(" and CheckDate >= convert(DateTime, '{0}', 121)", this.StartDate);
                    }
                    if (this.EndDate != "")
                    {
                        queryString = queryString + string.Format(" and CheckDate < convert(DateTime, '{0}', 121) + 1", this.EndDate);
                    }
                    queryString = string.Format(queryString + " group by a.CostCode, c.FullCode, isnull(a.PBSType, ''), isnull(a.PBSCode, '')", this.ProjectCode);
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    foreach (DataRow row in tbSrc.Rows)
                    {
                        DataRow drDst = this.tbPayment.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, tbSrc, this.tbPayment);
                        this.tbPayment.Rows.Add(drDst);
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

        private void SetPBS()
        {
            try
            {
                DataRow row;
                this.tbPBS.Clear();
                EntityData buildingNotAreaByProjectCode = ProductDAO.GetBuildingNotAreaByProjectCode(this.ProjectCode);
                DataView view = new DataView(buildingNotAreaByProjectCode.CurrentTable, "", "BuildingName", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow row2 = view2.Row;
                    row = this.tbPBS.NewRow();
                    row["PBSType"] = "B";
                    row["PBSCode"] = row2["BuildingCode"];
                    row["PBSName"] = row2["BuildingName"];
                    row["Area"] = row2["Area"];
                    this.tbPBS.Rows.Add(row);
                }
                buildingNotAreaByProjectCode.Dispose();
                row = this.tbPBS.NewRow();
                row["PBSType"] = "P";
                row["PBSCode"] = "";
                row["PBSName"] = "不可划分";
                this.tbPBS.Rows.Add(row);
                this.PBSApport();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public DataSet ds
        {
            get
            {
                return this.m_ds;
            }
        }

        public string EndDate
        {
            get
            {
                return this.m_EndDate;
            }
            set
            {
                this.m_EndDate = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this.m_ProjectCode;
            }
        }

        public string StartDate
        {
            get
            {
                return this.m_StartDate;
            }
            set
            {
                this.m_StartDate = value;
            }
        }

        public DataTable tb
        {
            get
            {
                return this.m_ds.Tables["Dtl"];
            }
        }

        public DataTable tbPayment
        {
            get
            {
                return this.m_ds.Tables["Payment"];
            }
        }

        public DataTable tbPBS
        {
            get
            {
                return this.m_ds.Tables["PBS"];
            }
        }

        public string TitleHtml1
        {
            get
            {
                return this.m_TitleHtml1;
            }
        }

        public string TitleHtmlArea
        {
            get
            {
                return this.m_TitleHtmlArea;
            }
        }

        public string TitleHtmlAreaPercent
        {
            get
            {
                return this.m_TitleHtmlAreaPercent;
            }
        }

        public decimal TotalPBSArea
        {
            get
            {
                return this.m_TotalPBSArea;
            }
        }
    }
}

