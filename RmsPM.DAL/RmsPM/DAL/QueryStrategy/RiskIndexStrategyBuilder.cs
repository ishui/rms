namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class RiskIndexStrategyBuilder : StandardQueryStringBuilder
    {
        public RiskIndexStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("RiskIndex", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((RiskIndexStrategyName) strategy.Name))
            {
                case RiskIndexStrategyName.IndexCode:
                    strategy.RelationFieldName = "IndexCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RiskIndexStrategyName.IndexName:
                    strategy.RelationFieldName = "IndexName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RiskIndexStrategyName.IndexLevel:
                    strategy.RelationFieldName = "IndexLevel";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case RiskIndexStrategyName.IndexLevelRange:
                    strategy.RelationFieldName = "IndexLevel";
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
            RiskIndexStrategyName name = (RiskIndexStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case RiskIndexStrategyName.False:
                        return "1=2";

                    case RiskIndexStrategyName.IndexCode:
                        return text;

                    case RiskIndexStrategyName.ExceptIndexCode:
                        return string.Format("IndexCode <> '{0}'", strategy.GetParameter(0));

                    case RiskIndexStrategyName.IsDefault:
                        return string.Format("isnull(IsDefault, 0) = {0}", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

