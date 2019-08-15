namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingStationStrategyBuilder : StandardQueryStringBuilder
    {
        public BuildingStationStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BuildingStation", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingStationStrategyName) strategy.Name))
            {
                case BuildingStationStrategyName.BuildingStationCodeEq:
                    strategy.RelationFieldName = "BuildingStationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStationStrategyName.BuildingCodeEq:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStationStrategyName.StationNameEq:
                    strategy.RelationFieldName = "StationName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingStationStrategyName.StationNumEq:
                    strategy.RelationFieldName = "StationNum";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case BuildingStationStrategyName.StationAreaRa:
                    strategy.RelationFieldName = "StationArea";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BuildingStationStrategyName.AreaForVolumeRateRa:
                    strategy.RelationFieldName = "AreaForVolumeRate";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BuildingStationStrategyName.StationRemarkLike:
                    strategy.RelationFieldName = "StationRemark";
                    strategy.Type = StrategyType.StringLike;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BuildingStationStrategyName name = (BuildingStationStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

