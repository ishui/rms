namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class CBSDAO
    {
        public static void DeleteBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
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

        public static void DeleteBudgetCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
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

        public static void DeleteBudgetMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetMonth"))
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

        public static void DeleteBudgetYear(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYear"))
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

        public static void DeleteBudgetYearName(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYearName"))
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

        public static void DeleteCBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
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

        public static void DeleteCBSModule(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSModule"))
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

        public static void DeleteCBSPerson(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSPerson"))
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

        public static void DeleteCBSTemplet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSTemplet"))
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

        public static void DeleteCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Cost"))
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

        public static void DeleteCostEstimateCheck(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostEstimateCheck"))
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

        public static void DeleteCostPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostPlan"))
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

        public static void DeleteDynamicCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DynamicCost"))
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

        public static void DeleteInvestment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Investment"))
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

        public static void DeleteInvestVoucher(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("InvestVoucher"))
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

        public static void DeleteReviseBudgetCheck(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ReviseBudgetCheck"))
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

        public static void DeleteStandard_Budget(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Budget"))
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

        public static void DeleteStandard_CBS(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CBS"))
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

        public static void DeleteStandard_Investment(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Investment"))
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

        public static void DeleteStandard_Templet(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Templet"))
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

        public static void DeleteTemplet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Templet"))
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

        public static EntityData GetAllBudget()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
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

        public static EntityData GetAllBudgetCost()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
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

        public static EntityData GetAllBudgetMonth()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetMonth"))
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

        public static EntityData GetAllBudgetYear()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYear"))
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

        public static EntityData GetAllBudgetYearName()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYearName"))
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

        public static EntityData GetAllCBSModule()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSModule"))
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

        public static EntityData GetAllCBSPerson()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSPerson"))
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

        public static EntityData GetAllCBSTemplet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSTemplet"))
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

        public static EntityData GetAllCost()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Cost"))
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

        public static EntityData GetAllCostEstimateCheck()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostEstimateCheck"))
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

        public static EntityData GetAllCostPlan()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostPlan"))
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

        public static EntityData GetAllDynamicCost()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DynamicCost"))
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

        public static EntityData GetAllInvestment()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Investment"))
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

        public static EntityData GetAllInvestVoucher()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("InvestVoucher"))
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

        public static EntityData GetAllReviseBudgetCheck()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ReviseBudgetCheck"))
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

        public static EntityData GetAllTemplet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Templet"))
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

        public static EntityData GetBudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
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

        public static EntityData GetBudgetByProject(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Budget");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { projectCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Budget", "SelectByProjectCode").GetSqlStringWithOrder(), Params, values, entitydata, "Budget");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBudgetCostByBudgetCode(string budgetCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BudgetCost");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BudgetCost", "SelectByBudgetCode").SqlString, "@BudgetCode", budgetCode, entitydata, "BudgetCost");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBudgetCostByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
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

        public static EntityData GetBudgetMonthByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetMonth"))
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

        public static EntityData GetBudgetYearByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYear"))
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

        public static EntityData GetBudgetYearNameByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYearName"))
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

        public static EntityData GetCBSByBudgetCodeAccountPoint(string budgetCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("CBS");
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
                {
                    string[] Params = new string[] { "@BudgetCode" };
                    object[] values = new object[] { budgetCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBS", "SelectCBSByBudgetCostAccountPoint").GetSqlStringWithOrder(), Params, values, entitydata, "CBS");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCBSByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
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

        public static EntityData GetCBSByCostCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("CBS");
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBS", "Select").SqlString, "@CostCode", code, entitydata, "CBS");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCBSByParentCode(string ParentCode)
        {
            EntityData data2;
            try
            {
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ParentCode, ParentCode));
                string queryString = builder.BuildQueryChildCountString();
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

        public static EntityData GetCBSByProject(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("CBS");
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { projectCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBS", "SelectByProjectCode").GetSqlStringWithOrder(), Params, values, entitydata, "CBS");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCBSBySortID(string SortID, string ProjectCode)
        {
            EntityData data2;
            try
            {
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CBSStrategyName.SortID, SortID));
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

        public static string GetCBSFullCode(string costCode)
        {
            string text2;
            if (costCode == "")
            {
                return "";
            }
            try
            {
                string text = "";
                EntityData cBSByCode = GetCBSByCode(costCode);
                if (cBSByCode.HasRecord())
                {
                    text = cBSByCode.GetString("FullCode");
                }
                cBSByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData getCbsInCostCode(string allCostCode, string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("CBS");
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@AllCostCode" };
                    object[] values = new object[] { projectCode, allCostCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBS", "SelectCbsInCostCode").SqlString, Params, values, entitydata, "CBS");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCBSModuleByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSModule"))
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

        public static EntityData GetCBSPersonByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSPerson"))
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

        public static EntityData GetCBSRootByProject(string ProjectCode)
        {
            EntityData data2;
            try
            {
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(CBSStrategyName.ParentCode, ""));
                string queryString = builder.BuildQueryChildCountString();
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

        public static EntityData GetCBSTempletByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSTemplet"))
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

        public static EntityData GetCostByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Cost"))
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

        public static EntityData GetCostByProject(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Cost");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Cost"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { projectCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Cost", "SelectByProjectCode").GetSqlStringWithOrder(), Params, values, entitydata, "Cost");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCostEstimateCheckByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostEstimateCheck"))
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

        public static EntityData GetCostPlanByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostPlan"))
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

        public static EntityData GetInvestmentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Investment"))
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

        public static EntityData GetInvestVoucherByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("InvestVoucher"))
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

        public static EntityData GetReviseBudgetCheckByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ReviseBudgetCheck"))
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

        public static EntityData GetReviseBudgetCheckByProject(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("ReviseBudgetCheck");
                using (SingleEntityDAO ydao = new SingleEntityDAO("ReviseBudgetCheck"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("ReviseBudgetCheck", "SelectByProjectCode").SqlString, "@ProjectCode", projectCode, entitydata, "ReviseBudgetCheck");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_BudgetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Budget");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Budget"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Budget", "Select").GetSqlStringWithOrder(), "@BudgetCode", code, entitydata, "Budget");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BudgetYear", "SelectByBudgetCode").GetSqlStringWithOrder(), "@BudgetCode", code, entitydata, "BudgetYear");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BudgetMonth", "SelectByBudgetCode").GetSqlStringWithOrder(), "@BudgetCode", code, entitydata, "BudgetMonth");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BudgetCost", "SelectByBudgetCode").GetSqlStringWithOrder(), "@BudgetCode", code, entitydata, "BudgetCost");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_CBSByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_CBS");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CBS"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBS", "Select").GetSqlStringWithOrder(), "@CostCode", code, entitydata, "CBS");
                    ydao.FillEntity(SqlManager.GetSqlStruct("Cost", "SelectByCostCode").SqlString, "@CostCode", code, entitydata, "Cost");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBSPerson", "SelectByCostCode").SqlString, "@CostCode", code, entitydata, "CBSPerson");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CostBudgetDtl", "SelectByCostCode").SqlString, "@CostCode", code, entitydata, "CostBudgetDtl");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_InvestmentByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Investment");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Investment"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Investment", "Select").GetSqlStringWithOrder(), "@InvestCode", code, entitydata, "Investment");
                    ydao.FillEntity(SqlManager.GetSqlStruct("InvestVoucher", "SelectByInvestCode").SqlString, "@InvestCode", code, entitydata, "InvestVoucher");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_TempletByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Templet");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Templet"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Templet", "Select").SqlString, "@TempletCode", code, entitydata, "Templet");
                    ydao.FillEntity(SqlManager.GetSqlStruct("CBSTemplet", "SelectByTempletCode").SqlString, "@TempletCode", code, entitydata, "CBSTemplet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempletByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Templet"))
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

        public static EntityData GetTempletByType(string type)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Templet");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Templet"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Templet", "SelectByType").SqlString, "@TempletType", type, entitydata, "Templet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBudgetCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBudgetMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetMonth"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBudgetYear(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYear"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBudgetYearName(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYearName"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCBSModule(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSModule"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCBSPerson(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSPerson"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCBSTemplet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSTemplet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Cost"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostEstimateCheck(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostEstimateCheck"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertCostPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostPlan"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertDynamicCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DynamicCost"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertInvestment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Investment"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertInvestVoucher(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("InvestVoucher"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertReviseBudgetCheck(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ReviseBudgetCheck"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Budget(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Budget"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_CBS(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CBS"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Investment(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Investment"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Templet(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Templet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTemplet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Templet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBudget(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
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

        public static void SubmitAllBudgetCost(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
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

        public static void SubmitAllBudgetMonth(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetMonth"))
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

        public static void SubmitAllBudgetYear(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYear"))
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

        public static void SubmitAllBudgetYearName(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYearName"))
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

        public static void SubmitAllCBS(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
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

        public static void SubmitAllCBSModule(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSModule"))
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

        public static void SubmitAllCostPlan(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostPlan"))
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

        public static void SubmitAllDynamicCost(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("DynamicCost"))
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

        public static void SubmitAllInvestment(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Investment"))
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

        public static void SubmitAllInvestVoucher(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("InvestVoucher"))
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

        public static void SubmitAllStandard_Budget(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Budget"))
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

        public static void SubmitAllStandard_CBS(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CBS"))
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

        public static void SubmitAllStandard_Investment(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Investment"))
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

        public static void SubmitAllStandard_Templet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Templet"))
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

        public static void UpdateBudget(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Budget"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBudgetCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetCost"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBudgetMonth(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetMonth"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBudgetYear(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYear"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBudgetYearName(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BudgetYearName"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBS"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCBSModule(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSModule"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCBSPerson(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSPerson"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCBSTemplet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CBSTemplet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Cost"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostEstimateCheck(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostEstimateCheck"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCostPlan(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("CostPlan"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateDynamicCost(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DynamicCost"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateInvestment(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Investment"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateInvestVoucher(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("InvestVoucher"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateReviseBudgetCheck(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ReviseBudgetCheck"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Budget(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Budget"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_CBS(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_CBS"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Investment(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Investment"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Templet(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Templet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTemplet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Templet"))
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

