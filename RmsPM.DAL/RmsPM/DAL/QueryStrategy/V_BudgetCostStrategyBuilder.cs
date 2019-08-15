namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class V_BudgetCostStrategyBuilder : StandardQueryStringBuilder
    {
        public V_BudgetCostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_BudgetCost", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((V_BudgetCostStrategyName) strategy.Name))
            {
                case V_BudgetCostStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BudgetCostStrategyName.BudgetCode:
                    strategy.RelationFieldName = "BudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BudgetCostStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_BudgetCostStrategyName.MakeDate:
                    strategy.RelationFieldName = "MakeDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case V_BudgetCostStrategyName.CheckDate:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case V_BudgetCostStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case V_BudgetCostStrategyName.IsDynamic:
                    strategy.RelationFieldName = "IsDynamic";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            V_BudgetCostStrategyName name = (V_BudgetCostStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

