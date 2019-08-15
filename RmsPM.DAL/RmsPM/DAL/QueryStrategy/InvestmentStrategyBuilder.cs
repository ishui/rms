namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class InvestmentStrategyBuilder : StandardQueryStringBuilder
    {
        public InvestmentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Investment", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((InvestmentStrategyName) strategy.Name))
            {
                case InvestmentStrategyName.InvestCode:
                    strategy.RelationFieldName = "InvestCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case InvestmentStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case InvestmentStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case InvestmentStrategyName.IMonth:
                    strategy.RelationFieldName = "IMonth";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case InvestmentStrategyName.IsPlan:
                    strategy.RelationFieldName = "IsPlan";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            InvestmentStrategyName name = (InvestmentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

