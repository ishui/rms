namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SalRule
    {
        public static string GetSalBudgetAfterPeriodDesc(DataRow dr)
        {
            string text2;
            try
            {
                string text = "";
                int num = ConvertRule.ToInt(dr["IYear"]);
                int num2 = ConvertRule.ToInt(dr["AfterPeriod"]);
                int num3 = num + 1;
                int num4 = num + num2;
                if (num2 > 0)
                {
                    text = string.Format("{0}年 到 {1}年", num3, num4);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetSalBudgetBeforePeriodSum(string ProjectCode, int IYear)
        {
            DataTable table2;
            try
            {
                SalBudgetDtlStrategyBuilder builder = new SalBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.ProjectCode, ProjectCode));
                ArrayList pas = new ArrayList();
                pas.Add("0");
                pas.Add(IYear.ToString());
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IYearRange, pas));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IMonth, "0"));
                string queryString = builder.BuildQuerySumString();
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

        public static string GetSalBudgetPeriodMonthDesc(DataRow dr)
        {
            string text2;
            try
            {
                int num = ConvertRule.ToInt(dr["IYear"]);
                int num2 = ConvertRule.ToInt(dr["PeriodMonth"]);
                int num3 = ConvertRule.ToInt(dr["StartMonth"]);
                int num4 = (num3 + num2) - 1;
                int num5 = num;
                if (num4 > 12)
                {
                    num5++;
                    num4 -= 12;
                }
                text2 = string.Format("{0}年{1}月 到 {2}年{3}月", new object[] { num, num3, num5, num4 });
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSalCBVoucherCode(string BuildingName, string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSalCBVoucherCode('{0}', '{1}')", BuildingName, ProjectCode));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSalContractVoucherCode(string ContractCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSalContractVoucherCode('{0}')", ContractCode));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSalJTVoucherCode(string BuildingName, string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSalJTVoucherCode('{0}', '{1}')", BuildingName, ProjectCode));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetSalPayItem(string SalPayCode, string ContractCode)
        {
            DataTable table2;
            try
            {
                SalPayPlanStrategyBuilder builder = new SalPayPlanStrategyBuilder();
                builder.AddStrategy(new Strategy(SalPayPlanStrategyName.ContractCode, ContractCode));
                builder.AddStrategy(new Strategy(SalPayPlanStrategyName.SalPayCode, SalPayCode));
                string queryString = builder.BuildMainQueryString();
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

        public static string GetSalPayItemName(string SalPayCode, string ContractCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((SalPayCode == "") || (ContractCode == ""))
                {
                    return text;
                }
                DataTable salPayItem = GetSalPayItem(SalPayCode, ContractCode);
                int count = salPayItem.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow row = salPayItem.Rows[i];
                    if (text != "")
                    {
                        text = text + ",";
                    }
                    text = text + ConvertRule.ToString(row["ItemName"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSalSuplNameBySuplCode(string SuplCode, string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSalSuplName('{0}', '{1}')", SuplCode, ProjectCode));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetSalTotalAreaByProjectBuilding(string ProjectCode, string BuildingName)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select sum(isnull(BuildDim, 0)) from SalContract where ProjectCode = '{0}' and BuildingName = '{1}'", ProjectCode, BuildingName));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        try
                        {
                            num = decimal.Parse(obj2.ToString());
                        }
                        catch
                        {
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetSalTotalCostByProjectBuilding(string ProjectCode, string BuildingName)
        {
            decimal num2;
            try
            {
                decimal @decimal = 0M;
                EntityData salCostByProjectBuilding = SalDAO.GetSalCostByProjectBuilding(ProjectCode, BuildingName);
                if (salCostByProjectBuilding.HasRecord())
                {
                    try
                    {
                        @decimal = salCostByProjectBuilding.GetDecimal("TotalCost");
                    }
                    catch
                    {
                    }
                }
                salCostByProjectBuilding.Dispose();
                num2 = @decimal;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }
    }
}

