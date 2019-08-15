namespace RmsPM.DAL.EntityDAO
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class SalDAO
    {
        public static void DeleteSalBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudget"))
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

        public static void DeleteSalBudgetDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudgetDtl"))
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

        public static void DeleteSalContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
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

        public static void DeleteSalCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
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

        public static void DeleteSalPay(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
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

        public static void DeleteSalPayPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
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

        public static void DeleteSalPayRela(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
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

        public static void DeleteSalSupl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
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

        public static void DeleteSalSuplByProjectCode(string ProjectCode)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                string queryString = "delete SalSupl where isnull(ProjectCode, '') = '" + ProjectCode + "'";
                agent.ExecuteScalar(queryString);
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_SalContract(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SalContract"))
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

        public static EntityData GetAllSalBudget()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudget"))
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

        public static EntityData GetAllSalBudgetDtl()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudgetDtl"))
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

        public static EntityData GetAllSalContract()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
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

        public static EntityData GetAllSalCost()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
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

        public static EntityData GetAllSalPay()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
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

        public static EntityData GetAllSalPayPlan()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
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

        public static EntityData GetAllSalPayRela()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
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

        public static EntityData GetAllSalSupl()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
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

        public static EntityData GetSalBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudget"))
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

        public static EntityData GetSalBudgetByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                SalBudgetStrategyBuilder builder = new SalBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("IYear", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SalBudget", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalBudgetByProjectYear(string ProjectCode, int IYear)
        {
            EntityData data2;
            try
            {
                SalBudgetStrategyBuilder builder = new SalBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(SalBudgetStrategyName.IYear, IYear.ToString()));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SalBudget", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalBudgetDtlByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudgetDtl"))
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

        public static EntityData GetSalBudgetDtlByProject(string ProjectCode)
        {
            EntityData data2;
            try
            {
                SalBudgetDtlStrategyBuilder builder = new SalBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("IYear", true);
                builder.AddOrder("IMonth", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SalBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalBudgetDtlByProjectYear(string ProjectCode, int IYear)
        {
            EntityData data2;
            try
            {
                SalBudgetDtlStrategyBuilder builder = new SalBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IYear, IYear.ToString()));
                builder.AddOrder("IMonth", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SalBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalBudgetDtlByProjectYM(string ProjectCode, int IYear, int IMonth)
        {
            EntityData data2;
            try
            {
                SalBudgetDtlStrategyBuilder builder = new SalBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IYear, IYear.ToString()));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IMonth, IMonth.ToString()));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SalBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalBudgetDtlByProjectYM(string ProjectCode, int IYear, int IMonth, string PBSTypeCode)
        {
            EntityData data2;
            try
            {
                SalBudgetDtlStrategyBuilder builder = new SalBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IYear, IYear.ToString()));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IMonth, IMonth.ToString()));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.PBSTypeCode, PBSTypeCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SalBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static DataSet GetSalBuildingByProjectCode(string code)
        {
            DataSet set2;
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    set2 = agent.ExecSqlForDataSet(string.Format("select distinct BuildingName from SalContract where ProjectCode = '{0}'", code));
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
            return set2;
        }

        public static EntityData GetSalContractByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
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

        public static EntityData GetSalContractByProjectCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalContract");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalContract", "SelectByProjectCode").SqlString, Params, values, entitydata, "SalContract");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalCostByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
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

        public static EntityData GetSalCostByProjectBuilding(string ProjectCode, string BuildingName)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalCost");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@BuildingName" };
                    object[] values = new object[] { ProjectCode, BuildingName };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalCost", "SelectByProjectBuilding").SqlString, Params, values, entitydata, "SalCost");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalCostByProjectCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalCost");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalCost", "SelectByProject").SqlString, Params, values, entitydata, "SalCost");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalPayByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
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

        public static EntityData GetSalPayByContract(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalPay");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
                {
                    string[] Params = new string[] { "@ContractCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPay", "SelectByContract").SqlString, Params, values, entitydata, "SalPay");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalPayPlanByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
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

        public static EntityData GetSalPayPlanByContract(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalPayPlan");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
                {
                    string[] Params = new string[] { "@ContractCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPayPlan", "SelectByContract").SqlString, Params, values, entitydata, "SalPayPlan");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalPayPlanByPayCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalPayPlan");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
                {
                    string[] Params = new string[] { "@PayCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPayPlan", "SelectByPayCode").SqlString, Params, values, entitydata, "SalPayPlan");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalPayRelaByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
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

        public static EntityData GetSalPayRelaByContract(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalPayRela");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
                {
                    string[] Params = new string[] { "@ContractCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPayRela", "SelectByContract").SqlString, Params, values, entitydata, "SalPayRela");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalPayRelaByPayCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalPayRela");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
                {
                    string[] Params = new string[] { "@PayCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPayRela", "SelectByPayCode").SqlString, Params, values, entitydata, "SalPayRela");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalPayRelaByPlanCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalPayRela");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
                {
                    string[] Params = new string[] { "@PayPlanCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPayRela", "SelectByPlanCode").SqlString, Params, values, entitydata, "SalPayRela");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSalSuplByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
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

        public static EntityData GetSalSuplByProjectCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SalSupl");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalSupl", "SelectByProjectCode").SqlString, Params, values, entitydata, "SalSupl");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_SalContractByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_SalContract");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SalContract"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalContract", "Select").SqlString, "@ContractCode", code, entitydata, "SalContract");
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPay", "SelectByContract").GetSqlStringWithOrder(), "@ContractCode", code, entitydata, "SalPay");
                    ydao.FillEntity(SqlManager.GetSqlStruct("SalPayPlan", "SelectByContract").GetSqlStringWithOrder(), "@ContractCode", code, entitydata, "SalPayPlan");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertSalBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudget"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalBudgetDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudgetDtl"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalPay(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalPayPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalPayRela(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSalSupl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_SalContract(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SalContract"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllSalBudget(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudget"))
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

        public static void SubmitAllSalBudgetDtl(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudgetDtl"))
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

        public static void SubmitAllSalContract(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
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

        public static void SubmitAllSalCost(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
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

        public static void SubmitAllSalPay(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
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

        public static void SubmitAllSalPayPlan(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
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

        public static void SubmitAllSalPayRela(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
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

        public static void SubmitAllSalSupl(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
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

        public static void SubmitAllStandard_Voucher(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Voucher"))
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

        public static void UpdateSalBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudget"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalBudgetDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalBudgetDtl"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalContract"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalCost"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalPay(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPay"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalPayPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayPlan"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalPayRela(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalPayRela"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSalSupl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SalSupl"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_SalContract(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SalContract"))
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

