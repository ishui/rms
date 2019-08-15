namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowSelSupplyHistoryStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowSelSupplyHistoryStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowSelSupplyHistory", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowSelSupplyHistoryStrategyName) strategy.Name))
            {
                case PurchaseFlowSelSupplyHistoryStrategyName.PurchaseFlowSelSupplyHistoryCode:
                    strategy.RelationFieldName = "PurchaseFlowSelSupplyHistoryCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.PurchaseFlowSelSupplyCode:
                    strategy.RelationFieldName = "PurchaseFlowSelSupplyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.Bidding:
                    strategy.RelationFieldName = "Bidding";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.VarietyProducing:
                    strategy.RelationFieldName = "VarietyProducing";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.PprovidePeriods:
                    strategy.RelationFieldName = "PprovidePeriods";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.PaymentCondition:
                    strategy.RelationFieldName = "PaymentCondition";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.TechnologyWindage:
                    strategy.RelationFieldName = "TechnologyWindage";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.AppraiseGather:
                    strategy.RelationFieldName = "AppraiseGather";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyHistoryStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PurchaseFlowSelSupplyHistoryStrategyName name = (PurchaseFlowSelSupplyHistoryStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

