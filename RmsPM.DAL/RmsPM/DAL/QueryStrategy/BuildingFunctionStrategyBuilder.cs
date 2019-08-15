namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingFunctionStrategyBuilder : StandardQueryStringBuilder
    {
        public BuildingFunctionStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BuildingFunction", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingFunctionStrategyName) strategy.Name))
            {
                case BuildingFunctionStrategyName.BuildingFunctionCodeEq:
                    strategy.RelationFieldName = "BuildingFunctionCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFunctionStrategyName.BuildingCodeEq:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFunctionStrategyName.FunctionNameEq:
                    strategy.RelationFieldName = "FunctionName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingFunctionStrategyName.FunctionNumEq:
                    strategy.RelationFieldName = "FunctionNum";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case BuildingFunctionStrategyName.FunctionAreaRa:
                    strategy.RelationFieldName = "FunctionArea";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BuildingFunctionStrategyName.FunctionRemarkLike:
                    strategy.RelationFieldName = "FunctionRemark";
                    strategy.Type = StrategyType.StringLike;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BuildingFunctionStrategyName name = (BuildingFunctionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

