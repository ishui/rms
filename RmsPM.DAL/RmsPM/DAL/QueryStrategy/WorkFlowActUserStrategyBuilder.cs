namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowActUserStrategyBuilder : StandardQueryStringBuilder
    {
        public WorkFlowActUserStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowActUser", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            WorkFlowActUserStrategyName name = (WorkFlowActUserStrategyName) strategy.Name;
            if (name == WorkFlowActUserStrategyName.ActCode)
            {
                strategy.RelationFieldName = "ActCode";
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
            WorkFlowActUserStrategyName name = (WorkFlowActUserStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

