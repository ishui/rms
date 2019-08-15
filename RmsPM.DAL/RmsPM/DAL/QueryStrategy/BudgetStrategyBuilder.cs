namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BudgetStrategyBuilder : StandardQueryStringBuilder
    {
        public BudgetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Budget", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BudgetStrategyName) strategy.Name))
            {
                case BudgetStrategyName.BudgetCode:
                    strategy.RelationFieldName = "BudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BudgetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BudgetStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case BudgetStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case BudgetStrategyName.IsDynamic:
                    strategy.RelationFieldName = "IsDynamic";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case BudgetStrategyName.MakeDate:
                    strategy.RelationFieldName = "MakeDate";
                    strategy.Type = StrategyType.DateTimeEqual;
                    break;

                case BudgetStrategyName.CheckDate:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BudgetStrategyName name = (BudgetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case BudgetStrategyName.AccessRange:
                        return "";
                }
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "Budget", "BudgetCode", "MakePerson");
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

