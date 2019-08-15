namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class CostBudgetRule
    {
        public static string m_BaseSetType = "预算";

        public static void AddCostBudgetDtlHis(DataTable tbHis, DataRow drCostBudgetDtl, DataRow drCostBudget)
        {
            try
            {
                decimal costBudgetDtlHisNextVerID = GetCostBudgetDtlHisNextVerID(drCostBudgetDtl["CostBudgetDtlCode"].ToString());
                DataRow row = tbHis.NewRow();
                row["CostBudgetDtlHisCode"] = SystemManageDAO.GetNewSysCode("CostBudgetDtlHisCode");
                row["CostBudgetDtlCode"] = drCostBudgetDtl["CostBudgetDtlCode"];
                row["CostBudgetSetCode"] = drCostBudget["CostBudgetSetCode"];
                row["ProjectCode"] = drCostBudget["ProjectCode"];
                row["CostCode"] = drCostBudgetDtl["CostCode"];
                row["BudgetMoney"] = drCostBudgetDtl["BudgetMoney"];
                row["Description"] = drCostBudgetDtl["Description"];
                row["TargetFlag"] = 0;
                row["VerID"] = costBudgetDtlHisNextVerID;
                if (drCostBudget["ModifyDate"] == DBNull.Value)
                {
                    row["ModifyPerson"] = drCostBudget["CreatePerson"];
                    row["ModifyDate"] = drCostBudget["CreateDate"];
                }
                else
                {
                    row["ModifyPerson"] = drCostBudget["ModifyPerson"];
                    row["ModifyDate"] = drCostBudget["ModifyDate"];
                }
                tbHis.Rows.Add(row);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void Backup(string ProjectCode, string BackupName, string BackupDescription, string UserCode)
        {
            try
            {
                Backup(ProjectCode, BackupName, BackupDescription, UserCode, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void Backup(string ProjectCode, string BackupName, string BackupDescription, string UserCode, string a_CostBudgetBackupCode)
        {
            try
            {
                DataRow currentRow;
                string text3;
                string code = a_CostBudgetBackupCode;
                EntityData entity = CostBudgetDAO.GetStandard_CostBudgetBackupByCode(code);
                entity.SetCurrentTable("CostBudgetBackup");
                if (entity.HasRecord())
                {
                    currentRow = entity.CurrentRow;
                    entity.SetCurrentTable("CostBudgetBackupSet");
                    foreach (DataRow row2 in entity.CurrentTable.Rows)
                    {
                        row2.Delete();
                    }
                    entity.SetCurrentTable("CostBudgetBackupDtl");
                    foreach (DataRow row2 in entity.CurrentTable.Rows)
                    {
                        row2.Delete();
                    }
                    entity.SetCurrentTable("CostBudgetBackupMonth");
                    foreach (DataRow row2 in entity.CurrentTable.Rows)
                    {
                        row2.Delete();
                    }
                    entity.SetCurrentTable("CostBudgetBackup");
                }
                else
                {
                    currentRow = entity.CurrentTable.NewRow();
                    if (code == "")
                    {
                        code = SystemManageDAO.GetNewSysCode("CostBudgetBackupCode");
                    }
                    currentRow["CostBudgetBackupCode"] = code;
                    entity.CurrentTable.Rows.Add(currentRow);
                }
                currentRow["ProjectCode"] = ProjectCode;
                currentRow["BackupName"] = BackupName;
                currentRow["BackupDescription"] = BackupDescription;
                currentRow["BackupDate"] = DateTime.Now;
                currentRow["BackupPerson"] = UserCode;
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("GroupSortID", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data2 = agent.FillEntityData("CostBudgetSet", queryString);
                agent.Dispose();
                DataView view = new DataView(data2.CurrentTable, "PBSType = 'P'", "", DataViewRowState.CurrentRows);
                CostBudgetDynamic[] dynamicArray = new CostBudgetDynamic[view.Count];
                int index = -1;
                foreach (DataRowView view2 in view)
                {
                    index++;
                    text3 = ConvertRule.ToString(view2.Row["CostBudgetSetCode"]);
                    dynamicArray[index] = new CostBudgetDynamic(ProjectCode, text3);
                    dynamicArray[index].StartY = "1900";
                    dynamicArray[index].EndY = "3000";
                    dynamicArray[index].AutoRefreshHtml = false;
                    dynamicArray[index].Generate();
                    dynamicArray[index].Backup(entity);
                }
                view = new DataView(data2.CurrentTable, "isnull(PBSType, '') <> 'P'", "", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    text3 = ConvertRule.ToString(view2.Row["CostBudgetSetCode"]);
                    CostBudgetDynamic dynamic = new CostBudgetDynamic(ProjectCode, text3);
                    dynamic.StartY = "1900";
                    dynamic.EndY = "3000";
                    dynamic.AutoRefreshHtml = false;
                    int num2 = 0;
                    foreach (CostBudgetDynamic dynamic2 in dynamicArray)
                    {
                        if (dynamic2.entitySet.GetString("GroupCode") == ConvertRule.ToString(view2.Row["GroupCode"]))
                        {
                            num2++;
                        }
                    }
                    CostBudgetDynamic[] dynamicArray2 = new CostBudgetDynamic[num2];
                    int num3 = -1;
                    foreach (CostBudgetDynamic dynamic2 in dynamicArray)
                    {
                        if (dynamic2.entitySet.GetString("GroupCode") == ConvertRule.ToString(view2.Row["GroupCode"]))
                        {
                            num3++;
                            dynamicArray2[num3] = dynamic2;
                        }
                    }
                    dynamic.arrDynProject = dynamicArray2;
                    dynamic.AutoRefreshDynProject = false;
                    dynamic.Generate();
                    dynamic.Backup(entity);
                }
                CostBudgetDAO.SubmitAllStandard_CostBudgetBackup(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable BuildTempTargetDtl(string ProjectCode, string CostBudgetSetCode, DataTable tbScreen, ref int status)
        {
            DataTable currentTable;
            try
            {
                string code = "";
                EntityData data = null;
                EntityData costBudgetDtlByCostBudgetCode = null;
                bool flag = false;
                status = 0;
                int num = 0;
                EntityData allCBSBySet = GetAllCBSBySet(ProjectCode, CostBudgetSetCode);
                EntityData data4 = CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "0,3", false);
                if (data4.HasRecord())
                {
                    code = data4.GetString("CostBudgetCode");
                    status = data4.GetInt("status");
                }
                else
                {
                    flag = true;
                    data = CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "1", false);
                    if (data.HasRecord())
                    {
                        status = 3;
                    }
                    else
                    {
                        status = 0;
                    }
                }
                EntityData data5 = CostBudgetDAO.GetV_CostBudgetDtlByCostBudgetCode(code);
                if (flag && (status == 3))
                {
                    costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(data.GetString("CostBudgetCode"));
                    foreach (DataRow row in costBudgetDtlByCostBudgetCode.CurrentTable.Rows)
                    {
                        num--;
                        DataRow drDst = data5.CurrentTable.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, costBudgetDtlByCostBudgetCode.CurrentTable, data5.CurrentTable, new string[] { "CostBudgetDtlCode", "CostBudgetCode" });
                        drDst["CostBudgetDtlCode"] = num;
                        CostBudgetPageRule.FillCostBudgetDtlCBSData(drDst, allCBSBySet.CurrentTable);
                        data5.CurrentTable.Rows.Add(drDst);
                    }
                }
                foreach (DataRow row3 in tbScreen.Rows)
                {
                    DataRow drDtl;
                    string text2 = ConvertRule.ToString(row3["CostCode"]);
                    DataRow[] rowArray = data5.CurrentTable.Select("CostCode = '" + text2 + "'");
                    if (rowArray.Length <= 0)
                    {
                        num--;
                        drDtl = data5.CurrentTable.NewRow();
                        drDtl["CostBudgetDtlCode"] = num;
                        drDtl["CostCode"] = text2;
                        CostBudgetPageRule.FillCostBudgetDtlCBSData(drDtl, allCBSBySet.CurrentTable);
                        data5.CurrentTable.Rows.Add(drDtl);
                    }
                    else
                    {
                        drDtl = rowArray[0];
                    }
                    drDtl["BudgetMoney"] = row3["BudgetMoney"];
                    drDtl["Description"] = row3["Description"];
                }
                DataTable tb = tbScreen.Copy();
                CBSRule.RemoveChildCBS(tb);
                foreach (DataRow row5 in tb.Rows)
                {
                    string[] textArray = row5["FullCode"].ToString().Split("-".ToCharArray());
                    for (int i = textArray.Length - 2; i >= 0; i--)
                    {
                        DataRow row6;
                        if (allCBSBySet.CurrentTable.Select("CostCode = '" + textArray[i] + "'").Length <= 0)
                        {
                            break;
                        }
                        DataRow[] rowArray2 = data5.CurrentTable.Select("CostCode = '" + textArray[i] + "'");
                        if (rowArray2.Length > 0)
                        {
                            row6 = rowArray2[0];
                        }
                        else
                        {
                            num--;
                            row6 = data5.CurrentTable.NewRow();
                            row6["CostBudgetDtlCode"] = num;
                            row6["CostCode"] = textArray[i];
                            CostBudgetPageRule.FillCostBudgetDtlCBSData(row6, allCBSBySet.CurrentTable);
                            data5.CurrentTable.Rows.Add(row6);
                        }
                        decimal num3 = MathRule.SumColumn(data5.CurrentTable.Select("ParentCode = '" + row6["CostCode"].ToString() + "'"), "BudgetMoney");
                        row6["BudgetMoney"] = num3;
                    }
                }
                data4.Dispose();
                allCBSBySet.Dispose();
                if (data != null)
                {
                    data.Dispose();
                }
                if (costBudgetDtlByCostBudgetCode != null)
                {
                    costBudgetDtlByCostBudgetCode.Dispose();
                }
                currentTable = data5.CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static void CheckCostBudget(string CostBudgetCode, string UserCode)
        {
            Exception exception;
            try
            {
                EntityData entitydata = CostBudgetDAO.GetCostBudgetByCode(CostBudgetCode);
                if (!entitydata.HasRecord())
                {
                    throw new Exception("预算表不存在");
                }
                if ((entitydata.GetInt("Status") != 0) && (entitydata.GetInt("Status") != 3))
                {
                    throw new Exception("预算表的状态不是未审或调整，不能审核");
                }
                string projectCode = entitydata.GetString("ProjectCode");
                string firstCostBudgetCode = entitydata.GetString("FirstCostBudgetCode");
                DataRow currentRow = entitydata.CurrentRow;
                currentRow["Status"] = 1;
                currentRow["CheckPerson"] = UserCode;
                currentRow["CheckDate"] = DateTime.Now;
                int targetFlag = ConvertRule.ToInt(currentRow["TargetFlag"]);
                EntityData data2 = null;
                if (firstCostBudgetCode != "")
                {
                    data2 = GetRelationCostBudget(projectCode, firstCostBudgetCode, "1", CostBudgetCode);
                    foreach (DataRow row2 in data2.CurrentTable.Rows)
                    {
                        if (row2["CostBudgetCode"].ToString() != CostBudgetCode)
                        {
                            row2["Status"] = 2;
                        }
                    }
                }
                using (StandardEntityDAO ydao = new StandardEntityDAO("CostBudget"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entitydata);
                        if (data2 != null)
                        {
                            ydao.SubmitEntity(data2);
                        }
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        try
                        {
                            ydao.RollBackTrans();
                        }
                        catch
                        {
                        }
                        throw exception;
                    }
                }
                entitydata.Dispose();
                data2.Dispose();
                UpdateAllCBSTargetMoney(projectCode, targetFlag);
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static string CheckCostBudgetContractInput(string ProjectCode, string CostBudgetSetCode, string CostCode, string ContractCode, string RelationType, decimal BudgetMoney)
        {
            string text3;
            try
            {
                string text = "";
                if (BudgetMoney > 0M)
                {
                    decimal costTargetMoneyByCostCode = GetCostTargetMoneyByCostCode(CostBudgetSetCode, CostCode);
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        string queryString = string.Format("select sum(isnull(b.BudgetMoney, 0)) from Bidding a, BiddingDtl d, CostBudgetContract b where a.BiddingCode = b.ContractCode and a.BiddingCode = d.BiddingCode and d.CostCode = b.CostCode and d.CostBudgetSetCode = b.CostBudgetSetCode and b.RelationType = 'Bidding' and isnull(a.state, 0) = 0 and b.CostBudgetSetCode = '{0}' and b.CostCode = '{1}'", CostBudgetSetCode, CostCode);
                        if (RelationType == "Bidding")
                        {
                            queryString = queryString + string.Format(" and b.ContractCode <> '{0}'", ContractCode);
                        }
                        decimal num2 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        queryString = string.Format("select sum(isnull(b.BudgetMoney, 0)) from Contract a, CostBudgetContract b where a.ContractCode = b.ContractCode and b.RelationType = 'Contract' and isnull(a.Status, 0) in (0, 1, 2, 4) and b.CostBudgetSetCode = '{0}' and b.CostCode = '{1}'", CostBudgetSetCode, CostCode);
                        if (RelationType == "Contract")
                        {
                            queryString = queryString + string.Format(" and b.ContractCode <> '{0}'", ContractCode);
                        }
                        decimal num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                        decimal num4 = (num2 + num3) + BudgetMoney;
                        if (num4 > costTargetMoneyByCostCode)
                        {
                            return string.Format("合同预算之和({1:N}元)不能超出费用项的预算({0:N}元)", costTargetMoneyByCostCode, num4);
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static void DeleteChangingTarget(string CostBudgetCode, StandardEntityDAO dao)
        {
            try
            {
                EntityData entitydata = CostBudgetDAO.GetStandard_CostBudgetByCode(dao, CostBudgetCode);
                dao.DeleteAllRow(entitydata);
                dao.DeleteEntity(entitydata);
                entitydata.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteCostBudget(string CostBudgetCode)
        {
            try
            {
                EntityData entity = CostBudgetDAO.GetStandard_CostBudgetByCode(CostBudgetCode);
                CostBudgetDAO.DeleteStandard_CostBudget(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteCostBudgetBackup(string CostBudgetBackupCode)
        {
            try
            {
                EntityData entity = CostBudgetDAO.GetStandard_CostBudgetBackupByCode(CostBudgetBackupCode);
                CostBudgetDAO.DeleteStandard_CostBudgetBackup(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteCostBudgetMonthOther(EntityData entity, string StartY, string EndY)
        {
            try
            {
                if ((StartY != "") && (EndY != ""))
                {
                    int num = ConvertRule.ToInt(StartY);
                    int num2 = ConvertRule.ToInt(EndY);
                    entity.SetCurrentTable("CostBudgetMonth");
                    for (int i = entity.CurrentTable.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = entity.CurrentTable.Rows[i];
                        if (row.RowState != DataRowState.Deleted)
                        {
                            int num5 = ConvertRule.ToInt(row["IYear"]);
                            if ((num5 < num) || (num5 > num2))
                            {
                                row.Delete();
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

        public static void DeleteCostBudgetSet(string CostBudgetSetCode)
        {
            try
            {
                if (IsCostBudgetSetHasBudget(CostBudgetSetCode))
                {
                    throw new Exception("已制定预算，不能删除预算设置表");
                }
                EntityData entity = CostBudgetDAO.GetCostBudgetSetByCode(CostBudgetSetCode);
                CostBudgetDAO.DeleteCostBudgetSet(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllCBSByGroup(string ProjectCode, string GroupCode)
        {
            EntityData data3;
            try
            {
                EntityData rootCBSByGroup = GetRootCBSByGroup(ProjectCode, GroupCode);
                string allParentFullCode = "";
                foreach (DataRow row in rootCBSByGroup.CurrentTable.Rows)
                {
                    if (allParentFullCode != "")
                    {
                        allParentFullCode = allParentFullCode + ",";
                    }
                    allParentFullCode = allParentFullCode + row["FullCode"].ToString();
                }
                EntityData cBSAllChildAndSelfByParent = GetCBSAllChildAndSelfByParent(ProjectCode, allParentFullCode);
                ResetCBSDeep(cBSAllChildAndSelfByParent.CurrentTable);
                rootCBSByGroup.Dispose();
                data3 = cBSAllChildAndSelfByParent;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data3;
        }

        public static EntityData GetAllCBSBySet(string ProjectCode, string CostBudgetSetCode)
        {
            EntityData data3;
            try
            {
                EntityData rootCBSBySet = GetRootCBSBySet(ProjectCode, CostBudgetSetCode);
                string allParentFullCode = "";
                foreach (DataRow row in rootCBSBySet.CurrentTable.Rows)
                {
                    if (allParentFullCode != "")
                    {
                        allParentFullCode = allParentFullCode + ",";
                    }
                    allParentFullCode = allParentFullCode + row["FullCode"].ToString();
                }
                EntityData cBSAllChildAndSelfByParent = GetCBSAllChildAndSelfByParent(ProjectCode, allParentFullCode);
                ResetCBSDeep(cBSAllChildAndSelfByParent.CurrentTable);
                rootCBSBySet.Dispose();
                data3 = cBSAllChildAndSelfByParent;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data3;
        }

        public static EntityData GetCBSAllChildAndSelfByParent(string ProjectCode, string AllParentFullCode)
        {
            EntityData data2;
            try
            {
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CBSStrategyName.FullCodeInLike, AllParentFullCode, "L"));
                builder.AddOrder("SortID", true);
                builder.AddOrder("Deep", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CBS", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static DataTable GetCostBudgetBackupSelectTable(string ProjectCode, string a_SelectCostBudgetBackupCode)
        {
            DataTable table2;
            try
            {
                string code = a_SelectCostBudgetBackupCode;
                DataTable tbDst = new DataTable("CostBudgetBackup");
                tbDst.Columns.Add("CostBudgetBackupCode");
                tbDst.Columns.Add("Desc");
                DataRow row = tbDst.NewRow();
                row["CostBudgetBackupCode"] = "";
                row["Desc"] = "即时";
                tbDst.Rows.Add(row);
                CostBudgetBackupStrategyBuilder builder = new CostBudgetBackupStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("BackupDate", false);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(4);
                EntityData costBudgetBackupByCode = agent.FillEntityData("CostBudgetBackup", queryString);
                agent.Dispose();
                foreach (DataRow row2 in costBudgetBackupByCode.CurrentTable.Rows)
                {
                    row = tbDst.NewRow();
                    ConvertRule.DataRowCopy(row2, row, costBudgetBackupByCode.CurrentTable, tbDst);
                    if (ConvertRule.ToString(row2["CostBudgetBackupCode"]).StartsWith("Offline_"))
                    {
                        row["Desc"] = "非即时 " + ConvertRule.ToDateString(row2["BackupDate"], "yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        row["Desc"] = "存档 " + ConvertRule.ToDateString(row2["BackupDate"], "yyyy-MM-dd HH:mm:ss");
                    }
                    tbDst.Rows.Add(row);
                }
                costBudgetBackupByCode.Dispose();
                if ((code != "") && (tbDst.Select("CostBudgetBackupCode = '" + code + "'").Length == 0))
                {
                    costBudgetBackupByCode = CostBudgetDAO.GetCostBudgetBackupByCode(code);
                    if (costBudgetBackupByCode.HasRecord())
                    {
                        row = tbDst.NewRow();
                        ConvertRule.DataRowCopy(costBudgetBackupByCode.CurrentRow, row, costBudgetBackupByCode.CurrentTable, tbDst);
                        row["Desc"] = ConvertRule.ToDateString(costBudgetBackupByCode.CurrentRow["BackupDate"], "yyyy-MM-dd HH:mm:ss");
                        tbDst.Rows.Add(row);
                    }
                    costBudgetBackupByCode.Dispose();
                }
                row = tbDst.NewRow();
                row["CostBudgetBackupCode"] = "more";
                row["Desc"] = "更多存档...";
                tbDst.Rows.Add(row);
                table2 = tbDst;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static decimal GetCostBudgetDtlHisNextVerID(string CostBudgetDtlCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                CostBudgetDtlHisStrategyBuilder builder = new CostBudgetDtlHisStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetDtlHisStrategyName.CostBudgetDtlCode, CostBudgetDtlCode));
                builder.AddOrder("VerID", false);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("CostBudgetDtlHis", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    num = (data.GetDecimal("VerID")) + 1;
                }
                data.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetCostBudgetProjectArea(string ProjectCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                EntityData costBudgetSetByProjectCode = CostBudgetDAO.GetCostBudgetSetByProjectCode(ProjectCode);
                DataRow[] rowArray = costBudgetSetByProjectCode.CurrentTable.Select("PBSType='P' and BuildingArea <> 0");
                if (rowArray.Length > 0)
                {
                    num = ConvertRule.ToDecimal(rowArray[0]["BuildingArea"]);
                }
                costBudgetSetByProjectCode.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetCostBudgetSetName(string CostBudgetSetCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData costBudgetSetByCode = CostBudgetDAO.GetCostBudgetSetByCode(CostBudgetSetCode);
                if (costBudgetSetByCode.HasRecord())
                {
                    text = costBudgetSetByCode.GetString("CostBudgetSetName");
                }
                costBudgetSetByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetCostBudgetSetTypeTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable("SetType");
                table.Columns.Add("Code");
                table.Columns.Add("Name");
                table.Rows.Add(new object[] { m_BaseSetType, m_BaseSetType });
                EntityData dictionaryItemByNameProject = SystemManageDAO.GetDictionaryItemByNameProject("预算设置自定义类型", "");
                int count = dictionaryItemByNameProject.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dictionaryItemByNameProject.SetCurrentRow(i);
                    if (dictionaryItemByNameProject.GetString("Name") != m_BaseSetType)
                    {
                        table.Rows.Add(new object[] { dictionaryItemByNameProject.GetString("Name"), dictionaryItemByNameProject.GetString("Name") });
                    }
                }
                dictionaryItemByNameProject.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void GetCostBudgetStartEnd(string ProjectCode, ref string StartYm, ref string EndYm)
        {
            try
            {
                StartYm = "";
                EndYm = "";
                EntityData projectByCode = ProjectDAO.GetProjectByCode(ProjectCode);
                if (projectByCode.HasRecord())
                {
                    StartYm = projectByCode.GetDateTime("kgDate", "yyyy");
                    EndYm = projectByCode.GetDateTime("jgDate", "yyyy");
                }
                projectByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetCostBudgetVerName(DataRow drCostBudget)
        {
            string text2;
            try
            {
                string text = "";
                text = ConvertRule.ToString(drCostBudget["CostBudgetName"]);
                if (text == "")
                {
                    text = "版本" + ConvertRule.ToString(drCostBudget["VerID"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetCostBudgetVerName(string CostBudgetCode)
        {
            string text2;
            try
            {
                string costBudgetVerName = "";
                EntityData costBudgetByCode = CostBudgetDAO.GetCostBudgetByCode(CostBudgetCode);
                if (costBudgetByCode.HasRecord())
                {
                    costBudgetVerName = GetCostBudgetVerName(costBudgetByCode.CurrentRow);
                }
                costBudgetByCode.Dispose();
                text2 = costBudgetVerName;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetCostTargetMoneyByCostCode(string CostBudgetSetCode, string CostCode)
        {
            decimal num2;
            try
            {
                decimal @decimal = 0M;
                EntityData data = GetValidCostBudget(CostBudgetSetCode, 1, false);
                if (data.HasRecord())
                {
                    CostBudgetDtlStrategyBuilder builder = new CostBudgetDtlStrategyBuilder();
                    builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostBudgetCode, data.GetString("CostBudgetCode")));
                    builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostCode, CostCode));
                    string queryString = builder.BuildMainQueryString();
                    QueryAgent agent = new QueryAgent();
                    EntityData data2 = agent.FillEntityData("CostBudgetDtl", queryString);
                    if (data2.HasRecord())
                    {
                        @decimal = data2.GetDecimal("BudgetMoney");
                    }
                    data2.Dispose();
                    agent.Dispose();
                }
                data.Dispose();
                num2 = @decimal;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static EntityData GetCurrentCostBudget(string CostBudgetSetCode, int TargetFlag, bool isView)
        {
            EntityData data2;
            try
            {
                EntityData data = CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "0,3", isView);
                if (!data.HasRecord())
                {
                    data.Dispose();
                    data = CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, 1, "1", isView);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetDynamicDateDesc(string CostBudgetBackupCode, EntityData entityBackup)
        {
            string text3;
            try
            {
                string text = "";
                string dateTime = "";
                if (CostBudgetBackupCode != "")
                {
                    if (CostBudgetBackupCode.StartsWith(CostBudgetDAO.m_OfflineBackupCodeStartWith))
                    {
                        text = "非即时";
                    }
                    else
                    {
                        text = "存档";
                    }
                    if (entityBackup.HasRecord())
                    {
                        dateTime = entityBackup.GetDateTime("BackupDate", "yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    text = "即时";
                    dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                text3 = text + " " + dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetFirstCostBudgetCode(string ProjectCode, string CostBudgetCode)
        {
            string text3;
            try
            {
                string text = "";
                CostBudgetStrategyBuilder builder = new CostBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.CostBudgetCode, CostBudgetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("CostBudget", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("FirstCostBudgetCode");
                }
                data.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetFirstCostBudgetCode(string ProjectCode, string CostBudgetSetCode, int TargetFlag)
        {
            string text3;
            try
            {
                string text = "";
                CostBudgetStrategyBuilder builder = new CostBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.CostBudgetSetCode, CostBudgetSetCode));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.TargetFlag, TargetFlag.ToString()));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("CostBudget", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("FirstCostBudgetCode");
                }
                data.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetNoTimeDateDesc(string CostBudgetBackupCode, EntityData entityBackup)
        {
            string text3;
            try
            {
                string text = "";
                string dateTime = "";
                if (CostBudgetBackupCode != "")
                {
                    if (CostBudgetBackupCode.StartsWith(CostBudgetDAO.m_OfflineBackupCodeStartWith))
                    {
                        text = "非即时";
                    }
                    else
                    {
                        text = "存档";
                    }
                    if (entityBackup.HasRecord())
                    {
                        dateTime = entityBackup.GetDateTime("BackupDate", "yyyy-MM-dd");
                    }
                }
                else
                {
                    text = "即时";
                    dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                }
                text3 = text + " " + dateTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetOfflineBackupCode(string ProjectCode)
        {
            string text2;
            try
            {
                text2 = CostBudgetDAO.m_OfflineBackupCodeStartWith + ProjectCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetOfflineValidHours(string ProjectCode)
        {
            decimal num;
            try
            {
                num = ConvertRule.ToDecimal(SystemRule.GetProjectConfigValue(ProjectCode, "CostBudgetOffineValidHours"));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static string GetPBSDistrictCode(string PBSType, string PBSCode)
        {
            string text2;
            try
            {
                string text = "";
                if (PBSType == "B")
                {
                    EntityData buildingByCode = ProductDAO.GetBuildingByCode(PBSCode);
                    if (buildingByCode.HasRecord())
                    {
                        if (buildingByCode.GetInt("IsArea") == 1)
                        {
                            text = buildingByCode.GetString("BuildingCode");
                        }
                        else
                        {
                            text = buildingByCode.GetString("ParentCode");
                        }
                    }
                    buildingByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPBSName(string CostBudgetSetCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData data = CostBudgetDAO.GetV_CostBudgetSetByCode(CostBudgetSetCode);
                if (data.HasRecord())
                {
                    text = data.GetString("PBSName");
                }
                data.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPBSName(string PBSType, string PBSCode)
        {
            string text3;
            try
            {
                string queryString = string.Format("select dbo.GetPBSName('{0}', '{1}')", PBSType, PBSCode);
                QueryAgent agent = new QueryAgent();
                text3 = ConvertRule.ToString(agent.ExecuteScalar(queryString));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static EntityData GetRelationCostBudget(string ProjectCode, string FirstCostBudgetCode, string InStatus, string ExceptCostBudgetCode)
        {
            EntityData data2;
            try
            {
                CostBudgetStrategyBuilder builder = new CostBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.FirstCostBudgetCode, FirstCostBudgetCode));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.Status, InStatus));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.ExceptCostBudgetCode, ExceptCostBudgetCode));
                builder.AddOrder("VerID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudget", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRelationV_CostBudget(string ProjectCode, string FirstCostBudgetCode, string InStatus, string ExceptCostBudgetCode)
        {
            EntityData data2;
            try
            {
                CostBudgetStrategyBuilder builder = new CostBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.FirstCostBudgetCode, FirstCostBudgetCode));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.Status, InStatus));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.ExceptCostBudgetCode, ExceptCostBudgetCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudget", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRootCBSByGroup(string ProjectCode, string BudgetType)
        {
            EntityData data2;
            try
            {
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CBSStrategyName.BudgetType, BudgetType));
                builder.AddOrder("SortID", true);
                builder.AddOrder("Deep", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CBS", queryString);
                agent.Dispose();
                CBSRule.RemoveChildCBS(data.CurrentTable);
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRootCBSBySet(string ProjectCode, string CostBudgetSetCode)
        {
            EntityData rootCBSByGroup;
            try
            {
                string budgetType = "";
                EntityData costBudgetSetByCode = CostBudgetDAO.GetCostBudgetSetByCode(CostBudgetSetCode);
                if (costBudgetSetByCode.HasRecord())
                {
                    budgetType = costBudgetSetByCode.GetString("GroupCode");
                }
                costBudgetSetByCode.Dispose();
                if (budgetType == "")
                {
                    return null;
                }
                rootCBSByGroup = GetRootCBSByGroup(ProjectCode, budgetType);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return rootCBSByGroup;
        }

        public static decimal GetTotalMoneyByCBS(DataTable tbDtl, string FieldName)
        {
            decimal num2;
            try
            {
                DataTable tb = tbDtl.Copy();
                tb.AcceptChanges();
                DataRow[] rowArray = tb.Select("CostCode = 'R_0'");
                foreach (DataRow row in rowArray)
                {
                    tb.Rows.Remove(row);
                }
                CBSRule.RemoveChildCBS(tb);
                num2 = MathRule.SumColumn(tb, FieldName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static EntityData GetValidCostBudget(string CostBudgetSetCode, int TargetFlag)
        {
            EntityData data;
            try
            {
                data = GetValidCostBudget(CostBudgetSetCode, TargetFlag, false);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }

        public static EntityData GetValidCostBudget(string CostBudgetSetCode, int TargetFlag, bool isView)
        {
            EntityData data;
            try
            {
                data = CostBudgetDAO.GetCostBudgetByStatus(CostBudgetSetCode, TargetFlag, "1", isView);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }

        public static bool IsCostBudgetSetHasBudget(string CostBudgetSetCode)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select top 1 1 from CostBudget where CostBudgetSetCode = '{0}'", CostBudgetSetCode);
                    if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                    {
                        return true;
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
            return false;
        }

        public static bool IsCostTargetNeedCheck(string CostBudgetSetCode, DataTable tbDtl)
        {
            bool flag2;
            try
            {
                bool flag = false;
                EntityData validCostBudget = GetValidCostBudget(CostBudgetSetCode, 1);
                if (!validCostBudget.HasRecord())
                {
                    return true;
                }
                EntityData costBudgetDtlByCostBudgetCode = CostBudgetDAO.GetCostBudgetDtlByCostBudgetCode(validCostBudget.GetString("CostBudgetCode"));
                decimal totalMoneyByCBS = GetTotalMoneyByCBS(tbDtl, "BudgetMoney");
                decimal num2 = GetTotalMoneyByCBS(costBudgetDtlByCostBudgetCode.CurrentTable, "BudgetMoney");
                if (totalMoneyByCBS != num2)
                {
                    return true;
                }
                costBudgetDtlByCostBudgetCode.Dispose();
                validCostBudget.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsOfflineExpire(decimal OfflineValidHours, DateTime dtBackupDate)
        {
            try
            {
                if (OfflineValidHours <= 0M)
                {
                    return true;
                }
                if (DateTime.Now.Subtract(dtBackupDate).Duration().TotalHours >= ((double) OfflineValidHours))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return false;
        }

        public static string OnlineUpdate(string ProjectCode, string UserCode)
        {
            string text2;
            try
            {
                string offlineBackupCode = GetOfflineBackupCode(ProjectCode);
                Backup(ProjectCode, "即时更新版本", "即时更新版本", UserCode, offlineBackupCode);
                text2 = offlineBackupCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void ResetCBSDeep(DataTable tbAllCBS)
        {
            try
            {
                DataTable tb = tbAllCBS.Copy();
                CBSRule.RemoveChildCBS(tb);
                int num = 1;
                foreach (DataRow row in tb.Rows)
                {
                    string text = ConvertRule.ToString(row["CostCode"]);
                    string text2 = ConvertRule.ToString(row["FullCode"]);
                    int num3 = ConvertRule.ToInt(row["Deep"]) - num;
                    DataRow[] rowArray = tbAllCBS.Select("CostCode = '" + text + "'");
                    foreach (DataRow row2 in rowArray)
                    {
                        row2["Deep"] = num;
                        row2["ParentCode"] = "";
                    }
                    DataRow[] rowArray2 = tbAllCBS.Select("FullCode like '" + text2 + "-%'");
                    foreach (DataRow row3 in rowArray2)
                    {
                        row3["Deep"] = ConvertRule.ToInt(row3["Deep"]) - num3;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SaveCostBudgetDtl(EntityData entity, DataTable tbDtl, string StartY, string EndY)
        {
            try
            {
                string newSysCode;
                string text4;
                DataRow[] rowArray;
                entity.SetCurrentTable("CostBudget");
                string text = entity.GetString("CostBudgetCode");
                string text2 = entity.GetString("ProjectCode");
                entity.SetCurrentTable("CostBudgetDtl");
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    newSysCode = ConvertRule.ToString(row["CostBudgetDtlCode"]);
                    text4 = row["CostCode"].ToString();
                    if (tbDtl.Select("CostCode='" + text4 + "'").Length == 0)
                    {
                        row.Delete();
                        rowArray = entity.Tables["CostBudgetMonth"].Select("CostBudgetDtlCode='" + newSysCode + "'");
                        foreach (DataRow row2 in rowArray)
                        {
                            row2.Delete();
                        }
                    }
                }
                foreach (DataRow row3 in tbDtl.Rows)
                {
                    DataRow row4;
                    if (ConvertRule.ToString(row3["CostBudgetDtlCode"]).StartsWith("R_"))
                    {
                        continue;
                    }
                    text4 = row3["CostCode"].ToString();
                    entity.SetCurrentTable("CostBudgetDtl");
                    rowArray = entity.CurrentTable.Select("CostCode='" + text4 + "'");
                    if (rowArray.Length == 0)
                    {
                        row4 = entity.CurrentTable.NewRow();
                        newSysCode = SystemManageDAO.GetNewSysCode("CostBudgetDtlCode");
                        row4["CostBudgetCode"] = text;
                        row4["ProjectCode"] = text2;
                        row4["CostBudgetDtlCode"] = newSysCode;
                        row4["CostCode"] = text4;
                        entity.CurrentTable.Rows.Add(row4);
                    }
                    else
                    {
                        row4 = rowArray[0];
                        newSysCode = ConvertRule.ToString(row4["CostBudgetDtlCode"]);
                    }
                    row4["Price"] = row3["Price"];
                    row4["Qty"] = row3["Qty"];
                    row4["BudgetMoney"] = row3["BudgetMoney"];
                    row4["IsExpand"] = row3["IsExpand"];
                    row4["Description"] = row3["Description"];
                    entity.SetCurrentTable("CostBudgetMonth");
                    foreach (DataColumn column in tbDtl.Columns)
                    {
                        if (column.ColumnName.StartsWith("BudgetMoney_"))
                        {
                            DataRow row5;
                            string text5 = column.ColumnName.Replace("BudgetMoney_", "");
                            string val = text5.Substring(0, 4);
                            string text7 = text5.Substring(4, 2);
                            int num = ConvertRule.ToInt(val);
                            int num2 = ConvertRule.ToInt(text7);
                            DataRow[] rowArray2 = entity.CurrentTable.Select("CostBudgetDtlCode = '" + newSysCode + "' and IYear = " + num.ToString() + " and IMonth = " + num2.ToString());
                            if (rowArray2.Length > 0)
                            {
                                row5 = rowArray2[0];
                            }
                            else
                            {
                                row5 = entity.CurrentTable.NewRow();
                                row5["CostBudgetMonthCode"] = SystemManageDAO.GetNewSysCode("CostBudgetMonthCode");
                                row5["CostBudgetDtlCode"] = newSysCode;
                                row5["CostBudgetCode"] = text;
                                row5["ProjectCode"] = text2;
                                row5["IYear"] = num;
                                row5["IMonth"] = num2;
                                entity.CurrentTable.Rows.Add(row5);
                            }
                            row5["BudgetMoney"] = ConvertRule.ToDecimal(row3[column.ColumnName]);
                        }
                    }
                }
                DeleteCostBudgetMonthOther(entity, StartY, EndY);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SaveCostBudgetTotalBudgetMoney(DataTable tb, DataTable tbDtl)
        {
            try
            {
                if (tb.Rows.Count != 0)
                {
                    decimal totalMoneyByCBS = GetTotalMoneyByCBS(tbDtl, "BudgetMoney");
                    tb.Rows[0]["TotalBudgetMoney"] = totalMoneyByCBS;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SaveTempTarget(EntityData entity, EntityData entityValid, string ProjectCode, string CostBudgetSetCode, int Status, string UserCode, string CostBudgetName)
        {
            try
            {
                entity.SetCurrentTable("CostBudget");
                DataRow currentRow = null;
                if (!entity.HasRecord())
                {
                    string newSysCode = SystemManageDAO.GetNewSysCode("CostBudgetCode");
                    currentRow = entity.CurrentTable.NewRow();
                    currentRow["CostBudgetCode"] = newSysCode;
                    currentRow["CostBudgetSetCode"] = CostBudgetSetCode;
                    currentRow["ProjectCode"] = ProjectCode;
                    currentRow["Status"] = Status;
                    switch (ConvertRule.ToInt(currentRow["Status"]))
                    {
                        case 0:
                            currentRow["VerID"] = 0;
                            currentRow["FirstCostBudgetCode"] = newSysCode;
                            break;

                        case 3:
                            currentRow["VerID"] = (entityValid.GetDecimal("VerID")) + 1;
                            currentRow["FirstCostBudgetCode"] = entityValid.GetString("FirstCostBudgetCode");
                            break;
                    }
                    currentRow["CreatePerson"] = UserCode;
                    currentRow["CreateDate"] = DateTime.Now;
                    entity.CurrentTable.Rows.Add(currentRow);
                }
                else
                {
                    currentRow = entity.CurrentRow;
                }
                currentRow["TargetFlag"] = 1;
                currentRow["CostBudgetName"] = CostBudgetName;
                currentRow["ModifyPerson"] = UserCode;
                currentRow["ModifyDate"] = DateTime.Now;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateAllCBSTargetMoney(string ProjectCode, int TargetFlag)
        {
            try
            {
                string format;
                QueryAgent agent = new QueryAgent();
                if (TargetFlag == 1)
                {
                    format = "update CBS   set TotalTargetMoney = b.BudgetMoney  from CBS a       left join (               select b.CostCode, sum(isnull(b.BudgetMoney, 0)) as BudgetMoney                 from CostBudget a, CostBudgetDtl b                where a.CostBudgetCode = b.CostBudgetCode                  and a.Status = 1                  and isnull(a.TargetFlag, 0) = 1                group by b.CostCode               ) as b on b.CostCode = a.CostCode where a.ProjectCode = '{0}'";
                }
                else
                {
                    format = "update CBS   set TotalBudgetMoney = b.BudgetMoney  from CBS a       left join (               select b.CostCode, sum(isnull(b.BudgetMoney, 0)) as BudgetMoney                 from CostBudget a, CostBudgetDtl b                where a.CostBudgetCode = b.CostBudgetCode                  and a.Status = 1                  and isnull(a.TargetFlag, 0) = 0                group by b.CostCode               ) as b on b.CostCode = a.CostCode where a.ProjectCode = '{0}'";
                }
                format = string.Format(format, ProjectCode);
                agent.ExecuteSql(format);
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

