namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class StyleListStrategyBuilder : StandardQueryStringBuilder
    {
        public StyleListStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("StyleList", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((StyleListStrategyName) strategy.Name))
            {
                case StyleListStrategyName.StyleID:
                    strategy.RelationFieldName = "StyleID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case StyleListStrategyName.CreatTime:
                    strategy.RelationFieldName = "SytleName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case StyleListStrategyName.CreatName:
                    strategy.RelationFieldName = "SytleName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            StyleListStrategyName name = (StyleListStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

