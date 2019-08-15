namespace RmsPM.DAL.EntityDAO
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class ConstructDAO
    {
        public static void DeleteConstructAnnualPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructAnnualPlan"))
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

        public static void DeleteConstructPlanStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
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

        public static void DeleteConstructProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgress"))
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

        public static void DeleteConstructProgressRisk(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressRisk"))
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

        public static void DeleteConstructProgressStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
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

        public static void DeleteGroundWork(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GroundWork"))
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

        public static void DeleteRiskIndex(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskIndex"))
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

        public static void DeleteRiskType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskType"))
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

        public static void DeleteStandard_ConstructProgress(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_ConstructProgress"))
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

        public static void DeleteVisualProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("VisualProgress"))
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

        public static EntityData GetAllConstructAnnualPlan()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructAnnualPlan"))
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

        public static EntityData GetAllConstructPlanStep()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
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

        public static EntityData GetAllConstructProgress()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgress"))
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

        public static EntityData GetAllConstructProgressRisk()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressRisk"))
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

        public static EntityData GetAllConstructProgressStep()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
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

        public static EntityData GetAllGroundWork()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GroundWork"))
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

        public static EntityData GetAllRiskIndex()
        {
            EntityData data2;
            try
            {
                RiskIndexStrategyBuilder builder = new RiskIndexStrategyBuilder();
                builder.AddOrder("IndexLevel", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("RiskIndex", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllRiskType()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskType"))
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

        public static EntityData GetAllVisualProgress()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("VisualProgress"))
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

        public static EntityData GetConstructAnnualPlanByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructAnnualPlan"))
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

        public static EntityData GetConstructAnnualPlanByPBSUnit(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                ConstructAnnualPlanStrategyBuilder builder = new ConstructAnnualPlanStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("IYear", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructAnnualPlan", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructAnnualPlanByPBSUnitYear(string PBSUnitCode, int IYear)
        {
            EntityData data2;
            try
            {
                ConstructAnnualPlanStrategyBuilder builder = new ConstructAnnualPlanStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.IYear, IYear.ToString()));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructAnnualPlan", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructAnnualPlanByProjectYear(string ProjectCode, int IYear)
        {
            EntityData data2;
            try
            {
                ConstructAnnualPlanStrategyBuilder builder = new ConstructAnnualPlanStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.IYear, IYear.ToString()));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructAnnualPlan", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static DataTable GetConstructAnnualPlanYearByProject(string ProjectCode)
        {
            DataTable table2;
            try
            {
                string queryString = string.Format("select distinct IYear from ConstructAnnualPlan where ProjectCode = '{0}' order by 1", ProjectCode);
                QueryAgent agent = new QueryAgent();
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                agent.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static EntityData GetConstructPlanStepByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
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

        public static EntityData GetConstructPlanStepByPBSUnit(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                ConstructPlanStepStrategyBuilder builder = new ConstructPlanStepStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructPlanStepStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("IYear", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructPlanStep", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructPlanStepByPBSUnitYear(string PBSUnitCode, int IYear)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("ConstructPlanStep");
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    string[] Params = new string[] { "@PBSUnitCode", "@IYear" };
                    object[] values = new object[] { PBSUnitCode, IYear };
                    ydao.FillEntity(SqlManager.GetSqlStruct("ConstructPlanStep", "SelectByPBSUnitCodeYear").SqlString, Params, values, entitydata, "ConstructPlanStep");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructPlanStepByProjectYear(string ProjectCode, int IYear)
        {
            EntityData data2;
            try
            {
                ConstructPlanStepStrategyBuilder builder = new ConstructPlanStepStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructPlanStepStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(ConstructPlanStepStrategyName.IYear, IYear.ToString()));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructPlanStep", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructProgressByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgress"))
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

        public static EntityData GetConstructProgressByPBSUnit(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("ConstructProgress");
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgress"))
                {
                    string[] Params = new string[] { "@PBSUnitCode" };
                    object[] values = new object[] { PBSUnitCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("ConstructProgress", "SelectByPBSUnitCode").SqlString, Params, values, entitydata, "ConstructProgress");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructProgressRiskByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressRisk"))
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

        public static EntityData GetConstructProgressRiskByPBSUnitCode(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                ConstructProgressRiskStrategyBuilder builder = new ConstructProgressRiskStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressRiskStrategyName.PBSUnitCode, PBSUnitCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructProgressRisk", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructProgressRiskByProgressCode(string ProgressCode)
        {
            EntityData data2;
            try
            {
                ConstructProgressRiskStrategyBuilder builder = new ConstructProgressRiskStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressRiskStrategyName.ProgressCode, ProgressCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructProgressRisk", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructProgressStepByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
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

        public static EntityData GetConstructProgressStepByPBSUnit(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("ConstructProgressStep");
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
                {
                    string[] Params = new string[] { "@PBSUnitCode" };
                    object[] values = new object[] { PBSUnitCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("ConstructProgressStep", "SelectByPBSUnitCode").SqlString, Params, values, entitydata, "ConstructProgressStep");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCurrConstructAnnualPlanByPBSUnit(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                ConstructAnnualPlanStrategyBuilder builder = new ConstructAnnualPlanStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("IYear", false);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("ConstructAnnualPlan", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDefaultRiskIndex()
        {
            EntityData data2;
            try
            {
                RiskIndexStrategyBuilder builder = new RiskIndexStrategyBuilder();
                builder.AddStrategy(new Strategy(RiskIndexStrategyName.IsDefault, "1"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("RiskIndex", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetFirstVisualProgress(string ProgressType)
        {
            EntityData data2;
            try
            {
                VisualProgressStrategyBuilder builder = new VisualProgressStrategyBuilder();
                if (ProgressType != "")
                {
                    builder.AddStrategy(new Strategy(VisualProgressStrategyName.ProgressType, ProgressType.ToString()));
                }
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("VisualProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetGroundWorkByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("GroundWork"))
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

        public static EntityData GetGroundWorkByParentCode(string ParentCode)
        {
            EntityData data2;
            try
            {
                GroundWorkStrategyBuilder builder = new GroundWorkStrategyBuilder();
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.ParentCode, ParentCode));
                builder.AddOrder("TaskName", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("GroundWork", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetGroundWorkByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                GroundWorkStrategyBuilder builder = new GroundWorkStrategyBuilder();
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("TaskName", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("GroundWork", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetGroundWorkByWBSCode(string WBSCode)
        {
            EntityData data2;
            try
            {
                GroundWorkStrategyBuilder builder = new GroundWorkStrategyBuilder();
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.WBSCode, WBSCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("GroundWork", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetGroundWorkRootByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                GroundWorkStrategyBuilder builder = new GroundWorkStrategyBuilder();
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.ParentCode, ""));
                builder.AddOrder("TaskName", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("GroundWork", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetLastVisualProgress(string ProgressType)
        {
            EntityData data2;
            try
            {
                VisualProgressStrategyBuilder builder = new VisualProgressStrategyBuilder();
                if (ProgressType != "")
                {
                    builder.AddStrategy(new Strategy(VisualProgressStrategyName.ProgressType, ProgressType.ToString()));
                }
                builder.AddOrder("SortID", false);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("VisualProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRiskIndexByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskIndex"))
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

        public static EntityData GetRiskTypeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskType"))
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

        public static EntityData GetStandard_ConstructProgressByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_ConstructProgress");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_ConstructProgress"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("ConstructProgress", "Select").SqlString, "@ProgressCode", code, entitydata, "ConstructProgress");
                    ydao.FillEntity(SqlManager.GetSqlStruct("ConstructProgressRisk", "SelectByProgressCode").SqlString, "@ProgressCode", code, entitydata, "ConstructProgressRisk");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_ConstructProgressByPBSUnit(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                ConstructProgressStrategyBuilder builder = new ConstructProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("ReportDate", true);
                builder.AddOrder("ProgressCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ConstructProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetVisualProgressByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("VisualProgress"))
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

        public static EntityData GetVisualProgressByProgressType(int ProgressType)
        {
            EntityData data2;
            try
            {
                VisualProgressStrategyBuilder builder = new VisualProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(VisualProgressStrategyName.ProgressType, ProgressType.ToString()));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("VisualProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertConstructAnnualPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructAnnualPlan"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertConstructPlanStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertConstructProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgress"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertConstructProgressRisk(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressRisk"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertConstructProgressStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertGroundWork(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GroundWork"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertRiskIndex(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskIndex"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertRiskType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskType"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_ConstructProgress(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_ConstructProgress"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertVisualProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("VisualProgress"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllConstructAnnualPlan(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructAnnualPlan"))
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

        public static void SubmitAllConstructPlanStep(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
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

        public static void SubmitAllConstructProgressRisk(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressRisk"))
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

        public static void SubmitAllConstructProgressStep(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
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

        public static void SubmitAllGroundWork(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GroundWork"))
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

        public static void SubmitAllRiskIndex(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskIndex"))
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

        public static void SubmitAllRiskType(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskType"))
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

        public static void SubmitAllStandard_ConstructProgress(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_ConstructProgress"))
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

        public static void SubmitAllVisualProgress(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("VisualProgress"))
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

        public static void UpdateConstructAnnualPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructAnnualPlan"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateConstructPlanStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateConstructProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgress"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateConstructProgressRisk(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressRisk"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateConstructProgressStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructProgressStep"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateGroundWork(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("GroundWork"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateRiskIndex(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskIndex"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateRiskType(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RiskType"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_ConstructProgress(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_ConstructProgress"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateVisualProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("VisualProgress"))
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

