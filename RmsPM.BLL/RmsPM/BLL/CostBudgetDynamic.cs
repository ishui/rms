namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class CostBudgetDynamic
    {
        public bool AutoRefreshDynProject;
        public bool AutoRefreshHtml;
        private DataView dvTargetHis;
        private EntityData entityTargetHis;
        public bool IsContractNew;
        public bool IsModify;
        private CostBudgetDynamic[] m_arrDynProject;
        private string[] m_arrMoneyField;
        private string m_CostBudgetBackupCode;
        private string m_CostBudgetCode;
        private string m_CostBudgetSetCode;
        private string m_CostTargetCode;
        private DataSet m_ds;
        private string m_EndY;
        private EntityData m_entityBackup;
        private EntityData m_entityBackupSet;
        private EntityData m_entityCurrTarget;
        private EntityData m_entitySet;
        private int m_iEndY;
        private int m_iStartY;
        public bool m_IsUnionOneContract;
        private RmsPM.BLL.CostBudgetPageRule.m_MoneyUnit m_MoneyUnit;
        private string m_ProjectCode;
        private string m_StartY;
        private int m_TargetChangeFlag;
        public int MaxCBSDeep;
        public bool ShowApportion;
        public bool ShowContractAccountMoney;
        public bool ShowContractBudget;
        public bool ShowTargetChange;
        public bool ShowTargetHis;

        public CostBudgetDynamic(string a_ProjectCode, string a_CostBudgetSetCode)
        {
            this.m_arrMoneyField = new string[] { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance" };
            this.m_IsUnionOneContract = true;
            this.IsContractNew = true;
            this.m_ds = null;
            this.m_CostBudgetCode = "";
            this.m_CostBudgetSetCode = "";
            this.m_ProjectCode = "";
            this.m_CostBudgetBackupCode = "";
            this.m_CostTargetCode = "";
            this.m_entityCurrTarget = null;
            this.m_TargetChangeFlag = 0;
            this.m_iStartY = 0;
            this.m_iEndY = 0;
            this.m_StartY = "";
            this.m_EndY = "";
            this.IsModify = false;
            this.ShowApportion = true;
            this.ShowContractBudget = true;
            this.ShowTargetChange = true;
            this.ShowTargetHis = false;
            this.MaxCBSDeep = 0x270f;
            this.AutoRefreshHtml = true;
            this.m_entityBackup = null;
            this.m_entityBackupSet = null;
            this.m_arrDynProject = null;
            this.AutoRefreshDynProject = true;
            this.ShowContractAccountMoney = false;
            this.entityTargetHis = null;
            this.dvTargetHis = null;
            this.m_ProjectCode = a_ProjectCode;
            this.m_CostBudgetSetCode = a_CostBudgetSetCode;
            this.InitSetInfo();
            this.InitValidCostBudgetInfo();
        }

        public CostBudgetDynamic(string a_ProjectCode, string a_CostBudgetSetCode, string a_CostBudgetBackupCode)
        {
            this.m_arrMoneyField = new string[] { "BudgetMoney", "BudgetChangeMoney", "ContractTotalMoney", "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractBudgetBalance", "ContractPay", "ContractPayReal", "ContractPayBalance" };
            this.m_IsUnionOneContract = true;
            this.IsContractNew = true;
            this.m_ds = null;
            this.m_CostBudgetCode = "";
            this.m_CostBudgetSetCode = "";
            this.m_ProjectCode = "";
            this.m_CostBudgetBackupCode = "";
            this.m_CostTargetCode = "";
            this.m_entityCurrTarget = null;
            this.m_TargetChangeFlag = 0;
            this.m_iStartY = 0;
            this.m_iEndY = 0;
            this.m_StartY = "";
            this.m_EndY = "";
            this.IsModify = false;
            this.ShowApportion = true;
            this.ShowContractBudget = true;
            this.ShowTargetChange = true;
            this.ShowTargetHis = false;
            this.MaxCBSDeep = 0x270f;
            this.AutoRefreshHtml = true;
            this.m_entityBackup = null;
            this.m_entityBackupSet = null;
            this.m_arrDynProject = null;
            this.AutoRefreshDynProject = true;
            this.ShowContractAccountMoney = false;
            this.entityTargetHis = null;
            this.dvTargetHis = null;
            this.m_ProjectCode = a_ProjectCode;
            this.m_CostBudgetSetCode = a_CostBudgetSetCode;
            this.m_CostBudgetBackupCode = a_CostBudgetBackupCode;
            if (this.m_CostBudgetBackupCode == "")
            {
                this.InitSetInfo();
                this.InitValidCostBudgetInfo();
            }
        }

        public void AddRowBalance(DataRow drDtl, DataRow[] drsContract, DataRow[] drsNoContract, DataRow[] drsBalance)
        {
            try
            {
                if ((drsBalance.Length > 0) && (drDtl != null))
                {
                    string text = ConvertRule.ToString(drDtl["CostCode"]);
                    if ((((ConvertRule.ToBool(drDtl["IsLeafCBS"]) || (drsContract.Length > 0)) || (drsNoContract.Length > 0)) && !ConvertRule.ToString(drDtl["ContractCode"]).StartsWith("Balance_")) && ((((ConvertRule.ToDecimal(drsBalance[0]["ContractMoney"]) != 0M) || (ConvertRule.ToDecimal(drsBalance[0]["ContractChangeMoney"]) != 0M)) || (ConvertRule.ToDecimal(drsBalance[0]["ContractApplyMoney"]) != 0M)) || (this.tbContractMonth.Select(string.Format("ContractCode = 'Balance_{0}'", text)).Length != 0)))
                    {
                        foreach (DataRow row in drsBalance)
                        {
                            DataRow drDst = this.tbHtml.NewRow();
                            ConvertRule.DataRowCopy(row, drDst, this.tbBalance, this.tbHtml);
                            drDst["CostBudgetDtlCode"] = "B_" + ConvertRule.ToString(row["CostCode"]) + ":" + ConvertRule.ToString(row["ContractCode"]);
                            drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdBalance;
                            drDst["Deep"] = drDtl["Deep"];
                            drDst["ParentCode"] = drDtl["ParentCode"];
                            drDst["SortID"] = drDtl["SortID"];
                            drDst["FullCode"] = drDtl["FullCode"];
                            drDst["SupplierNameHtml"] = CostBudgetPageRule.GetSupplierHref(ConvertRule.ToString(drDst["SupplierCode"]), ConvertRule.ToString(drDst["SupplierName"]));
                            drDst["ContractIDHtml"] = drDst["ContractID"];
                            drDst["ContractNameHtml"] = drDst["ContractName"];
                            drDst["DescriptionHtml"] = drDst["Description"];
                            drDst["RecordType"] = "Balance";
                            this.tbHtml.Rows.Add(drDst);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void AddRowContract(DataRow drDtl, DataRow[] drsContract)
        {
            try
            {
                if ((drsContract.Length > 0) && (ConvertRule.ToString(drDtl["ContractCode"]) == ""))
                {
                    foreach (DataRow row in drsContract)
                    {
                        DataRow drDst = this.tbHtml.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, this.tbContract, this.tbHtml);
                        drDst["CostBudgetDtlCode"] = "C_" + ConvertRule.ToString(row["CostCode"]) + ":" + ConvertRule.ToString(row["ContractCode"]);
                        drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdContract;
                        drDst["Deep"] = drDtl["Deep"];
                        drDst["ParentCode"] = drDtl["ParentCode"];
                        drDst["SortID"] = drDtl["SortID"];
                        drDst["FullCode"] = drDtl["FullCode"];
                        drDst["SupplierNameHtml"] = CostBudgetPageRule.GetSupplierHref(ConvertRule.ToString(drDst["SupplierCode"]), ConvertRule.ToString(drDst["SupplierName"]));
                        drDst["ContractIDHtml"] = drDst["ContractID"];
                        drDst["ContractNameHtml"] = CostBudgetPageRule.GetContractHref(ConvertRule.ToString(drDst["ContractCode"]), ConvertRule.ToString(drDst["ContractName"]));
                        drDst["DescriptionHtml"] = drDst["Description"];
                        this.tbHtml.Rows.Add(drDst);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void AddRowNoContract(DataRow drDtl, DataRow[] drsNoContract)
        {
            try
            {
                if ((drsNoContract.Length > 0) && (ConvertRule.ToString(drDtl["ContractCode"]) == ""))
                {
                    foreach (DataRow row in drsNoContract)
                    {
                        DataRow drDst = this.tbHtml.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, this.tbNoContract, this.tbHtml);
                        drDst["CostBudgetDtlCode"] = "C_" + ConvertRule.ToString(row["CostCode"]) + ":" + ConvertRule.ToString(row["ContractCode"]);
                        drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdNoContract;
                        drDst["Deep"] = drDtl["Deep"];
                        drDst["ParentCode"] = drDtl["ParentCode"];
                        drDst["SortID"] = drDtl["SortID"];
                        drDst["FullCode"] = drDtl["FullCode"];
                        drDst["SupplierNameHtml"] = CostBudgetPageRule.GetSupplierHref(ConvertRule.ToString(drDst["SupplierCode"]), ConvertRule.ToString(drDst["SupplierName"]));
                        drDst["ContractIDHtml"] = drDst["ContractID"];
                        drDst["ContractNameHtml"] = CostBudgetPageRule.GetContractHref(ConvertRule.ToString(drDst["ContractCode"]), ConvertRule.ToString(drDst["ContractName"]));
                        drDst["DescriptionHtml"] = drDst["Description"];
                        this.tbHtml.Rows.Add(drDst);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Backup(EntityData entity)
        {
            try
            {
                DataRow drDst;
                DataRow row4;
                string text = entity.Tables["CostBudgetBackup"].Rows[0]["CostBudgetBackupCode"].ToString();
                entity.SetCurrentTable("CostBudgetBackupSet");
                DataRow row = entity.CurrentTable.NewRow();
                ConvertRule.DataRowCopy(this.entitySet.CurrentRow, row, this.entitySet.CurrentTable, entity.CurrentTable);
                string text2 = text + "_" + this.CostBudgetSetCode;
                row["CostBudgetBackupCode"] = text;
                row["CostBudgetBackupSetCode"] = text2;
                row["CostBudgetSetCode"] = this.CostBudgetSetCode;
                row["ProjectCode"] = this.ProjectCode;
                if (this.entityCurrTarget.HasRecord())
                {
                    row["VerID"] = this.entityCurrTarget.CurrentRow["VerID"];
                    row["CreatePerson"] = this.entityCurrTarget.CurrentRow["CreatePerson"];
                    row["CreateDate"] = this.entityCurrTarget.CurrentRow["CreateDate"];
                    row["ModifyPerson"] = this.entityCurrTarget.CurrentRow["ModifyPerson"];
                    row["ModifyDate"] = this.entityCurrTarget.CurrentRow["ModifyDate"];
                    row["CheckPerson"] = this.entityCurrTarget.CurrentRow["CheckPerson"];
                    row["CheckDate"] = this.entityCurrTarget.CurrentRow["CheckDate"];
                    row["Description"] = this.entityCurrTarget.CurrentRow["Description"];
                }
                row["TargetChangeFlag"] = this.m_TargetChangeFlag;
                EntityData data = CostBudgetRule.GetValidCostBudget(this.CostBudgetSetCode, 0, false);
                if (data.HasRecord())
                {
                    row["DynamicModifyPerson"] = data.CurrentRow["ModifyPerson"];
                    row["DynamicModifyDate"] = data.CurrentRow["ModifyDate"];
                }
                data.Dispose();
                row["RecordType"] = "";
                entity.CurrentTable.Rows.Add(row);
                entity.SetCurrentTable("CostBudgetBackupDtl");
                int num = 0;
                foreach (DataRow row2 in this.tb.Rows)
                {
                    drDst = entity.CurrentTable.NewRow();
                    num++;
                    ConvertRule.DataRowCopy(row2, drDst, this.tb, entity.CurrentTable);
                    drDst["CostBudgetBackupDtlCode"] = text2 + "_" + num.ToString().PadLeft(6, "0"[0]);
                    drDst["CostBudgetBackupCode"] = text;
                    drDst["CostBudgetBackupSetCode"] = text2;
                    drDst["ProjectCode"] = this.ProjectCode;
                    drDst["RecordType"] = "";
                    entity.CurrentTable.Rows.Add(drDst);
                }
                foreach (DataRow row2 in this.tbContract.Rows)
                {
                    drDst = entity.CurrentTable.NewRow();
                    num++;
                    ConvertRule.DataRowCopy(row2, drDst, this.tbContract, entity.CurrentTable);
                    drDst["CostBudgetBackupDtlCode"] = text2 + "_" + num.ToString().PadLeft(6, "0"[0]);
                    drDst["CostBudgetBackupCode"] = text;
                    drDst["CostBudgetBackupSetCode"] = text2;
                    drDst["ProjectCode"] = this.ProjectCode;
                    entity.CurrentTable.Rows.Add(drDst);
                }
                foreach (DataRow row2 in this.tbNoContract.Rows)
                {
                    drDst = entity.CurrentTable.NewRow();
                    num++;
                    ConvertRule.DataRowCopy(row2, drDst, this.tbNoContract, entity.CurrentTable);
                    drDst["CostBudgetBackupDtlCode"] = text2 + "_" + num.ToString().PadLeft(6, "0"[0]);
                    drDst["CostBudgetBackupCode"] = text;
                    drDst["CostBudgetBackupSetCode"] = text2;
                    drDst["ProjectCode"] = this.ProjectCode;
                    drDst["RecordType"] = "NoContract";
                    entity.CurrentTable.Rows.Add(drDst);
                }
                foreach (DataRow row2 in this.tbBalance.Rows)
                {
                    drDst = entity.CurrentTable.NewRow();
                    num++;
                    ConvertRule.DataRowCopy(row2, drDst, this.tbBalance, entity.CurrentTable);
                    drDst["CostBudgetBackupDtlCode"] = text2 + "_" + num.ToString().PadLeft(6, "0"[0]);
                    drDst["CostBudgetBackupCode"] = text;
                    drDst["CostBudgetBackupSetCode"] = text2;
                    drDst["ProjectCode"] = this.ProjectCode;
                    drDst["RecordType"] = "Balance";
                    entity.CurrentTable.Rows.Add(drDst);
                }
                entity.SetCurrentTable("CostBudgetBackupMonth");
                num = 0;
                foreach (DataRow row2 in this.tbMonth.Rows)
                {
                    row4 = entity.CurrentTable.NewRow();
                    num++;
                    ConvertRule.DataRowCopy(row2, row4, this.tbMonth, entity.CurrentTable);
                    row4["CostBudgetBackupMonthCode"] = text2 + "_" + num.ToString().PadLeft(6, "0"[0]);
                    row4["CostBudgetBackupCode"] = text;
                    row4["CostBudgetBackupSetCode"] = text2;
                    row4["ProjectCode"] = this.ProjectCode;
                    entity.CurrentTable.Rows.Add(row4);
                }
                foreach (DataRow row2 in this.tbContractMonth.Rows)
                {
                    row4 = entity.CurrentTable.NewRow();
                    num++;
                    ConvertRule.DataRowCopy(row2, row4, this.tbContractMonth, entity.CurrentTable);
                    row4["CostBudgetBackupMonthCode"] = text2 + "_" + num.ToString().PadLeft(6, "0"[0]);
                    row4["CostBudgetBackupCode"] = text;
                    row4["CostBudgetBackupSetCode"] = text2;
                    row4["ProjectCode"] = this.ProjectCode;
                    entity.CurrentTable.Rows.Add(row4);
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
                CostBudgetPageRule.CalcPercent(dr, this.entitySet.CurrentRow);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void CostBudgetDtlAddRelationRow(DataTable tbContract, DataTable tbSrc, string MoneyField, CostBudgetPageRule.m_DynamicRowType RowType)
        {
            try
            {
                foreach (DataRow row in tbSrc.Rows)
                {
                    DataRow row2;
                    string text = ConvertRule.ToString(row["ContractCode"]);
                    string text2 = row["ContractCode"].ToString();
                    DataRow[] rowArray = tbContract.Select("ContractCode = '" + text2 + "' and CostCode='" + ConvertRule.ToString(row["CostCode"]) + "'");
                    if (rowArray.Length == 0)
                    {
                        row2 = tbContract.NewRow();
                        row2["ContractCode"] = text2;
                        row2["ContractID"] = row["ContractID"];
                        row2["ContractName"] = row["ContractName"];
                        if (tbSrc.Columns.Contains("SupplierCode"))
                        {
                            row2["SupplierName"] = row["SupplierName"];
                            row2["SupplierCode"] = row["SupplierCode"];
                        }
                        row2["CostCode"] = row["CostCode"];
                        row2["FullCode"] = row["FullCode"];
                        if (tbSrc.Columns.Contains("Description"))
                        {
                            row2["Description"] = row["Description"];
                            row2["DescriptionHtml"] = row["Description"];
                        }
                        if (tbSrc.Columns.Contains("RecordType"))
                        {
                            row2["RecordType"] = row["RecordType"];
                        }
                        else
                        {
                            row2["RecordType"] = "";
                        }
                        if (tbSrc.Columns.Contains("ContractPay"))
                        {
                            row2["ContractPay"] = row["ContractPay"];
                        }
                        if (tbSrc.Columns.Contains("ContractPayReal"))
                        {
                            row2["ContractPayReal"] = row["ContractPayReal"];
                        }
                        tbContract.Rows.Add(row2);
                    }
                    else
                    {
                        row2 = rowArray[0];
                    }
                    string text3 = ConvertRule.ToString(row2["AllContractCode"]);
                    if (text3.IndexOf("'" + text + "'") < 0)
                    {
                        if (text3 != "")
                        {
                            text3 = text3 + ",";
                        }
                        text3 = text3 + "'" + text + "'";
                    }
                    row2["AllContractCode"] = text3;
                    row2[MoneyField] = ConvertRule.ToDecimal(row2[MoneyField]) + ConvertRule.ToDecimal(row["Money"]);
                    if (tbSrc.Columns.Contains("PlanningPayDate") && (row["PlanningPayDate"] != DBNull.Value))
                    {
                        string text4 = ((DateTime) row["PlanningPayDate"]).ToString("yyyyMM");
                        string name = "ContractMoney_" + text4;
                        if (tbContract.Columns.Contains(name))
                        {
                            row2[name] = ConvertRule.ToDecimal(row2[name]) + ConvertRule.ToDecimal(row["Money"]);
                        }
                        string text6 = ((DateTime) row["PlanningPayDate"]).ToString("yyyy") + "00";
                        name = "ContractMoney_" + text6;
                        if (tbContract.Columns.Contains(name))
                        {
                            row2[name] = ConvertRule.ToDecimal(row2[name]) + ConvertRule.ToDecimal(row["Money"]);
                        }
                    }
                    CostBudgetPageRule.CostBudgetDtlCalcField(row2, RowType, this.entitySet.CurrentRow, this.iStartY, this.iEndY);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void CostBudgetDtlReCalcByRelation()
        {
            try
            {
                DataRow drArea = this.entitySet.CurrentRow;
                foreach (DataRow row2 in this.tb.Rows)
                {
                    string text = ConvertRule.ToString(row2["CostCode"]);
                    string text2 = ConvertRule.ToString(row2["FullCode"]);
                    if (ConvertRule.ToString(row2["FullCode"]) != "")
                    {
                        DataRow[] drs = this.tbContract.Select("FullCode like '" + text2 + "%'");
                        string[] arrColumnName = new string[] { "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractPay", "ContractPayReal" };
                        decimal[] numArray = MathRule.SumColumn(drs, arrColumnName);
                        row2["ContractMoney"] = numArray[0];
                        row2["ContractChangeMoney"] = numArray[1];
                        row2["ContractApplyMoney"] = numArray[2];
                        row2["ContractAccountMoney"] = numArray[3];
                        row2["ContractPay"] = numArray[4];
                        row2["ContractPayReal"] = numArray[5];
                        DataRow[] rowArray2 = this.tbNoContract.Select("FullCode like '" + text2 + "%'");
                        arrColumnName = new string[] { "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractPay", "ContractPayReal" };
                        numArray = MathRule.SumColumn(rowArray2, arrColumnName);
                        row2["ContractMoney"] = ConvertRule.ToDecimal(row2["ContractMoney"]) + numArray[0];
                        row2["ContractChangeMoney"] = ConvertRule.ToDecimal(row2["ContractChangeMoney"]) + numArray[1];
                        row2["ContractApplyMoney"] = ConvertRule.ToDecimal(row2["ContractApplyMoney"]) + numArray[2];
                        row2["ContractAccountMoney"] = ConvertRule.ToDecimal(row2["ContractAccountMoney"]) + numArray[3];
                        row2["ContractPay"] = ConvertRule.ToDecimal(row2["ContractPay"]) + numArray[4];
                        row2["ContractPayReal"] = ConvertRule.ToDecimal(row2["ContractPayReal"]) + numArray[5];
                        row2["BalanceContractMoney"] = 0;
                        DataRow[] rowArray3 = this.tbBalance.Select("FullCode like '" + text2 + "%'");
                        arrColumnName = new string[] { "ContractMoney", "ContractChangeMoney", "ContractApplyMoney", "ContractAccountMoney", "ContractPay", "ContractPayReal" };
                        numArray = MathRule.SumColumn(rowArray3, arrColumnName);
                        row2["ContractMoney"] = ConvertRule.ToDecimal(row2["ContractMoney"]) + numArray[0];
                        row2["ContractChangeMoney"] = ConvertRule.ToDecimal(row2["ContractChangeMoney"]) + numArray[1];
                        row2["ContractApplyMoney"] = ConvertRule.ToDecimal(row2["ContractApplyMoney"]) + numArray[2];
                        row2["ContractAccountMoney"] = ConvertRule.ToDecimal(row2["ContractAccountMoney"]) + numArray[3];
                        row2["ContractPay"] = ConvertRule.ToDecimal(row2["ContractPay"]) + numArray[4];
                        row2["ContractPayReal"] = ConvertRule.ToDecimal(row2["ContractPayReal"]) + numArray[5];
                        CostBudgetPageRule.CostBudgetDtlCalcField(row2, CostBudgetPageRule.m_DynamicRowType.CBS, drArea, this.iStartY, this.iEndY);
                        row2["ContractCode"] = DBNull.Value;
                        row2["ContractID"] = DBNull.Value;
                        row2["ContractName"] = DBNull.Value;
                        row2["SupplierCode"] = DBNull.Value;
                        row2["SupplierName"] = DBNull.Value;
                        row2["DescriptionHtml"] = row2["Description"];
                        if (this.m_IsUnionOneContract && ConvertRule.ToBool(row2["IsLeafCBS"]))
                        {
                            int num = 0;
                            int num2 = 0;
                            DataRow row3 = null;
                            if (num < 2)
                            {
                                foreach (DataRow row4 in drs)
                                {
                                    if (ConvertRule.ToString(row4["CostCode"]) == ConvertRule.ToString(row2["CostCode"]))
                                    {
                                        num++;
                                        if (row3 == null)
                                        {
                                            row3 = row4;
                                            num2 = 1;
                                        }
                                        else
                                        {
                                            row3 = null;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (num < 2)
                            {
                                foreach (DataRow row5 in rowArray2)
                                {
                                    if (ConvertRule.ToString(row5["CostCode"]) == ConvertRule.ToString(row2["CostCode"]))
                                    {
                                        num++;
                                        if (row3 == null)
                                        {
                                            row3 = row5;
                                            num2 = 1;
                                        }
                                        else
                                        {
                                            row3 = null;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (((num < 2) && (rowArray3.Length == 1)) && ((ConvertRule.ToDecimal(rowArray3[0]["ContractApplyMoney"]) != 0M) || (this.tbContractMonth.Select(string.Format("ContractCode = 'Balance_{0}'", text)).Length > 0)))
                            {
                                if (row3 == null)
                                {
                                    row3 = rowArray3[0];
                                    num2 = 3;
                                }
                                num++;
                            }
                            if ((num == 1) && (row3 != null))
                            {
                                switch (num2)
                                {
                                    case 1:
                                    case 2:
                                        row2["ContractCode"] = row3["ContractCode"];
                                        row2["ContractID"] = row3["ContractID"];
                                        row2["ContractName"] = row3["ContractName"];
                                        row2["SupplierCode"] = row3["SupplierCode"];
                                        row2["SupplierName"] = row3["SupplierName"];
                                        if (ConvertRule.ToString(row3["Description"]).Trim() != "")
                                        {
                                            row2["DescriptionHtml"] = string.Format("<span class='{1}'>{0}</span>", ConvertRule.ToString(row3["Description"]), CostBudgetPageRule.m_ClassTdContract);
                                        }
                                        break;

                                    default:
                                        row2["ContractCode"] = rowArray3[0]["ContractCode"];
                                        row2["ContractName"] = rowArray3[0]["ContractName"];
                                        if (ConvertRule.ToString(rowArray3[0]["Description"]).Trim() != "")
                                        {
                                            row2["DescriptionHtml"] = string.Format("<span class='{1}'>{0}</span>", ConvertRule.ToString(rowArray3[0]["Description"]), CostBudgetPageRule.m_ClassTdBalance);
                                        }
                                        break;
                                }
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
                    foreach (DataRow row2 in this.tb.Rows)
                    {
                        string text3 = "";
                        foreach (string text in list)
                        {
                            text2 = "BudgetMoneyHis_" + text;
                            text3 = text3 + string.Format("<td align=right nowrap title='{1}'>{0}</td>", CostBudgetPageRule.GetMoneyShowString(row2[text2], this.MoneyUnit), CostBudgetPageRule.GetWanDecimalShowHint(row2[text2]));
                        }
                        row2["BudgetMoneyHisHtml"] = text3;
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

        private void FillTarget()
        {
            try
            {
                EntityData costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(this.CostTargetCode);
                this.tbMonth.Clear();
                if ((this.StartY != "") && (this.EndY != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        string queryString = string.Format("select d.CostCode     , c.FullCode     , m.IYear, m.IMonth, m.BudgetMoney as Money     , '' as RecordType  from CostBudgetMonth m     , CostBudgetDtl d       left join CBS c on c.CostCode = d.CostCode where m.CostBudgetDtlCode = d.CostBudgetDtlCode and m.BudgetMoney <> 0 and d.CostBudgetCode = '{0}' and m.IYear between {1} and {2}", this.CostTargetCode, this.iStartY, this.iEndY);
                        DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row in tbSrc.Rows)
                        {
                            DataRow drDst = this.tbMonth.NewRow();
                            ConvertRule.DataRowCopy(row, drDst, tbSrc, this.tbMonth);
                            this.tbMonth.Rows.Add(drDst);
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                foreach (DataRow row3 in this.tb.Rows)
                {
                    row3["Price"] = DBNull.Value;
                    row3["Qty"] = DBNull.Value;
                    row3["BudgetMoney"] = DBNull.Value;
                    row3["IsExpand"] = DBNull.Value;
                    row3["Description"] = DBNull.Value;
                    DataRow[] rowArray = costBudgetDtlByCostBudgetCode.CurrentTable.Select("CostCode='" + ConvertRule.ToString(row3["CostCode"]) + "'");
                    if (rowArray.Length > 0)
                    {
                        row3["Price"] = rowArray[0]["Price"];
                        row3["Qty"] = rowArray[0]["Qty"];
                        row3["BudgetMoney"] = rowArray[0]["BudgetMoney"];
                        row3["IsExpand"] = rowArray[0]["IsExpand"];
                        row3["Description"] = rowArray[0]["Description"];
                    }
                }
                costBudgetDtlByCostBudgetCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void FillTargetChangeMoney(string ChangeMoneyField, string ChangeDescriptionField)
        {
            try
            {
                this.m_TargetChangeFlag = 0;
                if (this.tb.Columns.Contains(ChangeMoneyField) && this.tb.Columns.Contains(ChangeDescriptionField))
                {
                    EntityData data = CostBudgetDAO.GetCostBudgetByStatus(this.CostBudgetSetCode, 1, "0,3", false);
                    if (data.HasRecord())
                    {
                        this.m_TargetChangeFlag = (data.GetInt("Status") == 3) ? 2 : 1;
                        EntityData costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(data.GetString("CostBudgetCode"));
                        if (costBudgetDtlByCostBudgetCode.HasRecord())
                        {
                            foreach (DataRow row in this.tb.Rows)
                            {
                                DataRow[] rowArray = costBudgetDtlByCostBudgetCode.CurrentTable.Select("CostCode = '" + ConvertRule.ToString(row["CostCode"]) + "'");
                                if (rowArray.Length > 0)
                                {
                                    row[ChangeMoneyField] = rowArray[0]["BudgetMoney"];
                                    row[ChangeDescriptionField] = rowArray[0]["Description"];
                                }
                            }
                        }
                        costBudgetDtlByCostBudgetCode.Dispose();
                    }
                    data.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public DataSet Generate()
        {
            DataSet ds;
            try
            {
                this.m_ds = CostBudgetPageRule.GenerateEmptyCostBudgetDtl(this.StartY, this.EndY, null);
                if (this.CostBudgetBackupCode != "")
                {
                    DataRow drDst;
                    this.m_entityBackup = CostBudgetDAO.GetCostBudgetBackupByCode(this.CostBudgetBackupCode);
                    this.m_entityBackupSet = CostBudgetDAO.GetCostBudgetBackupSetByBackupCode(this.CostBudgetBackupCode, this.CostBudgetSetCode, true);
                    this.m_entitySet = this.m_entityBackupSet;
                    this.m_TargetChangeFlag = this.m_entityBackupSet.GetInt("TargetChangeFlag");
                    EntityData costBudgetBackupDtlByBackupSetCode = CostBudgetDAO.GetCostBudgetBackupDtlByBackupSetCode(this.m_entityBackupSet.GetString("CostBudgetBackupSetCode"), false);
                    DataView view = new DataView(costBudgetBackupDtlByBackupSetCode.CurrentTable, "isnull(RecordType, '') = ''", "CostBudgetBackupDtlCode", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        drDst = this.tb.NewRow();
                        ConvertRule.DataRowCopy(view2.Row, drDst, costBudgetBackupDtlByBackupSetCode.CurrentTable, this.tb);
                        drDst["CostBudgetDtlCode"] = view2.Row["CostCode"];
                        drDst["IsLeafCBS"] = ConvertRule.ToInt(drDst["ChildCount"]) <= 0;
                        drDst["DescriptionHtml"] = drDst["Description"];
                        drDst["DescriptionHtml"] = drDst["Description"];
                        this.tb.Rows.Add(drDst);
                    }
                    view = new DataView(costBudgetBackupDtlByBackupSetCode.CurrentTable, "RecordType in ('Contract', 'Bidding')", "CostBudgetBackupDtlCode", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        drDst = this.tbContract.NewRow();
                        ConvertRule.DataRowCopy(view2.Row, drDst, costBudgetBackupDtlByBackupSetCode.CurrentTable, this.tbContract);
                        drDst["DescriptionHtml"] = drDst["Description"];
                        this.tbContract.Rows.Add(drDst);
                    }
                    view = new DataView(costBudgetBackupDtlByBackupSetCode.CurrentTable, "RecordType = 'NoContract'", "CostBudgetBackupDtlCode", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        drDst = this.tbNoContract.NewRow();
                        ConvertRule.DataRowCopy(view2.Row, drDst, costBudgetBackupDtlByBackupSetCode.CurrentTable, this.tbNoContract);
                        drDst["DescriptionHtml"] = drDst["Description"];
                        this.tbNoContract.Rows.Add(drDst);
                    }
                    view = new DataView(costBudgetBackupDtlByBackupSetCode.CurrentTable, "RecordType = 'Balance'", "CostBudgetBackupDtlCode", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        drDst = this.tbBalance.NewRow();
                        ConvertRule.DataRowCopy(view2.Row, drDst, costBudgetBackupDtlByBackupSetCode.CurrentTable, this.tbBalance);
                        drDst["DescriptionHtml"] = drDst["Description"];
                        this.tbBalance.Rows.Add(drDst);
                    }
                    costBudgetBackupDtlByBackupSetCode.Dispose();
                    if ((this.StartY != "") && (this.EndY != ""))
                    {
                        this.tbMonth.Clear();
                        this.tbContractMonth.Clear();
                        QueryAgent agent = new QueryAgent();
                        try
                        {
                            DataRow drSrc;
                            DataRow row3;
                            string queryString = string.Format("select *  from CostBudgetBackupMonth where Money <> 0 and CostBudgetBackupSetCode = '{0}' and IYear between {1} and {2}", this.m_entityBackupSet.GetString("CostBudgetBackupSetCode"), this.iStartY, this.iEndY);
                            DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                            view = new DataView(table, "isnull(RecordType, '') = ''", "", DataViewRowState.CurrentRows);
                            foreach (DataRowView view3 in view)
                            {
                                drSrc = view3.Row;
                                row3 = this.tbMonth.NewRow();
                                ConvertRule.DataRowCopy(drSrc, row3, table, this.tbMonth);
                                this.tbMonth.Rows.Add(row3);
                            }
                            view = new DataView(table, "isnull(RecordType, '') <> ''", "", DataViewRowState.CurrentRows);
                            foreach (DataRowView view3 in view)
                            {
                                drSrc = view3.Row;
                                row3 = this.tbContractMonth.NewRow();
                                ConvertRule.DataRowCopy(drSrc, row3, table, this.tbContractMonth);
                                this.tbContractMonth.Rows.Add(row3);
                            }
                        }
                        finally
                        {
                            agent.Dispose();
                        }
                    }
                }
                else
                {
                    this.m_CostTargetCode = "";
                    this.m_entityCurrTarget = CostBudgetRule.GetValidCostBudget(this.CostBudgetSetCode, 1);
                    if (this.entityCurrTarget.HasRecord())
                    {
                        this.m_CostTargetCode = this.entityCurrTarget.GetString("CostBudgetCode");
                    }
                    this.GenerateCurrentCostTargetDtl();
                    this.SetCostBudgetDtlRelationData();
                    this.SumR0(GetR0(this.tb));
                    if (this.ShowApportion)
                    {
                        this.RefreshApportion();
                    }
                    if (this.ShowContractBudget)
                    {
                        this.RefreshCostBudgetContract();
                    }
                }
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

        private void GenerateCurrentCostTargetDtl()
        {
            try
            {
                DataRow row = null;
                if (this.NeedApport)
                {
                    row = this.tb.NewRow();
                    row["CostBudgetDtlCode"] = "R_Total";
                    row["CostCode"] = row["CostBudgetDtlCode"];
                    row["CostName"] = "分摊后总金额";
                    row["Deep"] = 1;
                    row["ParentCode"] = "";
                    row["ChildCount"] = 1;
                    row["IsExpand"] = 1;
                    this.tb.Rows.Add(row);
                }
                DataRow row2 = this.tb.NewRow();
                row2["CostBudgetDtlCode"] = "R_0";
                row2["CostCode"] = row2["CostBudgetDtlCode"];
                if (this.NeedApport)
                {
                    row2["CostName"] = this.m_entitySet.GetString("CostBudgetSetName") + "分摊前总金额";
                    row2["Deep"] = 2;
                    row2["ParentCode"] = row["CostBudgetDtlCode"];
                }
                else
                {
                    row2["CostName"] = this.m_entitySet.GetString("CostBudgetSetName") + "合计";
                    row2["Deep"] = 1;
                    row2["ParentCode"] = "";
                }
                row2["ChildCount"] = 1;
                row2["IsExpand"] = 1;
                this.tb.Rows.Add(row2);
                EntityData allCBSBySet = CostBudgetRule.GetAllCBSBySet(this.ProjectCode, this.CostBudgetSetCode);
                if (allCBSBySet != null)
                {
                    foreach (DataRow row3 in allCBSBySet.CurrentTable.Rows)
                    {
                        if (ConvertRule.ToInt(row3["Deep"]) <= this.MaxCBSDeep)
                        {
                            DataRow drDtl = this.tb.NewRow();
                            drDtl["CostBudgetDtlCode"] = row3["CostCode"];
                            CostBudgetPageRule.FillCostBudgetDtlCBSData(drDtl, row3);
                            if (ConvertRule.ToInt(drDtl["Deep"]) == 1)
                            {
                                drDtl["ParentCode"] = row2["CostBudgetDtlCode"];
                            }
                            drDtl["Deep"] = ConvertRule.ToInt(drDtl["Deep"]) + ConvertRule.ToInt(row2["Deep"]);
                            this.tb.Rows.Add(drDtl);
                        }
                    }
                    this.FillTarget();
                    if (this.ShowTargetChange)
                    {
                        this.FillTargetChangeMoney("BudgetChangeMoney", "BudgetChangeDescription");
                    }
                    if (this.ShowTargetHis)
                    {
                        this.LoadTargetHis();
                    }
                    if (this.NeedApport)
                    {
                        DataRow row5 = this.tb.NewRow();
                        row5["CostBudgetDtlCode"] = "R_1";
                        row5["CostCode"] = row5["CostBudgetDtlCode"];
                        row5["CostName"] = "应分摊金额";
                        row5["Deep"] = 2;
                        row5["ParentCode"] = "R_Total";
                        row5["ChildCount"] = 0;
                        row5["IsExpand"] = 0;
                        this.tb.Rows.Add(row5);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void GenerateTargetHisHead(ref string head1, ref string head2)
        {
            try
            {
                head1 = "";
                head2 = "";
                if (this.ShowTargetHis)
                {
                    head1 = string.Format("<th align=center nowrap colspan='{0}'>历史预算</th>", this.dvTargetHis.Count);
                    foreach (DataRowView view in this.dvTargetHis)
                    {
                        head2 = head2 + string.Format("<th align=center nowrap>{0}<br>{1}</th>", ConvertRule.ToString(CostBudgetRule.GetCostBudgetVerName(view.Row)), ConvertRule.ToDateString(view.Row["CheckDate"], "yyyy-MM-dd"));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetBuildingCostBudgetSet(string ProjectCode, string GroupCode)
        {
            EntityData data2;
            try
            {
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.GroupCode, GroupCode));
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.PBSType, "B"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetChangeDescShowHtml(object HasChange, object Desc, object ChangeDesc, object ChangeDescHtml)
        {
            string text2;
            try
            {
                string text = "";
                if (!ConvertRule.ToBool(HasChange))
                {
                    return text;
                }
                if (ConvertRule.ToString(ChangeDesc).Trim() != ConvertRule.ToString(Desc).Trim())
                {
                    text = text + "(" + ConvertRule.ToString(ChangeDescHtml) + ")";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetChangeMoneyShowHtml(object HasChange, object Money, object ChangeMoney, object ChangeMoneyHtml, object RecordType)
        {
            string text2;
            try
            {
                string text = "";
                if (!ConvertRule.ToBool(HasChange))
                {
                    return text;
                }
                if (ConvertRule.ToString(RecordType) != "")
                {
                    return text;
                }
                if (!MathRule.CheckDecimalEqual(ConvertRule.ToDecimal(ChangeMoney), ConvertRule.ToDecimal(Money)))
                {
                    text = text + "(" + ConvertRule.ToString(ChangeMoneyHtml) + ")";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private string GetContractOriginalCode(DataRow drContract)
        {
            string text2;
            try
            {
                string text = ConvertRule.ToString(drContract["ContractCode"]);
                if (text.StartsWith("Bidding_"))
                {
                    text = text.Remove(0, "Bidding_".Length);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static CostBudgetDynamic[] GetProjectDynamicByGroup(string ProjectCode, string GroupCode, string BuildingCode, string CostBudgetBackupCode, string StartY, string EndY)
        {
            CostBudgetDynamic[] dynamicArray2;
            try
            {
                string buildingParentCode = "";
                if (BuildingCode != "")
                {
                    buildingParentCode = ProductRule.GetBuildingParentCode(BuildingCode);
                }
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.GroupCode, GroupCode));
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.PBSType, "P"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetSet", queryString);
                agent.Dispose();
                if (buildingParentCode != "")
                {
                    builder = new CostBudgetSetStrategyBuilder();
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.GroupCode, GroupCode));
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.PBSType, "B"));
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.PBSCode, buildingParentCode));
                    queryString = builder.BuildMainQueryString();
                    agent = new QueryAgent();
                    EntityData data2 = agent.FillEntityData("CostBudgetSet", queryString);
                    foreach (DataRow row in data2.CurrentTable.Rows)
                    {
                        DataRow drDst = data.CurrentTable.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, data2.CurrentTable, data.CurrentTable);
                        data.CurrentTable.Rows.Add(drDst);
                    }
                    data2.Dispose();
                    agent.Dispose();
                }
                CostBudgetDynamic[] dynamicArray = new CostBudgetDynamic[data.CurrentTable.Rows.Count];
                int index = -1;
                foreach (DataRow row2 in data.CurrentTable.Rows)
                {
                    index++;
                    string text3 = ConvertRule.ToString(row2["CostBudgetSetCode"]);
                    dynamicArray[index] = new CostBudgetDynamic(ProjectCode, text3, CostBudgetBackupCode);
                    dynamicArray[index].StartY = StartY;
                    dynamicArray[index].EndY = EndY;
                    dynamicArray[index].AutoRefreshHtml = false;
                    dynamicArray[index].Generate();
                }
                data.Dispose();
                dynamicArray2 = dynamicArray;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return dynamicArray2;
        }

        public static DataRow GetR0(DataTable tb)
        {
            return GetRow(tb, "R_0");
        }

        private static DataRow GetRow(DataTable tb, string CostBudgetDtlCode)
        {
            try
            {
                DataRow[] rowArray = tb.Select("CostBudgetDtlCode = '" + CostBudgetDtlCode + "'");
                if (rowArray.Length > 0)
                {
                    return rowArray[0];
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return null;
        }

        public static DataRow GetRTotal(DataTable tb)
        {
            return GetRow(tb, "R_Total");
        }

        public string GetTargetCheckDate()
        {
            string text2;
            try
            {
                string dateTimeOnlyDate = "";
                if (this.CostBudgetBackupCode != "")
                {
                    if (this.m_entityBackupSet.HasRecord())
                    {
                        dateTimeOnlyDate = this.m_entityBackupSet.GetDateTimeOnlyDate("CheckDate");
                    }
                }
                else if ((this.entityCurrTarget != null) && this.entityCurrTarget.HasRecord())
                {
                    dateTimeOnlyDate = this.entityCurrTarget.GetDateTimeOnlyDate("CheckDate");
                }
                text2 = dateTimeOnlyDate;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public string GetTargetVerID()
        {
            string text2;
            try
            {
                string text = "";
                if (this.CostBudgetBackupCode != "")
                {
                    if (this.m_entityBackupSet.HasRecord() && (this.m_entityBackupSet.GetDateTimeOnlyDate("CheckDate") != ""))
                    {
                        text = this.m_entityBackupSet.GetDecimal("VerID").ToString();
                    }
                }
                else if ((this.entityCurrTarget != null) && this.entityCurrTarget.HasRecord())
                {
                    text = this.entityCurrTarget.GetDecimal("VerID").ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public string GetTargetVerName()
        {
            string text2;
            try
            {
                string costBudgetVerName = "";
                if (this.CostBudgetBackupCode != "")
                {
                    if (this.m_entityBackupSet.HasRecord() && (this.m_entityBackupSet.GetDateTimeOnlyDate("CheckDate") != ""))
                    {
                        costBudgetVerName = "版本" + this.m_entityBackupSet.GetDecimal("VerID").ToString();
                    }
                }
                else if ((this.entityCurrTarget != null) && this.entityCurrTarget.HasRecord())
                {
                    costBudgetVerName = CostBudgetRule.GetCostBudgetVerName(this.entityCurrTarget.CurrentRow);
                }
                text2 = costBudgetVerName;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private void InitSetInfo()
        {
            try
            {
                this.m_entitySet = CostBudgetDAO.GetV_CostBudgetSetByCode(this.m_CostBudgetSetCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
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

        private void LoadTargetHis()
        {
            try
            {
                this.entityTargetHis = null;
                string firstCostBudgetCode = CostBudgetRule.GetFirstCostBudgetCode(this.ProjectCode, this.CostBudgetSetCode, 1);
                this.entityTargetHis = CostBudgetRule.GetRelationCostBudget(this.ProjectCode, firstCostBudgetCode, "2", "");
                this.dvTargetHis = new DataView(this.entityTargetHis.CurrentTable, "", "VerID desc", DataViewRowState.CurrentRows);
                CostBudgetPageRule.SetColumnTargetHis(this.tb, this.entityTargetHis);
                this.FillCostTargetHisHtml();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void RefreshApportion()
        {
            try
            {
                if (this.NeedApport)
                {
                    DataRow[] rowArray = this.tb.Select("CostBudgetDtlCode = 'R_1'");
                    if (rowArray.Length > 0)
                    {
                        DataRow dr = rowArray[0];
                        foreach (string text in this.m_arrMoneyField)
                        {
                            dr[text] = 0;
                        }
                        if (this.AutoRefreshDynProject || (this.m_arrDynProject == null))
                        {
                            this.m_arrDynProject = GetProjectDynamicByGroup(this.ProjectCode, this.entitySet.GetString("GroupCode"), this.entitySet.GetString("PBSCode"), this.CostBudgetBackupCode, this.StartY, this.EndY);
                        }
                        foreach (CostBudgetDynamic dynamic in this.m_arrDynProject)
                        {
                            DataRow row2 = GetR0(dynamic.tb);
                            CostApportion apportion = new CostApportion();
                            apportion.RoundDec = 0;
                            foreach (string text in this.m_arrMoneyField)
                            {
                                apportion.SetTotalMoney(text, ConvertRule.ToDecimal(row2[text]));
                            }
                            apportion.IsCustomTotalArea = true;
                            apportion.TotalArea = dynamic.entitySet.GetDecimal("BuildingArea");
                            apportion.SetArea(this.CostBudgetSetCode, this.entitySet.GetDecimal("BuildingArea"));
                            apportion.DoApportion();
                            DataRow[] rowArray2 = apportion.tbArea.Select("ID = '" + this.CostBudgetSetCode + "'");
                            if (rowArray2.Length > 0)
                            {
                                DataRow row3 = rowArray2[0];
                                foreach (string text in this.m_arrMoneyField)
                                {
                                    dr[text] = ConvertRule.ToDecimal(dr[text]) + ConvertRule.ToDecimal(row3[text]);
                                }
                                CostBudgetPageRule.CalcPercent(dr, this.entitySet.CurrentRow);
                            }
                            if ((this.StartY != "") && (this.EndY != ""))
                            {
                            }
                        }
                    }
                    this.SumRow(GetRTotal(this.tb));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshBalance()
        {
            try
            {
                CostBudgetPageRule.ClearFieldValue(this.tbNoContract, new string[] { "ContractTotalMoney", "Description" });
                this.SetCostBudgetDtlBalance();
                this.CostBudgetDtlReCalcByRelation();
                this.SumR0(GetR0(this.tb));
                this.SumRow(GetRTotal(this.tb));
                if (this.AutoRefreshHtml)
                {
                    this.RefreshHtml();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshBidding()
        {
            try
            {
                this.SetCostBudgetDtlRelationBidding(true);
                this.CostBudgetDtlReCalcByRelation();
                this.SumR0(GetR0(this.tb));
                this.SumRow(GetRTotal(this.tb));
                if (this.AutoRefreshHtml)
                {
                    this.RefreshHtml();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshChangeTarget()
        {
            try
            {
                this.FillTarget();
                CostBudgetPageRule.ClearFieldValue(this.tb.Select("CostBudgetDtlCode <> 'R_1'"), new string[] { "BudgetChangeMoney", "BudgetChangeDescription" });
                this.FillTargetChangeMoney("BudgetChangeMoney", "BudgetChangeDescription");
                this.CostBudgetDtlReCalcByRelation();
                this.SumR0(GetR0(this.tb));
                this.SumRow(GetRTotal(this.tb));
                if (this.AutoRefreshHtml)
                {
                    this.RefreshHtml();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshCostBudgetContract()
        {
            try
            {
                this.tbCostBudgetContract.Clear();
                EntityData costBudgetContractByCostBudgetSetCode = CostBudgetDAO.GetCostBudgetContractByCostBudgetSetCode(this.CostBudgetSetCode);
                foreach (DataRow row in costBudgetContractByCostBudgetSetCode.CurrentTable.Rows)
                {
                    DataRow drDst = this.tbCostBudgetContract.NewRow();
                    ConvertRule.DataRowCopy(row, drDst, costBudgetContractByCostBudgetSetCode.CurrentTable, this.tbCostBudgetContract);
                    this.tbCostBudgetContract.Rows.Add(drDst);
                }
                costBudgetContractByCostBudgetSetCode.Dispose();
                foreach (DataRow row3 in this.tbContract.Rows)
                {
                    DataRow[] rowArray;
                    string contractOriginalCode = this.GetContractOriginalCode(row3);
                    string text2 = ConvertRule.ToString(row3["RecordType"]);
                    string text3 = ConvertRule.ToString(row3["CostCode"]);
                    if (text2 == "")
                    {
                        text2 = "Contract";
                    }
                    decimal num = 0M;
                    decimal num2 = 0M;
                    if (text2 == "Bidding")
                    {
                        string text4 = "";
                        string[] textArray = contractOriginalCode.Split(new char[] { "#"[0] });
                        if (textArray.Length > 0)
                        {
                            text4 = textArray[0];
                        }
                        rowArray = this.tbCostBudgetContract.Select(string.Format("ContractCode = '{0}' and RelationType = '{1}' and CostCode = '{2}'", text4, text2, text3));
                        if (rowArray.Length > 0)
                        {
                            num = ConvertRule.ToDecimal(rowArray[0]["BudgetMoney"]);
                        }
                    }
                    else
                    {
                        rowArray = this.tbCostBudgetContract.Select(string.Format("ContractCode = '{0}' and RelationType = '{1}' and CostCode = '{2}'", contractOriginalCode, text2, text3));
                        if (rowArray.Length > 0)
                        {
                            num = ConvertRule.ToDecimal(rowArray[0]["BudgetMoney"]);
                        }
                    }
                    row3["BudgetMoney"] = num;
                    if (num > 0M)
                    {
                        num2 = ConvertRule.ToDecimal(row3["ContractTotalMoney"]) - ConvertRule.ToDecimal(row3["BudgetMoney"]);
                    }
                    row3["ContractBudgetBalance"] = num2;
                }
                if (this.AutoRefreshHtml)
                {
                    this.RefreshHtml();
                }
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
                if (!this.ds.Tables.Contains("Html"))
                {
                    this.ds.Tables.Add(CostBudgetPageRule.CreateDynamicHtmlTable(this.StartY, this.EndY, this.tb));
                }
                this.tbHtml.Clear();
                foreach (DataRow row in this.tb.Rows)
                {
                    string text = ConvertRule.ToString(row["CostBudgetDtlCode"]);
                    string text2 = ConvertRule.ToString(row["CostCode"]);
                    DataRow drDst = this.tbHtml.NewRow();
                    ConvertRule.DataRowCopy(row, drDst, this.tb, this.tbHtml);
                    drDst["SupplierNameHtml"] = CostBudgetPageRule.GetSupplierHref(ConvertRule.ToString(drDst["SupplierCode"]), ConvertRule.ToString(drDst["SupplierName"]));
                    drDst["ContractIDHtml"] = CostBudgetPageRule.GetSpan(drDst["ContractID"], CostBudgetPageRule.m_ClassTdContract);
                    if (ConvertRule.ToString(drDst["ContractName"]) == "预留金额")
                    {
                        drDst["ContractNameHtml"] = string.Format("<span class='{1}'>{0}</span>", ConvertRule.ToString(drDst["ContractName"]), CostBudgetPageRule.m_ClassTdBalance);
                    }
                    else
                    {
                        drDst["ContractNameHtml"] = CostBudgetPageRule.GetContractHref(ConvertRule.ToString(drDst["ContractCode"]), ConvertRule.ToString(drDst["ContractName"]));
                    }
                    drDst["DescriptionHtml"] = drDst["Description"];
                    drDst["ClassTd"] = CostBudgetPageRule.m_ClassTdCBS;
                    this.tbHtml.Rows.Add(drDst);
                    if (!text.StartsWith("R_"))
                    {
                        DataRow[] drsContract = this.tbContract.Select("CostCode = '" + text2 + "'", "ContractID, ContractName", DataViewRowState.CurrentRows);
                        this.AddRowContract(row, drsContract);
                        DataRow[] drsNoContract = this.tbNoContract.Select("CostCode = '" + text2 + "'");
                        this.AddRowNoContract(row, drsNoContract);
                        DataRow[] drsBalance = this.tbBalance.Select("CostCode = '" + text2 + "'");
                        this.AddRowBalance(row, drsContract, drsNoContract, drsBalance);
                    }
                }
                if ((this.StartY != "") && (this.EndY != ""))
                {
                    int objYear;
                    int objMonth;
                    string text3;
                    string name;
                    DataRow[] rowArray4;
                    foreach (DataRow row3 in this.tbMonth.Rows)
                    {
                        objYear = ConvertRule.ToInt(row3["IYear"]);
                        objMonth = ConvertRule.ToInt(row3["IMonth"]);
                        text3 = ConvertRule.FormatYYYYMM(objYear, objMonth);
                        name = "BudgetMoney_" + text3;
                        if (this.tbHtml.Columns.Contains(name))
                        {
                            rowArray4 = this.tbHtml.Select(string.Format("CostCode = '{0}'", ConvertRule.ToString(row3["CostCode"])));
                            if (rowArray4.Length > 0)
                            {
                                rowArray4[0][name] = row3["Money"];
                            }
                        }
                    }
                    foreach (DataRow row3 in this.tbContractMonth.Rows)
                    {
                        objYear = ConvertRule.ToInt(row3["IYear"]);
                        objMonth = ConvertRule.ToInt(row3["IMonth"]);
                        rowArray4 = this.tbHtml.Select(string.Format("CostCode = '{0}' and ContractCode = '{1}'", ConvertRule.ToString(row3["CostCode"]), ConvertRule.ToString(row3["ContractCode"])));
                        text3 = ConvertRule.FormatYYYYMM(objYear, objMonth);
                        name = "ContractMoney_" + text3;
                        if ((rowArray4.Length > 0) && this.tbHtml.Columns.Contains(name))
                        {
                            rowArray4[0][name] = row3["Money"];
                        }
                        text3 = ConvertRule.FormatYYYYMM(objYear, 0);
                        name = "ContractMoney_" + text3;
                        if ((rowArray4.Length > 0) && this.tbHtml.Columns.Contains(name))
                        {
                            rowArray4[0][name] = ConvertRule.ToDecimal(rowArray4[0][name]) + ConvertRule.ToDecimal(row3["Money"]);
                        }
                    }
                    string[] arrColumnName = CostBudgetPageRule.BuildArrayFieldByMonth(this.StartY, this.EndY, "ContractMoney_");
                    DataView view = new DataView(this.tbHtml, "isnull(RecordType, '') = ''", "", DataViewRowState.CurrentRows);
                    foreach (DataRowView view2 in view)
                    {
                        DataRow row = view2.Row;
                        string text5 = ConvertRule.ToString(row["FullCode"]);
                        if (text5 != "")
                        {
                            decimal[] numArray = MathRule.SumColumn(this.tbHtml.Select("FullCode like '" + text5 + "%'"), arrColumnName);
                            int index = -1;
                            foreach (string text6 in arrColumnName)
                            {
                                index++;
                                row[text6] = numArray[index];
                            }
                        }
                    }
                }
                this.SumR0(GetR0(this.tbHtml));
                this.SumRow(GetRTotal(this.tbHtml));
                if ((this.StartY != "") && (this.EndY != ""))
                {
                    foreach (DataRow row2 in this.tbHtml.Rows)
                    {
                        string text7 = ConvertRule.ToString(row2["RecordType"]);
                        string classTd = ConvertRule.ToString(row2["ClassTd"]);
                        if (text7 == "")
                        {
                            row2["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row2, this.iStartY, this.iEndY, new string[] { "BudgetMoney_", "ContractMoney_" }, new string[] { classTd, CostBudgetPageRule.m_ClassTdContract }, new string[] { "预算", "合同" });
                        }
                        else
                        {
                            row2["PlanDataHtml"] = CostBudgetPageRule.GenerateCostBudgetPlanDataHtml(row2, this.iStartY, this.iEndY, "ContractMoney_", classTd);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshMoneyUnit()
        {
            try
            {
                this.FillCostTargetHisHtml();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RefreshTargetHis()
        {
            try
            {
                this.LoadTargetHis();
                this.SumR0(GetR0(this.tb));
                this.SumRow(GetRTotal(this.tb));
                if (this.AutoRefreshHtml)
                {
                    this.RefreshHtml();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetCostBudgetDtlBalance()
        {
            try
            {
                DataTable table = this.ds.Tables["CostBudgetDtl"];
                DataTable table2 = this.ds.Tables["Balance"];
                DataTable tbDst = this.ds.Tables["ContractMonth"];
                table2.Rows.Clear();
                table2.AcceptChanges();
                DataRow[] rowArray = tbDst.Select("RecordType = 'Balance'");
                int length = rowArray.Length;
                for (int i = length - 1; i >= 0; i--)
                {
                    tbDst.Rows.Remove(rowArray[i]);
                }
                EntityData costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(this.CostBudgetCode);
                foreach (DataRow row in table.Rows)
                {
                    DataRow row2;
                    if (row["CostBudgetDtlCode"].ToString().StartsWith("R_"))
                    {
                        continue;
                    }
                    string text = ConvertRule.ToString(row["CostCode"]);
                    rowArray = table2.Select("CostCode = '" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        row2 = rowArray[0];
                    }
                    else
                    {
                        row2 = table2.NewRow();
                        row2["ContractCode"] = "Balance_" + row["CostCode"];
                        row2["ContractName"] = "预留金额";
                        row2["CostCode"] = row["CostCode"];
                        row2["FullCode"] = row["FullCode"];
                        table2.Rows.Add(row2);
                    }
                    if (ConvertRule.ToBool(row["IsLeafCBS"]))
                    {
                        DataRow[] rowArray2 = costBudgetDtlByCostBudgetCode.CurrentTable.Select("CostCode = '" + text + "'");
                        if (rowArray2.Length > 0)
                        {
                            row2["ContractApplyMoney"] = rowArray2[0]["BudgetMoney"];
                            row2["Description"] = rowArray2[0]["Description"];
                            row2["DescriptionHtml"] = rowArray2[0]["Description"];
                            CostBudgetPageRule.CostBudgetDtlCalcField(row2, CostBudgetPageRule.m_DynamicRowType.Balance, this.entitySet.CurrentRow, this.iStartY, this.iEndY);
                        }
                    }
                }
                costBudgetDtlByCostBudgetCode.Dispose();
                if (((this.iStartY != 0) && (this.iEndY != 0)) && (this.CostBudgetCode != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        string queryString = string.Format("select c.FullCode, a.CostCode, 'Balance' as RecordType, 'Balance_' + a.CostCode as ContractCode, b.IYear, b.IMonth, b.BudgetMoney as Money from CostBudgetDtl a left join CBS c on c.CostCode = a.CostCode, CostBudgetMonth b where a.CostBudgetDtlCode = b.CostBudgetDtlCode and a.ProjectCode = '{0}' and a.CostBudgetCode = '{1}' and b.IYear between {2} and {3}", new object[] { this.ProjectCode, this.CostBudgetCode, this.iStartY, this.iEndY });
                        DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row3 in tbSrc.Rows)
                        {
                            DataRow drDst = tbDst.NewRow();
                            ConvertRule.DataRowCopy(row3, drDst, tbSrc, tbDst);
                            tbDst.Rows.Add(drDst);
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetCostBudgetDtlRelationBidding(bool IsNeedClear)
        {
            try
            {
                DataTable table = this.ds.Tables["CostBudgetDtl"];
                DataTable tbContract = this.ds.Tables["Contract"];
                DataTable tbDst = this.ds.Tables["ContractMonth"];
                if (IsNeedClear)
                {
                    int index;
                    DataRow[] rowArray = tbContract.Select("RecordType = 'Bidding'");
                    int length = rowArray.Length;
                    for (index = length - 1; index >= 0; index--)
                    {
                        tbContract.Rows.Remove(rowArray[index]);
                    }
                    rowArray = tbDst.Select("RecordType = 'Contract'");
                    length = rowArray.Length;
                    for (index = length - 1; index >= 0; index--)
                    {
                        tbDst.Rows.Remove(rowArray[index]);
                    }
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select c.FullCode, 'Bidding' as RecordType, 'Bidding_' + b.BiddingCode + '#' + b.BiddingDtlCode as ContractCode, '' as ContractID, a.title + '(' + b.title + ')' as ContractName, b.* from Bidding a , BiddingDtl b left join CBS c on c.CostCode = b.CostCode where a.ProjectCode = '{0}' and b.CostBudgetSetCode = '{1}' and not isnull(a.state, 0) in (43) and a.BiddingCode = b.BiddingCode", this.ProjectCode, this.CostBudgetSetCode);
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractApplyMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    if ((this.iStartY != 0) && (this.iEndY != 0))
                    {
                        queryString = string.Format("select c.FullCode, 'Bidding' as RecordType, 'Bidding_' + a.BiddingCode + '#' + a.BiddingDtlCode as ContractCode, a.CostCode, Year(p.PlanningPayDate) as IYear, Month(p.PlanningPayDate) as IMonth, sum(isnull(p.Money, 0)) as Money from BiddingDtl a left join CBS c on c.CostCode = a.CostCode, Bidding b, BiddingDtlPlan p where a.BiddingCode = b.BiddingCode and a.BiddingDtlCode = p.BiddingDtlCode and not isnull(b.state, 0) in (43) and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and Year(p.PlanningPayDate) between {2} and {3} group by c.FullCode, a.CostCode, 'Bidding_' + a.BiddingCode + '#' + a.BiddingDtlCode, Year(p.PlanningPayDate), Month(p.PlanningPayDate)", new object[] { this.ProjectCode, this.CostBudgetSetCode, this.iStartY, this.iEndY });
                        tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row in tbSrc.Rows)
                        {
                            DataRow drDst = tbDst.NewRow();
                            ConvertRule.DataRowCopy(row, drDst, tbSrc, tbDst);
                            tbDst.Rows.Add(drDst);
                        }
                    }
                    CostBudgetPageRule.CostBudgetDtlCalcAllRow(tbContract, CostBudgetPageRule.m_DynamicRowType.Contract, this.entitySet.CurrentRow, this.iStartY, this.iEndY);
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

        private void SetCostBudgetDtlRelationContract(bool IsNeedClear)
        {
            try
            {
                DataTable table = this.ds.Tables["CostBudgetDtl"];
                DataTable tbContract = this.ds.Tables["Contract"];
                DataTable tbDst = this.ds.Tables["ContractMonth"];
                if (IsNeedClear)
                {
                    int index;
                    DataRow[] rowArray = tbContract.Select("RecordType = 'Contract'");
                    int length = rowArray.Length;
                    for (index = length - 1; index >= 0; index--)
                    {
                        tbContract.Rows.Remove(rowArray[index]);
                    }
                    rowArray = tbDst.Select("RecordType = 'Contract'");
                    length = rowArray.Length;
                    for (index = length - 1; index >= 0; index--)
                    {
                        tbDst.Rows.Remove(rowArray[index]);
                    }
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select c.FullCode, 'Contract' as RecordType, s.SupplierName, a.*, b.* from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and b.Status in (0, 2) and not exists (select * from ContractChange g where g.ContractCode = b.ContractCode)", this.ProjectCode, this.CostBudgetSetCode);
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    queryString = string.Format("select c.FullCode, 'Contract' as RecordType, a.Money as Money, s.SupplierName, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and b.Status in (0, 2, 4) and a.ContractChangeCode = (select min(ContractChangeCode) from ContractChange g where g.ContractCode = b.ContractCode) and exists (select * from ContractChange g where g.ContractCode = b.ContractCode)", this.ProjectCode, this.CostBudgetSetCode);
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    queryString = string.Format("select c.FullCode, 'Contract' as RecordType, a.ChangeMoney as Money, s.SupplierName, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode, ContractChange g where a.ContractCode = b.ContractCode and b.ContractCode = g.ContractCode and a.ContractChangeCode = g.ContractChangeCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and b.Status not in (3) and g.Status in (0) and a.ChangeMoney <> 0", this.ProjectCode, this.CostBudgetSetCode);
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractChangeMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    queryString = string.Format("select c.FullCode, 'Contract' as RecordType, s.SupplierName, a.*, b.* from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and b.Status in (1, 7)", this.ProjectCode, this.CostBudgetSetCode);
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractApplyMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    queryString = string.Format("select c.FullCode, 'Contract' as RecordType, s.SupplierName, a.ChangeMoney as Money, a.*, b.* from ContractCostChange a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode, ContractChange g where a.ContractCode = b.ContractCode and b.ContractCode = g.ContractCode and a.ContractChangeCode = g.ContractChangeCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and b.Status not in (3) and g.Status in (1, 2) and a.ChangeMoney <> 0", this.ProjectCode, this.CostBudgetSetCode);
                    tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractApplyMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    if (this.ShowContractAccountMoney)
                    {
                        queryString = string.Format("select c.FullCode, 'Contract' as RecordType, s.SupplierName, a.*, b.* from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b left join Supplier s on s.SupplierCode = b.SupplierCode where a.ContractCode = b.ContractCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and b.Status in (2)", this.ProjectCode, this.CostBudgetSetCode);
                        tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractAccountMoney", CostBudgetPageRule.m_DynamicRowType.Contract);
                    }
                    if ((this.iStartY != 0) && (this.iEndY != 0))
                    {
                        queryString = string.Format("select c.FullCode, 'Contract' as RecordType, a.CostCode, a.ContractCode, Year(p.PlanningPayDate) as IYear, Month(p.PlanningPayDate) as IMonth, sum(isnull(p.Money, 0)) as Money from ContractCost a left join CBS c on c.CostCode = a.CostCode, Contract b, ContractCostPlan p where a.ContractCode = b.ContractCode and a.ContractCostCode = p.ContractCostCode and b.ProjectCode = '{0}' and a.CostBudgetSetCode = '{1}' and Year(p.PlanningPayDate) between {2} and {3} group by c.FullCode, a.CostCode, a.ContractCode, Year(p.PlanningPayDate), Month(p.PlanningPayDate)", new object[] { this.ProjectCode, this.CostBudgetSetCode, this.iStartY, this.iEndY });
                        tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row in tbSrc.Rows)
                        {
                            DataRow drDst = tbDst.NewRow();
                            ConvertRule.DataRowCopy(row, drDst, tbSrc, tbDst);
                            tbDst.Rows.Add(drDst);
                        }
                    }
                    foreach (DataRow row3 in tbContract.Rows)
                    {
                        if (!ConvertRule.ToString(row3["ContractCode"]).StartsWith("Bidding_") && (ConvertRule.ToString(row3["AllContractCode"]) != ""))
                        {
                            queryString = string.Format("select sum(isnull(mi.ItemMoney, 0)) as PaymentMoney from PaymentItem mi, Payment m where mi.PaymentCode = m.PaymentCode and m.Status in (1, 2) and m.ProjectCode = '{0}' and m.ContractCode in ({1}) and exists (select * from ContractCost c where c.ContractCostCode = mi.AllocateCode and c.CostCode = '{2}' and c.CostBudgetSetCode = '{3}')", new object[] { this.ProjectCode, ConvertRule.ToString(row3["AllContractCode"]), ConvertRule.ToString(row3["CostCode"]), this.CostBudgetSetCode });
                            row3["ContractPay"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            queryString = string.Format("select sum(isnull(oi.PayoutMoney, 0)) from PayoutItem oi, Payout o, PaymentItem mi, Payment m where oi.PaymentItemCode = mi.PaymentItemCode and mi.PaymentCode = m.PaymentCode and o.PayoutCode = oi.PayoutCode and o.Status in (1) and m.ProjectCode = '{0}' and m.ContractCode in ({1}) and exists (select * from ContractCost c where c.ContractCostCode = mi.AllocateCode and c.CostCode = '{2}' and c.CostBudgetSetCode = '{3}')", new object[] { this.ProjectCode, ConvertRule.ToString(row3["AllContractCode"]), ConvertRule.ToString(row3["CostCode"]), this.CostBudgetSetCode });
                            row3["ContractPayReal"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        }
                    }
                    CostBudgetPageRule.CostBudgetDtlCalcAllRow(tbContract, CostBudgetPageRule.m_DynamicRowType.Contract, this.entitySet.CurrentRow, this.iStartY, this.iEndY);
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

        private void SetCostBudgetDtlRelationData()
        {
            try
            {
                DataTable table = this.ds.Tables["CostBudgetDtl"];
                this.SetCostBudgetDtlBalance();
                this.SetCostBudgetDtlRelationContract(false);
                this.SetCostBudgetDtlRelationBidding(false);
                this.SetCostBudgetDtlRelationNoContract(false);
                this.CostBudgetDtlReCalcByRelation();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SetCostBudgetDtlRelationNoContract(bool IsNeedClear)
        {
            try
            {
                DataTable table = this.ds.Tables["CostBudgetDtl"];
                DataTable tbContract = this.ds.Tables["NoContract"];
                if (IsNeedClear)
                {
                    tbContract.Rows.Clear();
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select 'Payment_' + b.PaymentCode as ContractCode, '' as ContractID, 'NoContract' as RecordType, '非合同' + b.PaymentID as ContractName, b.SupplyCode as SupplierCode, b.SupplyName as SupplierName, a.CostCode, c.FullCode, c.SortID, c.Deep, c.ParentCode, a.ItemMoney as Money, b.PayDate as PlanningPayDate from PaymentItem a left join CBS c on c.CostCode = a.CostCode, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '{0}' and isnull(b.IsContract, 0) = 0 and a.CostBudgetSetCode = '{1}'", this.ProjectCode, this.CostBudgetSetCode);
                    DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                    this.CostBudgetDtlAddRelationRow(tbContract, tbSrc, "ContractMoney", CostBudgetPageRule.m_DynamicRowType.NoContract);
                    queryString = string.Format("select a.CostCode, 'Payment_' + b.PaymentCode as ContractCode, sum(isnull(a.ItemMoney, 0)) as Money, sum(case b.Status when 2 then isnull(a.ItemMoney, 0) else 0 end) as AccountMoney from PaymentItem a, Payment b where a.PaymentCode = b.PaymentCode and b.ProjectCode = '{0}' and b.Status in (1, 2) and isnull(b.IsContract, 0) = 0 and a.CostBudgetSetCode = '{1}' group by a.CostCode, 'Payment_' + b.PaymentCode", this.ProjectCode, this.CostBudgetSetCode);
                    DataTable table4 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    queryString = string.Format("select mi.CostCode, 'Payment_' + m.PaymentCode as ContractCode, sum(isnull(a.PayoutMoney, 0)) as Money from PayoutItem a, Payout b, PaymentItem mi, Payment m where a.PaymentItemCode = mi.PaymentItemCode and a.PayoutCode = b.PayoutCode and mi.PaymentCode = m.PaymentCode and m.ProjectCode = '{0}' and isnull(m.IsContract, 0) = 0 and mi.CostBudgetSetCode = '{1}' and b.Status in (1, 2) group by mi.CostCode, 'Payment_' + m.PaymentCode", this.ProjectCode, this.CostBudgetSetCode);
                    DataTable table5 = agent.ExecSqlForDataSet(queryString).Tables[0];
                    foreach (DataRow row in tbContract.Rows)
                    {
                        string text2 = ConvertRule.ToString(row["CostCode"]);
                        string text3 = ConvertRule.ToString(row["ContractCode"]);
                        DataRow[] rowArray = table4.Select("CostCode = '" + text2 + "' and ContractCode = '" + text3 + "'");
                        if (rowArray.Length > 0)
                        {
                            row["ContractPay"] = rowArray[0]["Money"];
                            if (this.ShowContractAccountMoney)
                            {
                                row["ContractAccountMoney"] = rowArray[0]["AccountMoney"];
                            }
                        }
                        else
                        {
                            row["ContractPay"] = DBNull.Value;
                            if (this.ShowContractAccountMoney)
                            {
                                row["ContractAccountMoney"] = DBNull.Value;
                            }
                        }
                        rowArray = table5.Select("CostCode = '" + text2 + "' and ContractCode = '" + text3 + "'");
                        if (rowArray.Length > 0)
                        {
                            row["ContractPayReal"] = rowArray[0]["Money"];
                        }
                        else
                        {
                            row["ContractPayReal"] = DBNull.Value;
                        }
                    }
                    CostBudgetPageRule.CostBudgetDtlCalcAllRow(tbContract, CostBudgetPageRule.m_DynamicRowType.NoContract, this.entitySet.CurrentRow, this.iStartY, this.iEndY);
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

        private void SumR0(DataRow dr)
        {
            try
            {
                if (dr != null)
                {
                    string filterExpression = "";
                    if (dr.Table.Columns.Contains("RecordType"))
                    {
                        filterExpression = "ParentCode = '" + dr["CostBudgetDtlCode"].ToString() + "' and isnull(RecordType, '') = ''";
                    }
                    else
                    {
                        filterExpression = "ParentCode = '" + dr["CostBudgetDtlCode"].ToString() + "' and isnull(CostName, '') <> ''";
                    }
                    DataRow[] drsChild = dr.Table.Select(filterExpression);
                    this.CalcByChilds(dr, drsChild);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SumRow(DataRow dr)
        {
            try
            {
                if (dr != null)
                {
                    DataRow[] drsChild = dr.Table.Select("ParentCode = '" + dr["CostBudgetDtlCode"].ToString() + "'");
                    this.CalcByChilds(dr, drsChild);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SumRow(DataTable tb, string CostBudgetDtlCode)
        {
            try
            {
                this.SumRow(GetRow(tb, CostBudgetDtlCode));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public CostBudgetDynamic[] arrDynProject
        {
            get
            {
                return this.m_arrDynProject;
            }
            set
            {
                this.m_arrDynProject = value;
            }
        }

        public string CostBudgetBackupCode
        {
            get
            {
                return this.m_CostBudgetBackupCode;
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

        public EntityData entityBackup
        {
            get
            {
                return this.m_entityBackup;
            }
        }

        public EntityData entityBackupSet
        {
            get
            {
                return this.m_entityBackupSet;
            }
        }

        public EntityData entityCurrTarget
        {
            get
            {
                return this.m_entityCurrTarget;
            }
        }

        public EntityData entitySet
        {
            get
            {
                return this.m_entitySet;
            }
        }

        public bool HasTargetChange
        {
            get
            {
                return (this.m_TargetChangeFlag != 0);
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

        public RmsPM.BLL.CostBudgetPageRule.m_MoneyUnit MoneyUnit
        {
            get
            {
                return this.m_MoneyUnit;
            }
            set
            {
                this.m_MoneyUnit = value;
            }
        }

        public bool NeedApport
        {
            get
            {
                return (this.m_entitySet.GetString("PBSType") == "B");
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
                return ((this.m_TargetChangeFlag == 2) ? "调整" : ((this.m_TargetChangeFlag == 1) ? "申请" : ""));
            }
        }

        public int TargetChangeFlag
        {
            get
            {
                return this.m_TargetChangeFlag;
            }
        }

        public DataTable tb
        {
            get
            {
                return this.m_ds.Tables["CostBudgetDtl"];
            }
        }

        public DataTable tbBalance
        {
            get
            {
                return this.m_ds.Tables["Balance"];
            }
        }

        public DataTable tbContract
        {
            get
            {
                return this.m_ds.Tables["Contract"];
            }
        }

        public DataTable tbContractMonth
        {
            get
            {
                return this.m_ds.Tables["ContractMonth"];
            }
        }

        public DataTable tbCostBudgetContract
        {
            get
            {
                return this.m_ds.Tables["CostBudgetContract"];
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

        public DataTable tbNoContract
        {
            get
            {
                return this.m_ds.Tables["NoContract"];
            }
        }
    }
}

