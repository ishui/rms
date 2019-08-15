namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;
    using TiannuoPM.MODEL;

    public sealed class ContractRule
    {
        private static DataTable _GetNexusList(DataTable CodeTypeTable, SqlConnection Connection, SqlTransaction Transaction)
        {
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("Code", Type.GetType("System.String"));
            DataColumn column2 = new DataColumn("Type", Type.GetType("System.String"));
            DataColumn column3 = new DataColumn("Name", Type.GetType("System.String"));
            DataColumn column4 = new DataColumn("Id", Type.GetType("System.String"));
            DataColumn column5 = new DataColumn("Person", Type.GetType("System.String"));
            DataColumn column6 = new DataColumn("Date", Type.GetType("System.DateTime"));
            DataColumn column7 = new DataColumn("Path", Type.GetType("System.String"));
            DataColumn column8 = new DataColumn("Money", Type.GetType("System.Decimal"));
            table.Columns.Add(column);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            table.Columns.Add(column8);
            string text = "0";
            foreach (DataRow row in CodeTypeTable.Select())
            {
                if (row["Type"].ToString() == "Vise")
                {
                    text = text + "," + row["Code"].ToString();
                }
            }
            LocaleViseQueryModel viseQueryModel = new LocaleViseQueryModel();
            viseQueryModel.ViseCodeInStr = text;
            LocaleViseBLL ebll = new LocaleViseBLL();
            List<LocaleViseModel> list = null;
            if (Connection != null)
            {
                list = ebll.Select(viseQueryModel, Connection);
            }
            else
            {
                list = ebll.Select(viseQueryModel, Transaction);
            }
            foreach (LocaleViseModel model2 in list)
            {
                DataRow row = table.NewRow();
                row["Code"] = model2.ViseCode;
                row["Type"] = "Vise";
                row["Name"] = model2.ViseName;
                row["Id"] = model2.ViseId;
                row["Person"] = model2.VisePerson;
                row["Date"] = model2.ViseDate;
                row["Path"] = "../LocaleVise/LocaleViseInfo.aspx?ViseCode=" + model2.ViseCode;
                if (Connection != null)
                {
                    row["Money"] = ebll.GetViseSumCheckMoney(model2.ViseCode, Connection);
                }
                else if (Transaction != null)
                {
                    row["Money"] = ebll.GetViseSumCheckMoney(model2.ViseCode, Transaction);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static string AutoCreateContractID(string ContractCode, string PMName)
        {
            string text7;
            try
            {
                string formatSysCode = "";
                EntityData contractByCode = ContractDAO.GetContractByCode(ContractCode);
                if (!contractByCode.HasRecord())
                {
                    throw new Exception("合同不存在");
                }
                string projectCode = contractByCode.GetString("ProjectCode");
                string projectID = "";
                string format = "";
                string text8 = PMName.ToLower();
                if ((text8 == null) || (text8 != "yefengpm"))
                {
                    throw new Exception("未知的合同编号规则，不能生成合同编号");
                }
                projectID = ProjectRule.GetProjectID(projectCode);
                if (projectID == "")
                {
                    throw new Exception("未定义项目编号，不能生成合同编号");
                }
                string text5 = SystemGroupRule.GetSystemGroupNameByDeep(contractByCode.GetString("Type"), 3).Trim();
                if (text5.Length > 1)
                {
                    text5 = text5.Substring(0, 1);
                }
                if (text5 == "")
                {
                    throw new Exception("合同类型的三级类型为空，不能生成合同编号");
                }
                string sysCodeName = "ContractID-" + projectID + "-";
                format = projectID + "-" + DateTime.Now.ToString("yyyy-MM") + "(" + text5 + "){###}";
                formatSysCode = SystemManageDAO.GetFormatSysCode(sysCodeName, format);
                if (projectID == "")
                {
                    throw new Exception("合同编号生成为空");
                }
                contractByCode.Dispose();
                text7 = formatSysCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text7;
        }

        public static int BatchDealwith(DataTable CodeTypeTable, SqlTransaction Transaction)
        {
            int num = 0;
            string viseCodes = "0";
            foreach (DataRow row in CodeTypeTable.Select())
            {
                if (row["Type"].ToString() == "Vise")
                {
                    viseCodes = viseCodes + "," + row["Code"].ToString();
                }
            }
            LocaleViseBLL ebll = new LocaleViseBLL();
            return (num + ebll.BatchBalance(viseCodes, Transaction));
        }

        public static int BatchNoDealwith(DataTable CodeTypeTable, SqlTransaction Transaction)
        {
            int num = 0;
            string viseCodes = "0";
            foreach (DataRow row in CodeTypeTable.Select())
            {
                if (row["Type"].ToString() == "Vise")
                {
                    viseCodes = viseCodes + "," + row["Code"].ToString();
                }
            }
            LocaleViseBLL ebll = new LocaleViseBLL();
            return (num + ebll.BatchNoBalance(viseCodes, Transaction));
        }

        public static int BatchWaitDealwith(DataTable CodeTypeTable, SqlTransaction Transaction)
        {
            int num = 0;
            string viseCodes = "0";
            foreach (DataRow row in CodeTypeTable.Select())
            {
                if (row["Type"].ToString() == "Vise")
                {
                    viseCodes = viseCodes + "," + row["Code"].ToString();
                }
            }
            LocaleViseBLL ebll = new LocaleViseBLL();
            return (num + ebll.BatchWaitBalance(viseCodes, Transaction));
        }

        public static string BuildContractChangeByNexusCodeType(string pm_sContractCode, DataTable pm_dtNexusCodeType, SqlTransaction pm_SqlTransaction, string pm_sUserCode)
        {
            string text2;
            try
            {
                EntityData data = ContractDAO.GetStandard_ContractByCode(pm_sContractCode);
                string text = BuildContractChangeByNexusCodeType(pm_sContractCode, data, pm_dtNexusCodeType, pm_SqlTransaction, pm_sUserCode);
                ContractChangeAuditing(data, text, true);
                BatchDealwith(pm_dtNexusCodeType, pm_SqlTransaction);
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string BuildContractChangeByNexusCodeType(string pm_sContractCode, EntityData pm_EntityContract, DataTable pm_dtNexusCodeType, SqlTransaction pm_SqlTransaction, string pm_sUserCode)
        {
            string text2;
            try
            {
                string newSysCode = SystemManageDAO.GetNewSysCode("ContractChangeCode");
                DataRow row = pm_EntityContract.Tables["ContractChange"].NewRow();
                row["ContractChangeCode"] = newSysCode;
                row["ContractCode"] = pm_sContractCode;
                pm_EntityContract.Tables["ContractChange"].Rows.Add(row);
                text2 = BuildContractChangeByNexusCodeType(pm_sContractCode, newSysCode, pm_EntityContract, pm_dtNexusCodeType, pm_SqlTransaction, pm_sUserCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string BuildContractChangeByNexusCodeType(string pm_sContractCode, string pm_sContractChangeCode, EntityData pm_EntityContract, DataTable pm_dtNexusCodeType, SqlTransaction pm_SqlTransaction, string pm_sUserCode)
        {
            string text8;
            try
            {
                string filterExpression = string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode);
                DataTable nexusList = GetNexusList(pm_dtNexusCodeType, pm_SqlTransaction);
                DataTable nexusListForCost = GetNexusListForCost(pm_dtNexusCodeType, pm_SqlTransaction);
                foreach (DataRow row in pm_EntityContract.Tables["ContractCostChange"].Select(filterExpression, "", DataViewRowState.CurrentRows))
                {
                    row.Delete();
                }
                CopyCostTableToChangeTable(pm_EntityContract, pm_sContractCode, pm_sContractChangeCode);
                foreach (DataRow row in nexusListForCost.Rows)
                {
                    decimal num4;
                    string text2 = row["CostCode"].ToString();
                    string text3 = row["CostBudgetSetCode"].ToString();
                    string text4 = "人民币 (RMB)";
                    decimal num = 1M;
                    string text5 = string.Format("ContractChangeCode='{0}' and CostCode='{1}' and CostBudgetSetCode='{2}' and MoneyType='{3}'", new object[] { pm_sContractChangeCode, text2, text3, text4 });
                    bool flag = false;
                    foreach (DataRow row2 in pm_EntityContract.Tables["ContractCostChange"].Select(text5, "", DataViewRowState.CurrentRows))
                    {
                        flag = true;
                        flag = true;
                        decimal num2 = ConvertRule.ToDecimal(row2["Cash"]);
                        decimal num3 = ConvertRule.ToDecimal(row2["ExchangeRate"]);
                        num4 = ConvertRule.ToDecimal(row["Money"]);
                        row2["ChangeCash"] = num4;
                        row2["ChangeMoney"] = num4 * num3;
                        row2["NewCash"] = num4 + num2;
                        row2["NewMoney"] = (num4 + num2) * num3;
                    }
                    if (!flag)
                    {
                        DataRow row3 = pm_EntityContract.Tables["ContractCostChange"].NewRow();
                        string newSysCode = SystemManageDAO.GetNewSysCode("ContractCostChangeCode");
                        num4 = ConvertRule.ToDecimal(row["Money"]);
                        row3["ContractCostChangeCode"] = newSysCode;
                        row3["ContractCode"] = pm_sContractCode;
                        row3["ContractChangeCode"] = pm_sContractChangeCode;
                        row3["CostCode"] = text2;
                        row3["CostBudgetSetCode"] = text3;
                        row3["ExchangeRate"] = num;
                        row3["MoneyType"] = text4;
                        row3["Money"] = 0M;
                        row3["OriginalMoney"] = 0M;
                        row3["TotalChangeMoney"] = 0M;
                        row3["NewMoney"] = num4;
                        row3["ChangeMoney"] = num4;
                        row3["Cash"] = 0M;
                        row3["OriginalCash"] = 0M;
                        row3["TotalChangeCash"] = 0M;
                        row3["NewCash"] = num4;
                        row3["ChangeCash"] = num4;
                        row3["Status"] = 1;
                        pm_EntityContract.Tables["ContractCostChange"].Rows.Add(row3);
                    }
                }
                foreach (DataRow row in pm_EntityContract.Tables["ContractNexus"].Select(filterExpression, "", DataViewRowState.CurrentRows))
                {
                    row.Delete();
                }
                foreach (DataRow row in nexusList.Rows)
                {
                    DataRow row4 = pm_EntityContract.Tables["ContractNexus"].NewRow();
                    string text7 = SystemManageDAO.GetNewSysCode("ContractNexusCode");
                    row4["ContractNexusCode"] = text7;
                    row4["ContractCode"] = pm_sContractCode;
                    row4["ContractChangeCode"] = pm_sContractChangeCode;
                    row4["Code"] = row["Code"];
                    row4["Type"] = row["Type"];
                    row4["Name"] = row["Name"];
                    row4["ID"] = row["ID"];
                    row4["Person"] = row["Person"];
                    row4["Date"] = row["Date"];
                    row4["Path"] = row["Path"];
                    row4["Money"] = row["Money"];
                    pm_EntityContract.Tables["ContractNexus"].Rows.Add(row4);
                }
                pm_EntityContract.SetCurrentTable("Contract");
                decimal @decimal = pm_EntityContract.GetDecimal("TotalMoney");
                decimal num7 = pm_EntityContract.GetDecimal("OriginalMoney");
                decimal num6 = @decimal - num7;
                decimal num9 = MathRule.SumColumn(pm_EntityContract.Tables["ContractCostChange"].Select(string.Format("ContractChangeCode='{0}' and Status in (0,1)", pm_sContractChangeCode)), "ChangeMoney");
                decimal num8 = @decimal + num9;
                foreach (DataRow row in pm_EntityContract.Tables["ContractChange"].Select(string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode)))
                {
                    row["Money"] = @decimal;
                    row["OriginalMoney"] = num7;
                    row["TotalChangeMoney"] = num6;
                    row["ChangeMoney"] = num9;
                    row["NewMoney"] = num8;
                    row["ChangePerson"] = pm_sUserCode;
                    row["ChangeDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                text8 = pm_sContractChangeCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text8;
        }

        public static object CalcContractPayDateByTask(string AllocateCode, DataTable tbPayCondition)
        {
            object obj5;
            try
            {
                object obj2 = null;
                if (AllocateCode == "")
                {
                    return obj2;
                }
                DataRow[] rowArray = tbPayCondition.Select("AllocateCode='" + AllocateCode + "'");
                if (rowArray.Length == 0)
                {
                    return obj2;
                }
                DataRow row = rowArray[0];
                string text = ConvertRule.ToString(row["ConditionCode"]);
                string code = ConvertRule.ToString(row["WBSCode"]);
                int percent = ConvertRule.ToInt(row["CompletePercent"]);
                int num2 = ConvertRule.ToInt(row["DelayType"]);
                int num3 = ConvertRule.ToInt(row["DelayDays"]);
                EntityData taskByCode = WBSDAO.GetTaskByCode(code);
                if (taskByCode.HasRecord())
                {
                    DataRow currentRow = taskByCode.CurrentRow;
                    object startDate = null;
                    object endDate = null;
                    if (currentRow["ActualStartDate"] != DBNull.Value)
                    {
                        startDate = (DateTime) currentRow["ActualStartDate"];
                    }
                    else if (currentRow["PlannedStartDate"] != DBNull.Value)
                    {
                        startDate = (DateTime) currentRow["PlannedStartDate"];
                    }
                    if (currentRow["ActualFinishDate"] != DBNull.Value)
                    {
                        endDate = (DateTime) currentRow["ActualFinishDate"];
                    }
                    else if (currentRow["PlannedFinishDate"] != DBNull.Value)
                    {
                        endDate = (DateTime) currentRow["PlannedFinishDate"];
                    }
                    int addDays = num3 * num2;
                    obj2 = GetDateByDateRangePercent(startDate, endDate, percent, addDays);
                }
                taskByCode.Dispose();
                obj5 = obj2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return obj5;
        }

        public static void ContractChangeAuditing(EntityData pm_Entity, string pm_sContractChangeCode)
        {
            ContractChangeAuditing(pm_Entity, pm_sContractChangeCode, false);
        }

        public static void ContractChangeAuditing(EntityData pm_Entity, string pm_sContractChangeCode, bool pm_bSubmitData)
        {
            try
            {
                string filterExpression = string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode);
                string text2 = "";
                string text3 = "";
                string text4 = "";
                string text5 = "";
                foreach (DataRow row in pm_Entity.Tables["ContractCostChange"].Select(filterExpression, "", DataViewRowState.CurrentRows))
                {
                    row["Status"] = 0;
                    text5 = row["ContractCode"].ToString();
                    if ((row["ContractCostCode"].ToString() != "") && (row["ContractCostCashCode"].ToString() != ""))
                    {
                        decimal num = 0M;
                        text3 = string.Format("ContractCostCashCode='{0}'", row["ContractCostCashCode"].ToString());
                        text2 = string.Format("ContractCostCode='{0}'", row["ContractCostCode"].ToString());
                        foreach (DataRow row2 in pm_Entity.Tables["ContractCostCash"].Select(text3, "", DataViewRowState.CurrentRows))
                        {
                            decimal num2 = ConvertRule.ToDecimal(row["NewCash"]);
                            decimal num3 = ConvertRule.ToDecimal(row["ExchangeRate"]);
                            decimal num4 = num2 * num3;
                            row2["Cash"] = num2;
                            row2["Money"] = num4;
                            num += num4;
                        }
                        foreach (DataRow row3 in pm_Entity.Tables["ContractCost"].Select(text2, "", DataViewRowState.CurrentRows))
                        {
                            row3["Money"] = num;
                            row3["Description"] = row["Description"];
                        }
                    }
                    else
                    {
                        string newSysCode = SystemManageDAO.GetNewSysCode("ContractCostCode");
                        string text7 = SystemManageDAO.GetNewSysCode("ContractCostCashCode");
                        DataRow newRecord = pm_Entity.GetNewRecord("ContractCostCash");
                        newRecord["ContractCostCashCode"] = text7;
                        newRecord["ContractCode"] = text5;
                        newRecord["ContractCostCode"] = newSysCode;
                        newRecord["Money"] = row["NewMoney"];
                        newRecord["Cash"] = row["NewCash"];
                        newRecord["OriginalCash"] = row["OriginalCash"];
                        newRecord["MoneyType"] = row["MoneyType"];
                        newRecord["ExchangeRate"] = row["ExchangeRate"];
                        pm_Entity.Tables["ContractCostCash"].Rows.Add(newRecord);
                        DataRow row5 = pm_Entity.GetNewRecord("ContractCost");
                        row5["ContractCostCode"] = newSysCode;
                        row5["ContractCode"] = text5;
                        row5["CostCode"] = row["CostCode"];
                        row5["Money"] = row["NewMoney"];
                        row5["CostBudgetDtlCode"] = row["CostBudgetDtlCode"];
                        row5["CostBudgetSetCode"] = row["CostBudgetSetCode"];
                        row5["PBSType"] = row["PBSType"];
                        row5["PBSCode"] = row["PBSCode"];
                        row5["Description"] = row["Description"];
                        row5["OriginalMoney"] = row["OriginalMoney"];
                        pm_Entity.Tables["ContractCost"].Rows.Add(row5);
                    }
                }
                foreach (DataRow row6 in pm_Entity.Tables["ContractChange"].Select(filterExpression, "", DataViewRowState.CurrentRows))
                {
                    row6["Status"] = 0;
                    text4 = string.Format("ContractCode='{0}'", text5);
                    foreach (DataRow row7 in pm_Entity.Tables["Contract"].Select(text4, "", DataViewRowState.CurrentRows))
                    {
                        row7["ChangePerson"] = row6["ChangePerson"];
                        row7["ChangeReason"] = row6["ChangeReason"];
                        row7["ChangeDate"] = row6["ChangeDate"];
                        row7["TotalMoney"] = ((decimal) row7["TotalMoney"]) + ((decimal) row6["ChangeMoney"]);
                        row7["ChangeCount"] = ((int) row7["ChangeCount"]) + 1;
                        if (pm_Entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=2", text5)).Length > 0)
                        {
                            row7["ChangeStatus"] = 3;
                        }
                        else if (pm_Entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=1", text5)).Length > 0)
                        {
                            row7["ChangeStatus"] = 1;
                        }
                        else
                        {
                            row7["ChangeStatus"] = 2;
                        }
                    }
                }
                if (pm_bSubmitData)
                {
                    ContractDAO.SubmitAllStandard_Contract(pm_Entity);
                    string text8 = string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode);
                    DataTable table = new DataTable();
                    table.Columns.Add("Code");
                    table.Columns.Add("Type");
                    foreach (DataRow row8 in pm_Entity.Tables["ContractNexus"].Select(text8, "", DataViewRowState.CurrentRows))
                    {
                        DataRow row9 = table.NewRow();
                        row9["Code"] = row8["Code"];
                        row9["Type"] = row8["Type"];
                        table.Rows.Add(row9);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool ContractChangeAuditing(EntityData pm_entity, string pm_sContractChangeCode, StandardEntityDAO dao)
        {
            try
            {
                string text = "";
                foreach (DataRow row in pm_entity.Tables["ContractCostChange"].Select(string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode), "", DataViewRowState.CurrentRows))
                {
                    row["Status"] = 0;
                    if ((row["ContractCostCode"].ToString() != "") && (row["ContractCostCashCode"].ToString() != ""))
                    {
                        decimal num = 0M;
                        foreach (DataRow row2 in pm_entity.Tables["ContractCostCash"].Select(string.Format("ContractCostCashCode='{0}'", row["ContractCostCashCode"].ToString()), "", DataViewRowState.CurrentRows))
                        {
                            decimal num2 = ConvertRule.ToDecimal(row["NewCash"]);
                            decimal num3 = ConvertRule.ToDecimal(row["ExchangeRate"]);
                            decimal num4 = num2 * num3;
                            row2["Cash"] = num2;
                            row2["Money"] = num4;
                            num += num4;
                        }
                        foreach (DataRow row3 in pm_entity.Tables["ContractCost"].Select(string.Format("ContractCostCode='{0}'", row["ContractCostCode"].ToString()), "", DataViewRowState.CurrentRows))
                        {
                            row3["Money"] = num;
                            row3["Description"] = row["Description"];
                        }
                    }
                    else
                    {
                        string newSysCode = SystemManageDAO.GetNewSysCode("ContractCostCode");
                        string text3 = SystemManageDAO.GetNewSysCode("ContractCostCashCode");
                        DataRow newRecord = pm_entity.GetNewRecord("ContractCostCash");
                        newRecord["ContractCostCashCode"] = text3;
                        newRecord["ContractCode"] = text;
                        newRecord["ContractCostCode"] = newSysCode;
                        newRecord["Money"] = row["NewMoney"];
                        newRecord["Cash"] = row["NewCash"];
                        newRecord["OriginalCash"] = row["OriginalCash"];
                        newRecord["MoneyType"] = row["MoneyType"];
                        newRecord["ExchangeRate"] = row["ExchangeRate"];
                        pm_entity.Tables["ContractCostCash"].Rows.Add(newRecord);
                        DataRow row5 = pm_entity.GetNewRecord("ContractCost");
                        row5["ContractCostCode"] = newSysCode;
                        row5["ContractCode"] = text;
                        row5["CostCode"] = row["CostCode"];
                        row5["Money"] = row["NewMoney"];
                        row5["CostBudgetDtlCode"] = row["CostBudgetDtlCode"];
                        row5["CostBudgetSetCode"] = row["CostBudgetSetCode"];
                        row5["PBSType"] = row["PBSType"];
                        row5["PBSCode"] = row["PBSCode"];
                        row5["Description"] = row["Description"];
                        row5["OriginalMoney"] = row["OriginalMoney"];
                        pm_entity.Tables["ContractCost"].Rows.Add(row5);
                    }
                }
                foreach (DataRow row6 in pm_entity.Tables["ContractChange"].Select(string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode), "", DataViewRowState.CurrentRows))
                {
                    row6["Status"] = 0;
                    foreach (DataRow row7 in pm_entity.Tables["Contract"].Select(string.Format("ContractCode='{0}'", text), "", DataViewRowState.CurrentRows))
                    {
                        row7["ChangePerson"] = row6["ChangePerson"];
                        row7["ChangeReason"] = row6["ChangeReason"];
                        row7["ChangeDate"] = row6["ChangeDate"];
                        row7["TotalMoney"] = row6["NewMoney"];
                        row7["ChangeCount"] = (row7["ChangeCount"] == DBNull.Value) ? 1 : (((int) row7["ChangeCount"]) + 1);
                        if (pm_entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=2", text)).Length > 0)
                        {
                            row7["ChangeStatus"] = 3;
                        }
                        else if (pm_entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=1", text)).Length > 0)
                        {
                            row7["ChangeStatus"] = 1;
                        }
                        else
                        {
                            row7["ChangeStatus"] = 2;
                        }
                    }
                }
                if (dao == null)
                {
                    dao = new StandardEntityDAO("Standard_Contract");
                }
                dao.SubmitEntity(pm_entity);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return true;
        }

        public static bool ContractChangeDetele(EntityData pm_entity, string pm_sContractChangeCode)
        {
            bool flag5;
            try
            {
                string text = "";
                string text2 = "";
                bool flag = false;
                string filterExpression = string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode);
                foreach (DataRow row in pm_entity.Tables["ContractChange"].Select(filterExpression, "", DataViewRowState.CurrentRows))
                {
                    text2 = row["Status"].ToString();
                    text = row["ContractCode"].ToString();
                    if (text2 != "0")
                    {
                        foreach (DataRow row2 in pm_entity.Tables["ContractCostChange"].Select(filterExpression, "", DataViewRowState.CurrentRows))
                        {
                            row2.Delete();
                        }
                        row.Delete();
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    foreach (DataRow row3 in pm_entity.Tables["Contract"].Select(string.Format("ContractCode='{0}'", text)))
                    {
                        if (pm_entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=2", text)).Length > 0)
                        {
                            row3["ChangeStatus"] = 3;
                        }
                        else if (pm_entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=1", text)).Length > 0)
                        {
                            row3["ChangeStatus"] = 1;
                        }
                        else if (pm_entity.Tables["ContractChange"].Select(string.Format("ContractCode='{0}' and status=0", text)).Length > 0)
                        {
                            row3["ChangeStatus"] = 2;
                        }
                        else
                        {
                            row3["ChangeStatus"] = 0;
                        }
                    }
                    ContractDAO.SubmitAllStandard_Contract(pm_entity);
                }
                flag5 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag5;
        }

        public static bool ContractChangeStatusChange(StandardEntityDAO dao, string pm_sContractChangeCode, int pm_iStatus)
        {
            if (dao == null)
            {
                dao = new StandardEntityDAO("ContractChange");
            }
            else
            {
                dao.EntityName = "ContractChange";
            }
            EntityData contractChangeByCode = ContractDAO.GetContractChangeByCode(pm_sContractChangeCode, dao);
            bool flag = ContractChangeStatusChange(contractChangeByCode, "", pm_iStatus, null, false);
            dao.SubmitEntity(contractChangeByCode);
            return flag;
        }

        public static bool ContractChangeStatusChange(EntityData pm_Entity, string pm_sContractChangeCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                pm_Entity.SetCurrentTable("ContractChange");
                if (pm_sContractChangeCode.Trim() == "")
                {
                    if (pm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("ContractChangeCode='{0}'", pm_sContractChangeCode.Trim());
                    if (pm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in pm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (pm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["Status"] != DBNull.Value) && ((((int) row["Status"]) == (nullable = pm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["Status"] = pm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["Status"] = pm_iStatus;
                    }
                }
                if (flag && pm_bSubmitData)
                {
                    ContractDAO.UpdateContractChange(pm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool ContractStatusChange(string pm_sContractCode, int pm_iStatus)
        {
            return ContractStatusChange(pm_sContractCode, pm_iStatus, null, true);
        }

        public static bool ContractStatusChange(EntityData pm_Entity, int pm_iStatus)
        {
            return ContractStatusChange(pm_Entity, "", pm_iStatus, null, false);
        }

        public static bool ContractStatusChange(string pm_sContractCode, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return ContractStatusChange(pm_sContractCode, pm_iStatus, pm_iOriginalStatus, true);
        }

        public static bool ContractStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return ContractStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, false);
        }

        public static bool ContractStatusChange(EntityData pm_Entity, int pm_iStatus, bool pm_bSubmitData)
        {
            return ContractStatusChange(pm_Entity, "", pm_iStatus, null, pm_bSubmitData);
        }

        public static bool ContractStatusChange(StandardEntityDAO dao, string pm_sContractCode, int pm_iStatus)
        {
            if (dao == null)
            {
                dao = new StandardEntityDAO("Standard_Contract");
            }
            else
            {
                dao.EntityName = "Standard_Contract";
            }
            EntityData data = ContractDAO.GetStandard_ContractByCode(pm_sContractCode, dao);
            bool flag = ContractStatusChange(data, "", pm_iStatus, null, false);
            dao.SubmitEntity(data);
            return flag;
        }

        public static bool ContractStatusChange(string pm_sContractCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return ContractStatusChange(ContractDAO.GetStandard_ContractByCode(pm_sContractCode), pm_sContractCode, pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool ContractStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return ContractStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool ContractStatusChange(EntityData pm_Entity, string pm_sContractCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                pm_Entity.SetCurrentTable("Contract");
                if (pm_sContractCode.Trim() == "")
                {
                    if (pm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("ContractCode='{0}'", pm_sContractCode.Trim());
                    if (pm_Entity.CurrentTable.Select(filterExpression).Length != 1)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    return flag;
                }
                foreach (DataRow row in pm_Entity.CurrentTable.Select(filterExpression))
                {
                    if (pm_iOriginalStatus.HasValue)
                    {
                        int? nullable;
                        if ((row["Status"] != DBNull.Value) && ((((int) row["Status"]) == (nullable = pm_iOriginalStatus).GetValueOrDefault()) && nullable.HasValue))
                        {
                            row["Status"] = pm_iStatus;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        row["Status"] = pm_iStatus;
                    }
                }
                if (flag && pm_bSubmitData)
                {
                    ContractDAO.SubmitAllStandard_Contract(pm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void CopyCostTableToChangeTable(EntityData entity, string pm_sContractCode, string pm_sContractChangeCode)
        {
            foreach (DataRow row in entity.Tables["ContractCost"].Rows)
            {
                string text = row["ContractCostCode"].ToString();
                foreach (DataRow row2 in entity.Tables["ContractCostCash"].Select(string.Format("ContractCostCode = '{0}'", text)))
                {
                    string newSysCode = SystemManageDAO.GetNewSysCode("ContractCostChangeCode");
                    DataRow newRecord = entity.GetNewRecord("ContractCostChange");
                    newRecord["ContractCostChangeCode"] = newSysCode;
                    newRecord["ContractCode"] = pm_sContractCode;
                    newRecord["ContractCostCode"] = row["ContractCostCode"];
                    newRecord["ContractChangeCode"] = pm_sContractChangeCode;
                    newRecord["CostCode"] = row["CostCode"];
                    newRecord["CostBudgetDtlCode"] = row["CostBudgetDtlCode"];
                    newRecord["CostBudgetSetCode"] = row["CostBudgetSetCode"];
                    newRecord["PBSType"] = row["PBSType"];
                    newRecord["PBSCode"] = row["PBSCode"];
                    newRecord["Description"] = row["Description"];
                    newRecord["Status"] = 1;
                    newRecord["ContractCostCashCode"] = row2["ContractCostCashCode"];
                    decimal num4 = ConvertRule.ToDecimal(row2["Cash"]);
                    decimal num5 = ConvertRule.ToDecimal(row2["OriginalCash"]);
                    decimal num = ConvertRule.ToDecimal(row2["Money"]);
                    decimal num7 = ConvertRule.ToDecimal(row2["ExchangeRate"]);
                    decimal num2 = num5 * num7;
                    decimal num6 = num4 - num5;
                    decimal num3 = num - num2;
                    newRecord["Cash"] = num4;
                    newRecord["OriginalCash"] = num5;
                    newRecord["TotalChangeCash"] = num6;
                    newRecord["NewCash"] = num4;
                    newRecord["ChangeCash"] = 0M;
                    newRecord["Money"] = num;
                    newRecord["OriginalMoney"] = num2;
                    newRecord["TotalChangeMoney"] = num3;
                    newRecord["NewMoney"] = num;
                    newRecord["ChangeMoney"] = 0M;
                    newRecord["ExchangeRate"] = num7;
                    newRecord["MoneyType"] = row2["MoneyType"];
                    entity.Tables["ContractCostChange"].Rows.Add(newRecord);
                }
            }
        }

        public static decimal GetAHCash(string pm_sContractCode)
        {
            return GetAHCash(pm_sContractCode, -1);
        }

        public static decimal GetAHCash(string pm_sContractCode, int pm_iCashIndex)
        {
            decimal num = 0M;
            if (pm_sContractCode == "")
            {
                return num;
            }
            ContractStrategyBuilder builder = new ContractStrategyBuilder();
            builder.AddStrategy(new Strategy(ContractStrategyName.ContractCode, pm_sContractCode));
            string queryString = builder.BuildQueryViewPayMoneyString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("Contract", queryString);
            agent.Dispose();
            if (!data.HasRecord())
            {
                return num;
            }
            if (pm_iCashIndex == -1)
            {
                return data.GetDecimal("AHCash");
            }
            return data.GetDecimal("AHCash" + pm_iCashIndex.ToString());
        }

        public static decimal GetAHMoney(string contractCode)
        {
            decimal @decimal = 0M;
            if (contractCode != "")
            {
                ContractStrategyBuilder builder = new ContractStrategyBuilder();
                builder.AddStrategy(new Strategy(ContractStrategyName.ContractCode, contractCode));
                string queryString = builder.BuildQueryViewPayMoneyString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Contract", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    @decimal = data.GetDecimal("AHMoney");
                }
            }
            return @decimal;
        }

        public static decimal GetAPCash(string contractCode)
        {
            decimal @decimal = 0M;
            if (contractCode != "")
            {
                ContractStrategyBuilder builder = new ContractStrategyBuilder();
                builder.AddStrategy(new Strategy(ContractStrategyName.ContractCode, contractCode));
                string queryString = builder.BuildQueryViewPayMoneyString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Contract", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    @decimal = data.GetDecimal("APCash");
                }
            }
            return @decimal;
        }

        public static decimal GetAPMoney(string contractCode)
        {
            decimal @decimal = 0M;
            if (contractCode != "")
            {
                ContractStrategyBuilder builder = new ContractStrategyBuilder();
                builder.AddStrategy(new Strategy(ContractStrategyName.ContractCode, contractCode));
                string queryString = builder.BuildQueryViewPayMoneyString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Contract", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    @decimal = data.GetDecimal("APMoney");
                }
            }
            return @decimal;
        }

        public static string GetChangeStatusNameInContract(string status)
        {
            string text = "";
            string text3 = status;
            if (text3 == null)
            {
                return text;
            }
            if (text3 != "0")
            {
                if (text3 != "1")
                {
                    if (text3 == "2")
                    {
                        return "已变更";
                    }
                    if (text3 != "3")
                    {
                        return text;
                    }
                    return "变更审核中";
                }
            }
            else
            {
                return "无变更";
            }
            return "变更申请";
        }

        public static string GetChangingContractVersionCode(string inputContractCode)
        {
            return GetContractVersionCode(inputContractCode, "4");
        }

        public static string GetContractChangeStatusName(string status)
        {
            string text2;
            try
            {
                string text = "";
                switch (status)
                {
                    case "-1":
                        text = "作废";
                        break;

                    case "0":
                        text = "已审";
                        break;

                    case "1":
                        text = "申请";
                        break;

                    case "2":
                        text = "申请流程中";
                        break;

                    default:
                        text = "未知";
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

        public static string GetContractCodeByAccountCode(string accountCode)
        {
            string text2;
            try
            {
                string text = "";
                if (accountCode == "")
                {
                    return text;
                }
                EntityData contractAccountByCode = ContractDAO.GetContractAccountByCode(accountCode);
                if (contractAccountByCode.HasRecord())
                {
                    text = contractAccountByCode.GetString("ContractCode");
                }
                contractAccountByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractCodeByChangeCode(string changeCode)
        {
            string text2;
            try
            {
                string text = "";
                if (changeCode == "")
                {
                    return text;
                }
                EntityData contractChangeByCode = ContractDAO.GetContractChangeByCode(changeCode);
                if (contractChangeByCode.HasRecord())
                {
                    text = contractChangeByCode.GetString("ContractCode");
                }
                contractChangeByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractCodeByLabel(string label)
        {
            string text2;
            try
            {
                string text = "";
                if (label == "")
                {
                    return text;
                }
                EntityData contractByLabel = ContractDAO.GetContractByLabel(label);
                if (contractByLabel.HasRecord())
                {
                    text = contractByLabel.GetString("ContractCode");
                }
                contractByLabel.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int GetContractCountByContractDefaultValueCode(string pm_sContractDefaultValueCode)
        {
            return ContractDAO.GetContractByContractDefaultValueCode(pm_sContractDefaultValueCode).Tables["Contract"].Rows.Count;
        }

        public static string GetContractID(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData contractByCode = ContractDAO.GetContractByCode(code);
                if (contractByCode.HasRecord())
                {
                    text = contractByCode.GetString("ContractID");
                }
                contractByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData contractByCode = ContractDAO.GetContractByCode(code);
                if (contractByCode.HasRecord())
                {
                    text = contractByCode.GetString("ContractName");
                }
                contractByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractPayConditionHtml(string AllocateCode, DataTable tbPayCondition, bool isModify)
        {
            string text8;
            try
            {
                string text = "";
                if (AllocateCode == "")
                {
                    return text;
                }
                DataRow[] rowArray = tbPayCondition.Select("AllocateCode='" + AllocateCode + "'");
                foreach (DataRow row in rowArray)
                {
                    string text2 = ConvertRule.ToString(row["ConditionCode"]);
                    string wBSCode = ConvertRule.ToString(row["WBSCode"]);
                    string text4 = ConvertRule.ToString(row["TaskName"]);
                    int num = ConvertRule.ToInt(row["DelayType"]);
                    string text5 = ConvertRule.ToString(row["DelayTypeName"]);
                    string text6 = " 完成" + ConvertRule.ToInt(row["CompletePercent"]) + "%后";
                    if (num != 0)
                    {
                        text6 = string.Concat(new object[] { text6, " ", text5, ConvertRule.ToInt(row["DelayDays"]), "天" });
                    }
                    if (text != "")
                    {
                        text = text + "<br>";
                    }
                    if (isModify)
                    {
                        text = text + "<a style=\"cursor:hand\" onclick=\"javascript:ModifyPayCondition(this.ConditionCode, this.AllocateCode);\" ConditionCode=\"" + text2 + "\" AllocateCode=\"" + AllocateCode + "\">" + text4 + text6 + "</a>";
                    }
                    else
                    {
                        string taskHintHtml = ConstructProgRule.GetTaskHintHtml(wBSCode);
                        text = text + "<a style=\"cursor:hand\" onclick=\"javascript:OpenTask(this.code);\" code=\"" + wBSCode + "\" hint=\"" + taskHintHtml + "\"  onMouseOver=\"init(myjoybox, joyboxTable, linktitle, hint);\" onMouseOut=\"mouseend();\">" + text4 + "</a>" + text6;
                    }
                }
                if (isModify && (rowArray.Length == 0))
                {
                    text = text + "<a style=\"cursor:hand\" onclick=\"javascript:AddPayCondition(this.AllocateCode);\" AllocateCode=\"" + AllocateCode + "\">关联</a>";
                }
                text8 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text8;
        }

        public static decimal GetContractPayment(string contractCode)
        {
            PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
            builder.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, contractCode));
            builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
            string queryString = builder.BuildQuerySumMoneyString();
            QueryAgent agent = new QueryAgent();
            decimal num = (decimal) agent.ExecuteScalar(queryString);
            agent.Dispose();
            return num;
        }

        public static string GetContractProjectCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData contractByCode = ContractDAO.GetContractByCode(code);
                if (contractByCode.HasRecord())
                {
                    text = contractByCode.GetString("ProjectCode");
                }
                contractByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractStatusName(string status)
        {
            switch (status)
            {
                case "0":
                    return "已审";

                case "1":
                    return "申请";

                case "2":
                    return "已结";

                case "3":
                    return "作废";

                case "4":
                    return "变更";

                case "6":
                    return "历史";

                case "7":
                    return "审核中";

                case "8":
                    return "已评审";

                case "9":
                    return "评审中";
            }
            return "";
        }

        public static string GetContractTypeName(string TypeCode)
        {
            return SystemGroupRule.GetSystemGroupFullName(TypeCode);
        }

        public static string GetContractVersionCode(string inputContractCode, string state)
        {
            string text4;
            try
            {
                string text = "";
                EntityData contractByCode = ContractDAO.GetContractByCode(inputContractCode);
                if (contractByCode.HasRecord())
                {
                    string text2 = contractByCode.GetString("ContractLabel");
                    ContractStrategyBuilder builder = new ContractStrategyBuilder();
                    builder.AddStrategy(new Strategy(ContractStrategyName.ContractLabel, text2));
                    builder.AddStrategy(new Strategy(ContractStrategyName.Status, state));
                    string queryString = builder.BuildMainQueryString();
                    QueryAgent agent = new QueryAgent();
                    EntityData data2 = agent.FillEntityData("Contract", queryString);
                    agent.Dispose();
                    if (data2.HasRecord())
                    {
                        text = data2.GetString("ContractCode");
                    }
                    data2.Dispose();
                }
                contractByCode.Dispose();
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static string GetCurrentContractVersionCode(string inputContractCode)
        {
            return GetContractVersionCode(inputContractCode, "0,2");
        }

        public static object GetDateByDateRangePercent(object StartDate, object EndDate, int Percent, int AddDays)
        {
            object obj3;
            try
            {
                object obj2 = null;
                if ((StartDate == null) && (EndDate == null))
                {
                    return obj2;
                }
                if (Percent <= 0)
                {
                    obj2 = StartDate;
                }
                else if ((StartDate != null) && (EndDate != null))
                {
                    TimeSpan span = (TimeSpan) (((DateTime) EndDate) - ((DateTime) StartDate));
                    int days = span.Days;
                    if (days < 0)
                    {
                        days = 0;
                    }
                    double num2 = Math.Round((double) ((days * Percent) / 100), 0);
                    obj2 = ((DateTime) StartDate).AddDays(num2);
                }
                if (obj2 != null)
                {
                    obj2 = ((DateTime) obj2).AddDays((double) AddDays);
                }
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return obj3;
        }

        public static string GetForeignMoneyType(EntityData entity)
        {
            string text2;
            try
            {
                string text = "";
                if ((entity.Tables["ContractCostCash"].Rows.Count > 0) && (ConvertRule.ToDecimal(entity.Tables["ContractCostCash"].Rows[0]["ExchangeRate"]) != 1M))
                {
                    text = ConvertRule.ToString(entity.Tables["ContractCostCash"].Rows[0]["MoneyType"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetNexusList(DataTable CodeTypeTable, SqlConnection Connection)
        {
            return _GetNexusList(CodeTypeTable, Connection, null);
        }

        public static DataTable GetNexusList(DataTable CodeTypeTable, SqlTransaction Transaction)
        {
            return _GetNexusList(CodeTypeTable, null, Transaction);
        }

        public static DataTable GetNexusListForCost(DataTable CodeTypeTable, SqlTransaction Transaction)
        {
            string viseCodes = "0";
            foreach (DataRow row in CodeTypeTable.Select())
            {
                if (row["Type"].ToString() == "Vise")
                {
                    viseCodes = viseCodes + "," + row["Code"].ToString();
                }
            }
            LocaleViseBLL ebll = new LocaleViseBLL();
            return ebll.GetVisesMoneyByCostGroup(viseCodes, Transaction).Tables[0];
        }

        public static bool IsContractIDUnique(string id, string code)
        {
            try
            {
                if (id != "")
                {
                    string text = "";
                    EntityData contractByID = ContractDAO.GetContractByID(id);
                    if (contractByID.HasRecord())
                    {
                        text = contractByID.GetString("ContractCode");
                    }
                    contractByID.Dispose();
                    return ((text == "") || (text == code));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return true;
        }

        public static void UpdateContractPayDateFromTask(string WBSCode)
        {
            try
            {
                EntityData contractPayConditionByWBSCode = ContractDAO.GetContractPayConditionByWBSCode(WBSCode);
                foreach (DataRow row in contractPayConditionByWBSCode.CurrentTable.Rows)
                {
                    string code = ConvertRule.ToString(row["AllocateCode"]);
                    EntityData entity = ContractDAO.GetContractAllocationByCode(code);
                    if (entity.HasRecord())
                    {
                        object obj2 = CalcContractPayDateByTask(code, contractPayConditionByWBSCode.CurrentTable);
                        if ((obj2 != null) && (ConvertRule.ToDate(entity.CurrentRow["PlanningPayDate"]) != obj2))
                        {
                            entity.CurrentRow["PlanningPayDate"] = obj2;
                            ContractDAO.UpdateContractAllocation(entity);
                        }
                    }
                    entity.Dispose();
                }
                contractPayConditionByWBSCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

