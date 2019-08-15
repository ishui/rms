namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SalBudgetDtlStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";

        public SalBudgetDtlStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SalBudgetDtl", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("SalBudgetDtl", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SalBudgetDtlStrategyName) strategy.Name))
            {
                case SalBudgetDtlStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalBudgetDtlStrategyName.BudgetCode:
                    strategy.RelationFieldName = "BudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalBudgetDtlStrategyName.SystemID:
                    strategy.RelationFieldName = "SystemID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalBudgetDtlStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SalBudgetDtlStrategyName.IYearRange:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                case SalBudgetDtlStrategyName.IMonth:
                    strategy.RelationFieldName = "IMonth";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case SalBudgetDtlStrategyName.IMonthRange:
                    strategy.RelationFieldName = "IMonth";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                case SalBudgetDtlStrategyName.PBSTypeCode:
                    strategy.RelationFieldName = "PBSTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + base.BuildStrategysString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            SalBudgetDtlStrategyName name = (SalBudgetDtlStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SalBudgetDtlStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

