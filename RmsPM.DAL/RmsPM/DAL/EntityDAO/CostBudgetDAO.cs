namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public class CostBudgetDAO
    {
        public static string m_OfflineBackupCodeStartWith = "Offline_";

        public static void DeleteCostBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudget"))
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

        public static void DeleteCostBudgetBackup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackup"))
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

        public static void DeleteCostBudgetBackupDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupDtl"))
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

        public static void DeleteCostBudgetBackupMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupMonth"))
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

        public static void DeleteCostBudgetBackupSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupSet"))
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

        public static void DeleteCostBudgetContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetContract"))
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

        public static void DeleteCostBudgetDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtl"))
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

        public static void DeleteCostBudgetDtlHis(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtlHis"))
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

        public static void DeleteCostBudgetMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetMonth"))
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

        public static void DeleteCostBudgetSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetSet"))
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

        public static void DeleteStandard_CostBudget(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudget"))
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

        public static void DeleteStandard_CostBudgetBackup(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudgetBackup"))
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

        public static EntityData GetAllCostBudget()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudget"))
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

        public static EntityData GetAllCostBudgetBackup()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackup"))
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

        public static EntityData GetAllCostBudgetBackupDtl()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupDtl"))
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

        public static EntityData GetAllCostBudgetBackupMonth()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupMonth"))
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

        public static EntityData GetAllCostBudgetBackupSet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupSet"))
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

        public static EntityData GetAllCostBudgetContract()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetContract"))
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

        public static EntityData GetAllCostBudgetDtl()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtl"))
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

        public static EntityData GetAllCostBudgetDtlHis()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtlHis"))
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

        public static EntityData GetAllCostBudgetMonth()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetMonth"))
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

        public static EntityData GetAllCostBudgetSet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetSet"))
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

        public static EntityData GetCostBudgetBackupByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackup"))
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

        public static EntityData GetCostBudgetBackupDtlByBackupCode(string CostBudgetBackupCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupDtlStrategyBuilder builder = new CostBudgetBackupDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupDtlStrategyName.CostBudgetBackupCode, CostBudgetBackupCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupDtlByBackupCode(string CostBudgetBackupCode, string CostCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupDtlStrategyBuilder builder = new CostBudgetBackupDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupDtlStrategyName.CostBudgetBackupCode, CostBudgetBackupCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupDtlStrategyName.CostCode, CostCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupDtlByBackupSetCode(string CostBudgetBackupSetCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupDtlStrategyBuilder builder = new CostBudgetBackupDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupDtlStrategyName.CostBudgetBackupSetCode, CostBudgetBackupSetCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupDtlByBackupSetCode(string CostBudgetBackupSetCode, string CostCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupDtlStrategyBuilder builder = new CostBudgetBackupDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupDtlStrategyName.CostBudgetBackupSetCode, CostBudgetBackupSetCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupDtlStrategyName.CostCode, CostCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupDtlByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupDtl"))
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

        public static EntityData GetCostBudgetBackupMonthByBackupSetCode(string CostBudgetBackupSetCode)
        {
            EntityData data2;
            try
            {
                CostBudgetBackupMonthStrategyBuilder builder = new CostBudgetBackupMonthStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupMonthStrategyName.CostBudgetBackupSetCode, CostBudgetBackupSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupMonth", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupMonthByBackupSetCode(string CostBudgetBackupSetCode, string CostCode)
        {
            EntityData data2;
            try
            {
                CostBudgetBackupMonthStrategyBuilder builder = new CostBudgetBackupMonthStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupMonthStrategyName.CostBudgetBackupSetCode, CostBudgetBackupSetCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupMonthStrategyName.CostCode, CostCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupMonth", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupMonthByBackupSetCode(string CostBudgetBackupSetCode, string CostCode, string ContractCode)
        {
            EntityData data2;
            try
            {
                CostBudgetBackupMonthStrategyBuilder builder = new CostBudgetBackupMonthStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupMonthStrategyName.CostBudgetBackupSetCode, CostBudgetBackupSetCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupMonthStrategyName.CostCode, CostCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupMonthStrategyName.ContractCode, ContractCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupMonth", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupMonthByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupMonth"))
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

        public static EntityData GetCostBudgetBackupSetByBackupCode(string CostBudgetBackupCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupSetStrategyBuilder builder = new CostBudgetBackupSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupSetStrategyName.CostBudgetBackupCode, CostBudgetBackupCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupSetByBackupCode(string CostBudgetBackupCode, string CostBudgetSetCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupSetStrategyBuilder builder = new CostBudgetBackupSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupSetStrategyName.CostBudgetBackupCode, CostBudgetBackupCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupSetStrategyName.CostBudgetSetCode, CostBudgetSetCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetBackupSetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupSet"))
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

        public static EntityData GetCostBudgetBackupSetByGroupCode(string CostBudgetBackupCode, string GroupCode, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetBackupSetStrategyBuilder builder = new CostBudgetBackupSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupSetStrategyName.CostBudgetBackupCode, CostBudgetBackupCode));
                builder.AddStrategy(new Strategy(CostBudgetBackupSetStrategyName.GroupCode, GroupCode));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudget"))
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

        public static EntityData GetCostBudgetByStatus(string CostBudgetSetCode, int TargetFlag, string Status, bool isView)
        {
            EntityData data2;
            try
            {
                string queryString;
                CostBudgetStrategyBuilder builder = new CostBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.CostBudgetSetCode, CostBudgetSetCode));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.TargetFlag, TargetFlag.ToString()));
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.Status, Status));
                if (isView)
                {
                    queryString = builder.BuildQueryViewString();
                }
                else
                {
                    queryString = builder.BuildMainQueryString();
                }
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

        public static EntityData GetCostBudgetContractByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetContract"))
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

        public static EntityData GetCostBudgetContractByContractCode(string CostBudgetSetCode, string CostCode, string ContractCode, string RelationType)
        {
            EntityData data2;
            try
            {
                CostBudgetContractStrategyBuilder builder = new CostBudgetContractStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.CostBudgetSetCode, CostBudgetSetCode));
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.CostCode, CostCode));
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.ContractCode, ContractCode));
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.RelationType, RelationType));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetContract", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetContractByCostBudgetSetCode(string CostBudgetSetCode)
        {
            EntityData data2;
            try
            {
                CostBudgetContractStrategyBuilder builder = new CostBudgetContractStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.CostBudgetSetCode, CostBudgetSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetContract", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetContractByCostCode(string CostBudgetSetCode, string CostCode)
        {
            EntityData data2;
            try
            {
                CostBudgetContractStrategyBuilder builder = new CostBudgetContractStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.CostBudgetSetCode, CostBudgetSetCode));
                builder.AddStrategy(new Strategy(CostBudgetContractStrategyName.CostCode, CostCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetContract", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetDtlByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtl"))
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

        public static EntityData GetCostBudgetDtlByCostBudgetCode(string CostBudgetCode)
        {
            EntityData data2;
            try
            {
                CostBudgetDtlStrategyBuilder builder = new CostBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostBudgetCode, CostBudgetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetDtlByCostBudgetCode(string CostBudgetCode, string CostCode)
        {
            EntityData data2;
            try
            {
                CostBudgetDtlStrategyBuilder builder = new CostBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostBudgetCode, CostBudgetCode));
                builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostCode, CostCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetDtlHisByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtlHis"))
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

        public static EntityData GetCostBudgetMonthByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetMonth"))
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

        public static EntityData GetCostBudgetMonthByCostBudgetCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetMonthStrategyBuilder builder = new CostBudgetMonthStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetMonthStrategyName.CostBudgetCode, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetMonth", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetMonthByCostBudgetDtlCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetMonthStrategyBuilder builder = new CostBudgetMonthStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetMonthStrategyName.CostBudgetDtlCode, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetMonth", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostBudgetSetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetSet"))
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

        public static EntityData GetCostBudgetSetByGroupCode(string GroupCode, string ProjectCode, string SetType)
        {
            EntityData data2;
            try
            {
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.GroupCode, GroupCode));
                if (SetType != "")
                {
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.SetType, SetType));
                }
                builder.AddOrder("PBSType", true);
                builder.AddOrder("CostBudgetSetName", true);
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

        public static EntityData GetCostBudgetSetByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("CostBudgetSetName", true);
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

        public static EntityData GetStandard_CostBudgetBackupByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_CostBudgetBackup");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudgetBackup"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackup", "Select").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackup");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackupSet", "SelectByCostBudgetBackupCode").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackupSet");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackupDtl", "SelectByCostBudgetBackupCode").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackupDtl");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackupMonth", "SelectByCostBudgetBackupCode").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackupMonth");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_CostBudgetBackupByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_CostBudgetBackup");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackup", "Select").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackup");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackupSet", "SelectByCostBudgetBackupCode").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackupSet");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackupDtl", "SelectByCostBudgetBackupCode").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackupDtl");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudgetBackupMonth", "SelectByCostBudgetBackupCode").SqlString, "@CostBudgetBackupCode", code, entitydata, "CostBudgetBackupMonth");
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_CostBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_CostBudget");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudget"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudget", "Select").SqlString, "@CostBudgetCode", code, entitydata, "CostBudget");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetDtl", "SelectByCostBudgetCode").SqlString, "@CostBudgetCode", code, entitydata, "CostBudgetDtl");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetMonth", "SelectByCostBudgetCode").SqlString, "@CostBudgetCode", code, entitydata, "CostBudgetMonth");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_CostBudgetByCode(StandardEntityDAO dao, string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_CostBudget");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudget", "Select").SqlString, "@CostBudgetCode", code, entitydata, "CostBudget");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudgetDtl", "SelectByCostBudgetCode").SqlString, "@CostBudgetCode", code, entitydata, "CostBudgetDtl");
                dao.FillEntity(SqlManager.GetSqlStruct("CostBudgetMonth", "SelectByCostBudgetCode").SqlString, "@CostBudgetCode", code, entitydata, "CostBudgetMonth");
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_V_CostBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_CostBudget");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudget"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudget", "SelectView").SqlString, "@CostBudgetCode", code, entitydata, "CostBudget");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetDtl", "SelectViewByCostBudgetCode").SqlString, "@CostBudgetCode", code, entitydata, "CostBudgetDtl");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetMonth", "SelectViewByCostBudgetCode").SqlString, "@CostBudgetCode", code, entitydata, "CostBudgetMonth");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_CostBudgetBackupByCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetBackupStrategyBuilder builder = new CostBudgetBackupStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupStrategyName.CostBudgetBackupCode, code));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_CostBudgetBackupSetByCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetBackupSetStrategyBuilder builder = new CostBudgetBackupSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetBackupSetStrategyName.CostBudgetBackupSetCode, code));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetBackupSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_CostBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetStrategyBuilder builder = new CostBudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetStrategyName.CostBudgetCode, code));
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

        public static EntityData GetV_CostBudgetDtlByCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetDtlStrategyBuilder builder = new CostBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostBudgetDtlCode, code));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_CostBudgetDtlByCostBudgetCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetDtlStrategyBuilder builder = new CostBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetDtlStrategyName.CostBudgetCode, code));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CostBudgetDtl", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_CostBudgetSetByCode(string code)
        {
            EntityData data2;
            try
            {
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.CostBudgetSetCode, code));
                string queryString = builder.BuildQueryViewString();
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

        public static EntityData GetV_CostBudgetSetByGroupCode(string GroupCode, string ProjectCode, string SetType)
        {
            EntityData data2;
            try
            {
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.GroupCode, GroupCode));
                if (SetType != "")
                {
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.SetType, SetType));
                }
                builder.AddOrder("PBSType", true);
                builder.AddOrder("CostBudgetSetName", true);
                string queryString = builder.BuildQueryViewString();
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

        public static EntityData GetV_CostBudgetSetByProjectCode(string ProjectCode, string SetType)
        {
            EntityData data2;
            try
            {
                CostBudgetSetStrategyBuilder builder = new CostBudgetSetStrategyBuilder();
                builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, ProjectCode));
                if (SetType != "")
                {
                    builder.AddStrategy(new Strategy(CostBudgetSetStrategyName.SetType, SetType));
                }
                builder.AddOrder("CostBudgetSetName", true);
                string queryString = builder.BuildQueryViewString();
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

        public static void InsertCostBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudget"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetBackup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackup"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetBackupDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupDtl"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetBackupMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupMonth"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetBackupSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupSet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetContract"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtl"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetDtlHis(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtlHis"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetMonth"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostBudgetSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetSet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_CostBudget(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudget"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_CostBudgetBackup(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudgetBackup"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllCostBudget(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudget"))
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

        public static void SubmitAllCostBudgetBackup(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackup"))
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

        public static void SubmitAllCostBudgetBackupDtl(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupDtl"))
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

        public static void SubmitAllCostBudgetBackupMonth(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupMonth"))
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

        public static void SubmitAllCostBudgetBackupSet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupSet"))
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

        public static void SubmitAllCostBudgetContract(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetContract"))
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

        public static void SubmitAllCostBudgetDtl(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtl"))
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

        public static void SubmitAllCostBudgetDtlHis(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtlHis"))
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

        public static void SubmitAllCostBudgetMonth(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetMonth"))
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

        public static void SubmitAllCostBudgetSet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetSet"))
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

        public static void SubmitAllStandard_CostBudget(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudget"))
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

        public static void SubmitAllStandard_CostBudgetBackup(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudgetBackup"))
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

        public static void UpdateCostBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudget"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetBackup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackup"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetBackupDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupDtl"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetBackupMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupMonth"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetBackupSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetBackupSet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetContract(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetContract"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetDtl(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtl"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetDtlHis(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetDtlHis"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetMonth"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostBudgetSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostBudgetSet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_CostBudget(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudget"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_CostBudgetBackup(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CostBudgetBackup"))
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

