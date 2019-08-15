namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowDetailStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowDetailStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowDetail", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            PurchaseFlowDetailStrategyName name = (PurchaseFlowDetailStrategyName) strategy.Name;
            if (name == PurchaseFlowDetailStrategyName.PurchaseCode)
            {
                strategy.RelationFieldName = "PurchaseCode";
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
            PurchaseFlowDetailStrategyName name = (PurchaseFlowDetailStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

