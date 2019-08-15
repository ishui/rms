namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CostBudgetGroupDynamic
    {
        public bool AutoRefreshHtml;
        public string FirstCostBudgetCode;
        public bool IsModify;
        public bool IsShowTargetMoneyHis;
        private string[] m_arrCostBudgetSetCode;
        private CostBudgetDynamic[] m_arrDyn;
        private string[] m_arrMoneyField;
        private string m_CostBudgetBackupCode;
        private string m_CostTargetCode;
        private DataSet m_ds;
        private string m_EndY;
        private EntityData m_entityGroup;
        private string m_GroupCode;
        private bool m_HasTargetChange;
        private int m_iEndY;
        private int m_iStartY;
        private string m_ProjectCode;
        private string m_StartY;
        private string m_TargetChangeDesc;
        private DataTable m_tbGroupArea;
        public bool ShowContractAccountMoney;

        public CostBudgetGroupDynamic(string a_ProjectCode, string a_GroupCode)
        {
            this.m_arrMoneyField = new string[] { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance" };
            this.m_ds = null;
            this.m_GroupCode = "";
            this.m_arrCostBudgetSetCode = null;
            this.m_arrDyn = null;
            this.m_ProjectCode = "";
            this.m_CostBudgetBackupCode = "";
            this.m_CostTargetCode = "";
            this.m_HasTargetChange = false;
            this.m_TargetChangeDesc = "";
            this.m_iStartY = 0;
            this.m_iEndY = 0;
            this.m_StartY = "";
            this.m_EndY = "";
            this.IsModify = false;
            this.AutoRefreshHtml = true;
            this.IsShowTargetMoneyHis = false;
            this.ShowContractAccountMoney = false;
            this.FirstCostBudgetCode = "";
            this.m_ProjectCode = a_ProjectCode;
            this.m_GroupCode = a_GroupCode;
            this.InitGroupInfo();
            this.InitSetArray();
        }

        public CostBudgetGroupDynamic(string a_ProjectCode, string a_GroupCode, string a_CostBudgetBackupCode)
        {
            this.m_arrMoneyField = new string[] { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance" };
            this.m_ds = null;
            this.m_GroupCode = "";
            this.m_arrCostBudgetSetCode = null;
            this.m_arrDyn = null;
            this.m_ProjectCode = "";
            this.m_CostBudgetBackupCode = "";
            this.m_CostTargetCode = "";
            this.m_HasTargetChange = false;
            this.m_TargetChangeDesc = "";
            this.m_iStartY = 0;
            this.m_iEndY = 0;
            this.m_StartY = "";
            this.m_EndY = "";
            this.IsModify = false;
            this.AutoRefreshHtml = true;
            this.IsShowTargetMoneyHis = false;
            this.ShowContractAccountMoney = false;
            this.FirstCostBudgetCode = "";
            this.m_ProjectCode = a_ProjectCode;
            this.m_GroupCode = a_GroupCode;
            this.m_CostBudgetBackupCode = a_CostBudgetBackupCode;
            this.InitGroupInfo();
            this.InitSetArray();
        }

        private void AddCostChildDyn(DataRow drCBS)
        {
            try
            {
                string text = ConvertRule.ToString(drCBS["CostCode"]);
                int num = ConvertRule.ToInt(drCBS["Deep"]);
                string text2 = ConvertRule.ToString(drCBS["SortID"]);
                string text3 = ConvertRule.ToString(drCBS["FullCode"]);
                foreach (CostBudgetDynamic dynamic in this.m_arrDyn)
                {
                    DataRow row = this.tb.NewRow();
                    row["CostBudgetDtlCode"] = "S_" + text + "_" + dynamic.CostBudgetSetCode;
                    row["CostBudgetSetCode"] = dynamic.CostBudgetSetCode;
                    row["CostCode"] = text;
                    row["CostName"] = dynamic.entitySet.GetString("CostBudgetSetName");
                    row["ParentCode"] = text;
                    row["FullCode"] = text3;
                    row["Deep"] = num + 1;
                    row["ChildCount"] = 0;
                    DataRow[] rowArray = dynamic.tb.Select("CostCode = '" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        foreach (string text4 in this.m_arrMoneyField)
                        {
                            row[text4] = rowArray[0][text4];
                        }
                        row["ContractPayPercent"] = rowArray[0]["ContractPayPercent"];
                        row["BuildingPrice"] = rowArray[0]["BuildingPrice"];
                        row["HousePrice"] = rowArray[0]["HousePrice"];
                        row["BudgetPrice"] = rowArray[0]["BudgetPrice"];
                        row["ContractOriginalPrice"] = rowArray[0]["ContractOriginalPrice"];
                    }
                    this.tb.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void CalcByChilds(DataRow dr, DataRow[] drsChild)
        {
            try
            {
                string[] arrMoneyField;
                if (dr.Table.TableName == "Html")
                {
                    string[] textArray2 = CostBudgetPageRule.BuildArrayFieldByMonth(this.StartY, this.EndY, "BudgetMoney_");
                    arrMoneyField = ConvertRule.ArrayConcat(this.m_arrMoneyField, textArray2);
                    textArray2 = CostBudgetPageRule.BuildArrayFieldByMonth(this.StartY, this.EndY, "ContractMoney_");
                    arrMoneyField = ConvertRule.ArrayConcat(arrMoneyField, textArray2);
                }
                else
                {
                    arrMoneyField = this.m_arrMoneyField;
                }
                decimal[] numArray = MathRule.SumColumn(drsChild, arrMoneyField);
                int index = -1;
                foreach (string text in arrMoneyField)
                {
                    index++;
                    dr[text] = numArray[index];
                }
                CostBudgetPageRule.CalcPercent(dr, this.m_tbGroupArea.Rows[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable CreateGroupAreaTable(string GroupCode, string ProjectCode, string CostBudgetBackupCode)
        {
            DataTable table2;
            try
            {
                EntityData data;
                string[] arrColumnName = new string[] { "BuildingArea", "HouseCount", "HouseArea" };
                DataTable table = new DataTable("GroupArea");
                table.Columns.Add("GroupCode");
                foreach (string text in arrColumnName)
                {
                    table.Columns.Add(text, typeof(decimal));
                }
                DataRow row = table.NewRow();
                row["GroupCode"] = GroupCode;
                table.Rows.Add(row);
                if (CostBudgetBackupCode != "")
                {
                    data = CostBudgetDAO.GetCostBudgetBackupSetByGroupCode(CostBudgetBackupCode, GroupCode, true);
                }
                else
                {
                    data = CostBudgetDAO.GetV_CostBudgetSetByGroupCode(GroupCode, ProjectCode, CostBudgetRule.m_BaseSetType);
                }
                if (data.HasRecord())
                {
                    DataRow[] drs = data.CurrentTable.Select("PBSType = 'P'");
                    if (drs.Length > 0)
                    {
                        foreach (string text in arrColumnName)
                        {
                            row[text] = drs[0][text];
                        }
                    }
                    else
                    {
                        drs = data.CurrentTable.Select("PBSType = 'B'");
                        if (drs.Length > 0)
                        {
                            decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                            int index = -1;
                            foreach (string text in arrColumnName)
                            {
                                index++;
                                row[text] = numArray[index];
                            }
                        }
                    }
                }
                data.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public DataSet Generate()
        {
            DataSet ds;
            try
            {
                this.m_ds = CostBudgetPageRule.GenerateEmptyCostBudgetDtl(this.StartY, this.EndY, null);
                this.tb.Columns.Add("CostBudgetSetCode");
                this.tbMonth.Columns.Add("CostBudgetSetCode");
                this.tbContractMonth.Columns.Add("CostBudgetSetCode");
                for (int i = 0; i < this.m_arrCostBudgetSetCode.Length; i++)
                {
                    this.m_arrDyn[i] = new CostBudgetDynamic(this.ProjectCode, this.m_arrCostBudgetSetCode[i], this.CostBudgetBackupCode);
                    this.m_arrDyn[i].StartY = this.StartY;
                    this.m_arrDyn[i].EndY = this.EndY;
                    this.m_arrDyn[i].ShowApportion = false;
                    this.m_arrDyn[i].ShowContractBudget = false;
                    this.m_arrDyn[i].ShowTargetChange = false;
                    this.m_arrDyn[i].MaxCBSDeep = 1;
                    this.m_arrDyn[i].AutoRefreshHtml = false;
                    this.m_arrDyn[i].ShowContractAccountMoney = this.ShowContractAccountMoney;
                    this.m_arrDyn[i].Generate();
                }
                this.SumDs();
                if (this.AutoRefreshHtml)
                {
                    this.RefreshHtml();
                }
                ds = this.ds;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return ds;
        }

        private void InitGroupInfo()
        {
            try
            {
                this.m_entityGroup = SystemManageDAO.GetSystemGroupByCode(this.m_GroupCode);
                this.m_tbGroupArea = CreateGroupAreaTable(this.m_GroupCode, this.m_ProjectCode, this.m_CostBudgetBackupCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void InitSetArray()
        {
            try
            {
                EntityData data = CostBudgetDAO.GetCostBudgetSetByGroupCode(this.GroupCode, this.ProjectCode, CostBudgetRule.m_BaseSetType);
                this.m_arrCostBudgetSetCode = new string[data.CurrentTable.Rows.Count];
                this.m_arrDyn = new CostBudgetDynamic[this.m_arrCostBudgetSetCode.Length];
                int index = -1;
                foreach (DataRow row in data.CurrentTable.Rows)
                {
                    index++;
                    this.m_arrCostBudgetSetCode[index] = row["CostBudgetSetCode"].ToString();
                }
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshHtml()
        {
            try
            {
                DataRow drDst;
                if (!this.ds.Tables.Contains("Html"))
                {
                    this.ds.Tables.Add(CostBudgetPageRule.CreateDynamicHtmlTable(this.StartY, this.EndY, this.tb));
                }
                this.tbHtml.Clear();
                foreach (DataRow row in this.tb.Rows)
                {
                    drDst = this.tbHtml.NewRow();
                    ConvertRule.DataRowCopy(row, drDst, this.tb, this.tbHtml);
                    this.tbHtml.Rows.Add(drDst);
                }
                if ((this.StartY != "") && (this.EndY != ""))
                {
                    DataRow[] drsChild;
                    DataView view = new DataView(this.tbHtml, "Deep = 3", "", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        int objYear;
                        int objMonth;
                        string text4;
                        string name;
                        drDst = view2.Row;
                        string text = ConvertRule.ToString(drDst["CostCode"]);
                        string text2 = ConvertRule.ToString(drDst["FullCode"]);
                        string text3 = ConvertRule.ToString(drDst["CostBudgetSetCode"]);
                        DataRow[] rowArray = this.tbMonth.Select(string.Format("CostCode = '{0}' and CostBudgetSetCode = '{1}'", text, text3));
                        foreach (DataRow row3 in rowArray)
                        {
                            objYear = ConvertRule.ToInt(row3["IYear"]);
                            objMonth = ConvertRule.ToInt(row3["IMonth"]);
                            text4 = ConvertRule.FormatYYYYMM(objYear, objMonth);
                            name = "BudgetMoney_" + text4;
                            if (this.tbHtml.Columns.Contains(name))
                            {
                                drDst[name] = ConvertRule.ToDecimal(drDst[name]) + ConvertRule.ToDecimal(row3["Money"]);
                            }
                        }
                        rowArray = this.tbContractMonth.Select(string.Format("FullCode like '{0}%' and CostBudgetSetCode = '{1}'", text2, text3));
                        foreach (DataRow row3 in rowArray)
                        {
                            objYear = ConvertRule.ToInt(row3["IYear"]);
                            objMonth = ConvertRule.ToInt(row3["IMonth"]);
                            text4 = ConvertRule.FormatYYYYMM(objYear, objMonth);
                            name = "ContractMoney_" + text4;
                            if (this.tbHtml.Columns.Contains(name))
                            {
                                drDst[name] = ConvertRule.ToDecimal(drDst[name]) + ConvertRule.ToDecimal(row3["Money"]);
                            }
                            text4 = ConvertRule.FormatYYYYMM(objYear, 0);
                            name = "ContractMoney_" + text4;
                            if (this.tbHtml.Columns.Contains(name))
                            {
                                drDst[name] = ConvertRule.ToDecimal(drDst[name]) + ConvertRule.ToDecimal(row3["Money"]);
                            }
                        }
                    }
                    view = new DataView(this.tbHtml, "Deep = 2", "", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        drDst = view2.Row;
                        drsChild = this.tbHtml.Select("ParentCode = '" + ConvertRule.ToString(drDst["CostBudgetDtlCode"]) + "'");
                        this.CalcByChilds(drDst, drsChild);
                    }
                    view = new DataView(this.tbHtml, "Deep = 1", "", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        drDst = view2.Row;
                        drsChild = this.tbHtml.Select("ParentCode = '" + ConvertRule.ToString(drDst["CostBudgetDtlCode"]) + "'");
                        this.CalcByChilds(drDst, drsChild);
                    }
                }
                foreach (DataRow row2 in this.tbHtml.Rows)
                {
                    string text6 = ConvertRule.ToString(row2["RecordType"]);
                    string classTd = ConvertRule.ToString(row2["ClassTd"]);
                    if (text6 == "")
                    {
                        row2["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row2, this.iStartY, this.iEndY, new string[] { "BudgetMoney_", "ContractMoney_" }, new string[] { classTd, CostBudgetPageRule.m_ClassTdContract }, new string[] { "预算", "合同" });
                    }
                    else
                    {
                        row2["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row2, this.iStartY, this.iEndY, "ContractMoney_", classTd);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SumDs()
        {
            try
            {
                DataRow[] drsChild;
                DataRow drDtl;
                this.tb.Rows.Clear();
                this.tbMonth.Rows.Clear();
                this.tbContractMonth.Rows.Clear();
                DataRow row = this.tb.NewRow();
                row["CostBudgetDtlCode"] = "R_0";
                row["CostName"] = this.m_entityGroup.GetString("GroupName") + "合计";
                row["Deep"] = 1;
                row["ParentCode"] = "";
                row["ChildCount"] = 1;
                row["IsExpand"] = 1;
                this.tb.Rows.Add(row);
                EntityData rootCBSByGroup = CostBudgetRule.GetRootCBSByGroup(this.ProjectCode, this.GroupCode);
                foreach (DataRow row2 in rootCBSByGroup.CurrentTable.Rows)
                {
                    string text = ConvertRule.ToString(row2["CostCode"]);
                    drDtl = this.tb.NewRow();
                    drDtl["CostBudgetDtlCode"] = text;
                    CostBudgetPageRule.FillCostBudgetDtlCBSData(drDtl, row2);
                    drDtl["Deep"] = ConvertRule.ToInt(row["Deep"]) + 1;
                    drDtl["ParentCode"] = row["CostBudgetDtlCode"];
                    drDtl["ChildCount"] = 1;
                    this.tb.Rows.Add(drDtl);
                    this.AddCostChildDyn(drDtl);
                    drsChild = this.tb.Select("ParentCode = '" + text + "'");
                    this.CalcByChilds(drDtl, drsChild);
                }
                rootCBSByGroup.Dispose();
                drsChild = this.tb.Select("ParentCode = '" + row["CostBudgetDtlCode"].ToString() + "'");
                this.CalcByChilds(row, drsChild);
                if ((this.StartY != "") && (this.EndY != ""))
                {
                    foreach (CostBudgetDynamic dynamic in this.m_arrDyn)
                    {
                        foreach (DataRow row4 in dynamic.tbMonth.Rows)
                        {
                            drDtl = this.tbMonth.NewRow();
                            ConvertRule.DataRowCopy(row4, drDtl, dynamic.tbMonth, this.tbMonth);
                            drDtl["CostBudgetSetCode"] = dynamic.CostBudgetSetCode;
                            this.tbMonth.Rows.Add(drDtl);
                        }
                        foreach (DataRow row5 in dynamic.tbContractMonth.Rows)
                        {
                            drDtl = this.tbContractMonth.NewRow();
                            ConvertRule.DataRowCopy(row5, drDtl, dynamic.tbContractMonth, this.tbContractMonth);
                            drDtl["CostBudgetSetCode"] = dynamic.CostBudgetSetCode;
                            this.tbContractMonth.Rows.Add(drDtl);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string[] arrCostBudgetSetCode
        {
            get
            {
                return this.m_arrCostBudgetSetCode;
            }
        }

        public CostBudgetDynamic[] arrDyn
        {
            get
            {
                return this.m_arrDyn;
            }
        }

        public string CostBudgetBackupCode
        {
            get
            {
                return this.m_CostBudgetBackupCode;
            }
        }

        public string CostTargetCode
        {
            get
            {
                return this.m_CostTargetCode;
            }
        }

        public DataSet ds
        {
            get
            {
                return this.m_ds;
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

        public EntityData entityGroup
        {
            get
            {
                return this.m_entityGroup;
            }
        }

        public string GroupCode
        {
            get
            {
                return this.m_GroupCode;
            }
        }

        public bool HasTargetChange
        {
            get
            {
                return this.m_HasTargetChange;
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

        public string TargetChangeDesc
        {
            get
            {
                return this.m_TargetChangeDesc;
            }
        }

        public DataTable tb
        {
            get
            {
                return this.m_ds.Tables["CostBudgetDtl"];
            }
        }

        public DataTable tbContractMonth
        {
            get
            {
                return this.m_ds.Tables["ContractMonth"];
            }
        }

        public DataTable tbGroupArea
        {
            get
            {
                return this.m_tbGroupArea;
            }
        }

        public DataTable tbHtml
        {
            get
            {
                return this.m_ds.Tables["Html"];
            }
        }

        public DataTable tbMonth
        {
            get
            {
                return this.m_ds.Tables["Month"];
            }
        }
    }
}

