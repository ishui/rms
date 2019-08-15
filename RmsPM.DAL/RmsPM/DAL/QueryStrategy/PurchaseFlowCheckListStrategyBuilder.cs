namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowCheckListStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowCheckListStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowCheckList", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowCheckListStrategyName) strategy.Name))
            {
                case PurchaseFlowCheckListStrategyName.PurchaseFlowCheckListCode:
                    strategy.RelationFieldName = "PurchaseFlowCheckListCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowCheckListStrategyName.CheckListName:
                    strategy.RelationFieldName = "CheckListName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowCheckListStrategyName.CheckListChecked:
                    strategy.RelationFieldName = "CheckListChecked";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowCheckListStrategyName.PurchaseCode:
                    strategy.RelationFieldName = "PurchaseCode";
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
            PurchaseFlowCheckListStrategyName name = (PurchaseFlowCheckListStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

