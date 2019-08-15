namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingFloorProgressStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public BuildingFloorProgressStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BuildingFloorProgress", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("BuildingFloorProgress", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingFloorProgressStrategyName) strategy.Name))
            {
                case BuildingFloorProgressStrategyName.ProgressCode:
                    strategy.RelationFieldName = "ProgressCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorProgressStrategyName.BuildingFloorCode:
                    strategy.RelationFieldName = "BuildingFloorCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorProgressStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorProgressStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorProgressStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorProgressStrategyName.VisualProgressCode:
                    strategy.RelationFieldName = "VisualProgressCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorProgressStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
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
            BuildingFloorProgressStrategyName name = (BuildingFloorProgressStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case BuildingFloorProgressStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

