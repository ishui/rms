namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingModelStrategyBuilder : StandardQueryStringBuilder
    {
        public BuildingModelStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BuildingModel", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingModelStrategyName) strategy.Name))
            {
                case BuildingModelStrategyName.BuildingModelCodeEq:
                    strategy.RelationFieldName = "BuildingModelCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingModelStrategyName.BuildingCodeEq:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingModelStrategyName.ModelCodeEq:
                    strategy.RelationFieldName = "ModelCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingModelStrategyName.BuildingStationCodeEq:
                    strategy.RelationFieldName = "BuildingStationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingModelStrategyName.BuildingFunctionCodeEq:
                    strategy.RelationFieldName = "BuildingFunctionCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingModelStrategyName.BModelNumEq:
                    strategy.RelationFieldName = "BModelNum";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case BuildingModelStrategyName.BModelAreaRa:
                    strategy.RelationFieldName = "BModelArea";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BuildingModelStrategyName.BModelRemarkLike:
                    strategy.RelationFieldName = "BModelRemark";
                    strategy.Type = StrategyType.StringLike;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BuildingModelStrategyName name = (BuildingModelStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

