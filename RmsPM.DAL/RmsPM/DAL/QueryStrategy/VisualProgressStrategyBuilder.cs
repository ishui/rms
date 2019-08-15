namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class VisualProgressStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public VisualProgressStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("VisualProgress", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("VisualProgress", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((VisualProgressStrategyName) strategy.Name))
            {
                case VisualProgressStrategyName.SystemID:
                    strategy.RelationFieldName = "SystemID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VisualProgressStrategyName.VisualProgress:
                    strategy.RelationFieldName = "VisualProgress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VisualProgressStrategyName.ProgressType:
                    strategy.RelationFieldName = "ProgressType";
                    strategy.Type = StrategyType.IntegerEqual;
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
            VisualProgressStrategyName name = (VisualProgressStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                VisualProgressStrategyName name2 = name;
                if (name2 != VisualProgressStrategyName.False)
                {
                    if (name2 != VisualProgressStrategyName.ProgressTypeNot)
                    {
                        return "";
                    }
                }
                else
                {
                    return "1=2";
                }
                return string.Format("ProgressType <> '{0}'", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

