namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowSelSupplyStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowSelSupplyStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowSelSupply", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowSelSupplyStrategyName) strategy.Name))
            {
                case PurchaseFlowSelSupplyStrategyName.PurchaseFlowSelSupplyCode:
                    strategy.RelationFieldName = "PurchaseFlowSelSupplyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyStrategyName.UseSuppyCode:
                    strategy.RelationFieldName = "UseSuppyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyStrategyName.PurchaseCode:
                    strategy.RelationFieldName = "PurchaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyStrategyName.RecommendUnit:
                    strategy.RelationFieldName = "RecommendUnit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyStrategyName.BaseFlag:
                    strategy.RelationFieldName = "BaseFlag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowSelSupplyStrategyName.MoneyFlag:
                    strategy.RelationFieldName = "MoneyFlag";
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
            PurchaseFlowSelSupplyStrategyName name = (PurchaseFlowSelSupplyStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

