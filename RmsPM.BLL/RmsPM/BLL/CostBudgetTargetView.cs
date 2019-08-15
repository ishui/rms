namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CostBudgetTargetView
    {
        private DataView dvTargetHis = null;
        private EntityData entityTargetHis = null;
        private string m_CostBudgetCode = "";
        private string m_CostBudgetSetCode = "";
        private string m_EndY = "";
        private string m_FirstCostBudgetCode = "";
        private int m_iEndY = 0;
        private int m_iStartY = 0;
        private RmsPM.BLL.CostBudgetPageRule.m_MoneyUnit m_MoneyUnit = RmsPM.BLL.CostBudgetPageRule.m_MoneyUnit.yuan;
        private string m_ProjectCode = "";
        private string m_StartY = "";
        private string m_TargetHisHead1 = "";
        private string m_TargetHisHead2 = "";
        public bool ShowTargetHis = false;
        private DataTable tb;

        public CostBudgetTargetView(string a_ProjectCode, string a_CostBudgetCode, string a_CostBudgetSetCode, string a_FirstCostBudgetCode)
        {
            this.m_ProjectCode = a_ProjectCode;
            this.m_CostBudgetSetCode = a_CostBudgetSetCode;
            this.m_CostBudgetCode = a_CostBudgetCode;
            this.m_FirstCostBudgetCode = a_FirstCostBudgetCode;
        }

        private void FillCostTargetHisHtml()
        {
            try
            {
                if (this.entityTargetHis != null)
                {
                    string text2;
                    ArrayList list = new ArrayList();
                    foreach (DataRowView view in this.dvTargetHis)
                    {
                        DataRow row = view.Row;
                        string text = ConvertRule.ToString(row["VerID"]);
                        if (text != "")
                        {
                            list.Add(text);
                            text2 = "BudgetMoneyHis_" + text;
                            EntityData costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(row["CostBudgetCode"].ToString());
                            if (costBudgetDtlByCostBudgetCode.HasRecord())
                            {
                                foreach (DataRow row2 in this.tb.Rows)
                                {
                                    DataRow[] rowArray = costBudgetDtlByCostBudgetCode.CurrentTable.Select("CostCode='" + ConvertRule.ToString(row2["CostCode"]) + "'");
                                    if (rowArray.Length > 0)
                                    {
                                        row2[text2] = rowArray[0]["BudgetMoney"];
                                    }
                                }
                            }
                            costBudgetDtlByCostBudgetCode.Dispose();
                        }
                    }
                    if (this.tb.Rows.Count > 0)
                    {
                        DataRow row3 = this.tb.Rows[0];
                        DataRow[] drs = this.tb.Select("ParentCode = '" + row3["CostBudgetDtlCode"].ToString() + "'");
                        string[] arrColumnName = new string[list.Count];
                        int index = -1;
                        foreach (string text in list)
                        {
                            index++;
                            arrColumnName[index] = "BudgetMoneyHis_" + text;
                        }
                        decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                        int num2 = -1;
                        foreach (string text3 in arrColumnName)
                        {
                            num2++;
                            row3[text3] = numArray[num2];
                        }
                    }
                    foreach (DataRow row2 in this.tb.Rows)
                    {
                        string text4 = "";
                        foreach (string text in list)
                        {
                            text2 = "BudgetMoneyHis_" + text;
                            text4 = text4 + string.Format("<td align=right nowrap title='{1}'>{0}</td>", CostBudgetPageRule.GetMoneyShowString(row2[text2], this.m_MoneyUnit), CostBudgetPageRule.GetWanDecimalShowHint(row2[text2]));
                        }
                        row2["BudgetMoneyHisHtml"] = text4;
                    }
                }
                else
                {
                    foreach (DataRow row2 in this.tb.Rows)
                    {
                        row2["BudgetMoneyHisHtml"] = "";
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public DataTable Generate()
        {
            DataTable tb;
            try
            {
                if (this.ShowTargetHis)
                {
                    this.LoadTargetHis();
                }
                this.tb = CostBudgetPageRule.GenerateEmptyCostTargetDtl(this.StartY, this.EndY, this.entityTargetHis);
                EntityData costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(this.CostBudgetCode);
                EntityData costBudgetMonthByCostBudgetCode = CostBudgetDAO.GetCostBudgetMonthByCostBudgetCode(this.CostBudgetCode);
                EntityData allCBSBySet = CostBudgetRule.GetAllCBSBySet(this.ProjectCode, this.CostBudgetSetCode);
                DataRow row = this.tb.NewRow();
                row["CostBudgetDtlCode"] = "R_0";
                row["CostCode"] = row["CostBudgetDtlCode"];
                row["CostName"] = "合计";
                row["Deep"] = 1;
                row["ParentCode"] = "";
                row["ChildCount"] = 1;
                row["IsExpand"] = 1;
                this.tb.Rows.Add(row);
                foreach (DataRow row2 in costBudgetDtlByCostBudgetCode.CurrentTable.Rows)
                {
                    string text = ConvertRule.ToString(row2["CostBudgetDtlCode"]);
                    DataRow drDst = this.tb.NewRow();
                    ConvertRule.DataRowCopy(row2, drDst, costBudgetDtlByCostBudgetCode.CurrentTable, this.tb);
                    DataRow[] rowArray = allCBSBySet.CurrentTable.Select("CostCode='" + ConvertRule.ToString(drDst["CostCode"]) + "'");
                    if (rowArray.Length > 0)
                    {
                        CostBudgetPageRule.FillCostBudgetDtlCBSData(drDst, rowArray[0]);
                    }
                    drDst["Deep"] = ConvertRule.ToInt(drDst["Deep"]) + 1;
                    if (ConvertRule.ToInt(drDst["Deep"]) == (ConvertRule.ToInt(row["Deep"]) + 1))
                    {
                        drDst["ParentCode"] = row["CostBudgetDtlCode"];
                    }
                    foreach (DataColumn column in this.tb.Columns)
                    {
                        if (column.ColumnName.StartsWith("BudgetMoney_"))
                        {
                            string text2 = column.ColumnName.Replace("BudgetMoney_", "");
                            string val = text2.Substring(0, 4);
                            string text4 = text2.Substring(4, 2);
                            int num = ConvertRule.ToInt(val);
                            int num2 = ConvertRule.ToInt(text4);
                            DataRow[] rowArray2 = costBudgetMonthByCostBudgetCode.CurrentTable.Select("CostBudgetDtlCode = '" + text + "' and IYear = " + num.ToString() + " and IMonth = " + num2.ToString());
                            if (rowArray2.Length > 0)
                            {
                                drDst[column.ColumnName] = rowArray2[0]["BudgetMoney"];
                            }
                        }
                    }
                    drDst["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(drDst, this.iStartY, this.iEndY, "BudgetMoney_");
                    this.tb.Rows.Add(drDst);
                }
                costBudgetDtlByCostBudgetCode.Dispose();
                costBudgetMonthByCostBudgetCode.Dispose();
                allCBSBySet.Dispose();
                DataRow[] drs = this.tb.Select("ParentCode = '" + row["CostBudgetDtlCode"].ToString() + "'");
                string[] textArray = new string[] { "BudgetMoney" };
                string[] textArray2 = CostBudgetPageRule.BuildArrayFieldByMonth(this.StartY, this.EndY, "BudgetMoney_");
                string[] arrColumnName = ConvertRule.ArrayConcat(textArray, textArray2);
                decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                int index = -1;
                foreach (string text5 in arrColumnName)
                {
                    index++;
                    row[text5] = numArray[index];
                }
                row["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row, this.iStartY, this.iEndY, "BudgetMoney_");
                CostBudgetPageRule.SetColumnTargetHis(this.tb, this.entityTargetHis);
                this.FillCostTargetHisHtml();
                this.GenerateTargetHisHead();
                tb = this.tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return tb;
        }

        private void GenerateTargetHisHead()
        {
            try
            {
                this.m_TargetHisHead1 = "";
                this.m_TargetHisHead2 = "";
                if (this.ShowTargetHis)
                {
                    this.m_TargetHisHead1 = string.Format("<th align=center nowrap colspan='{0}'>历史预算</th>", this.dvTargetHis.Count);
                    foreach (DataRowView view in this.dvTargetHis)
                    {
                        this.m_TargetHisHead2 = this.m_TargetHisHead2 + string.Format("<th align=center nowrap>{0}<br>{1}</th>", ConvertRule.ToString(CostBudgetRule.GetCostBudgetVerName(view.Row)), ConvertRule.ToDateString(view.Row["CheckDate"], "yyyy-MM-dd"));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void LoadTargetHis()
        {
            try
            {
                this.entityTargetHis = null;
                this.entityTargetHis = CostBudgetRule.GetRelationCostBudget(this.ProjectCode, this.FirstCostBudgetCode, "2", "");
                this.dvTargetHis = new DataView(this.entityTargetHis.CurrentTable, "", "VerID desc", DataViewRowState.CurrentRows);
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
            set
            {
                this.m_CostBudgetCode = value;
            }
        }

        public string CostBudgetSetCode
        {
            get
            {
                return this.m_CostBudgetSetCode;
            }
        }

        public string EndY
        {
            get
            {
                return this.m_EndY;
            }
            set
            {
                this.m_EndY = value;
                this.m_iEndY = ConvertRule.ToInt(value);
            }
        }

        public string FirstCostBudgetCode
        {
            get
            {
                return this.m_FirstCostBudgetCode;
            }
            set
            {
                this.m_FirstCostBudgetCode = value;
            }
        }

        public int iEndY
        {
            get
            {
                return this.m_iEndY;
            }
        }

        public int iStartY
        {
            get
            {
                return this.m_iStartY;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this.m_ProjectCode;
            }
        }

        public string StartY
        {
            get
            {
                return this.m_StartY;
            }
            set
            {
                this.m_StartY = value;
                this.m_iStartY = ConvertRule.ToInt(value);
            }
        }

        public string TargetHisHead1
        {
            get
            {
                return this.m_TargetHisHead1;
            }
        }

        public string TargetHisHead2
        {
            get
            {
                return this.m_TargetHisHead2;
            }
        }
    }
}

