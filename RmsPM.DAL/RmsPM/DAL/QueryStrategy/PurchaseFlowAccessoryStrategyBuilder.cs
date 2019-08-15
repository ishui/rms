namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowAccessoryStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowAccessoryStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowAccessory", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowAccessoryStrategyName) strategy.Name))
            {
                case PurchaseFlowAccessoryStrategyName.PurchaseFlowAccessoryCode:
                    strategy.RelationFieldName = "PurchaseFlowAccessoryCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowAccessoryStrategyName.PurchaseAccessoryType:
                    strategy.RelationFieldName = "PurchaseAccessoryType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowAccessoryStrategyName.ObjectCode:
                    strategy.RelationFieldName = "ObjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PurchaseFlowAccessoryStrategyName name = (PurchaseFlowAccessoryStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

