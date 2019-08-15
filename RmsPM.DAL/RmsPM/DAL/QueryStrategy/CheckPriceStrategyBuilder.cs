namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CheckPriceStrategyBuilder : StandardQueryStringBuilder
    {
        public CheckPriceStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskAttention", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            AttentionStrategyName name = (AttentionStrategyName) strategy.Name;
            if (name == AttentionStrategyName.ProjectCode)
            {
                strategy.RelationFieldName = "ProjectCode";
                strategy.Type = StrategyType.StringEqual;
            }
            else
            {
                strategy.Type = StrategyType.Other;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            AttentionStrategyName name = (AttentionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

