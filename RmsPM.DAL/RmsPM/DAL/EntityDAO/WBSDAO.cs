namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class WBSDAO
    {
        public static void DelAttentionRule(string strCode, string strUserCode)
        {
            try
            {
                EntityData entitydata = new EntityData("AccessRange");
                using (SingleEntityDAO ydao = new SingleEntityDAO("AccessRange"))
                {
                    string[] Params = new string[] { "@ResourceCode", "@UserCode" };
                    object[] values = new object[] { strCode, strUserCode };
                    string sqlString = "select * from accessrange where resourcecode=(select resourceCode from resource where classcode='0701' and relationcode=@ResourceCode) and AccessRangeType=0 and Relationcode=@UserCode and OperationCode='070110'";
                    ydao.FillEntity(sqlString, Params, values, entitydata, "AccessRange");
                }
                ResourceDAO.DeleteAccessRange(entitydata);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteFeedBack(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_TaskExecute(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TaskExecute"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_WBS(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBS"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_WBSTemplet(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBSTemplet"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTask(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Task"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskAttention(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskBudgetCondition(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskDocument(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskDocument"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskExecute(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskExecuteRisk(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecuteRisk"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskGuid(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskHistory(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskHistory"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskPerson(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTaskRelated(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteV_WBSTask(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_WBSTask"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteWBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("WBS"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllFeedBack()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllRootTask()
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ParentCode, ""));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllRuleTaskAttention(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskAttention");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskAttention", "SelectRuleByUserCode").GetSqlStringWithOrder(), "@UserCode", code, entitydata, "TaskAttention");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTask()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Task"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskAttention()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskBudget()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskBudgetCondition()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskContract()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskDocument()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskDocument"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskExecute()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskExecuteRisk()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecuteRisk"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskGuid()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskHistory()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskHistory"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskPerson()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskPlan()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTaskRelated()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllV_WBSTask()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_WBSTask"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllWBS()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("WBS"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAttentionByUrl(string strUrl)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskAttention");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskAttention", "SelectAttentionByUrl").SqlString, "@Url", strUrl, entitydata, "TaskAttention");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetChildTask(string WBSCode)
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ParentCode, WBSCode));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDoubleRelatedByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskRelated");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskRelated", "SelectDoubleRelatedByCode").GetSqlStringWithOrder(), "@TaskRelatedCode", code, entitydata, "TaskRelated");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetFeedBackByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetFeedBackByMasterCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData();
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("FeedBack", "SelectByMasterCode").GetSqlStringWithOrder(), "@MasterCode", code, entitydata, "FeedBack");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetProjectTaskByProject(string ProjectCode)
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(WBSStrategyName.Flag, "1"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_TaskExecuteByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_TaskExecute");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TaskExecute"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskExecute", "Select").SqlString, "@TaskExecuteCode", code, entitydata, "TaskExecute");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskExecuteRisk", "SelectByTaskExecuteCode").SqlString, "@TaskExecuteCode", code, entitydata, "TaskExecuteRisk");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_WBSByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_WBS");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBS"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Task", "SelectViewByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "Task");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskExecute", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskExecute");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskPerson", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskPerson");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskContract", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskContract");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskRelated", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskRelated");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskGuid", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskGuid");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskBudget", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskBudget");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskBudgetCondition", "SelectByWBSCode").SqlString, "@WBSCode", code, entitydata, "TaskBudgetCondition");
                    string[] Params = new string[] { "@DocumentTypeCode", "@Code" };
                    object[] values = new object[] { "000006", code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DocumentConfig", "SelectByWBSCode").SqlString, Params, values, entitydata, "DocumentConfig");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_WBSTempletByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_WBSTemplet");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBSTemplet"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Templet", "Select").SqlString, "@TempletCode", code, entitydata, "Templet");
                    ydao.FillEntity(SqlManager.GetSqlStruct("WBSTemplet", "SelectByTempletCode").SqlString, "@TempletCode", code, entitydata, "WBSTemplet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskAttentionByCode(string strTaskACode)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    data = ydao.SelectbyPrimaryKey(strTaskACode);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskAttentionByUserCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskAttention");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskAttention", "SelectByUserCode").GetSqlStringWithOrder(), "@UserCode", code, entitydata, "TaskAttention");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskBudgetByWBSCode(string wbsCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskBudget");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskBudget", "SelectByWBSCode").SqlString, "@WBSCode", wbsCode, entitydata, "TaskBudget");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskBudgetConditionByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskBudgetConditionByRelationiWBSCode(string wbsCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskBudgetCondition");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskBudgetCondition", "SelectByRelationWBSCode").SqlString, "@RelationWBSCode", wbsCode, entitydata, "TaskBudgetCondition");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskBudgetConditionByWBSCode(string wbsCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskBudgetCondition");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskBudgetCondition", "SelectByWBSCode").SqlString, "@WBSCode", wbsCode, entitydata, "TaskBudgetCondition");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Task"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskByProject(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Task");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Task"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Task", "SelectByProjectCode").SqlString, "@ProjectCode", projectCode, entitydata, "Task");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskByRelaCode(string RelaType, string RelaCode, string ProjectCode)
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                if (ProjectCode != "")
                {
                    builder.AddStrategy(new Strategy(WBSStrategyName.ProjectCode, ProjectCode));
                }
                builder.AddStrategy(new Strategy(WBSStrategyName.RelaType, RelaType));
                builder.AddStrategy(new Strategy(WBSStrategyName.RelaCode, RelaCode));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskBySortID(string SortID, string ProjectCode)
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(WBSStrategyName.SortID, SortID));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskContractByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskContractByWBSCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskContract");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskContract", "SelectByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "TaskContract");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskDocumentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskDocument"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskDocumentByWBSCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskDocument");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskDocument"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskDocument", "SelectByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "TaskDocument");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                TaskExecuteStrategyBuilder builder = new TaskExecuteStrategyBuilder();
                builder.AddStrategy(new Strategy(TaskExecuteStrategyName.ProjectCode, ProjectCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("TaskExecute", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteByWBSCode(string WBSCode)
        {
            EntityData data2;
            try
            {
                TaskExecuteStrategyBuilder builder = new TaskExecuteStrategyBuilder();
                builder.AddStrategy(new Strategy(TaskExecuteStrategyName.WBSCode, WBSCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("TaskExecute", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteDeskTop(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskExecute");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskExecute", "SelectDeskTopByUserCode").SqlString, "@UserCode", code, entitydata, "TaskExecute");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteRiskByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecuteRisk"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteRiskByTaskExecuteCode(string TaskExecuteCode)
        {
            EntityData data2;
            try
            {
                TaskExecuteRiskStrategyBuilder builder = new TaskExecuteRiskStrategyBuilder();
                builder.AddStrategy(new Strategy(TaskExecuteRiskStrategyName.TaskExecuteCode, TaskExecuteCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("TaskExecuteRisk", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskExecuteRiskByWBSCode(string WBSCode)
        {
            EntityData data2;
            try
            {
                TaskExecuteRiskStrategyBuilder builder = new TaskExecuteRiskStrategyBuilder();
                builder.AddStrategy(new Strategy(TaskExecuteRiskStrategyName.WBSCode, WBSCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("TaskExecuteRisk", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskGuidByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskGuidByWBSCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskGuid");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskGuid", "SelectByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "TaskGuid");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskHistoryByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData();
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskHistory"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskHistory", "SelectByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "TaskHistory");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskPerson(string UserCode, string WBSCode)
        {
            EntityData data2;
            string[] Params = new string[] { "@WBSCode", "@UserCode" };
            object[] values = new object[] { WBSCode, UserCode };
            try
            {
                EntityData entitydata = new EntityData("TaskPerson");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskPerson", "SelectTaskTaskPersonByWBSUser").SqlString, Params, values, entitydata, "TaskPerson");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskPersonByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskPersonByWBSCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskPerson");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskPerson", "SelectByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "TaskPerson");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskPlanByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskPlanByProjectCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskPlan");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskPlan", "SelectByProjectCode").GetSqlStringWithOrder(), "@ProjectCode", code, entitydata, "TaskPlan");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskRelatedByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTaskRelatedByWBSCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TaskRelated");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TaskRelated", "SelectByWBSCode").GetSqlStringWithOrder(), "@WBSCode", code, entitydata, "TaskRelated");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_TaskByCode(string WBSCode)
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.WBSCode, WBSCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_WBSTaskByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_WBSTask"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetWBSByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("WBS"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetWBSByProject(string strProjectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("WBS");
                using (SingleEntityDAO ydao = new SingleEntityDAO("WBS"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("WBS", "SelectByProjectCode").GetSqlStringWithOrder(), "@ProjectCode", strProjectCode, entitydata, "WBS");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetWBSFullCode(string wbsCode)
        {
            string text = "";
            try
            {
                EntityData wBSByCode = GetWBSByCode(wbsCode);
                if (wBSByCode.HasRecord())
                {
                    text = wBSByCode.GetString("FullCode");
                }
                wBSByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static void InsertFeedBack(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_TaskExecute(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TaskExecute"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_WBS(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBS"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_WBSTemplet(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBSTemplet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTask(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Task"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskAttention(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskBudgetCondition(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskDocument(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskDocument"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskExecute(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskExecuteRisk(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecuteRisk"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskGuid(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskHistory(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskHistory"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskPerson(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTaskRelated(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertV_WBSTask(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_WBSTask"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertWBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("WBS"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllFeedBack(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_TaskExecute(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TaskExecute"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_WBS(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBS"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_WBSTemplet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBSTemplet"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskAttention(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskBudget(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskBudgetCondition(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskContract(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskExecute(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskExecuteRisk(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecuteRisk"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskGuid(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskHistory(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskHistory"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskPerson(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskPlan(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTaskRelated(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllV_WBSTask(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_WBSTask"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void UpdateFeedBack(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FeedBack"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_TaskExecute(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TaskExecute"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_WBS(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBS"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_WBSTemplet(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_WBSTemplet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTask(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Task"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskAttention(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskAttention"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudget"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskBudgetCondition(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskBudgetCondition"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskContract"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskDocument(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskDocument"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskExecute(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecute"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskExecuteRisk(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskExecuteRisk"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskGuid(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskGuid"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskHistory(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskHistory"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskPerson(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPerson"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskPlan"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskRelated(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TaskRelated"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateV_WBSTask(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_WBSTask"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateWBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("WBS"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

