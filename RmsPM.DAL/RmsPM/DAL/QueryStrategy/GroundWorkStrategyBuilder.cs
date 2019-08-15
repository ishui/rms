namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GroundWorkStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public GroundWorkStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GroundWork", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("GroundWork", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GroundWorkStrategyName) strategy.Name))
            {
                case GroundWorkStrategyName.GroundWorkCode:
                    strategy.RelationFieldName = "GroundWorkCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GroundWorkStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GroundWorkStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GroundWorkStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
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
            GroundWorkStrategyName name = (GroundWorkStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                GroundWorkStrategyName name2 = name;
                if (name2 != GroundWorkStrategyName.False)
                {
                    if (name2 != GroundWorkStrategyName.GroundWorkCodeNot)
                    {
                        return "";
                    }
                }
                else
                {
                    return "1=2";
                }
                return string.Format("GroundWorkCode <> '{0}'", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

