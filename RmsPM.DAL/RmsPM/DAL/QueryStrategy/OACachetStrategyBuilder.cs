namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OACachetStrategyBuilder : StandardQueryStringBuilder
    {
        public OACachetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("OACachet", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OACachetStrategyName) strategy.Name))
            {
                case OACachetStrategyName.OACachetCode:
                    strategy.RelationFieldName = "OACachetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OACachetStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OACachetStrategyName.ApplyDate:
                    strategy.RelationFieldName = "ApplyDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OACachetStrategyName.Reason:
                    strategy.RelationFieldName = "Reason";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OACachetStrategyName.ApplyUser:
                    strategy.RelationFieldName = "ApplyUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            OACachetStrategyName name = (OACachetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

