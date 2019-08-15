namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ConstructProgressRiskStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public ConstructProgressRiskStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ConstructProgressRisk", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("ConstructProgressRisk", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ConstructProgressRiskStrategyName) strategy.Name))
            {
                case ConstructProgressRiskStrategyName.ProgressRiskCode:
                    strategy.RelationFieldName = "ProgressRiskCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressRiskStrategyName.ProgressCode:
                    strategy.RelationFieldName = "ProgressCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressRiskStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressRiskStrategyName.RiskTypeName:
                    strategy.RelationFieldName = "RiskTypeName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressRiskStrategyName.RiskIndexCode:
                    strategy.RelationFieldName = "RiskIndexCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressRiskStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqual;
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
            ConstructProgressRiskStrategyName name = (ConstructProgressRiskStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ConstructProgressRiskStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

