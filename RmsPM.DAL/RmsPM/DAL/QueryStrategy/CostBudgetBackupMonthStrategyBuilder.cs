namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetBackupMonthStrategyBuilder : StandardQueryStringBuilder
    {
        public CostBudgetBackupMonthStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetBackupMonth", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetBackupMonthStrategyName) strategy.Name))
            {
                case CostBudgetBackupMonthStrategyName.CostBudgetBackupMonthCode:
                    strategy.RelationFieldName = "CostBudgetBackupMonthCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupMonthStrategyName.CostBudgetBackupSetCode:
                    strategy.RelationFieldName = "CostBudgetBackupSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupMonthStrategyName.CostBudgetBackupCode:
                    strategy.RelationFieldName = "CostBudgetBackupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupMonthStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupMonthStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupMonthStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetBackupMonthStrategyName name = (CostBudgetBackupMonthStrategyName) strategy.Name;
            string text2 = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                CostBudgetBackupMonthStrategyName name2 = name;
                if (name2 != CostBudgetBackupMonthStrategyName.False)
                {
                    if (name2 != CostBudgetBackupMonthStrategyName.CostCodeIn)
                    {
                        return text3;
                    }
                }
                else
                {
                    return "1=2";
                }
                text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                if (text2 != "")
                {
                    text3 = string.Format(" CostCode in ({0}) ", text2);
                }
                return text3;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

