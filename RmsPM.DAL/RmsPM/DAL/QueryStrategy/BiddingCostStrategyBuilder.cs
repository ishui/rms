namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingCostStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingCostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingCost", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingCostStrategyName) strategy.Name))
            {
                case BiddingCostStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.Cash:
                    strategy.RelationFieldName = "Cash";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.MoneyType:
                    strategy.RelationFieldName = "MoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.ExchangeRate:
                    strategy.RelationFieldName = "ExchangeRate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.MoneyTypeID:
                    strategy.RelationFieldName = "MoneyTypeID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.BiddingCostCode:
                    strategy.RelationFieldName = "BiddingCostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.BiddingCostID:
                    strategy.RelationFieldName = "BiddingCostID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingCostStrategyName.ObligateCash:
                    strategy.RelationFieldName = "ObligateCash";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingCostStrategyName name = (BiddingCostStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

