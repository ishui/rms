namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseFlowInviteStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseFlowInviteStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseFlowInvite", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PurchaseFlowInviteStrategyName) strategy.Name))
            {
                case PurchaseFlowInviteStrategyName.PurchaseFlowInviteCode:
                    strategy.RelationFieldName = "PurchaseFlowInviteCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowInviteStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowInviteStrategyName.CheckListItem:
                    strategy.RelationFieldName = "CheckListItem";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowInviteStrategyName.IsChiked:
                    strategy.RelationFieldName = "IsChiked";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowInviteStrategyName.IOption:
                    strategy.RelationFieldName = "IOption";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PurchaseFlowInviteStrategyName.PurchaseCode:
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
            PurchaseFlowInviteStrategyName name = (PurchaseFlowInviteStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

