namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SalBudgetStrategyBuilder : StandardQueryStringBuilder
    {
        public SalBudgetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SalBudget", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SalBudgetStrategyName) strategy.Name))
            {
                case SalBudgetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalBudgetStrategyName.BudgetCode:
                    strategy.RelationFieldName = "BudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalBudgetStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SalBudgetStrategyName.IYearRange:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SalBudgetStrategyName name = (SalBudgetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SalBudgetStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

