namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class RiskTypeStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public RiskTypeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("RiskType", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("RiskType", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((RiskTypeStrategyName) strategy.Name))
            {
                case RiskTypeStrategyName.TypeCode:
                    strategy.RelationFieldName = "TypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RiskTypeStrategyName.TypeName:
                    strategy.RelationFieldName = "TypeName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case RiskTypeStrategyName.RemindIndexCode:
                    strategy.RelationFieldName = "RemindIndexCode";
                    strategy.Type = StrategyType.IntegerRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            RiskTypeStrategyName name = (RiskTypeStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case RiskTypeStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

