namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.SessionState;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class WBSRule
    {
        public static object CalcTaskBudgetDateByTask(string taskBudgetCode, DataTable tbTaskBudgetCondition)
        {
            object obj5;
            try
            {
                object obj2 = null;
                if (taskBudgetCode == "")
                {
                    return obj2;
                }
                DataRow[] rowArray = tbTaskBudgetCondition.Select("TaskBudgetCode='" + taskBudgetCode + "'");
                if (rowArray.Length == 0)
                {
                    return obj2;
                }
                DataRow row = rowArray[0];
                string text = ConvertRule.ToString(row["TaskBudgetConditionCode"]);
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
                    obj2 = ContractRule.GetDateByDateRangePercent(startDate, endDate, percent, addDays);
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

        public static void DeleteAttachByMaster(string type, string MasterCode)
        {
            try
            {
                EntityData entity = DAOFactory.GetAttachmentDAO().GetAttachMentByMasterCode(type, MasterCode);
                DAOFactory.GetAttachmentDAO().DeleteAttachMent(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteNoRightTask(DataTable tb, string UserCode)
        {
            try
            {
                if (tb.Rows.Count > 0)
                {
                    string[] arrHasRightFullCode = WBSStrategyBuilder.GetHasRightFullCodeArray(UserCode);
                    SetTaskIsRight(tb, "IsRight", arrHasRightFullCode);
                    DeleteNoRightTask(tb, arrHasRightFullCode);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteNoRightTask(DataTable tbTask, string[] arrHasRightFullCode)
        {
            for (int i = tbTask.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = tbTask.Rows[i];
                if (ConvertRule.ToString(row["IsRight"]) != "1")
                {
                    string text = ConvertRule.ToString(row["FullCode"]);
                    bool flag = false;
                    foreach (string text2 in arrHasRightFullCode)
                    {
                        if (text2.IndexOf(text) == 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        tbTask.Rows.Remove(row);
                    }
                }
            }
        }

        public static void DeleteTask(string WBSCode)
        {
            try
            {
                EntityData entity = WBSDAO.GetStandard_WBSByCode(WBSCode);
                WBSDAO.DeleteStandard_WBS(entity);
                entity.Dispose();
                EntityData doubleRelatedByCode = WBSDAO.GetDoubleRelatedByCode(WBSCode);
                if (doubleRelatedByCode.HasRecord())
                {
                    WBSDAO.DeleteTaskRelated(doubleRelatedByCode);
                }
                doubleRelatedByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskExecute(string TaskExecuteCode)
        {
            try
            {
                EntityData entity = WBSDAO.GetStandard_TaskExecuteByCode(TaskExecuteCode);
                if (entity.HasRecord())
                {
                    WBSDAO.DeleteStandard_TaskExecute(entity);
                    DeleteAttachByMaster("TaskExecute", TaskExecuteCode);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GenerateTaskExecuteRiskTable(string TaskExecuteCode, bool IsDefault)
        {
            DataTable table3;
            try
            {
                EntityData data = new EntityData("TaskExecuteRisk");
                EntityData allRiskType = ConstructDAO.GetAllRiskType();
                EntityData taskExecuteRiskByTaskExecuteCode = WBSDAO.GetTaskExecuteRiskByTaskExecuteCode(TaskExecuteCode);
                DataTable currentTable = data.CurrentTable;
                DataTable table2 = taskExecuteRiskByTaskExecuteCode.CurrentTable;
                currentTable.Columns.Add("RiskIndexName", typeof(string));
                int num = 0;
                int count = allRiskType.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allRiskType.SetCurrentRow(i);
                    string text = allRiskType.GetString("TypeName");
                    DataRow row = currentTable.NewRow();
                    row["RiskTypeName"] = text;
                    DataRow[] rowArray = table2.Select("RiskTypeName='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        row["TaskExecuteRiskCode"] = ConvertRule.ToString(rowArray[0]["TaskExecuteRiskCode"]);
                        row["RiskIndexCode"] = rowArray[0]["RiskIndexCode"];
                    }
                    else
                    {
                        num--;
                        row["TaskExecuteRiskCode"] = num.ToString();
                    }
                    if ((ConvertRule.ToString(row["RiskIndexCode"]) == "") && IsDefault)
                    {
                        EntityData defaultRiskIndex = ConstructDAO.GetDefaultRiskIndex();
                        if (defaultRiskIndex.HasRecord())
                        {
                            row["RiskIndexCode"] = defaultRiskIndex.GetString("IndexCode");
                            row["RiskIndexName"] = defaultRiskIndex.GetString("IndexName");
                        }
                        defaultRiskIndex.Dispose();
                    }
                    if (ConvertRule.ToString(row["RiskIndexName"]) == "")
                    {
                        row["RiskIndexName"] = ConstructRule.GetRiskIndexName(row["RiskIndexCode"]);
                    }
                    currentTable.Rows.Add(row);
                }
                taskExecuteRiskByTaskExecuteCode.Dispose();
                allRiskType.Dispose();
                data.Dispose();
                table3 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static string[] GetAllHasRightTask(HttpSessionState Session, string UserCode)
        {
            if (Session["TaskHasRightFullCode"] == null)
            {
                ResetAllHasRightTask(Session, UserCode);
            }
            return (string[]) Session["TaskHasRightFullCode"];
        }

        public static string GetAttachMentCodeByUserCode(string UserCode)
        {
            string text = "";
            EntityData attachMentByTypeAndMasterCode = DAOFactory.GetAttachmentDAO().GetAttachMentByTypeAndMasterCode("ImageSign", UserCode);
            if (attachMentByTypeAndMasterCode.HasRecord())
            {
                text = attachMentByTypeAndMasterCode.GetString("AttachMentCode");
            }
            attachMentByTypeAndMasterCode.Dispose();
            return text;
        }

        public static EntityData GetChangedDataByConstructProg(string BuildingCode, string WBSCode)
        {
            EntityData data4;
            try
            {
                string s = "";
                string dateTimeOnlyDate = "";
                string text3 = "";
                string text4 = "";
                EntityData taskByCode = WBSDAO.GetTaskByCode(WBSCode);
                EntityData buildingFloorByBuildingCode = ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
                s = taskByCode.CurrentTable.Rows[0]["PlannedStartDate"].ToString();
                dateTimeOnlyDate = taskByCode.CurrentTable.Rows[0]["PlannedFinishDate"].ToString();
                text3 = taskByCode.CurrentTable.Rows[0]["ActualStartDate"].ToString();
                text4 = taskByCode.CurrentTable.Rows[0]["ActualFinishDate"].ToString();
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                if (buildingFloorByBuildingCode.HasRecord())
                {
                    int count = buildingFloorByBuildingCode.CurrentTable.Rows.Count;
                    foreach (DataRow row in buildingFloorByBuildingCode.CurrentTable.Rows)
                    {
                        EntityData buildingFloorProgressByBuildingFloorWBSCode = ProductDAO.GetBuildingFloorProgressByBuildingFloorWBSCode(row["BuildingFloorCode"].ToString(), WBSCode);
                        if (buildingFloorProgressByBuildingFloorWBSCode.HasRecord())
                        {
                            if ("" == s)
                            {
                                s = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("PStartDate");
                                flag = true;
                            }
                            else if ((buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("PStartDate") != "") && (DateTime.Parse(buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("PStartDate")).Subtract(DateTime.Parse(s)).Days < 0))
                            {
                                s = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("PStartDate").ToString();
                                flag = true;
                            }
                            if ("" == dateTimeOnlyDate)
                            {
                                dateTimeOnlyDate = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("penddate");
                                flag2 = true;
                            }
                            else if ((buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("penddate") != "") && (DateTime.Parse(buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("penddate")).Subtract(DateTime.Parse(dateTimeOnlyDate)).Days > 0))
                            {
                                dateTimeOnlyDate = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("penddate").ToString();
                                flag2 = true;
                            }
                            if ("" == text3)
                            {
                                text3 = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("startdate");
                                flag3 = true;
                            }
                            else if ((buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("startdate") != "") && (DateTime.Parse(buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("startdate")).Subtract(DateTime.Parse(text3)).Days < 0))
                            {
                                text3 = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("startdate").ToString();
                                flag3 = true;
                            }
                            if (100 <= ConvertRule.ToInt(taskByCode.CurrentTable.Rows[0]["CompletePercent"]))
                            {
                                if ("" == text4)
                                {
                                    text4 = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("enddate");
                                    flag4 = true;
                                }
                                else if ((buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("enddate") != "") && (DateTime.Parse(buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("enddate")).Subtract(DateTime.Parse(text4)).Days > 0))
                                {
                                    text4 = buildingFloorProgressByBuildingFloorWBSCode.GetDateTimeOnlyDate("enddate").ToString();
                                    flag4 = true;
                                }
                            }
                        }
                        buildingFloorProgressByBuildingFloorWBSCode.Dispose();
                    }
                    if (flag)
                    {
                        taskByCode.CurrentTable.Rows[0]["PlannedStartDate"] = ConvertRule.ToDate(s);
                    }
                    if (flag2)
                    {
                        taskByCode.CurrentTable.Rows[0]["PlannedFinishDate"] = ConvertRule.ToDate(dateTimeOnlyDate);
                    }
                    if (flag3)
                    {
                        taskByCode.CurrentTable.Rows[0]["ActualStartDate"] = ConvertRule.ToDate(text3);
                    }
                    if (flag4)
                    {
                        taskByCode.CurrentTable.Rows[0]["ActualFinishDate"] = ConvertRule.ToDate(text4);
                    }
                }
                buildingFloorByBuildingCode.Dispose();
                data4 = taskByCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data4;
        }

        public static string GetFieldName(string Code, string FieldName)
        {
            string text2;
            try
            {
                string intString = "";
                if (Code == "")
                {
                    return intString;
                }
                EntityData data = WBSDAO.GetV_TaskByCode(Code);
                if (data.HasRecord() && data.CurrentTable.Columns.Contains(FieldName))
                {
                    if (data.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.Int32"))
                    {
                        intString = data.GetIntString(FieldName);
                    }
                    if (data.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.Decimal"))
                    {
                        intString = data.GetDecimalString(FieldName);
                    }
                    if (data.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.DateTime"))
                    {
                        intString = data.GetDateTimeOnlyDate(FieldName);
                    }
                    if (data.CurrentTable.Columns[FieldName].DataType == Type.GetType("System.String"))
                    {
                        intString = data.GetString(FieldName);
                    }
                }
                text2 = intString;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetHasRightChildTask(int Deep, string ProjectCode, string UserCode)
        {
            DataTable table2;
            try
            {
                DataTable tb;
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(WBSStrategyName.Deep, Deep.ToString()));
                builder.AddOrder(" PlannedStartDate ", false);
                builder.AddOrder(" SortID ", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                try
                {
                    EntityData data = agent.FillEntityData("Task", queryString);
                    try
                    {
                        tb = data.CurrentTable;
                        DeleteNoRightTask(tb, UserCode);
                    }
                    finally
                    {
                        data.Dispose();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetHasRightChildTask(string ParentCode, string ProjectCode, string UserCode)
        {
            DataTable table2;
            try
            {
                DataTable tb;
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(WBSStrategyName.ParentCode, ParentCode));
                builder.AddOrder(" PlannedStartDate ", false);
                builder.AddOrder(" SortID ", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                try
                {
                    EntityData data = agent.FillEntityData("Task", queryString);
                    try
                    {
                        tb = data.CurrentTable;
                        DeleteNoRightTask(tb, UserCode);
                    }
                    finally
                    {
                        data.Dispose();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                table2 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetProjectCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData taskByCode = WBSDAO.GetTaskByCode(code);
                if (taskByCode.HasRecord())
                {
                    text = taskByCode.GetString("ProjectCode");
                }
                taskByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetTaskBudgetPayConditionHtml(string TaskBudgetCode, DataTable tbPayCondition, bool isModify)
        {
            string text7;
            try
            {
                string text = "";
                if (TaskBudgetCode == "")
                {
                    return text;
                }
                DataRow[] rowArray = tbPayCondition.Select("TaskBudgetCode='" + TaskBudgetCode + "'");
                foreach (DataRow row in rowArray)
                {
                    string text2 = ConvertRule.ToString(row["TaskBudgetConditionCode"]);
                    string text3 = ConvertRule.ToString(row["RelationWBSCode"]);
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
                        text = text + "<a style=\"cursor:hand\" onclick=\"javascript:ModifyPayCondition(this.TaskBudgetConditionCode, this.TaskBudgetCode);\" TaskBudgetConditionCode=\"" + text2 + "\" TaskBudgetCode=\"" + TaskBudgetCode + "\">" + text4 + text6 + "</a>";
                    }
                    else
                    {
                        text = text + "<a style=\"cursor:hand\" onclick=\"javascript:OpenTask(this.code);\" code=\"" + text3 + "\">" + text4 + "</a>" + text6;
                    }
                }
                if (isModify && (rowArray.Length == 0))
                {
                    text = text + "<a style=\"cursor:hand\" onclick=\"javascript:AddPayCondition(this.TaskBudgetCode);\" TaskBudgetCode=\"" + TaskBudgetCode + "\">新增</a>";
                }
                text7 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text7;
        }

        public static int GetTaskCompletePercent(string WBSCode)
        {
            int num2;
            try
            {
                int @int = 0;
                if (WBSCode == "")
                {
                    return @int;
                }
                EntityData taskByCode = WBSDAO.GetTaskByCode(WBSCode);
                if (taskByCode.HasRecord())
                {
                    @int = taskByCode.GetInt("CompletePercent");
                }
                taskByCode.Dispose();
                num2 = @int;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetTaskPersonName(DataTable tbGroup, int Type)
        {
            string text2;
            try
            {
                string text = "";
                DataRow[] rowArray = tbGroup.Select("Type=" + Type.ToString());
                if (rowArray.Length > 0)
                {
                    text = ConvertRule.ToString(rowArray[0]["UserNames"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetTaskPersonNameDirector(DataTable tbGroup)
        {
            string taskPersonName;
            try
            {
                taskPersonName = GetTaskPersonName(tbGroup, 5);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return taskPersonName;
        }

        public static string GetTaskPersonNameExecuter(DataTable tbGroup)
        {
            string taskPersonName;
            try
            {
                taskPersonName = GetTaskPersonName(tbGroup, 0);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return taskPersonName;
        }

        public static DataTable GetTaskPersonNameGroupByType(string WBSCode)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Type", typeof(int));
                table.Columns.Add("UserCodes");
                table.Columns.Add("UserNames");
                EntityData taskPersonByWBSCode = WBSDAO.GetTaskPersonByWBSCode(WBSCode);
                foreach (DataRow row in taskPersonByWBSCode.CurrentTable.Rows)
                {
                    DataRow row2;
                    int num = ConvertRule.ToInt(row["RoleType"]);
                    int num2 = ConvertRule.ToInt(row["Type"]);
                    string userCode = ConvertRule.ToString(row["UserCode"]);
                    string userName = "";
                    DataRow[] rowArray = table.Select("Type=" + num2);
                    if (rowArray.Length == 0)
                    {
                        row2 = table.NewRow();
                        row2["Type"] = num2;
                        table.Rows.Add(row2);
                    }
                    else
                    {
                        row2 = rowArray[0];
                    }
                    if (num == 0)
                    {
                        userName = SystemRule.GetUserName(userCode);
                    }
                    else
                    {
                        userName = SystemRule.GetStationName(userCode);
                    }
                    string text3 = ConvertRule.ToString(row2["UserCodes"]);
                    if (text3 != "")
                    {
                        text3 = text3 + ",";
                    }
                    text3 = text3 + userCode;
                    string text4 = ConvertRule.ToString(row2["UserNames"]);
                    if (text4 != "")
                    {
                        text4 = text4 + ",";
                    }
                    text4 = text4 + userName;
                    row2["UserCodes"] = text3;
                    row2["UserNames"] = text4;
                }
                taskPersonByWBSCode.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetTaskPersonNameGroupByTypeIncludeEmpty(string WBSCode)
        {
            DataTable table3;
            try
            {
                DataTable taskPersonStructure = GetTaskPersonStructure();
                taskPersonStructure.Columns.Add("UserCodes");
                taskPersonStructure.Columns.Add("UserNames");
                DataTable taskPersonNameGroupByType = GetTaskPersonNameGroupByType(WBSCode);
                foreach (DataRow row in taskPersonStructure.Rows)
                {
                    DataRow[] rowArray = taskPersonNameGroupByType.Select("Type='" + row["Type"].ToString() + "'");
                    if (rowArray.Length > 0)
                    {
                        row["UserCodes"] = rowArray[0]["UserCodes"];
                        row["UserNames"] = rowArray[0]["UserNames"];
                    }
                }
                table3 = taskPersonStructure;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static string GetTaskPersonNameInputor(DataTable tbGroup)
        {
            string taskPersonName;
            try
            {
                taskPersonName = GetTaskPersonName(tbGroup, 2);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return taskPersonName;
        }

        public static string GetTaskPersonNameMaster(DataTable tbGroup)
        {
            string taskPersonName;
            try
            {
                taskPersonName = GetTaskPersonName(tbGroup, 9);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return taskPersonName;
        }

        public static string GetTaskPersonNameMonitor(DataTable tbGroup)
        {
            string taskPersonName;
            try
            {
                taskPersonName = GetTaskPersonName(tbGroup, 1);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return taskPersonName;
        }

        public static string GetTaskPersonNameViewer(DataTable tbGroup)
        {
            string taskPersonName;
            try
            {
                taskPersonName = GetTaskPersonName(tbGroup, 4);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return taskPersonName;
        }

        public static DataTable GetTaskPersonStructure()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Type", typeof(int));
                table.Columns.Add("TypeName");
                table.Columns.Add("SortID");
                for (int i = 0; i <= 9; i++)
                {
                    if (i != 3)
                    {
                        string taskPersonTypeName = GetTaskPersonTypeName(i);
                        if (taskPersonTypeName != "")
                        {
                            DataRow row = table.NewRow();
                            row["Type"] = i;
                            row["TypeName"] = taskPersonTypeName;
                            row["SortID"] = GetTaskPersonTypeSortID(i);
                            table.Rows.Add(row);
                        }
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

        public static string GetTaskPersonTypeName(int type)
        {
            string text2;
            try
            {
                string text = "";
                switch (type)
                {
                    case 0:
                        text = "参与人";
                        break;

                    case 1:
                        text = "监督人";
                        break;

                    case 2:
                        text = "录入人";
                        break;

                    case 3:
                        text = "工作报告分发范围";
                        break;

                    case 4:
                        text = "查看人";
                        break;

                    case 5:
                        text = "工作指示人";
                        break;

                    case 9:
                        text = "负责人";
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

        public static int GetTaskPersonTypeSortID(int type)
        {
            int num2;
            try
            {
                int num = 0;
                switch (type)
                {
                    case 0:
                        num = 2;
                        break;

                    case 1:
                        num = 1;
                        break;

                    case 2:
                        num = 3;
                        break;

                    case 3:
                        num = 9;
                        break;

                    case 4:
                        num = 4;
                        break;

                    case 5:
                        num = 5;
                        break;

                    case 9:
                        num = 0;
                        break;
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int GetTaskStatus(string WBSCode)
        {
            int num2;
            try
            {
                int @int = 0;
                if (WBSCode == "")
                {
                    return @int;
                }
                EntityData taskByCode = WBSDAO.GetTaskByCode(WBSCode);
                if (taskByCode.HasRecord())
                {
                    @int = taskByCode.GetInt("Status");
                }
                taskByCode.Dispose();
                num2 = @int;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetTaskStatusNameByCompletePercent(string CompletePercent)
        {
            if (CompletePercent == "0")
            {
                return "未开始";
            }
            if (CompletePercent == "100")
            {
                return "已完成";
            }
            if ((ConvertRule.ToInt(CompletePercent) < 100) && (ConvertRule.ToInt(CompletePercent) > 0))
            {
                return "进行中";
            }
            return "未知状态";
        }

        public static int GetTaskStatusNumberByCompletePercent(string CompletePercent)
        {
            if (CompletePercent == "0")
            {
                return 0;
            }
            if (CompletePercent == "100")
            {
                return 4;
            }
            if ((ConvertRule.ToInt(CompletePercent) < 100) && (ConvertRule.ToInt(CompletePercent) > 0))
            {
                return 1;
            }
            return 0;
        }

        public static string GetWBSFullCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData taskByCode = WBSDAO.GetTaskByCode(code);
                if (taskByCode.HasRecord())
                {
                    text = taskByCode.GetString("FullCode");
                }
                taskByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetWBSFullName(string WBSCode)
        {
            string text4;
            try
            {
                string text = "";
                string[] textArray = GetWBSFullCode(WBSCode).Split(new char[] { "-"[0] });
                foreach (string text3 in textArray)
                {
                    if (text != "")
                    {
                        text = text + "->";
                    }
                    text = text + GetWBSName(text3);
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static string GetWBSName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData taskByCode = WBSDAO.GetTaskByCode(code);
                if (taskByCode.HasRecord())
                {
                    text = taskByCode.GetString("TaskName");
                }
                taskByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetWBSTaskFullNameTable(string ProjectCode)
        {
            DataTable table2;
            try
            {
                V_WBSTaskStrategyBuilder builder = new V_WBSTaskStrategyBuilder();
                builder.AddStrategy(new Strategy(V_WBSTaskStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder(" FullCode ", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                DataSet set = agent.ExecSqlForDataSet(queryString);
                agent.Dispose();
                DataTable table = set.Tables[0];
                table.Columns.Add("FullName");
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    string text2 = "";
                    string text3 = (string) table.Rows[i]["TaskName"];
                    string text4 = "";
                    string text5 = "";
                    string text6 = (string) table.Rows[i]["WBSCode"];
                    if (!table.Rows[i].IsNull("ParentCode"))
                    {
                        text2 = (string) table.Rows[i]["ParentCode"];
                    }
                    if (text2 != "")
                    {
                        DataRow[] rowArray = table.Select(string.Format("wbsCode='{0}'", text2));
                        if (rowArray.Length > 0)
                        {
                            text5 = (string) rowArray[0]["FullName"];
                            text4 = text5 + " -> " + text3;
                        }
                    }
                    else
                    {
                        text4 = text3;
                    }
                    table.Rows[i]["FullName"] = text4;
                }
                set.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static bool HasChilTask(string WBSCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                if ("" == WBSCode)
                {
                    return false;
                }
                EntityData childTask = WBSDAO.GetChildTask(WBSCode);
                if (childTask.HasRecord())
                {
                    flag = true;
                }
                childTask.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsTaskAccess(string strWBSCode, string strUserCode)
        {
            WBSStrategyBuilder builder = new WBSStrategyBuilder();
            builder.AddStrategy(new Strategy(WBSStrategyName.WBSCode, strWBSCode));
            ArrayList pas = new ArrayList();
            pas.Add("070107");
            pas.Add(strUserCode);
            builder.AddStrategy(new Strategy(WBSStrategyName.AccessRange, pas));
            string queryString = builder.BuildQueryDeskTopString();
            QueryAgent agent = new QueryAgent();
            try
            {
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                if (table.Rows.Count > 0)
                {
                    return true;
                }
            }
            finally
            {
                agent.Dispose();
            }
            return false;
        }

        public static bool IsTaskAccessFullCode(string FullCode, string[] arrHasRightFullCode)
        {
            foreach (string text in arrHasRightFullCode)
            {
                if (FullCode.IndexOf(text) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTaskAccessFullCode(string FullCode, DataTable tbHasRightFullCode)
        {
            foreach (DataRow row in tbHasRightFullCode.Rows)
            {
                string text = ConvertRule.ToString(row["FullCode"]);
                if (FullCode.IndexOf(text) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTaskAccessFullCode(string FullCode, string HasRightFullCodes)
        {
            string[] arrHasRightFullCode = HasRightFullCodes.Split(",".ToCharArray());
            return IsTaskAccessFullCode(FullCode, arrHasRightFullCode);
        }

        public static bool IsTaskExecuteAccess(string strExecuteCode, string strUserCode)
        {
            TaskExecuteStrategyBuilder builder = new TaskExecuteStrategyBuilder();
            ArrayList pas = new ArrayList();
            pas.Add("070202");
            pas.Add(strUserCode);
            builder.AddStrategy(new Strategy(TaskExecuteStrategyName.AccessRange, pas));
            builder.AddStrategy(new Strategy(TaskExecuteStrategyName.TaskExecuteCode, strExecuteCode));
            string queryString = builder.BuildMainQueryString();
            DataSet set = new QueryAgent().ExecSqlForDataSet(queryString);
            return ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0));
        }

        public static bool IsTaskModify(string strWBSCode, string strUserCode)
        {
            WBSStrategyBuilder builder = new WBSStrategyBuilder();
            builder.AddStrategy(new Strategy(WBSStrategyName.WBSCode, strWBSCode));
            ArrayList pas = new ArrayList();
            pas.Add("070107");
            pas.Add(strUserCode);
            pas.Add("1,2,9");
            builder.AddStrategy(new Strategy(WBSStrategyName.AccessRange, pas));
            string queryString = builder.BuildQueryDeskTopString();
            QueryAgent agent = new QueryAgent();
            try
            {
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                if (table.Rows.Count > 0)
                {
                    return true;
                }
            }
            finally
            {
                agent.Dispose();
            }
            return false;
        }

        public static void ResetAllHasRightTask(HttpSessionState Session, string UserCode)
        {
            Session["TaskHasRightFullCode"] = WBSStrategyBuilder.GetHasRightFullCodeArray(UserCode);
        }

        public static void SetTaskIsRight(DataTable tbTask, string RightFieldName, string[] arrHasRightFullCode)
        {
            try
            {
                if (!tbTask.Columns.Contains(RightFieldName))
                {
                    tbTask.Columns.Add(RightFieldName);
                }
                foreach (DataRow row in tbTask.Rows)
                {
                    string fullCode = ConvertRule.ToString(row["FullCode"]);
                    row[RightFieldName] = IsTaskAccessFullCode(fullCode, arrHasRightFullCode) ? 1 : 0;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateParentCompletePercent(string ParentCode)
        {
            try
            {
                int num3;
                string parentCode = "";
                decimal num = 0M;
                decimal num2 = 0M;
                EntityData childTask = WBSDAO.GetChildTask(ParentCode);
                DataTable currentTable = childTask.CurrentTable;
                for (num3 = 0; num3 < currentTable.Rows.Count; num3++)
                {
                    num2 += ConvertRule.ToDecimal(currentTable.Rows[num3]["Proportion"]);
                }
                for (num3 = 0; num3 < currentTable.Rows.Count; num3++)
                {
                    if (0M < num2)
                    {
                        num += (ConvertRule.ToDecimal(currentTable.Rows[num3]["Proportion"]) / num2) * ConvertRule.ToDecimal(currentTable.Rows[num3]["CompletePercent"]);
                    }
                }
                childTask.Dispose();
                EntityData entity = WBSDAO.GetTaskByCode(ParentCode);
                if (entity.HasRecord())
                {
                    entity.CurrentTable.Rows[0]["CompletePercent"] = num;
                    if (num >= 100M)
                    {
                        entity.CurrentTable.Rows[0]["Status"] = 4;
                    }
                    parentCode = entity.CurrentTable.Rows[0]["ParentCode"].ToString();
                }
                WBSDAO.UpdateTask(entity);
                entity.Dispose();
                if ("" != parentCode)
                {
                    UpdateParentCompletePercent(parentCode);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateParentTaskData(string parentcode, string WBSCode)
        {
            try
            {
                EntityData entity = WBSDAO.GetTaskByCode(parentcode);
                EntityData taskByCode = WBSDAO.GetTaskByCode(WBSCode);
                string text = "";
                if (entity.HasRecord() && taskByCode.HasRecord())
                {
                    text = entity.CurrentTable.Rows[0]["ParentCode"].ToString();
                    if (((entity.GetDateTimeOnlyDate("PlannedStartDate") != "") && (taskByCode.GetDateTimeOnlyDate("PlannedStartDate") != "")) && (DateTime.Parse(entity.GetDateTimeOnlyDate("PlannedStartDate")).Subtract(DateTime.Parse(taskByCode.GetDateTimeOnlyDate("PlannedStartDate"))).Days > 0))
                    {
                        entity.CurrentTable.Rows[0]["PlannedStartDate"] = taskByCode.CurrentTable.Rows[0]["PlannedStartDate"];
                    }
                    if ((entity.GetDateTimeOnlyDate("PlannedStartDate") == "") && (taskByCode.GetDateTimeOnlyDate("PlannedStartDate") != ""))
                    {
                        entity.CurrentTable.Rows[0]["PlannedStartDate"] = taskByCode.CurrentTable.Rows[0]["PlannedStartDate"];
                    }
                    if (((entity.GetDateTimeOnlyDate("PlannedFinishDate") != "") && (taskByCode.GetDateTimeOnlyDate("PlannedFinishDate") != "")) && (DateTime.Parse(entity.GetDateTimeOnlyDate("PlannedFinishDate")).Subtract(DateTime.Parse(taskByCode.GetDateTimeOnlyDate("PlannedFinishDate"))).Days < 0))
                    {
                        entity.CurrentTable.Rows[0]["PlannedFinishDate"] = taskByCode.CurrentTable.Rows[0]["PlannedFinishDate"];
                    }
                    if ((entity.GetDateTimeOnlyDate("PlannedFinishDate") == "") && (taskByCode.GetDateTimeOnlyDate("PlannedFinishDate") != ""))
                    {
                        entity.CurrentTable.Rows[0]["PlannedFinishDate"] = taskByCode.CurrentTable.Rows[0]["PlannedFinishDate"];
                    }
                    if (((entity.GetDateTimeOnlyDate("ActualStartDate") != "") && (taskByCode.GetDateTimeOnlyDate("ActualStartDate") != "")) && (DateTime.Parse(entity.GetDateTimeOnlyDate("ActualStartDate")).Subtract(DateTime.Parse(taskByCode.GetDateTimeOnlyDate("ActualStartDate"))).Days > 0))
                    {
                        entity.CurrentTable.Rows[0]["ActualStartDate"] = taskByCode.CurrentTable.Rows[0]["ActualStartDate"];
                    }
                    if ((entity.GetDateTimeOnlyDate("ActualStartDate") == "") && (taskByCode.GetDateTimeOnlyDate("ActualStartDate") != ""))
                    {
                        entity.CurrentTable.Rows[0]["ActualStartDate"] = taskByCode.CurrentTable.Rows[0]["ActualStartDate"];
                    }
                    if (100 <= ConvertRule.ToInt(entity.CurrentRow["CompletePercent"]))
                    {
                        if (((entity.GetDateTimeOnlyDate("ActualFinishDate") != "") && (taskByCode.GetDateTimeOnlyDate("ActualFinishDate") != "")) && (DateTime.Parse(entity.GetDateTimeOnlyDate("ActualFinishDate")).Subtract(DateTime.Parse(taskByCode.GetDateTimeOnlyDate("ActualFinishDate"))).Days < 0))
                        {
                            entity.CurrentTable.Rows[0]["ActualFinishDate"] = taskByCode.CurrentTable.Rows[0]["ActualFinishDate"];
                        }
                        if ((entity.GetDateTimeOnlyDate("ActualFinishDate") == "") && (taskByCode.GetDateTimeOnlyDate("ActualFinishDate") != ""))
                        {
                            entity.CurrentTable.Rows[0]["ActualFinishDate"] = taskByCode.CurrentTable.Rows[0]["ActualFinishDate"];
                        }
                    }
                }
                WBSDAO.UpdateTask(entity);
                taskByCode.Dispose();
                entity.Dispose();
                if ("" != text)
                {
                    UpdateParentTaskData(text, parentcode);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpDateProportion(string strWBSCode, string strPercent)
        {
            string text = WBSDAO.GetTaskByCode(strWBSCode).GetString("ParentCode");
            if (text.Length > 0)
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ParentCode, text));
                string queryString = builder.BuildQueryDeskTopString();
                DataSet set = new QueryAgent().ExecSqlForDataSet(queryString);
                if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                {
                    bool flag = true;
                    double num = 0;
                    DataTable table = set.Tables[0];
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["Proportion"].ToString().Trim().Length == 0)
                        {
                            flag = false;
                        }
                        string text3 = table.Rows[i]["Proportion"].ToString().Trim();
                        string text4 = table.Rows[i]["CompletePercent"].ToString().Trim();
                        num += (double.Parse((text3.Length > 0) ? text3 : "0.0") * double.Parse((text4.Length > 0) ? text4 : "0.0")) / 100;
                    }
                    if (flag)
                    {
                        EntityData entity = WBSDAO.GetTaskByCode(text);
                        entity.CurrentRow["CompletePercent"] = Math.Round((double) (num * 100));
                        WBSDAO.UpdateTask(entity);
                        UpDateProportion(text, num.ToString());
                    }
                }
            }
        }

        public static void UpdateTaskBudgetDateFromTask(string WBSCode)
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
                        object obj2 = ContractRule.CalcContractPayDateByTask(code, contractPayConditionByWBSCode.CurrentTable);
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

        public static void UpdateTaskCompletePercent(string WBSCode, decimal CompletePercent)
        {
            try
            {
                string parentCode = "";
                EntityData entity = WBSDAO.GetTaskByCode(WBSCode);
                if (entity.HasRecord())
                {
                    parentCode = entity.CurrentTable.Rows[0]["ParentCode"].ToString();
                    entity.CurrentTable.Rows[0]["CompletePercent"] = CompletePercent;
                    if (100M <= CompletePercent)
                    {
                        entity.CurrentTable.Rows[0]["Status"] = 4;
                    }
                }
                WBSDAO.UpdateTask(entity);
                entity.Dispose();
                if ("" != parentCode)
                {
                    UpdateParentCompletePercent(parentCode);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskData(string BuildingCode, string WBSCode)
        {
            try
            {
                string parentcode = "";
                EntityData taskByCode = WBSDAO.GetTaskByCode(WBSCode);
                if (taskByCode.HasRecord())
                {
                    parentcode = taskByCode.CurrentTable.Rows[0]["ParentCode"].ToString();
                    EntityData entity = GetChangedDataByConstructProg(BuildingCode, WBSCode);
                    WBSDAO.UpdateTask(entity);
                    entity.Dispose();
                }
                if ("" != parentcode)
                {
                    UpdateParentTaskData(parentcode, WBSCode);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

