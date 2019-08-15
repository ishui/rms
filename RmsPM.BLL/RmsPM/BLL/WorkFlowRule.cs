namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Text;
    using Rms.ORMap;
    using Rms.WorkFlow;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class WorkFlowRule
    {
        private static EntityData _GetRoleUser(WorkCase WorkCaseCase, Task TaskCase, TaskActor TaskActorCase)
        {
            EntityData data2;
            try
            {
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(TaskCase.ProcedureCode, false);
                EntityData data = new EntityData();
                string propertyValue = "";
                string propertyCode = "";
                IDictionaryEnumerator casePropertyEnumerator = WorkCaseCase.GetCasePropertyEnumerator();
                while (casePropertyEnumerator.MoveNext())
                {
                    CaseProperty property = (CaseProperty) casePropertyEnumerator.Value;
                    if (TaskActorCase == null)
                    {
                        propertyCode = TaskCase.TaskProperty;
                    }
                    else
                    {
                        propertyCode = TaskActorCase.ActorProperty;
                    }
                    if (property.ProcedurePropertyCode == propertyCode)
                    {
                        propertyValue = property.ProcedurePropertyValue;
                    }
                }
                string procedurePropertyType = "";
                if (propertyCode != "")
                {
                    procedurePropertyType = procedureDifinition.GetProperty(propertyCode).ProcedurePropertyType;
                }
                string roleCode = "";
                if (TaskActorCase == null)
                {
                    roleCode = TaskCase.TaskRole;
                }
                else
                {
                    roleCode = TaskActorCase.ActorCode;
                }
                DataTable table = GetRoleCompriseUser(procedureDifinition.GetRole(roleCode), procedurePropertyType, propertyValue);
                data.Tables.Add(table);
                data2 = data;
            }
            catch
            {
                throw;
            }
            return data2;
        }

        public static void DeleteWorkFlowProcedure(string procedureCode)
        {
            try
            {
                EntityData entity = WorkFlowDAO.GetStandard_WorkFlowProcedureByCode(procedureCode);
                WorkFlowDAO.DeleteStandard_WorkFlowProcedure(entity);
                entity.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void FillRouterTaskName(DataSet ds)
        {
            DataTable table = ds.Tables["WorkFlowTask"];
            DataTable table2 = ds.Tables["WorkFlowRouter"];
            table2.Columns.Add("FromTaskName", typeof(string));
            table2.Columns.Add("ToTaskName", typeof(string));
            foreach (DataRow row in table2.Rows)
            {
                string text = "";
                string text2 = "";
                string text3 = "";
                string text4 = "";
                if (!row.IsNull("FromTaskCode"))
                {
                    text = (string) row["FromTaskCode"];
                }
                if (!row.IsNull("ToTaskCode"))
                {
                    text2 = (string) row["ToTaskCode"];
                }
                DataRow[] rowArray = table.Select(string.Format(" TaskCode='{0}' ", text));
                if (rowArray.Length > 0)
                {
                    text3 = (string) rowArray[0]["TaskName"];
                }
                rowArray = table.Select(string.Format(" TaskCode='{0}' ", text2));
                if (rowArray.Length > 0)
                {
                    text4 = (string) rowArray[0]["TaskName"];
                }
                row["FromTaskName"] = text3;
                row["ToTaskName"] = text4;
            }
        }

        public static int GetBeginCaseCountByProcedureNameAndApplicationCode(string ProcedureName, string ApplicationCode)
        {
            int count = 0;
            WorkFlowCaseStrategyBuilder builder = new WorkFlowCaseStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ProcedureCodeIn, GetProcedureCodeListByName(ProcedureName)));
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ApplicationCode, ApplicationCode));
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.Status, "'Begin'"));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowCase", queryString);
            count = data.CurrentTable.Rows.Count;
            data.Dispose();
            agent.Dispose();
            return count;
        }

        public static string GetCaseCodeByApplicationCode(string pm_sApplicationCode, string pm_sApplicationType)
        {
            return pm_sApplicationCode;
        }

        public static string GetCaseCodeByProcedureNameAndApplicationCode(string ProcedureName, string ApplicationCode)
        {
            string text = "";
            WorkFlowCaseStrategyBuilder builder = new WorkFlowCaseStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ProcedureCodeIn, GetProcedureCodeListByName(ProcedureName)));
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ApplicationCode, ApplicationCode));
            builder.AddOrder("CaseCode", false);
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowCase", queryString);
            if (data.HasRecord())
            {
                text = data.GetString("CaseCode");
            }
            data.Dispose();
            agent.Dispose();
            return text;
        }

        public static int GetCaseCountByProcedureCode(string ProcedureCode)
        {
            int count = 0;
            WorkFlowCaseStrategyBuilder builder = new WorkFlowCaseStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ProcedureCode, ProcedureCode));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowCase", queryString);
            count = data.CurrentTable.Rows.Count;
            data.Dispose();
            agent.Dispose();
            return count;
        }

        public static int GetCaseCountByProcedureNameAndApplicationCode(string ProcedureName, string ApplicationCode)
        {
            int count = 0;
            WorkFlowCaseStrategyBuilder builder = new WorkFlowCaseStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ProcedureCodeIn, GetProcedureCodeListByName(ProcedureName)));
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ApplicationCode, ApplicationCode));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowCase", queryString);
            count = data.CurrentTable.Rows.Count;
            data.Dispose();
            agent.Dispose();
            return count;
        }

        public static bool GetCheckExecdableDate(DateTime DateValue1, DateTime DateValue2)
        {
            TimeSpan span;
            string s = ConfigurationSettings.AppSettings["FlowTime"];
            if (s == "")
            {
                return false;
            }
            if (DateValue2 == DateTime.Parse("1900-1-1"))
            {
                span = (TimeSpan) (DateTime.Now - DateValue1);
            }
            else
            {
                span = (TimeSpan) (DateValue2 - DateValue1);
            }
            return (span.TotalHours > int.Parse(s));
        }

        public static int GetEndCaseCountByProcedureNameAndApplicationCode(string ProcedureName, string ApplicationCode)
        {
            int count = 0;
            WorkFlowCaseStrategyBuilder builder = new WorkFlowCaseStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ProcedureCodeIn, GetProcedureCodeListByName(ProcedureName)));
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ApplicationCode, ApplicationCode));
            builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.Status, "'End'"));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowCase", queryString);
            count = data.CurrentTable.Rows.Count;
            data.Dispose();
            agent.Dispose();
            return count;
        }

        public static string GetFormatExcedableDate(DateTime SendDate)
        {
            return GetFormatExcedableDate(SendDate, false);
        }

        public static string GetFormatExcedableDate(DateTime SendDate, bool ShortDateString)
        {
            DateTimeFormatInfo provider = new DateTimeFormatInfo();
            if (ShortDateString)
            {
                provider.LongDatePattern = "yyyy-MM-dd";
            }
            else
            {
                provider.LongDatePattern = "yyyy-MM-dd HH:mm";
            }
            if (GetCheckExecdableDate(SendDate, DateTime.Parse("1900-1-1")))
            {
                return ("<font color=\"red\">" + SendDate.ToString("D", provider) + "</font>");
            }
            return SendDate.ToString("D", provider);
        }

        public static string GetFormatExcedableDate(DateTime SendDate, string EndDate)
        {
            if (EndDate == "")
            {
                return "";
            }
            DateTimeFormatInfo provider = new DateTimeFormatInfo();
            provider.LongDatePattern = "yyyy-MM-dd HH:mm";
            if (GetCheckExecdableDate(SendDate, DateTime.Parse(EndDate)))
            {
                return ("<font color=\"red\">" + DateTime.Parse(EndDate).ToString("D", provider) + "</font>");
            }
            return DateTime.Parse(EndDate).ToString("D", provider);
        }

        public static string GetLoginUserLastActCode(string UserCode, string CaseCode)
        {
            string text = "";
            WorkFlowActStrategyBuilder builder = new WorkFlowActStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowActStrategyName.ActUserCode, UserCode));
            builder.AddStrategy(new Strategy(WorkFlowActStrategyName.CaseCode, CaseCode));
            string queryString = builder.BuildMainQueryString() + " order by FinishDate desc ";
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowAct", queryString);
            if (data.HasRecord())
            {
                text = data.CurrentRow["ActCode"].ToString();
            }
            data.Dispose();
            agent.Dispose();
            if (text == "")
            {
                Task firstTask = DefinitionManager.GetFirstTask(DefinitionManager.GetProcedureDifinition(WorkCaseManager.GetWorkCase(CaseCode).ProcedureCode, true));
                builder = new WorkFlowActStrategyBuilder();
                builder.AddStrategy(new Strategy(WorkFlowActStrategyName.CurrentTaskCode, firstTask.TaskCode));
                builder.AddStrategy(new Strategy(WorkFlowActStrategyName.CaseCode, CaseCode));
                queryString = builder.BuildMainQueryString() + " order by FinishDate ";
                agent = new QueryAgent();
                data = agent.FillEntityData("WorkFlowAct", queryString);
                if (data.HasRecord())
                {
                    text = data.CurrentRow["ActCode"].ToString();
                }
                data.Dispose();
                agent.Dispose();
            }
            return text;
        }

        public static string GetProcedureCodeByName(string procedureName)
        {
            return GetProcedureCodeByName(procedureName, "");
        }

        public static string GetProcedureCodeByName(string ProcedureName, string ProjectCode)
        {
            string text = "";
            try
            {
                WorkFlowProcedureStrategyBuilder builder = new WorkFlowProcedureStrategyBuilder();
                builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.ProcedureName, ProcedureName));
                if (ProjectCode == null)
                {
                    ProjectCode = "";
                }
                builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Activity, "1"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("WorkFlowProcedure", queryString);
                if (data.HasRecord())
                {
                    text = data.CurrentRow["ProcedureCode"].ToString();
                }
                if (text == "")
                {
                    builder.ClearStrategy();
                    builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.ProcedureName, ProcedureName));
                    builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.ProjectCode, ""));
                    builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Activity, "1"));
                    queryString = builder.BuildMainQueryString();
                    data = agent.FillEntityData("WorkFlowProcedure", queryString);
                    if (data.HasRecord())
                    {
                        text = data.CurrentRow["ProcedureCode"].ToString();
                    }
                }
                data.Dispose();
                agent.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureCodeListByName(string ProcedureName)
        {
            string text = "'0',";
            try
            {
                EntityData workFlowProcedureByName = WorkFlowDAO.GetWorkFlowProcedureByName(ProcedureName);
                foreach (DataRow row in workFlowProcedureByName.CurrentTable.Select())
                {
                    text = text + "'" + row["ProcedureCode"].ToString() + "',";
                }
                workFlowProcedureByName.Dispose();
            }
            catch
            {
                throw;
            }
            if (text.Length > 0)
            {
                return text.Remove(text.Length - 1, 1);
            }
            return text;
        }

        public static string GetProcedureNameByCode(string pm_sProcedureCode)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByCode = WorkFlowDAO.GetWorkFlowProcedureByCode(pm_sProcedureCode);
                if (workFlowProcedureByCode.HasRecord())
                {
                    text = workFlowProcedureByCode.GetString("ProcedureName");
                }
                workFlowProcedureByCode.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureNumberByName(string procedureName)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByName = WorkFlowDAO.GetWorkFlowProcedureByName(procedureName);
                if (workFlowProcedureByName.HasRecord())
                {
                    text = workFlowProcedureByName.GetString("Remark");
                }
                workFlowProcedureByName.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureSourceURLByCode(string procedureCode)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByCode = WorkFlowDAO.GetWorkFlowProcedureByCode(procedureCode);
                if (workFlowProcedureByCode.HasRecord())
                {
                    text = workFlowProcedureByCode.GetString("ApplicationInfoPath");
                }
                workFlowProcedureByCode.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureSourceURLByName(string procedureName)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByName = WorkFlowDAO.GetWorkFlowProcedureByName(procedureName);
                if (workFlowProcedureByName.HasRecord())
                {
                    text = workFlowProcedureByName.GetString("ApplicationInfoPath");
                }
                workFlowProcedureByName.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureTypeNameByValue(string Value)
        {
            switch (Value)
            {
                case "":
                    return "常规类型";

                case "1":
                    return "通用类型";
            }
            return "常规类型";
        }

        public static string GetProcedureURLByCode(string pm_sProcedureCode)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByCode = WorkFlowDAO.GetWorkFlowProcedureByCode(pm_sProcedureCode);
                if (workFlowProcedureByCode.HasRecord())
                {
                    text = workFlowProcedureByCode.GetString("ApplicationPath");
                }
                workFlowProcedureByCode.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureURLByName(string procedureName)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByName = WorkFlowDAO.GetWorkFlowProcedureByName(procedureName);
                if (workFlowProcedureByName.HasRecord())
                {
                    text = workFlowProcedureByName.GetString("ApplicationPath");
                }
                workFlowProcedureByName.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureVersionByCode(string pm_sProcedureCode)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByCode = WorkFlowDAO.GetWorkFlowProcedureByCode(pm_sProcedureCode);
                if (workFlowProcedureByCode.HasRecord())
                {
                    text = workFlowProcedureByCode.GetDecimal("VersionNumber").ToString();
                }
                workFlowProcedureByCode.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static string GetProcedureVersionDescriptionByCode(string pm_sProcedureCode)
        {
            string text = "";
            try
            {
                EntityData workFlowProcedureByCode = WorkFlowDAO.GetWorkFlowProcedureByCode(pm_sProcedureCode);
                if (workFlowProcedureByCode.HasRecord())
                {
                    text = workFlowProcedureByCode.GetString("VersionDescription").ToString();
                }
                workFlowProcedureByCode.Dispose();
            }
            catch
            {
                throw;
            }
            return text;
        }

        public static DataTable GetPropertyTable(WorkCase workCase, Procedure procedure)
        {
            CaseProperty property;
            DataTable table = new DataTable();
            IDictionaryEnumerator casePropertyEnumerator = workCase.GetCasePropertyEnumerator();
            while (casePropertyEnumerator.MoveNext())
            {
                property = (CaseProperty) casePropertyEnumerator.Value;
                Property property2 = procedure.GetProperty(property.ProcedurePropertyCode);
                if (property2.ProcedurePropertyType == "Int")
                {
                    table.Columns.Add(property2.ProcedurePropertyName, Type.GetType("System.Decimal"));
                }
                else
                {
                    table.Columns.Add(property2.ProcedurePropertyName);
                }
            }
            casePropertyEnumerator.Reset();
            while (casePropertyEnumerator.MoveNext())
            {
                string procedurePropertyName;
                property = (CaseProperty) casePropertyEnumerator.Value;
                if (table.Rows.Count == 0)
                {
                    DataRow row = table.NewRow();
                    procedurePropertyName = procedure.GetProperty(property.ProcedurePropertyCode).ProcedurePropertyName;
                    if (table.Columns[procedurePropertyName].DataType.ToString() == "System.Decimal")
                    {
                        if (MathRule.IsNum(property.ProcedurePropertyValue))
                        {
                            row[procedurePropertyName] = property.ProcedurePropertyValue;
                        }
                        else
                        {
                            row[procedurePropertyName] = 0;
                        }
                    }
                    else
                    {
                        row[procedurePropertyName] = property.ProcedurePropertyValue;
                    }
                    table.Rows.Add(row);
                }
                else
                {
                    procedurePropertyName = procedure.GetProperty(property.ProcedurePropertyCode).ProcedurePropertyName;
                    if (table.Columns[procedurePropertyName].DataType.ToString() == "System.Decimal")
                    {
                        if (MathRule.IsNum(property.ProcedurePropertyValue))
                        {
                            table.Rows[0][procedurePropertyName] = property.ProcedurePropertyValue;
                        }
                        else
                        {
                            table.Rows[0][procedurePropertyName] = 0;
                        }
                    }
                    else
                    {
                        table.Rows[0][procedurePropertyName] = property.ProcedurePropertyValue;
                    }
                }
            }
            return table;
        }

        public static DataTable GetRoleCompriseUser(Role RoleCase, string ProcedurePropertyType, string PropertyValue)
        {
            DataTable userDataTable = new DataTable();
            DataColumn column = new DataColumn();
            column.ColumnName = "UserName";
            column.DataType = Type.GetType("System.String");
            DataColumn column2 = new DataColumn();
            column2.ColumnName = "UserCode";
            column2.DataType = Type.GetType("System.String");
            DataColumn column3 = new DataColumn();
            column3.ColumnName = "ShortUserName";
            column3.DataType = Type.GetType("System.String");
            userDataTable.Columns.Add(column3);
            userDataTable.Columns.Add(column);
            userDataTable.Columns.Add(column2);
            IDictionaryEnumerator roleCompriseEnumerator = RoleCase.GetRoleCompriseEnumerator(RoleCase.WorkFlowRoleCode);
            while (roleCompriseEnumerator.MoveNext())
            {
                RoleComprise comprise = (RoleComprise) roleCompriseEnumerator.Value;
                switch (comprise.RoleType)
                {
                    case RoleType.Unit:
                        UnitFilter(ProcedurePropertyType, PropertyValue, comprise.RoleCompriseItem, userDataTable);
                        break;

                    case RoleType.Station:
                        StationFilter(ProcedurePropertyType, PropertyValue, comprise.RoleCompriseItem, userDataTable);
                        break;

                    case RoleType.Porson:
                        PersonFilter(ProcedurePropertyType, PropertyValue, comprise.RoleCompriseItem, userDataTable);
                        break;
                }
            }
            return userDataTable;
        }

        public static EntityData GetRoleUser(WorkCase WorkCaseCase, Task TaskCase)
        {
            return _GetRoleUser(WorkCaseCase, TaskCase, null);
        }

        public static EntityData GetRoleUser(WorkCase WorkCaseCase, TaskActor TaskActorCase)
        {
            EntityData data2;
            try
            {
                Procedure procedureDifinition = DefinitionManager.GetProcedureDifinition(WorkCaseCase.ProcedureCode, false);
                EntityData data = new EntityData();
                string propertyValue = "";
                IDictionaryEnumerator casePropertyEnumerator = WorkCaseCase.GetCasePropertyEnumerator();
                while (casePropertyEnumerator.MoveNext())
                {
                    CaseProperty property = (CaseProperty) casePropertyEnumerator.Value;
                    if (property.ProcedurePropertyCode == TaskActorCase.ActorProperty)
                    {
                        propertyValue = property.ProcedurePropertyValue;
                    }
                }
                string procedurePropertyType = "";
                if (TaskActorCase.ActorProperty != "")
                {
                    procedurePropertyType = procedureDifinition.GetProperty(TaskActorCase.ActorProperty).ProcedurePropertyType;
                }
                DataTable userDataTable = new DataTable();
                DataColumn column = new DataColumn();
                column.ColumnName = "UserName";
                column.DataType = Type.GetType("System.String");
                DataColumn column2 = new DataColumn();
                column2.ColumnName = "UserCode";
                column2.DataType = Type.GetType("System.String");
                DataColumn column3 = new DataColumn();
                column3.ColumnName = "ShortUserName";
                column3.DataType = Type.GetType("System.String");
                userDataTable.Columns.Add(column3);
                userDataTable.Columns.Add(column);
                userDataTable.Columns.Add(column2);
                IDictionaryEnumerator roleCompriseEnumerator = procedureDifinition.GetRole(TaskActorCase.ActorCode).GetRoleCompriseEnumerator(TaskActorCase.ActorCode);
                while (roleCompriseEnumerator.MoveNext())
                {
                    RoleComprise comprise = (RoleComprise) roleCompriseEnumerator.Value;
                    switch (comprise.RoleType)
                    {
                        case RoleType.Unit:
                            UnitFilter(procedurePropertyType, propertyValue, comprise.RoleCompriseItem, userDataTable);
                            break;

                        case RoleType.Station:
                            StationFilter(procedurePropertyType, propertyValue, comprise.RoleCompriseItem, userDataTable);
                            break;

                        case RoleType.Porson:
                            PersonFilter(procedurePropertyType, propertyValue, comprise.RoleCompriseItem, userDataTable);
                            break;
                    }
                }
                data.Tables.Add(userDataTable);
                data2 = data;
            }
            catch
            {
                throw;
            }
            return data2;
        }

        public static EntityData GetRoleUser(WorkCase WorkCaseCase, Task TaskCase, TaskActor TaskActorCase)
        {
            return _GetRoleUser(WorkCaseCase, TaskCase, TaskActorCase);
        }

        public static string GetTaskOpinionTypeByActCode(string actCode)
        {
            string opinionType = "";
            string taskActorCode = "";
            string taskCode = "";
            string procedureCode = "";
            WorkFlowActStrategyBuilder builder = new WorkFlowActStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowActStrategyName.ActCode, actCode));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowAct", queryString);
            procedureCode = data.CurrentRow["ProcedureCode"].ToString();
            taskCode = data.CurrentRow["ToTaskCode"].ToString();
            taskActorCode = data.CurrentRow["TaskActorID"].ToString();
            agent.Dispose();
            data.Dispose();
            Task task = DefinitionManager.GetProcedureDifinition(procedureCode, true).GetTask(taskCode);
            opinionType = task.OpinionType;
            if ((taskActorCode != "") && (task.GetTaskActor(taskActorCode) != null))
            {
                opinionType = task.GetTaskActor(taskActorCode).OpinionType;
            }
            return opinionType;
        }

        public static EntityData GetTaskUser(WorkCase workCase, Task task, string currentActCode)
        {
            EntityData data2;
            try
            {
                IDictionaryEnumerator taskActorEnumerator;
                ArrayList list;
                string text;
                string text2;
                TaskActor actor;
                ArrayList pas;
                EntityData data = new EntityData("SystemUser");
                UserStrategyBuilder builder = new UserStrategyBuilder();
                QueryAgent agent = new QueryAgent();
                string taskActorType = task.TaskActorType;
                if (taskActorType != null)
                {
                    if (taskActorType != "")
                    {
                        if (taskActorType != "*")
                        {
                            if (taskActorType == "**")
                            {
                                goto Label_0237;
                            }
                            if (taskActorType == "***")
                            {
                                goto Label_0266;
                            }
                            if (taskActorType == "****")
                            {
                                goto Label_0432;
                            }
                        }
                    }
                    else if (task.m_TaskActors.Count > 0)
                    {
                        taskActorEnumerator = task.GetTaskActorEnumerator();
                        list = new ArrayList();
                        text = "";
                        text2 = "";
                        while (taskActorEnumerator.MoveNext())
                        {
                            actor = (TaskActor) taskActorEnumerator.Value;
                            if (actor.ActorType == 0)
                            {
                                text2 = text2 + "'" + actor.ActorCode + "',";
                            }
                            else
                            {
                                text = text + "'" + actor.ActorCode + "',";
                            }
                        }
                        if (text2 != "")
                        {
                            text2 = text2.Substring(0, text2.Length - 1);
                        }
                        if (text != "")
                        {
                            text = text.Substring(0, text.Length - 1);
                        }
                        if ((text2 != "") && (text != ""))
                        {
                            pas = new ArrayList();
                            pas.Add(text2);
                            pas.Add(text);
                            builder.AddStrategy(new Strategy(UserStrategyName.UsersAndStations, pas));
                        }
                        else if (text2 != "")
                        {
                            builder.AddStrategy(new Strategy(UserStrategyName.UserCodes, text2));
                        }
                        else if (text != "")
                        {
                            builder.AddStrategy(new Strategy(UserStrategyName.StationCodes, text));
                        }
                        data = agent.FillEntityData("SystemUser", builder.BuildMainQueryString());
                    }
                }
                goto Label_0630;
            Label_0237:
                builder.AddStrategy(new Strategy(UserStrategyName.UserCode, workCase.SourceUserCode));
                data = agent.FillEntityData("SystemUser", builder.BuildMainQueryString());
                goto Label_0630;
            Label_0266:
                if (task.m_TaskActors.Count > 0)
                {
                    taskActorEnumerator = task.GetTaskActorEnumerator();
                    list = new ArrayList();
                    text = "";
                    text2 = "";
                    while (taskActorEnumerator.MoveNext())
                    {
                        actor = (TaskActor) taskActorEnumerator.Value;
                        if (actor.ActorType == 0)
                        {
                            text2 = text2 + "'" + actor.ActorCode + "',";
                        }
                        else
                        {
                            text = text + "'" + actor.ActorCode + "',";
                        }
                    }
                    if (text2 != "")
                    {
                        text2 = text2.Substring(0, text2.Length - 1);
                    }
                    if (text != "")
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                    if ((text2 != "") && (text != ""))
                    {
                        pas = new ArrayList();
                        pas.Add(text2);
                        pas.Add(text);
                        builder.AddStrategy(new Strategy(UserStrategyName.UsersAndStations, pas));
                    }
                    else if (text2 != "")
                    {
                        builder.AddStrategy(new Strategy(UserStrategyName.UserCodes, text2));
                    }
                    else if (text != "")
                    {
                        builder.AddStrategy(new Strategy(UserStrategyName.StationCodes, text));
                    }
                    builder.AddStrategy(new Strategy(UserStrategyName.UnitCode, workCase.SourceUnitCode));
                    data = agent.FillEntityData("SystemUser", builder.BuildMainQueryString());
                }
                goto Label_0630;
            Label_0432:
                if (task.m_TaskActors.Count > 0)
                {
                    taskActorEnumerator = task.GetTaskActorEnumerator();
                    list = new ArrayList();
                    text = "";
                    text2 = "";
                    while (taskActorEnumerator.MoveNext())
                    {
                        actor = (TaskActor) taskActorEnumerator.Value;
                        if (actor.ActorType == 0)
                        {
                            text2 = text2 + "'" + actor.ActorCode + "',";
                        }
                        else
                        {
                            text = text + "'" + actor.ActorCode + "',";
                        }
                    }
                    if (text2 != "")
                    {
                        text2 = text2.Substring(0, text2.Length - 1);
                    }
                    if (text != "")
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                    if ((text2 != "") && (text != ""))
                    {
                        pas = new ArrayList();
                        pas.Add(text2);
                        pas.Add(text);
                        builder.AddStrategy(new Strategy(UserStrategyName.UsersAndStations, pas));
                    }
                    else if (text2 != "")
                    {
                        builder.AddStrategy(new Strategy(UserStrategyName.UserCodes, text2));
                    }
                    else if (text != "")
                    {
                        builder.AddStrategy(new Strategy(UserStrategyName.StationCodes, text));
                    }
                    if (currentActCode != "")
                    {
                        string fromUnitCode = workCase.GetAct(currentActCode).FromUnitCode;
                        if (fromUnitCode != "")
                        {
                            builder.AddStrategy(new Strategy(UserStrategyName.UnitCode, fromUnitCode));
                        }
                    }
                    data = agent.FillEntityData("SystemUser", builder.BuildMainQueryString());
                }
            Label_0630:
                agent.Dispose();
                data2 = data;
            }
            catch
            {
                throw;
            }
            return data2;
        }

        public static WorkCase GetWorkCaseByApplicationCode(string procedureName, string applicationCode)
        {
            WorkCase workCase;
            try
            {
                workCase = WorkCaseManager.GetWorkCase(GetWorkCaseCodeByApplicationCode(procedureName, applicationCode));
            }
            catch
            {
                throw;
            }
            return workCase;
        }

        public static string GetWorkCaseCodeByApplicationCode(string procedureName, string applicationCode)
        {
            string text2;
            try
            {
                WorkFlowCaseStrategyBuilder builder = new WorkFlowCaseStrategyBuilder();
                builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ProcedureName, procedureName));
                builder.AddStrategy(new Strategy(WorkFlowCaseStrategyName.ApplicationCode, applicationCode));
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("WorkFlowCase", builder.BuildMainQueryString());
                agent.Dispose();
                string text = "";
                if (data.HasRecord())
                {
                    text = data.GetString("CaseCode");
                }
                text2 = text;
            }
            catch
            {
                throw;
            }
            return text2;
        }

        public static string GetWorkFlowCaseActUserEmails(string CaseCode)
        {
            string text = "";
            WorkFlowActStrategyBuilder builder = new WorkFlowActStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowActStrategyName.CaseCode, CaseCode));
            string queryString = builder.BuildMainQueryString();
            EntityData data = new QueryAgent().FillEntityData("WorkFlowAct", queryString);
            for (int i = 0; i < data.CurrentTable.Rows.Count; i++)
            {
                string text3 = data.CurrentTable.Rows[i]["ActUserCode"].ToString();
                if (text.IndexOf(text3) == -1)
                {
                    text = text + ";" + SystemRule.GetUserMailByCode(text3);
                }
            }
            data.Dispose();
            return text.Remove(text.Length - 1, 1);
        }

        public static string GetWorkFlowCaseProjectName(string CaseCode)
        {
            return ProjectRule.GetProjectShortName(GetWorkFlowPropertyValuebyName(CaseCode, "项目代码"));
        }

        public static string GetWorkFlowCaseTime(string CaseCode)
        {
            return GetWorkFlowPropertyValuebyName(CaseCode, "完成时间");
        }

        public static string GetWorkFlowCaseTitle(string CaseCode)
        {
            return GetWorkFlowPropertyValuebyName(CaseCode, "主题");
        }

        public static DataTable GetWorkFlowCommons(string Type)
        {
            WorkFlowProcedureStrategyBuilder builder = new WorkFlowProcedureStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Type, Type));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            DataTable currentTable = agent.FillEntityData("WorkFlowProcedure", queryString).CurrentTable;
            agent.Dispose();
            return currentTable;
        }

        public static DataTable GetWorkFlowCommons(string Type, string Activity)
        {
            WorkFlowProcedureStrategyBuilder builder = new WorkFlowProcedureStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Type, Type));
            builder.AddStrategy(new Strategy(WorkFlowProcedureStrategyName.Activity, Activity));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            DataTable currentTable = agent.FillEntityData("WorkFlowProcedure", queryString).CurrentTable;
            agent.Dispose();
            return currentTable;
        }

        public static string GetWorkFlowCommonStatusName(string pm_sStatus)
        {
            string text = "";
            string text3 = pm_sStatus;
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
                        return "审核中";
                    }
                    if (text3 != "3")
                    {
                        return text;
                    }
                    return "作废";
                }
            }
            else
            {
                return "申请";
            }
            return "已审";
        }

        public static string GetWorkFlowHandmade(string CaseCode)
        {
            return GetWorkFlowPropertyValuebyName(CaseCode, "手送资料");
        }

        public static string GetWorkFlowNumber(string CaseCode)
        {
            string workFlowPropertyValuebyName = GetWorkFlowPropertyValuebyName(CaseCode, "流水号");
            if (workFlowPropertyValuebyName == "")
            {
                workFlowPropertyValuebyName = CaseCode;
            }
            return workFlowPropertyValuebyName;
        }

        public static string GetWorkflowOpinionTextByActCode(string CaseCode, string ActCode)
        {
            IDictionaryEnumerator opinionEnumerator = WorkCaseManager.GetWorkCase(CaseCode).GetOpinionEnumerator();
            while (opinionEnumerator.MoveNext())
            {
                Opinion opinion = (Opinion) opinionEnumerator.Value;
                if (opinion.ApplicationCode == ActCode)
                {
                    return opinion.OpinionText;
                }
            }
            return "";
        }

        public static string GetWorkFlowPropertyValuebyName(string CaseCode, string PropertyName)
        {
            if (CaseCode == "")
            {
                return "";
            }
            WorkCase workCase = WorkCaseManager.GetWorkCase(CaseCode);
            string text = "";
            string text2 = "";
            WorkFlowProcedurePropertyStrategyBuilder builder = new WorkFlowProcedurePropertyStrategyBuilder();
            builder.AddStrategy(new Strategy(WorkFlowProcedurePropertyStrategyName.ProcedureCode, workCase.ProcedureCode));
            builder.AddStrategy(new Strategy(WorkFlowProcedurePropertyStrategyName.ProcedurePropertyName, PropertyName));
            string queryString = builder.BuildMainQueryString();
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("WorkFlowProcedureProperty", queryString);
            if (data.HasRecord())
            {
                text = data.GetString("WorkFlowProcedurePropertyCode");
            }
            data.Dispose();
            agent.Dispose();
            if (text != "")
            {
                WorkFlowCasePropertyStrategyBuilder builder2 = new WorkFlowCasePropertyStrategyBuilder();
                builder2.AddStrategy(new Strategy(WorkFlowCasePropertyStrategyName.WorkFlowCaseCode, workCase.CaseCode));
                builder2.AddStrategy(new Strategy(WorkFlowCasePropertyStrategyName.WorkFlowProcedurePropertyCode, text));
                queryString = builder2.BuildMainQueryString();
                EntityData data2 = new QueryAgent().FillEntityData("WorkFlowCaseProperty", queryString);
                if (data2.HasRecord())
                {
                    text2 = data2.GetString("WorkFlowProcedurePropertyValue");
                }
                data2.Dispose();
            }
            return text2;
        }

        public static string GetWorkFlowRate(string CaseCode)
        {
            return GetWorkFlowPropertyValuebyName(CaseCode, "工作缓急");
        }

        private static void InputRow(DataTable sourceTable, DataTable targetTable)
        {
            foreach (DataRow row in sourceTable.Rows)
            {
                targetTable.ImportRow(row);
            }
        }

        private static void PersonFilter(string ProcedurePropertyType, string PropertyValue, string RoleCompriseItem, DataTable UserDataTable)
        {
            DataRow row;
            if ((ProcedurePropertyType == "") || (PropertyValue == ""))
            {
                row = UserDataTable.NewRow();
                row["UserCode"] = RoleCompriseItem;
                row["UserName"] = SystemRule.GetUserName(RoleCompriseItem);
                row["ShortUserName"] = SystemRule.GetShortUserName(RoleCompriseItem);
                UserDataTable.Rows.Add(row);
            }
            else
            {
                switch (ProcedurePropertyType)
                {
                    case "Station":
                    {
                        string[] textArray = SystemRule.GetStationByUserCode(RoleCompriseItem).Split(new char[] { ',' });
                        foreach (string text in textArray)
                        {
                            if (PropertyValue == text)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = RoleCompriseItem;
                                row["UserName"] = SystemRule.GetUserName(RoleCompriseItem);
                                row["ShortUserName"] = SystemRule.GetShortUserName(RoleCompriseItem);
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;
                    }
                    case "Porson":
                    {
                        string[] textArray2 = RoleCompriseItem.Split(new char[] { ',' });
                        foreach (string text2 in textArray2)
                        {
                            if (text2 == PropertyValue)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = RoleCompriseItem;
                                row["UserName"] = SystemRule.GetUserName(RoleCompriseItem);
                                row["ShortUserName"] = SystemRule.GetShortUserName(RoleCompriseItem);
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;
                    }
                    case "Unit":
                    {
                        EntityData unitByUserCode = SystemManageDAO.GetUnitByUserCode(RoleCompriseItem);
                        for (int i = 0; i < unitByUserCode.CurrentTable.Rows.Count; i++)
                        {
                            if (("-" + SystemRule.GetUnitFullCode(unitByUserCode.CurrentTable.Rows[i]["UnitCode"].ToString()) + "-").IndexOf("-" + PropertyValue + "-") != -1)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = RoleCompriseItem;
                                row["UserName"] = SystemRule.GetUserName(RoleCompriseItem);
                                row["ShortUserName"] = SystemRule.GetShortUserName(RoleCompriseItem);
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static void SaveCaseProperty(CaseProperty CasePropertyCase)
        {
            try
            {
                DataRow newRecord;
                bool flag = false;
                EntityData entity = WorkFlowDAO.GetWorkFlowCasePropertyByCode(CasePropertyCase.WorkFlowCasePropertyCode);
                if (CasePropertyCase.WorkFlowCasePropertyCode == "")
                {
                    flag = true;
                    CasePropertyCase.WorkFlowCasePropertyCode = SystemManageDAO.GetNewSysCode("WorkFlowCaseProperty");
                    newRecord = entity.GetNewRecord();
                }
                else
                {
                    newRecord = entity.CurrentRow;
                }
                newRecord["WorkFlowCasePropertyCode"] = CasePropertyCase.WorkFlowCasePropertyCode;
                newRecord["WorkFlowCaseCode"] = CasePropertyCase.CaseCode;
                newRecord["WorkFlowProcedurePropertyCode"] = CasePropertyCase.ProcedurePropertyCode;
                newRecord["WorkFlowProcedurePropertyValue"] = CasePropertyCase.ProcedurePropertyValue;
                newRecord["Remak"] = "";
                if (flag)
                {
                    entity.AddNewRecord(newRecord);
                }
                WorkFlowDAO.SubmitAllWorkFlowCaseProperty(entity);
                entity.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void SaveWorkFlowCase(WorkCase workCase)
        {
            try
            {
                DataSet ds = WorkCaseManager.SaveWorkCaseData(workCase);
                EntityData entity = SaveWorkFlowCaseData(ds, workCase.CaseCode);
                ds.Dispose();
                WorkFlowDAO.SubmitAllStandard_WorkFlowCase(entity);
                entity.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void SaveWorkFlowCase(DataSet ds, string caseCode)
        {
            try
            {
                EntityData entity = SaveWorkFlowCaseData(ds, caseCode);
                WorkFlowDAO.SubmitAllStandard_WorkFlowCase(entity);
                entity.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static EntityData SaveWorkFlowCaseData(DataSet ds, string CaseCode)
        {
            EntityData data2;
            try
            {
                EntityData data = new EntityData("Standard_WorkFlowCase");
                InputRow(ds.Tables["WorkFlowCase"], data.Tables["WorkFlowCase"]);
                InputRow(ds.Tables["WorkFlowAct"], data.Tables["WorkFlowAct"]);
                InputRow(ds.Tables["WorkFlowActUser"], data.Tables["WorkFlowActUser"]);
                InputRow(ds.Tables["WorkFlowDataPackage"], data.Tables["WorkFlowDataPackage"]);
                InputRow(ds.Tables["WorkFlowOpinion"], data.Tables["WorkFlowOpinion"]);
                InputRow(ds.Tables["WorkFlowCaseProperty"], data.Tables["WorkFlowCaseProperty"]);
                data2 = data;
            }
            catch
            {
                throw;
            }
            return data2;
        }

        public static void SaveWorkFlowProcedure(DataSet ds, string procedureCode)
        {
            try
            {
                EntityData entitydata = WorkFlowDAO.GetStandard_WorkFlowProcedureByCode(procedureCode);
                EntityData data2 = new EntityData("Standard_WorkFlowProcedure");
                InputRow(ds.Tables["WorkFlowProcedure"], data2.Tables["WorkFlowProcedure"]);
                InputRow(ds.Tables["WorkFlowTask"], data2.Tables["WorkFlowTask"]);
                InputRow(ds.Tables["WorkFlowRouter"], data2.Tables["WorkFlowRouter"]);
                InputRow(ds.Tables["WorkFlowTaskActor"], data2.Tables["WorkFlowTaskActor"]);
                InputRow(ds.Tables["WorkFlowCondition"], data2.Tables["WorkFlowCondition"]);
                InputRow(ds.Tables["WorkFlowRole"], data2.Tables["WorkFlowRole"]);
                InputRow(ds.Tables["WorkFlowRoleComprise"], data2.Tables["WorkFlowRoleComprise"]);
                InputRow(ds.Tables["WorkFlowProcedureProperty"], data2.Tables["WorkFlowProcedureProperty"]);
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WorkFlowProcedure"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.DeleteAllRow(entitydata);
                        ydao.DeleteEntity(entitydata);
                        ydao.SubmitEntity(data2);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception)
                    {
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static string SendEmailToActUsers(string CaseCode)
        {
            MailRule rule = new MailRule();
            rule.Title = "项目管理系统流程结束提醒(日期：" + DateTime.Now.ToShortDateString() + ")";
            rule.Body = "";
            rule.ToMail = GetWorkFlowCaseActUserEmails(CaseCode);
            rule.sendMail();
            return "";
        }

        private static void StationFilter(string ProcedurePropertyType, string PropertyValue, string RoleCompriseItem, DataTable UserDataTable)
        {
            EntityData userByStation;
            int num;
            DataRow row;
            if ((ProcedurePropertyType == "") || (PropertyValue == ""))
            {
                userByStation = SystemRule.GetUserByStation(RoleCompriseItem);
                for (num = 0; num < userByStation.CurrentTable.Rows.Count; num++)
                {
                    row = UserDataTable.NewRow();
                    row["UserCode"] = userByStation.CurrentTable.Rows[num]["UserCode"].ToString();
                    row["UserName"] = userByStation.CurrentTable.Rows[num]["UserName"].ToString();
                    row["ShortUserName"] = userByStation.CurrentTable.Rows[num]["ShortUserName"].ToString();
                    UserDataTable.Rows.Add(row);
                }
            }
            else
            {
                switch (ProcedurePropertyType)
                {
                    case "Station":
                        if (PropertyValue == RoleCompriseItem)
                        {
                            userByStation = SystemRule.GetUserByStation(RoleCompriseItem);
                            for (num = 0; num < userByStation.CurrentTable.Rows.Count; num++)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = userByStation.CurrentTable.Rows[num]["UserCode"].ToString();
                                row["UserName"] = userByStation.CurrentTable.Rows[num]["UserName"].ToString();
                                row["ShortUserName"] = userByStation.CurrentTable.Rows[num]["ShortUserName"].ToString();
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;

                    case "Porson":
                        userByStation = SystemRule.GetUserByStation(RoleCompriseItem);
                        for (num = 0; num < userByStation.CurrentTable.Rows.Count; num++)
                        {
                            if (PropertyValue.IndexOf(userByStation.CurrentTable.Rows[num]["UserCode"].ToString()) != -1)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = userByStation.CurrentTable.Rows[num]["UserCode"].ToString();
                                row["UserName"] = userByStation.CurrentTable.Rows[num]["UserName"].ToString();
                                row["ShortUserName"] = userByStation.CurrentTable.Rows[num]["ShortUserName"].ToString();
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;

                    case "Unit":
                        if (("-" + SystemRule.GetUnitFullCode(SystemRule.GetUnitByStationCode(RoleCompriseItem)) + "-").IndexOf("-" + PropertyValue + "-") != -1)
                        {
                            userByStation = SystemRule.GetUserByStation(RoleCompriseItem);
                            for (num = 0; num < userByStation.CurrentTable.Rows.Count; num++)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = userByStation.CurrentTable.Rows[num]["UserCode"].ToString();
                                row["UserName"] = userByStation.CurrentTable.Rows[num]["UserName"].ToString();
                                row["ShortUserName"] = userByStation.CurrentTable.Rows[num]["ShortUserName"].ToString();
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;
                }
            }
        }

        private static void UnitFilter(string ProcedurePropertyType, string PropertyValue, string RoleCompriseItem, DataTable UserDataTable)
        {
            EntityData usersByUnit;
            int num;
            DataRow row;
            if ((ProcedurePropertyType == "") || (PropertyValue == ""))
            {
                usersByUnit = SystemRule.GetUsersByUnit(RoleCompriseItem);
                for (num = 0; num < usersByUnit.CurrentTable.Rows.Count; num++)
                {
                    row = UserDataTable.NewRow();
                    row["UserCode"] = usersByUnit.CurrentTable.Rows[num]["UserCode"].ToString();
                    row["UserName"] = usersByUnit.CurrentTable.Rows[num]["UserName"].ToString();
                    row["ShortUserName"] = usersByUnit.CurrentTable.Rows[num]["ShortUserName"].ToString();
                    UserDataTable.Rows.Add(row);
                }
            }
            else
            {
                switch (ProcedurePropertyType)
                {
                    case "Station":
                        if (SystemRule.GetUnitByStationCode(PropertyValue) == RoleCompriseItem)
                        {
                            usersByUnit = SystemRule.GetUserByStation(RoleCompriseItem);
                            for (num = 0; num < usersByUnit.CurrentTable.Rows.Count; num++)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = usersByUnit.CurrentTable.Rows[num]["UserCode"].ToString();
                                row["UserName"] = usersByUnit.CurrentTable.Rows[num]["UserName"].ToString();
                                row["ShortUserName"] = usersByUnit.CurrentTable.Rows[num]["ShortUserName"].ToString();
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;

                    case "Porson":
                        usersByUnit = SystemRule.GetUsersByUnit(RoleCompriseItem);
                        for (num = 0; num < usersByUnit.CurrentTable.Rows.Count; num++)
                        {
                            if (PropertyValue.IndexOf(usersByUnit.CurrentTable.Rows[num]["UserCode"].ToString()) != -1)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = usersByUnit.CurrentTable.Rows[num]["UserCode"].ToString();
                                row["UserName"] = usersByUnit.CurrentTable.Rows[num]["UserName"].ToString();
                                row["ShortUserName"] = usersByUnit.CurrentTable.Rows[num]["ShortUserName"].ToString();
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;

                    case "Unit":
                        if (("-" + SystemRule.GetUnitFullCode(RoleCompriseItem) + "-").IndexOf("-" + PropertyValue + "-") != -1)
                        {
                            usersByUnit = SystemRule.GetUsersByUnit(RoleCompriseItem);
                            for (num = 0; num < usersByUnit.CurrentTable.Rows.Count; num++)
                            {
                                row = UserDataTable.NewRow();
                                row["UserCode"] = usersByUnit.CurrentTable.Rows[num]["UserCode"].ToString();
                                row["UserName"] = usersByUnit.CurrentTable.Rows[num]["UserName"].ToString();
                                row["ShortUserName"] = usersByUnit.CurrentTable.Rows[num]["ShortUserName"].ToString();
                                UserDataTable.Rows.Add(row);
                            }
                        }
                        break;
                }
            }
        }

        public static bool WorkFlowCommonStatusChange(string pm_sWorkFlowCommonCode, int pm_iStatus)
        {
            return WorkFlowCommonStatusChange(pm_sWorkFlowCommonCode, pm_iStatus, null, true);
        }

        public static bool WorkFlowCommonStatusChange(EntityData pm_Entity, int pm_iStatus)
        {
            return WorkFlowCommonStatusChange(pm_Entity, "", pm_iStatus, null, false);
        }

        public static bool WorkFlowCommonStatusChange(string pm_sWorkFlowCommonCode, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return WorkFlowCommonStatusChange(pm_sWorkFlowCommonCode, pm_iStatus, pm_iOriginalStatus, true);
        }

        public static bool WorkFlowCommonStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus)
        {
            return WorkFlowCommonStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, false);
        }

        public static bool WorkFlowCommonStatusChange(EntityData pm_Entity, int pm_iStatus, bool pm_bSubmitData)
        {
            return WorkFlowCommonStatusChange(pm_Entity, "", pm_iStatus, null, pm_bSubmitData);
        }

        public static bool WorkFlowCommonStatusChange(StandardEntityDAO dao, string pm_sWorkFlowCommonCode, int pm_iStatus)
        {
            if (dao == null)
            {
                dao = new StandardEntityDAO("WorkFlowCommon");
            }
            else
            {
                dao.EntityName = "WorkFlowCommon";
            }
            EntityData workFlowCommonByCode = WorkFlowDAO.GetWorkFlowCommonByCode(pm_sWorkFlowCommonCode, dao);
            bool flag = WorkFlowCommonStatusChange(workFlowCommonByCode, "", pm_iStatus, null, false);
            dao.SubmitEntity(workFlowCommonByCode);
            return flag;
        }

        public static bool WorkFlowCommonStatusChange(string pm_sWorkFlowCommonCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return WorkFlowCommonStatusChange(WorkFlowDAO.GetWorkFlowCommonByCode(pm_sWorkFlowCommonCode), pm_sWorkFlowCommonCode, pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool WorkFlowCommonStatusChange(EntityData pm_Entity, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            return WorkFlowCommonStatusChange(pm_Entity, "", pm_iStatus, pm_iOriginalStatus, pm_bSubmitData);
        }

        public static bool WorkFlowCommonStatusChange(EntityData pm_Entity, string pm_sWorkFlowCommonCode, int pm_iStatus, int? pm_iOriginalStatus, bool pm_bSubmitData)
        {
            bool flag2;
            try
            {
                string filterExpression = "";
                bool flag = true;
                pm_Entity.SetCurrentTable("WorkFlowCommon");
                if (pm_sWorkFlowCommonCode.Trim() == "")
                {
                    if (pm_Entity.CurrentTable.Rows.Count != 1)
                    {
                        flag = false;
                    }
                }
                else
                {
                    filterExpression = string.Format("WorkFlowCommonCode='{0}'", pm_sWorkFlowCommonCode.Trim());
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
                    WorkFlowDAO.SubmitAllWorkFlowCommon(pm_Entity);
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static string WriteOpinion(Opinion opinion)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\t<table border=0 cellpadding=0 cellspacing=0  width=100% ><tr><td> ");
            builder.Append(opinion.OpinionText);
            builder.Append("<br></td></tr><tr><td align =right>");
            builder.Append(opinion.OpinionDate + "&nbsp;&nbsp;&nbsp;" + SystemRule.GetUserName(opinion.UserCode));
            builder.Append("</td></tr></table>");
            return builder.ToString();
        }
    }
}

