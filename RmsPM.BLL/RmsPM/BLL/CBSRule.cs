namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class CBSRule
    {
        public const int IMaxMonth = 12;
        public const int IMaxPeriod = 10;

        public static void AdCostEstimate(EntityData cbs, EntityData budget, int iYear, int iMonth, int periodMonth, int afterPeriod, string budgetCode, string lastPeriodLastDate, string projectCode)
        {
            DataRow[] rowArray = cbs.CurrentTable.Select("ChildCount>0", "Deep DESC");
            int length = rowArray.Length;
            for (int i = 0; i < length; i++)
            {
                string text = (string) rowArray[i]["CostCode"];
                int num3 = 0;
                DataRow[] rowArray2 = budget.Tables["BudgetCost"].Select(string.Format("CostCode='{0}'", text));
                if ((rowArray2.Length > 0) && !rowArray2[0].IsNull("AccountPoint"))
                {
                    num3 = (int) rowArray2[0]["AccountPoint"];
                }
                if (num3 == 2)
                {
                    SumChild(cbs, budget, text, iYear, iMonth, periodMonth, afterPeriod, budgetCode, lastPeriodLastDate, projectCode);
                }
            }
            SumTotalMoney(cbs, budget);
        }

        public static void AdCostEstimate(string costCode, string type, EntityData cbs, EntityData budget, int iYear, int iMonth, int periodMonth, int afterPeriod, string budgetCode, string lastPeriodLastDate, string projectCode)
        {
            string[] textArray = CBSDAO.GetCBSFullCode(costCode).Split(new char[] { '-' });
            for (int i = textArray.Length - 1; i >= 0; i--)
            {
                string text2 = textArray[i];
                int num2 = 2;
                if ((i == (textArray.Length - 1)) && (type == ""))
                {
                    num2 = 1;
                }
                SumChild(cbs, budget, text2, iYear, iMonth, periodMonth, afterPeriod, budgetCode, lastPeriodLastDate, projectCode);
                foreach (DataRow row in budget.Tables["BudgetCost"].Select(string.Format("CostCode='{0}'", text2)))
                {
                    row["AccountPoint"] = num2;
                }
            }
            SumTotalMoney(cbs, budget);
        }

        public static DataTable BuildAdjustStringTable(EntityData budget, EntityData refBudget)
        {
            DataTable table = new DataTable();
            table.Columns.Add("CostCode");
            table.Columns.Add("CostName");
            table.Columns.Add("AdjustString");
            int @int = refBudget.GetInt("IYear");
            int num2 = refBudget.GetInt("IMonth");
            int num3 = refBudget.GetInt("PeriodMonth");
            int num4 = refBudget.GetInt("AfterPeriod");
            int num5 = budget.GetInt("IDynamicStartMonth");
            foreach (DataRow row in budget.Tables["BudgetCost"].Select("IsModify=1"))
            {
                int num10;
                string text = (string) row["CostCode"];
                string text2 = "";
                decimal num6 = 0M;
                DateTime time = DateTime.Parse(string.Format("{0}-{1}-1", @int, num2));
                decimal num7 = 0M;
                DataRow[] rowArray = refBudget.Tables["BudgetCost"].Select(string.Format("CostCode='{0}'", text));
                if ((rowArray.Length > 0) && !rowArray[0].IsNull("SurplusCost"))
                {
                    num7 = (decimal) rowArray[0]["SurplusCost"];
                }
                decimal num = 0M;
                if (!row.IsNull("SurplusCost"))
                {
                    num = (decimal) row["SurplusCost"];
                }
                decimal num9 = MathRule.SumColumn(refBudget.Tables["BudgetMonth"], "Money", string.Format("IMonth<{0} and CostCode='{1}'", num5, text)) - GetAHMoney(text, time.ToString("yyyy-MM-dd"), DateTime.Parse(DateTime.Now.ToString("yyyy-MM-1")).AddDays(-1).ToString("yyyy-MM-dd"));
                text2 = (text2 + "本月前预算累计节余：" + StringRule.BuildMoneyWanFormatString(num7 + num9, 1) + "<br>") + "调整后节余：" + StringRule.BuildMoneyWanFormatString(num, 1) + "<br>";
                DataRow row2 = table.NewRow();
                row2["CostCode"] = text;
                row2["CostName"] = GetCostName(text);
                for (num10 = num5; num10 <= num3; num10++)
                {
                    decimal num11 = MathRule.SumColumn(budget.Tables["BudgetMonth"], "Money", string.Format("IMonth={0} and CostCode='{1}'", num10, text));
                    decimal num12 = MathRule.SumColumn(refBudget.Tables["BudgetMonth"], "Money", string.Format("IMonth={0} and CostCode='{1}'", num10, text));
                    if (!MathRule.CheckDecimalEqual(num11, num12))
                    {
                        text2 = ((text2 + GetBudgetMonthString(@int, num2, num10) + "：") + "原预算： <font color red>" + StringRule.BuildMoneyWanFormatString(num12, 1) + "</font> ，") + "新预算：<font color blue>" + StringRule.BuildMoneyWanFormatString(num11, 1) + "</font> ，";
                        if (num11 > num12)
                        {
                            text2 = text2 + "增加：";
                        }
                        else
                        {
                            text2 = text2 + "减少：";
                        }
                        text2 = text2 + StringRule.BuildMoneyWanFormatString(Math.Abs((decimal) (num11 - num12)), 1) + "。 <br> ";
                        num6 += num11 - num12;
                    }
                }
                for (num10 = 1; num10 <= num4; num10++)
                {
                    decimal num13 = MathRule.SumColumn(budget.Tables["BudgetYear"], "Money", string.Format("IYear={0} and CostCode='{1}'", num10, text));
                    decimal num14 = MathRule.SumColumn(refBudget.Tables["BudgetYear"], "Money", string.Format("IYear={0} and CostCode='{1}'", num10, text));
                    if (!MathRule.CheckDecimalEqual(num13, num14))
                    {
                        text2 = ((text2 + ((@int + num10)).ToString() + "年：") + "原预算：<font color red>" + StringRule.BuildMoneyWanFormatString(num14, 1) + "</font> ，") + "新预算：<font color blue>" + StringRule.BuildMoneyWanFormatString(num13, 1) + "</font> ，";
                        if (num13 > num14)
                        {
                            text2 = text2 + "增加：";
                        }
                        else
                        {
                            text2 = text2 + "减少：";
                        }
                        text2 = text2 + StringRule.BuildMoneyWanFormatString(Math.Abs((decimal) (num13 - num14)), 1) + "。 <br> ";
                        num6 += num13 - num14;
                    }
                }
                decimal num16 = (num6 - (num9 + num7)) - num;
                if (num16 > 0M)
                {
                    text2 = text2 + "预算总体增加：<font color=red>" + StringRule.BuildMoneyWanFormatString(num16, 1) + "</font> 。<br>";
                }
                else if (num16 < 0M)
                {
                    text2 = text2 + "预算总体节余：<font color=blue>" + StringRule.BuildMoneyWanFormatString(Math.Abs(num16), 1) + "</font>。<br>";
                }
                else
                {
                    text2 = text2 + "总额没有变化。<br>";
                }
                row2["AdjustString"] = text2;
                table.Rows.Add(row2);
            }
            return table;
        }

        public static bool CanDeleteCBS(string CostCode, ref string hint)
        {
            try
            {
                hint = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select top 1 1 from ContractCost where CostCode = '{0}'", CostCode);
                    if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                    {
                        hint = "费用项已有合同，不能删除";
                        return false;
                    }
                    queryString = string.Format("select top 1 1 from ContractCostChange where CostCode = '{0}'", CostCode);
                    if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                    {
                        hint = "费用项已有合同变更，不能删除";
                        return false;
                    }
                    queryString = string.Format("select top 1 1 from PaymentItem where CostCode = '{0}'", CostCode);
                    if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                    {
                        hint = "费用项已有请款，不能删除";
                        return false;
                    }
                    queryString = string.Format("select top 1 1 from CostBudgetDtl a, CostBudget b where a.CostCode = '{0}' and isnull(a.BudgetMoney, 0) <> 0 and a.CostBudgetCode = b.CostBudgetCode and b.TargetFlag = 1", CostCode);
                    if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                    {
                        hint = "费用项已有目标费用，不能删除";
                        return false;
                    }
                    queryString = string.Format("select top 1 1 from CostBudgetDtl a, CostBudget b where a.CostCode = '{0}' and IsNull(a.BudgetMoney, 0) <> 0 and a.CostBudgetCode = b.CostBudgetCode and isnull(b.TargetFlag, 0) = 0", CostCode);
                    if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                    {
                        hint = "费用项已有预留金额，不能删除";
                        return false;
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
            return true;
        }

        public static bool CheckCBSLeafNode(string costCode)
        {
            bool flag2;
            try
            {
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.AllSubNodeIncludeSelf, costCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("CBS", queryString);
                agent.Dispose();
                bool flag = data.CurrentTable.Rows.Count == 1;
                data.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void ClearBudgetData(EntityData budget, string costCodeTemp)
        {
            DataRow[] rowArray = budget.Tables["BudgetMonth"].Select(string.Format(" CostCode='{0}' ", costCodeTemp));
            foreach (DataRow row in rowArray)
            {
                row["Money"] = 0M;
            }
            DataRow[] rowArray2 = budget.Tables["BudgetYear"].Select(string.Format(" CostCode='{0}' ", costCodeTemp));
            foreach (DataRow row2 in rowArray2)
            {
                row2["Money"] = 0M;
            }
            DataRow[] rowArray3 = budget.Tables["BudgetCost"].Select(string.Format(" CostCode='{0}' ", costCodeTemp));
            if (rowArray3.Length > 0)
            {
                rowArray3[0]["BudgetCost"] = 0M;
                rowArray3[0]["BeforeHappenCost"] = 0M;
                rowArray3[0]["CurrentPlanCost"] = 0M;
                rowArray3[0]["AfterPlanCost"] = 0M;
                rowArray3[0]["SurplusCost"] = 0M;
            }
        }

        public static void DeleteCBS(string CostCode)
        {
            try
            {
                EntityData entity = CBSDAO.GetCBSByCode(CostCode);
                if (!entity.HasRecord())
                {
                    throw new Exception("费用项不存在");
                }
                string text = entity.GetString("ProjectCode");
                string text2 = entity.GetString("FullCode");
                entity.Dispose();
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ProjectCode, text));
                builder.AddStrategy(new Strategy(CBSStrategyName.FullCodeInLike, text2, "L"));
                builder.AddOrder("SortID", true);
                builder.AddOrder("Deep", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                entity = agent.FillEntityData("CBS", queryString);
                agent.Dispose();
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    string text4 = ConvertRule.ToString(row["CostCode"]);
                    string hint = "";
                    if (!CanDeleteCBS(CostCode, ref hint))
                    {
                        throw new Exception(hint);
                    }
                }
                entity.Dispose();
                entity = CBSDAO.GetCBSByProject(text);
                DeleteCBSNode(entity, CostCode);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteCBSNode(EntityData entity, string CostCode)
        {
            try
            {
                DataRow[] rowArray = entity.CurrentTable.Select(string.Format(" ParentCode = '{0}' ", CostCode));
                int length = rowArray.Length;
                for (int i = 0; i < length; i++)
                {
                    string costCode = (string) rowArray[i]["CostCode"];
                    DeleteCBSNode(entity, costCode);
                }
                DeleteStandardCBS(CostCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandardCBS(string CostCode)
        {
            try
            {
                EntityData entity = CBSDAO.GetStandard_CBSByCode(CostCode);
                CBSDAO.DeleteStandard_CBS(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAccessCBS(string projectCode, string userCode, string stationCodes)
        {
            EntityData data;
            try
            {
                data = GetAccessCostOperation(projectCode, userCode, stationCodes, "040101", true);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }

        public static EntityData GetAccessCostOperation(string projectCode, string userCode, string stationCodes, string operationCode, bool isBu)
        {
            EntityData data3;
            try
            {
                QueryAgent agent = new QueryAgent();
                CBSStrategyBuilder builder = new CBSStrategyBuilder();
                builder.AddStrategy(new Strategy(CBSStrategyName.ProjectCode, projectCode));
                ArrayList pas = new ArrayList();
                pas.Add(operationCode);
                pas.Add(userCode);
                pas.Add(stationCodes);
                builder.AddStrategy(new Strategy(CBSStrategyName.AccessRange, pas));
                string queryString = builder.BuildMainQueryString();
                EntityData entity = agent.FillEntityData("CBS", queryString);
                entity.CurrentTable.Columns.Add("CanAccess", Type.GetType("System.String"));
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    row["CanAccess"] = "1";
                }
                if (isBu)
                {
                    EntityData refEntity = CBSDAO.GetCBSByProject(projectCode);
                    int count = entity.CurrentTable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataRow row = entity.CurrentTable.Rows[i];
                        string costCode = row["CostCode"].ToString();
                        string parentCode = row["ParentCode"].ToString();
                        int num3 = (int) row["Deep"];
                        if (num3 > 1)
                        {
                            ImportCostRow(entity, refEntity, costCode, parentCode);
                        }
                    }
                    refEntity.Dispose();
                }
                agent.Dispose();
                data3 = entity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data3;
        }

        public static EntityData GetAccessV_CBSCost(string projectCode, string userCode, string stationCodes, bool isBu, string fromNodeCode)
        {
            EntityData data3;
            try
            {
                string text = "-1";
                string entityName = "V_CBSCost";
                V_CBSCostStrategyBuilder builder = new V_CBSCostStrategyBuilder();
                builder.AddStrategy(new Strategy(V_CBSCostStrategyName.ProjectCode, projectCode));
                builder.AddStrategy(new Strategy(V_CBSCostStrategyName.Flag, text));
                QueryAgent agent = new QueryAgent();
                EntityData refEntity = agent.FillEntityData(entityName, builder.BuildMainQueryString());
                if (fromNodeCode != "")
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(fromNodeCode);
                    pas.Add("0");
                    builder.AddStrategy(new Strategy(V_CBSCostStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                ArrayList list2 = new ArrayList();
                list2.Add("040201");
                list2.Add(userCode);
                list2.Add(stationCodes);
                builder.AddStrategy(new Strategy(V_CBSCostStrategyName.AccessRange, list2));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                EntityData entity = agent.FillEntityData(entityName, queryString);
                entity.CurrentTable.Columns.Add("CanAccess", Type.GetType("System.String"));
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    row["CanAccess"] = "1";
                }
                if (isBu)
                {
                    int count = entity.CurrentTable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataRow row = entity.CurrentTable.Rows[i];
                        string costCode = row["CostCode"].ToString();
                        string parentCode = row["ParentCode"].ToString();
                        int num3 = (int) row["Deep"];
                        if (num3 > 1)
                        {
                            ImportCostRow(entity, refEntity, costCode, parentCode);
                        }
                    }
                }
                agent.Dispose();
                refEntity.Dispose();
                data3 = entity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data3;
        }

        public static decimal GetAHCash(string pm_sCostCode, string pm_sContractCode, string pm_sIsContract, string pm_sContractCostCashCode)
        {
            return GetAHCash(pm_sCostCode, "", "", pm_sContractCode, pm_sIsContract, pm_sContractCostCashCode);
        }

        public static decimal GetAHCash(string pm_sCostCode, string pm_sStartDate, string pm_sEndDate, string pm_sContractCode, string pm_sIsContract, string pm_sContractCostCashCode)
        {
            decimal num2;
            try
            {
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                if (pm_sCostCode != "")
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(pm_sCostCode);
                    pas.Add("0");
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                if ((pm_sEndDate != "") || (pm_sStartDate != ""))
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(pm_sStartDate);
                    list2.Add(pm_sEndDate);
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, list2));
                }
                if (pm_sIsContract != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.IsContract, pm_sIsContract));
                }
                if (pm_sContractCode != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, pm_sContractCode));
                }
                if (pm_sContractCostCashCode != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCostCashCode, pm_sContractCostCashCode));
                }
                string queryString = builder.BuildQuerySumCashString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static EntityData GetAHEntity(string costCode, string startDate, string endDate, string contractCode, string isContract)
        {
            EntityData data2;
            try
            {
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                if (costCode != "")
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(costCode);
                    pas.Add("0");
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                if ((endDate != "") || (startDate != ""))
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(startDate);
                    list2.Add(endDate);
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, list2));
                }
                if (isContract != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.IsContract, isContract));
                }
                if (contractCode != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, contractCode));
                }
                builder.AddOrder("PayDate", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("V_PaymentItem", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static decimal GetAHMoney(string costCode, string startDate, string endDate)
        {
            decimal num;
            try
            {
                num = GetAHMoney(costCode, startDate, endDate, "", "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static decimal GetAHMoney(string costCode, string startDate, string endDate, string contractCode, string isContract)
        {
            decimal num;
            try
            {
                num = GetAHMoney(costCode, startDate, endDate, contractCode, isContract, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static decimal GetAHMoney(string costCode, string startDate, string endDate, string contractCode, string isContract, string allocateCode)
        {
            decimal num2;
            try
            {
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                if (costCode != "")
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(costCode);
                    pas.Add("0");
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                if ((endDate != "") || (startDate != ""))
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(startDate);
                    list2.Add(endDate);
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, list2));
                }
                if (isContract != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.IsContract, isContract));
                }
                if (contractCode != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, contractCode));
                }
                if (allocateCode != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.AllocateCode, allocateCode));
                }
                string queryString = builder.BuildQuerySumMoneyString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetAPCash(string pm_sContractCode, string pm_sContractCostCashCode)
        {
            decimal num2;
            try
            {
                PayoutItemStrategyBuilder builder = new PayoutItemStrategyBuilder("V_PayoutItem");
                if (pm_sContractCostCashCode != "")
                {
                    builder.AddStrategy(new Strategy(PayoutItemStrategyName.ContractCostCashCode, pm_sContractCostCashCode));
                }
                if (PaymentRule.IsPayoutMoneyIncludeNotCheck == 0)
                {
                    builder.AddStrategy(new Strategy(PayoutItemStrategyName.Status, "1,2"));
                }
                string queryString = builder.BuildQuerySumCashString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetAPCost(string costCode, string startDate, string endDate)
        {
            return GetAPCost(costCode, startDate, endDate, "", "");
        }

        public static decimal GetAPCost(string costCode, string startDate, string endDate, string contractCode, string isContract)
        {
            decimal num;
            try
            {
                num = GetAPCost(costCode, startDate, endDate, contractCode, isContract, "1,2");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static decimal GetAPCost(string costCode, string startDate, string endDate, string contractCode, string isContract, string paymentStatus)
        {
            decimal num2;
            try
            {
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                ArrayList pas = new ArrayList();
                pas.Add(costCode);
                pas.Add("0");
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                if (paymentStatus != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, paymentStatus));
                }
                if (isContract != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.IsContract, isContract));
                }
                if (contractCode != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, contractCode));
                }
                if ((endDate != "") || (startDate != ""))
                {
                    ArrayList list2 = new ArrayList();
                    list2.Add(startDate);
                    list2.Add(endDate);
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, list2));
                }
                string queryString = builder.BuildQuerySumMoneyString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetAPMoney(string contractCode, string allocateCode)
        {
            decimal num2;
            try
            {
                PayoutItemStrategyBuilder builder = new PayoutItemStrategyBuilder("V_PayoutItem");
                if (allocateCode != "")
                {
                    builder.AddStrategy(new Strategy(PayoutItemStrategyName.AllocateCode, allocateCode));
                }
                string queryString = builder.BuildQuerySumMoneyString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetApplyContractAllocationCost(string costCode, string contractCode, string projectCode, string payStartDate, string payEndDate)
        {
            return GetContractAllocationCost(costCode, contractCode, projectCode, payStartDate, payEndDate, "1");
        }

        public static string GetBudgetMonthString(int iBudgetStartYear, int iBudgetStartMonth, int iMonth)
        {
            int num = (iBudgetStartMonth + iMonth) - 1;
            int num2 = iBudgetStartYear;
            if (num > 12)
            {
                num2++;
                num -= 12;
            }
            return (num2.ToString() + "年" + num.ToString() + "月");
        }

        public static string GetCBSFullCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData cBSByCode = CBSDAO.GetCBSByCode(code);
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

        public static string GetCBSParentCode(string costCode)
        {
            string text = "";
            try
            {
                EntityData cBSByCode = CBSDAO.GetCBSByCode(costCode);
                if (cBSByCode.HasRecord())
                {
                    text = cBSByCode.GetString("ParentCode");
                }
                cBSByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string GetCBSSubjectCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData cBSByCode = CBSDAO.GetCBSByCode(code);
                if (cBSByCode.HasRecord())
                {
                    text = cBSByCode.GetString("SubjectCode");
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

        public static decimal GetContractAllocationAHMoney(string startDate, string endDate, string allocateCode)
        {
            decimal num;
            try
            {
                num = GetAHMoney("", startDate, endDate, "", "", allocateCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static decimal GetContractAllocationCost(string costCode, string contractCode, string projectCode, string payStartDate, string payEndDate)
        {
            return GetContractAllocationCost(costCode, contractCode, projectCode, payStartDate, payEndDate, "0,2");
        }

        public static decimal GetContractAllocationCost(string costCode, string contractCode, string projectCode, string payStartDate, string payEndDate, string contractStatus)
        {
            decimal num2;
            try
            {
                ArrayList pas;
                ContractAllocationStrategyBuilder builder = new ContractAllocationStrategyBuilder();
                if (costCode != "")
                {
                    pas = new ArrayList();
                    pas.Add(costCode);
                    pas.Add("0");
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                if (contractStatus != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractStatus, contractStatus));
                }
                if (contractCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractCode, contractCode));
                }
                if ((payStartDate != "") || (payEndDate != ""))
                {
                    pas = new ArrayList();
                    pas.Add(payStartDate);
                    pas.Add(payEndDate);
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.PlanningPayDate, pas));
                }
                if (projectCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ProjectCode, projectCode));
                }
                string queryString = builder.BuildQuerySumString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static DataSet GetContractAllocationDataSet(string costCode, string contractCode, string projectCode, string payStartDate, string payEndDate, string contractStatus)
        {
            DataSet set2;
            try
            {
                ArrayList pas;
                ContractAllocationStrategyBuilder builder = new ContractAllocationStrategyBuilder();
                if (costCode != "")
                {
                    pas = new ArrayList();
                    pas.Add(costCode);
                    pas.Add("1");
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                if (contractStatus != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractStatus, contractStatus));
                }
                if (contractCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractCode, contractCode));
                }
                if ((payStartDate != "") || (payEndDate != ""))
                {
                    pas = new ArrayList();
                    pas.Add(payStartDate);
                    pas.Add(payEndDate);
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.PlanningPayDate, pas));
                }
                if (projectCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ProjectCode, projectCode));
                }
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                DataSet set = agent.ExecSqlForDataSet(queryString);
                agent.Dispose();
                set2 = set;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static decimal GetContractAllocationHappenedCost(string costCode, string contractCode, string projectCode, string payStartDate, string payEndDate)
        {
            decimal num2;
            try
            {
                ArrayList pas;
                ContractAllocationStrategyBuilder builder = new ContractAllocationStrategyBuilder();
                if (costCode != "")
                {
                    pas = new ArrayList();
                    pas.Add(costCode);
                    pas.Add("1");
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                }
                builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractStatus, "0,2"));
                if (contractCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractCode, contractCode));
                }
                if ((payStartDate != "") || (payEndDate != ""))
                {
                    pas = new ArrayList();
                    pas.Add(payStartDate);
                    pas.Add(payEndDate);
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.PlanningPayDate, pas));
                }
                if (projectCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ProjectCode, projectCode));
                }
                string text = builder.BuildQueryKeyString();
                PaymentItemStrategyBuilder builder2 = new PaymentItemStrategyBuilder();
                builder2.AddStrategy(new Strategy(PaymentItemStrategyName.AllocateCodeKeyIn, text));
                builder2.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                string queryString = builder2.BuildQuerySumMoneyString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetCostAccountPointCode(string projectCode, string budgetCode, string costCode)
        {
            string text = "";
            try
            {
                EntityData budgetCostByBudgetCode = CBSDAO.GetBudgetCostByBudgetCode(budgetCode);
                EntityData cBSByProject = CBSDAO.GetCBSByProject(projectCode);
                string[] textArray = CBSDAO.GetCBSFullCode(costCode).Split(new char[] { '-' });
                for (int i = textArray.Length - 1; i >= 0; i--)
                {
                    string text3 = textArray[i];
                    DataRow row = budgetCostByBudgetCode.CurrentTable.Select(string.Format("CostCode='{0}'", text3))[0];
                    int num2 = 2;
                    if (!row.IsNull("AccountPoint"))
                    {
                        num2 = (int) row["AccountPoint"];
                    }
                    if (num2 == 1)
                    {
                        text = text3;
                        break;
                    }
                }
                cBSByProject.Dispose();
                budgetCostByBudgetCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static EntityData GetCostEstimate(string projectCode)
        {
            EntityData data2;
            try
            {
                V_CBSCostStrategyBuilder builder = new V_CBSCostStrategyBuilder();
                builder.AddStrategy(new Strategy(V_CBSCostStrategyName.ProjectCode, projectCode));
                builder.AddStrategy(new Strategy(V_CBSCostStrategyName.Flag, "-1"));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("V_CBSCost", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetCostFullName(string code)
        {
            string costFullName;
            try
            {
                costFullName = GetCostFullName(code, false);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return costFullName;
        }

        public static string GetCostFullName(string code, bool isShowRootName)
        {
            string text3;
            try
            {
                char[] separator = "-".ToCharArray();
                string text = "";
                if (code == "")
                {
                    return text;
                }
                string[] textArray = GetCBSFullCode(code).Split(separator);
                int num = 0;
                if (!isShowRootName)
                {
                    num = 1;
                }
                for (int i = num; i < textArray.Length; i++)
                {
                    if (text != "")
                    {
                        text = text + "->";
                    }
                    text = text + GetCostName(textArray[i]);
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetCostName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData cBSByCode = CBSDAO.GetCBSByCode(code);
                if (cBSByCode.HasRecord())
                {
                    text = cBSByCode.GetString("CostName");
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

        public static DataTable GetCostRelationContract(string costCode, string projectCode)
        {
            return GetCostRelationContract(costCode, projectCode, -1);
        }

        public static DataTable GetCostRelationContract(string costCode, string projectCode, int topNumber)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("ContractCode", Type.GetType("System.String"));
                table.Columns.Add("ContractName", Type.GetType("System.String"));
                table.Columns.Add("ContractTotalMoney", Type.GetType("System.Decimal"));
                table.Columns.Add("ContractCostMoney", Type.GetType("System.Decimal"));
                table.Columns.Add("ContractPayed", Type.GetType("System.Decimal"));
                ContractAllocationStrategyBuilder builder = new ContractAllocationStrategyBuilder();
                if (projectCode != "")
                {
                    builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ProjectCode, projectCode));
                }
                builder.AddStrategy(new Strategy(ContractAllocationStrategyName.ContractStatus, "0,2"));
                ArrayList pas = new ArrayList();
                pas.Add(costCode);
                pas.Add("1");
                builder.AddStrategy(new Strategy(ContractAllocationStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                if (topNumber > 0)
                {
                    agent.SetTopNumber(topNumber);
                }
                EntityData data = agent.FillEntityData("ContractAllocation", queryString);
                agent.Dispose();
                foreach (DataRow row in data.CurrentTable.Rows)
                {
                    string text2 = (string) row["ContractCode"];
                    if (table.Select(string.Format("ContractCode='{0}'", text2)).Length == 0)
                    {
                        DataRow row2 = table.NewRow();
                        row2["ContractCode"] = text2;
                        row2["ContractName"] = row["ContractName"];
                        row2["ContractTotalMoney"] = row["TotalMoney"];
                        row2["ContractCostMoney"] = MathRule.SumColumn(data.CurrentTable, "Money", string.Format("ContractCode='{0}'  ", text2));
                        decimal num = GetAHMoney(costCode, "", "", text2, "1");
                        row2["ContractPayed"] = num;
                        table.Rows.Add(row2);
                    }
                }
                data.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetCostRelationNoContractPayment(string costCode, int topNumber)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("PaymentCode", Type.GetType("System.String"));
                table.Columns.Add("PaymentTotalMoney", Type.GetType("System.Decimal"));
                table.Columns.Add("PaymentCostMoney", Type.GetType("System.Decimal"));
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                ArrayList pas = new ArrayList();
                pas.Add(costCode);
                pas.Add("1");
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.CostCodeIncludeSubNodeAndLeaf, pas));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                if (topNumber > 0)
                {
                    agent.SetTopNumber(topNumber);
                }
                EntityData data = agent.FillEntityData("PaymentItem", queryString);
                agent.Dispose();
                foreach (DataRow row in data.CurrentTable.Rows)
                {
                    string text2 = (string) row["paymentCode"];
                    if (table.Select(string.Format("paymentCode='{0}'", text2)).Length == 0)
                    {
                        DataRow row2 = table.NewRow();
                        row2["paymentCode"] = text2;
                        row2["PaymentTotalMoney"] = row["PaymentTotalMoney"];
                        row2["PaymentCostMoney"] = MathRule.SumColumn(data.CurrentTable, "ItemMoney", string.Format("PaymentCode='{0}'", text2));
                        table.Rows.Add(row2);
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

        public static decimal GetCostSpace(string projectCode, string costCode)
        {
            return (GetDynamicCost(projectCode, costCode) - GetAHMoney(costCode, "", ""));
        }

        public static decimal GetCostSpace(string projectCode, string costCode, DateTime payDate, ref string startDate, ref string endDate)
        {
            decimal num5;
            try
            {
                decimal num = GetDynamicCost(projectCode, costCode, payDate, ref startDate, ref endDate);
                decimal num2 = GetAHMoney(costCode, startDate, endDate, "", "0");
                decimal num3 = GetContractAllocationCost(costCode, "", "", startDate, endDate);
                decimal num4 = (num - num2) - num3;
                num5 = num4;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num5;
        }

        public static string GetCurrentDynamicCode(string projectCode)
        {
            string text3;
            try
            {
                string text = "";
                BudgetStrategyBuilder builder = new BudgetStrategyBuilder();
                builder.AddStrategy(new Strategy(BudgetStrategyName.ProjectCode, projectCode));
                builder.AddStrategy(new Strategy(BudgetStrategyName.IsDynamic, "1"));
                builder.AddStrategy(new Strategy(BudgetStrategyName.Flag, "0"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Budget", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("BudgetCode");
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static EntityData GetCurrentDynamicEntity(string projectCode, ref string budgetCode)
        {
            EntityData data;
            try
            {
                if (budgetCode == "")
                {
                    budgetCode = GetCurrentDynamicCode(projectCode);
                }
                data = CBSDAO.GetStandard_BudgetByCode(budgetCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }

        public static decimal GetDynamicCost(string projectCode, string costCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if (costCode == "")
                {
                    return num;
                }
                string budgetCode = "";
                EntityData currentDynamicEntity = GetCurrentDynamicEntity(projectCode, ref budgetCode);
                num = MathRule.SumColumn(currentDynamicEntity.Tables["BudgetCost"], "BudgetCost", string.Format("CostCode='{0}'", costCode));
                currentDynamicEntity.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetDynamicCost(string projectCode, string costCode, DateTime payDate, ref string startDate, ref string endDate)
        {
            decimal num9;
            try
            {
                decimal num = 0M;
                if (costCode == "")
                {
                    return num;
                }
                string budgetCode = "";
                EntityData currentDynamicEntity = GetCurrentDynamicEntity(projectCode, ref budgetCode);
                int @int = currentDynamicEntity.GetInt("IYear");
                int num3 = currentDynamicEntity.GetInt("IMonth");
                int months = currentDynamicEntity.GetInt("PeriodMonth");
                int num5 = currentDynamicEntity.GetInt("AfterPeriod");
                DateTime time = DateTime.Parse(string.Format("{0}-{1}-1", @int, num3));
                int num6 = 0;
                int num7 = 1;
                if (time.AddMonths(months) > payDate)
                {
                    num6 = 0;
                    num7 = (((payDate.Year - @int) * 12) + (payDate.Month - num3)) + 1;
                    startDate = DateTime.Parse(payDate.Year.ToString() + "-" + payDate.Month.ToString() + "-1").ToString("yyyy-MM-dd");
                    endDate = DateTime.Parse(startDate).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {
                    int result;
                    num6 = Math.DivRem(((payDate.Year - @int) * 12) + (payDate.Month - num3), months, out result);
                    startDate = time.AddMonths(num6 * months).ToString("yyyy-MM-dd");
                    endDate = time.AddMonths((num6 + 1) * months).AddDays(-1).ToString("yyyy-MM-dd");
                }
                if (num6 == 0)
                {
                    num = MathRule.SumColumn(currentDynamicEntity.Tables["BudgetMonth"], "Money", string.Format("CostCode='{0}' and IYear={1} and IMonth={2} ", costCode, num6, num7));
                }
                else
                {
                    num = MathRule.SumColumn(currentDynamicEntity.Tables["BudgetYear"], "Money", string.Format("CostCode='{0}' and IYear={1}", costCode, num6));
                }
                currentDynamicEntity.Dispose();
                num9 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num9;
        }

        public static string GetFirstCode(EntityData costEntity, string costCode)
        {
            string text = "";
            DataRow[] rowArray = costEntity.CurrentTable.Select(string.Format("CostCode='{0}'", costCode));
            if (rowArray.Length > 0)
            {
                string text2 = (string) rowArray[0]["FullCode"];
                text = text2.Split(new char[] { '-' })[0];
            }
            return text;
        }

        private static decimal GetMonthSum(DataRow[] childCBSRows, EntityData budget, int m)
        {
            decimal num = 0M;
            foreach (DataRow row in childCBSRows)
            {
                string text = (string) row["CostCode"];
                num += MathRule.SumColumn(budget.Tables["BudgetMonth"], "Money", string.Format(" CostCode='{0}' and IMonth={1} ", text, m));
            }
            return num;
        }

        public static string GetParentCostCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData cBSByCode = CBSDAO.GetCBSByCode(code);
                if (cBSByCode.HasRecord())
                {
                    text = cBSByCode.GetString("ParentCode");
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

        public static string GetParentCostName(string code)
        {
            string text3;
            try
            {
                string parentCostCode = GetParentCostCode(code);
                string text2 = "";
                if (parentCostCode != "")
                {
                    EntityData cBSByCode = CBSDAO.GetCBSByCode(parentCostCode);
                    text2 = cBSByCode.GetString("CostName");
                    cBSByCode.Dispose();
                }
                text3 = text2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static decimal GetProjectAHMoney(string projectCode, string startDate, string endDate)
        {
            decimal num2;
            try
            {
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                if ((endDate != "") || (startDate != ""))
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(startDate);
                    pas.Add(endDate);
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, pas));
                }
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.ProjectCode, projectCode));
                string queryString = builder.BuildQuerySumMoneyString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetProjectAPCost(string projectCode, string startDate, string endDate, string isContract)
        {
            decimal num2;
            try
            {
                PaymentItemStrategyBuilder builder = new PaymentItemStrategyBuilder();
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.Status, "1,2"));
                builder.AddStrategy(new Strategy(PaymentItemStrategyName.ProjectCode, projectCode));
                if (isContract != "")
                {
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.IsContract, isContract));
                }
                if ((endDate != "") || (startDate != ""))
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(startDate);
                    pas.Add(endDate);
                    builder.AddStrategy(new Strategy(PaymentItemStrategyName.PayDate, pas));
                }
                string queryString = builder.BuildQuerySumMoneyString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        private static decimal GetYearSum(DataRow[] childCBSRows, EntityData budget, int m)
        {
            decimal num = 0M;
            foreach (DataRow row in childCBSRows)
            {
                string text = (string) row["CostCode"];
                num += MathRule.SumColumn(budget.Tables["BudgetYear"], "Money", string.Format(" CostCode='{0}' and IYear={1} ", text, m));
            }
            return num;
        }

        public static void ImportCostRow(EntityData entity, EntityData refEntity, string costCode, string parentCode)
        {
            if (entity.CurrentTable.Select(string.Format("CostCode='{0}'", parentCode)).Length == 0)
            {
                DataRow[] rowArray = refEntity.CurrentTable.Select(string.Format("CostCode='{0}'", parentCode));
                if (rowArray.Length > 0)
                {
                    string text = rowArray[0]["CostCode"].ToString();
                    string text2 = rowArray[0]["ParentCode"].ToString();
                    int num = (int) rowArray[0]["Deep"];
                    entity.CurrentTable.ImportRow(rowArray[0]);
                    if (num > 1)
                    {
                        ImportCostRow(entity, refEntity, text2, parentCode);
                    }
                }
            }
        }

        public static bool IsChildCBS(string ChildCostCode, string ParentCostCode)
        {
            try
            {
                string cBSFullCode = GetCBSFullCode(ChildCostCode);
                if (("-" + cBSFullCode + "-").IndexOf("-" + ParentCostCode + "-") >= 0)
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

        public static void RemoveChildCBS(DataTable tb)
        {
            try
            {
                if (!tb.Columns.Contains("FullCode"))
                {
                    tb.Columns.Add("FullCode");
                    foreach (DataRow row in tb.Rows)
                    {
                        row["FullCode"] = GetCBSFullCode(ConvertRule.ToString(row["CostCode"]));
                    }
                }
                DataTable table = tb.Copy();
                foreach (DataRow row2 in table.Rows)
                {
                    DataRow[] rowArray = tb.Select("FullCode like '" + ConvertRule.ToString(row2["FullCode"]) + "-%'");
                    foreach (DataRow row3 in rowArray)
                    {
                        tb.Rows.Remove(row3);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SumBudgetMoney(string projectCode, string budgetCode, ref decimal budgetCost, ref decimal beforeHappenCost, ref decimal currentPlanCost, ref decimal afterPlanCost)
        {
            try
            {
                EntityData cBSByProject = CBSDAO.GetCBSByProject(projectCode);
                EntityData data2 = CBSDAO.GetStandard_BudgetByCode(budgetCode);
                data2.SetCurrentTable("BudgetCost");
                decimal num = 0M;
                decimal num2 = 0M;
                decimal num3 = 0M;
                decimal num4 = 0M;
                DataRow[] rowArray = cBSByProject.CurrentTable.Select("Deep=1");
                int length = rowArray.Length;
                for (int i = 0; i < length; i++)
                {
                    string text = (string) rowArray[i]["CostCode"];
                    DataRow[] rowArray2 = data2.CurrentTable.Select(string.Format(" CostCode='{0}' ", text));
                    if (rowArray2.Length > 0)
                    {
                        if (!rowArray2[0].IsNull("BudgetCost"))
                        {
                            num += (decimal) rowArray2[0]["BudgetCost"];
                        }
                        if (!rowArray2[0].IsNull("BeforeHappenCost"))
                        {
                            num2 += (decimal) rowArray2[0]["BeforeHappenCost"];
                        }
                        if (!rowArray2[0].IsNull("CurrentPlanCost"))
                        {
                            num3 += (decimal) rowArray2[0]["CurrentPlanCost"];
                        }
                        if (!rowArray2[0].IsNull("AfterPlanCost"))
                        {
                            num4 += (decimal) rowArray2[0]["AfterPlanCost"];
                        }
                    }
                }
                budgetCost = num;
                beforeHappenCost = num2;
                currentPlanCost = num3;
                afterPlanCost = num4;
                cBSByProject.Dispose();
                data2.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void SumChild(EntityData cbs, EntityData budget, string costCode, int iYear, int iMonth, int periodMonth, int afterPeriod, string budgetCode, string lastPeriodLastDate, string projectCode)
        {
            string text = (string) cbs.CurrentTable.Select(string.Format("CostCode='{0}'", costCode))[0]["fullCode"];
            decimal num = 0M;
            decimal num2 = 0M;
            DataRow[] childCBSRows = cbs.CurrentTable.Select(string.Format("FullCode like '{0}%' and parentCode ='{1}' ", text, costCode), "FullCode");
            foreach (DataRow row in childCBSRows)
            {
                string text2 = (string) row["CostCode"];
                DataRow[] rowArray3 = budget.Tables["BudgetCost"].Select(string.Format("CostCode='{0}'", text2));
                if (rowArray3.Length > 0)
                {
                    if (!rowArray3[0].IsNull("BudgetCost"))
                    {
                        num += (decimal) rowArray3[0]["BudgetCost"];
                    }
                    if (!rowArray3[0].IsNull("SurplusCost"))
                    {
                        num2 += (decimal) rowArray3[0]["SurplusCost"];
                    }
                }
            }
            if (!MathRule.CheckDecimalEqual(0M, num))
            {
                int num3;
                ClearBudgetData(budget, costCode);
                for (num3 = 1; num3 <= 12; num3++)
                {
                    DataRow[] rowArray4 = budget.Tables["BudgetMonth"].Select(string.Format(" CostCode='{0}' and IMonth={1} ", costCode, num3));
                    if (rowArray4.Length > 0)
                    {
                        if (num3 > periodMonth)
                        {
                            rowArray4[0]["Money"] = 0M;
                        }
                        else
                        {
                            rowArray4[0]["Money"] = GetMonthSum(childCBSRows, budget, num3);
                        }
                    }
                    else
                    {
                        DataRow newRecord = budget.GetNewRecord("BudgetMonth");
                        newRecord["BudgetMonthCode"] = SystemManageDAO.GetNewSysCode("BudgetMonthCode");
                        newRecord["BudgetCode"] = budgetCode;
                        newRecord["IYear"] = 0;
                        newRecord["IMonth"] = num3;
                        newRecord["ProjectCode"] = projectCode;
                        newRecord["CostCode"] = costCode;
                        budget.AddNewRecord(newRecord, "BudgetMonth");
                        newRecord["Money"] = GetMonthSum(childCBSRows, budget, num3);
                    }
                }
                DataRow[] rowArray5 = budget.Tables["BudgetYear"].Select(string.Format(" CostCode='{0}' and IYear=0 ", costCode));
                decimal num4 = MathRule.SumColumn(budget.Tables["BudgetMonth"], "Money", string.Format(" CostCode='{0}'  ", costCode));
                if (rowArray5.Length > 0)
                {
                    rowArray5[0]["Money"] = num4;
                }
                else
                {
                    DataRow row3 = budget.GetNewRecord("BudgetYear");
                    row3["BudgetYearCode"] = SystemManageDAO.GetNewSysCode("BudgetYearCode");
                    row3["BudgetCode"] = budgetCode;
                    row3["IYear"] = 0;
                    row3["ProjectCode"] = projectCode;
                    row3["CostCode"] = costCode;
                    budget.AddNewRecord(row3, "BudgetYear");
                    row3["Money"] = num4;
                }
                for (num3 = 1; num3 < 10; num3++)
                {
                    DataRow[] rowArray6 = budget.Tables["BudgetYear"].Select(string.Format(" CostCode='{0}' and IYear={1} ", costCode, num3));
                    if (rowArray6.Length > 0)
                    {
                        if (num3 > afterPeriod)
                        {
                            rowArray6[0]["Money"] = 0M;
                        }
                        else
                        {
                            rowArray6[0]["Money"] = GetYearSum(childCBSRows, budget, num3);
                        }
                    }
                    else
                    {
                        DataRow row4 = budget.GetNewRecord("BudgetYear");
                        row4["BudgetYearCode"] = SystemManageDAO.GetNewSysCode("BudgetYearCode");
                        row4["BudgetCode"] = budgetCode;
                        row4["IYear"] = num3;
                        row4["ProjectCode"] = projectCode;
                        row4["CostCode"] = costCode;
                        budget.AddNewRecord(row4, "BudgetYear");
                        row4["Money"] = GetYearSum(childCBSRows, budget, num3);
                    }
                }
                DataRow[] rowArray7 = budget.Tables["BudgetCost"].Select(string.Format(" CostCode='{0}' ", costCode));
                DataRow row5 = null;
                if (rowArray7.Length > 0)
                {
                    row5 = rowArray7[0];
                }
                else
                {
                    row5 = budget.GetNewRecord("BudgetCost");
                    row5["BudgetCostCode"] = SystemManageDAO.GetNewSysCode("BudgetCostCode");
                    row5["BudgetCode"] = budgetCode;
                    row5["ProjectCode"] = projectCode;
                    row5["CostCode"] = costCode;
                    budget.AddNewRecord(row5, "BudgetCost");
                }
                decimal num5 = GetAHMoney(costCode, "", lastPeriodLastDate);
                decimal num6 = MathRule.SumColumn(budget.Tables["BudgetYear"], "Money", string.Format(" CostCode='{0}' and IYear<>0 ", costCode));
                row5["BeforeHappenCost"] = num5;
                row5["AfterPlanCost"] = num6;
                row5["CurrentPlanCost"] = num4;
                row5["BudgetCost"] = (num5 + num6) + num4;
                row5["SurplusCost"] = num2;
            }
        }

        public static decimal SumTaskBudgetByCostEx(string costCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if (costCode == "")
                {
                    return num;
                }
                TaskBudgetStrategyBuilder builder = new TaskBudgetStrategyBuilder();
                ArrayList pas = new ArrayList();
                pas.Add(costCode);
                pas.Add("0");
                builder.AddStrategy(new Strategy(TaskBudgetStrategyName.CostCodeEx, pas));
                QueryAgent agent = new QueryAgent();
                num = (decimal) agent.ExecuteScalar(builder.BuildSumQueryString());
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal SumTotalEstimateCost(string costCode, string projectCode)
        {
            decimal num = 0M;
            try
            {
                V_CBSCostStrategyBuilder builder = new V_CBSCostStrategyBuilder();
                if (costCode == "")
                {
                    builder.AddStrategy(new Strategy(V_CBSCostStrategyName.Deep, "1"));
                    builder.AddStrategy(new Strategy(V_CBSCostStrategyName.ProjectCode, projectCode));
                }
                else
                {
                    builder.AddStrategy(new Strategy(V_CBSCostStrategyName.CostCode, costCode));
                }
                builder.AddStrategy(new Strategy(V_CBSCostStrategyName.Flag, "-1"));
                string queryString = builder.BuilSumQueryString();
                QueryAgent agent = new QueryAgent();
                num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static void SumTotalMoney(EntityData cbs, EntityData budget)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            DataRow[] rowArray = cbs.CurrentTable.Select(" Deep =1 ");
            int length = rowArray.Length;
            for (int i = 0; i < length; i++)
            {
                string text = (string) rowArray[i]["CostCode"];
                DataRow[] rowArray2 = budget.Tables["BudgetCost"].Select(string.Format("CostCode='{0}'", text));
                if (rowArray2.Length > 0)
                {
                    if (!rowArray2[0].IsNull("BeforeHappenCost"))
                    {
                        num += (decimal) rowArray2[0]["BeforeHappenCost"];
                    }
                    if (!rowArray2[0].IsNull("AfterPlanCost"))
                    {
                        num2 += (decimal) rowArray2[0]["AfterPlanCost"];
                    }
                    if (!rowArray2[0].IsNull("CurrentPlanCost"))
                    {
                        num3 += (decimal) rowArray2[0]["CurrentPlanCost"];
                    }
                    if (!rowArray2[0].IsNull("BudgetCost"))
                    {
                        num4 += (decimal) rowArray2[0]["BudgetCost"];
                    }
                }
            }
            DataRow row = budget.Tables["Budget"].Rows[0];
            row["BeforeHappenCost"] = num;
            row["AfterPlanCost"] = num2;
            row["CurrentPlanCost"] = num3;
            row["TotalMoney"] = num4;
        }

        public static void UpdateCBSParent(string CostCode, string ParentCode)
        {
            try
            {
                EntityData cBSByCode = CBSDAO.GetCBSByCode(CostCode);
                try
                {
                    if (cBSByCode.HasRecord() && (cBSByCode.GetString("ParentCode") != ParentCode))
                    {
                        if (CostCode == ParentCode)
                        {
                            throw new Exception("上级费用项不能是自己 ！");
                        }
                        if (IsChildCBS(ParentCode, CostCode))
                        {
                            throw new Exception("上级费用项不能是自己的子项 ！");
                        }
                        string oldValue = GetCBSFullCode(cBSByCode.GetString("ParentCode"));
                        int num = cBSByCode.GetInt("Deep") - 1;
                        string newValue = "";
                        int @int = 0;
                        if (ParentCode != "")
                        {
                            EntityData data2 = CBSDAO.GetCBSByCode(ParentCode);
                            if (!data2.HasRecord())
                            {
                                throw new Exception("上级费用项不存在 ！");
                            }
                            newValue = data2.GetString("FullCode");
                            @int = data2.GetInt("Deep");
                            data2.Dispose();
                        }
                        int num3 = @int - num;
                        CBSStrategyBuilder builder = new CBSStrategyBuilder();
                        builder.AddStrategy(new Strategy(CBSStrategyName.AllSubNodeIncludeSelf, CostCode));
                        string queryString = builder.BuildMainQueryString();
                        QueryAgent agent = new QueryAgent();
                        EntityData entity = agent.FillEntityData("CBS", queryString);
                        foreach (DataRow row in entity.CurrentTable.Rows)
                        {
                            if (row["CostCode"].ToString() == CostCode)
                            {
                                row["ParentCode"] = ParentCode;
                            }
                            row["Deep"] = ConvertRule.ToInt(row["Deep"]) + num3;
                            if (oldValue == "")
                            {
                                row["FullCode"] = newValue + "-" + ConvertRule.ToString(row["FullCode"]);
                            }
                            else
                            {
                                row["FullCode"] = ConvertRule.ToString(row["FullCode"]).Replace(oldValue, newValue);
                            }
                            if (row["FullCode"].ToString().StartsWith("-"))
                            {
                                row["FullCode"] = row["FullCode"].ToString().Substring(1);
                            }
                        }
                        CBSDAO.SubmitAllCBS(entity);
                        entity.Dispose();
                        agent.Dispose();
                    }
                }
                finally
                {
                    cBSByCode.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

