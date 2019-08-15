namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class StationDesktopStrategyBuilder : StandardQueryStringBuilder
    {
        public StationDesktopStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("StationDesktop", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((StationDesktopStrategyName) strategy.Name))
            {
                case StationDesktopStrategyName.StationID:
                    strategy.RelationFieldName = "StationID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case StationDesktopStrategyName.StyleID:
                    strategy.RelationFieldName = "StyleID";
                    strategy.Type = StrategyType.StringIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            StationDesktopStrategyName name = (StationDesktopStrategyName) strategy.Name;
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

