namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingFloorStrategyBuilder : StandardQueryStringBuilder
    {
        public BuildingFloorStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BuildingFloor", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingFloorStrategyName) strategy.Name))
            {
                case BuildingFloorStrategyName.BuildingFloorCode:
                    strategy.RelationFieldName = "BuildingFloorCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorStrategyName.FloorName:
                    strategy.RelationFieldName = "FloorName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFloorStrategyName.FloorNameLike:
                    strategy.RelationFieldName = "FloorName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case BuildingFloorStrategyName.FloorIndex:
                    strategy.RelationFieldName = "FloorIndex";
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
            BuildingFloorStrategyName name = (BuildingFloorStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case BuildingFloorStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

