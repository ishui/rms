namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingDtlStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingDtlStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingDtl", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingDtlStrategyName) strategy.Name))
            {
                case BiddingDtlStrategyName.BiddingDtlCode:
                    strategy.RelationFieldName = "BiddingDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.Title:
                    strategy.RelationFieldName = "Title";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.Unit:
                    strategy.RelationFieldName = "Unit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.Money:
                    strategy.RelationFieldName = "Money";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.remark:
                    strategy.RelationFieldName = "remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.flag:
                    strategy.RelationFieldName = "flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.OtherMoney:
                    strategy.RelationFieldName = "OtherMoney";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.OtherMoneyType:
                    strategy.RelationFieldName = "OtherMoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlStrategyName.OtherMoneyRate:
                    strategy.RelationFieldName = "OtherMoneyRate";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingDtlStrategyName name = (BiddingDtlStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

