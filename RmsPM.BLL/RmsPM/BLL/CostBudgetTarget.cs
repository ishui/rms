namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CostBudgetTarget
    {
        private EntityData entityTargetHis = null;
        public string FirstCostBudgetCode = "";
        public bool IsModify = false;
        public bool IsShowTargetMoneyHis = false;
        private string m_CostBudgetCode = "";
        private string m_CostBudgetSetCode = "";
        private string m_EndY = "";
        private int m_iEndY = 0;
        private int m_iStartY = 0;
        private string m_ProjectCode = "";
        private string m_StartY = "";
        private DataTable m_tb = null;
        public string ShowCostCode = "";

        public CostBudgetTarget(string a_ProjectCode, string a_CostBudgetCode, string a_CostBudgetSetCode)
        {
            this.m_ProjectCode = a_ProjectCode;
            this.m_CostBudgetCode = a_CostBudgetCode;
            this.m_CostBudgetSetCode = a_CostBudgetSetCode;
        }

        public DataTable GenerateCurrent()
        {
            DataTable table2;
            try
            {
                if (this.entityTargetHis != null)
                {
                    this.entityTargetHis.Dispose();
                }
                if (this.IsShowTargetMoneyHis)
                {
                    this.entityTargetHis = CostBudgetRule.GetRelationCostBudget(this.ProjectCode, this.FirstCostBudgetCode, "1,2", this.CostBudgetCode);
                }
                DataTable table = CostBudgetPageRule.GenerateEmptyCostTargetDtl(this.StartY, this.EndY, this.entityTargetHis);
                EntityData allCBSBySet = CostBudgetRule.GetAllCBSBySet(this.ProjectCode, this.CostBudgetSetCode);
                if (allCBSBySet == null)
                {
                    return table;
                }
                if (this.ShowCostCode != "")
                {
                    string cBSFullCode = CBSRule.GetCBSFullCode(this.ShowCostCode);
                    DataRow[] rowArray = allCBSBySet.CurrentTable.Select(string.Format("FullCode not like '{0}%'", cBSFullCode));
                    foreach (DataRow row in rowArray)
                    {
                        allCBSBySet.CurrentTable.Rows.Remove(row);
                    }
                    CostBudgetRule.ResetCBSDeep(allCBSBySet.CurrentTable);
                }
                DataRow row2 = null;
                if (this.ShowCostCode == "")
                {
                    row2 = table.NewRow();
                    row2["CostBudgetDtlCode"] = "R_0";
                    row2["CostCode"] = row2["CostBudgetDtlCode"];
                    row2["CostName"] = "合计";
                    row2["Deep"] = 1;
                    row2["ParentCode"] = "";
                    row2["ChildCount"] = 1;
                    row2["IsExpand"] = 1;
                    table.Rows.Add(row2);
                }
                int num = 0;
                foreach (DataRow row3 in allCBSBySet.CurrentTable.Rows)
                {
                    num--;
                    DataRow drDtl = table.NewRow();
                    drDtl["CostBudgetDtlCode"] = num;
                    CostBudgetPageRule.FillCostBudgetDtlCBSData(drDtl, row3);
                    if (row2 != null)
                    {
                        if (ConvertRule.ToInt(drDtl["Deep"]) == 1)
                        {
                            drDtl["ParentCode"] = row2["CostBudgetDtlCode"];
                        }
                        drDtl["Deep"] = ConvertRule.ToInt(drDtl["Deep"]) + 1;
                    }
                    else if (ConvertRule.ToInt(drDtl["Deep"]) == 1)
                    {
                        drDtl["ParentCode"] = "";
                    }
                    table.Rows.Add(drDtl);
                }
                EntityData validCostBudget = CostBudgetRule.GetValidCostBudget(this.CostBudgetSetCode, 1);
                EntityData costBudgetDtlByCostBudgetCode = null;
                if (validCostBudget.HasRecord())
                {
                    costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(validCostBudget.GetString("CostBudgetCode"));
                }
                EntityData data4 = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(this.CostBudgetCode);
                EntityData costBudgetMonthByCostBudgetCode = CostBudgetDAO.GetCostBudgetMonthByCostBudgetCode(this.CostBudgetCode);
                foreach (DataRow row5 in table.Rows)
                {
                    DataRow[] rowArray2 = data4.CurrentTable.Select("CostCode='" + ConvertRule.ToString(row5["CostCode"]) + "'");
                    if (rowArray2.Length > 0)
                    {
                        row5["CostBudgetDtlCode"] = rowArray2[0]["CostBudgetDtlCode"];
                        row5["Price"] = rowArray2[0]["Price"];
                        row5["Qty"] = rowArray2[0]["Qty"];
                        row5["BudgetMoney"] = rowArray2[0]["BudgetMoney"];
                        row5["IsExpand"] = rowArray2[0]["IsExpand"];
                        row5["Description"] = rowArray2[0]["Description"];
                        row5["DescriptionHtml"] = rowArray2[0]["Description"];
                    }
                    if (costBudgetDtlByCostBudgetCode != null)
                    {
                        DataRow[] rowArray3 = costBudgetDtlByCostBudgetCode.CurrentTable.Select("CostCode='" + ConvertRule.ToString(row5["CostCode"]) + "'");
                        if (rowArray3.Length > 0)
                        {
                            row5["BudgetValidMoney"] = rowArray3[0]["BudgetMoney"];
                        }
                    }
                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName.StartsWith("BudgetMoney_"))
                        {
                            string text2 = column.ColumnName.Replace("BudgetMoney_", "");
                            string val = text2.Substring(0, 4);
                            string text4 = text2.Substring(4, 2);
                            int num2 = ConvertRule.ToInt(val);
                            int num3 = ConvertRule.ToInt(text4);
                            DataRow[] rowArray4 = costBudgetMonthByCostBudgetCode.CurrentTable.Select("CostBudgetDtlCode = '" + ConvertRule.ToString(row5["CostBudgetDtlCode"]) + "' and IYear = " + num2.ToString() + " and IMonth = " + num3.ToString());
                            if (rowArray4.Length > 0)
                            {
                                row5[column.ColumnName] = rowArray4[0]["BudgetMoney"];
                            }
                        }
                    }
                    if (!this.IsModify)
                    {
                        row5["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row5, this.iStartY, this.iEndY, "BudgetMoney_");
                    }
                }
                allCBSBySet.Dispose();
                data4.Dispose();
                costBudgetMonthByCostBudgetCode.Dispose();
                validCostBudget.Dispose();
                if (costBudgetDtlByCostBudgetCode != null)
                {
                    costBudgetDtlByCostBudgetCode.Dispose();
                }
                if (row2 != null)
                {
                    DataRow[] drs = table.Select("ParentCode = '" + row2["CostBudgetDtlCode"].ToString() + "'");
                    string[] textArray = new string[] { "BudgetMoney", "BudgetValidMoney" };
                    string[] textArray2 = CostBudgetPageRule.BuildArrayFieldByMonth(this.StartY, this.EndY, "BudgetMoney_");
                    string[] arrColumnName = ConvertRule.ArrayConcat(textArray, textArray2);
                    decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                    int index = -1;
                    foreach (string text5 in arrColumnName)
                    {
                        index++;
                        row2[text5] = numArray[index];
                    }
                    if (!this.IsModify)
                    {
                        row2["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row2, this.iStartY, this.iEndY, "BudgetMoney_");
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

        public DataTable tb
        {
            get
            {
                return this.m_tb;
            }
        }
    }
}

