namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingReturnCostStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingReturnCostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingReturnCost", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingReturnCostStrategyName) strategy.Name))
            {
                case BiddingReturnCostStrategyName.BiddingReturnCode:
                    strategy.RelationFieldName = "BiddingReturnCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.BiddingReturnCostCode:
                    strategy.RelationFieldName = "BiddingReturnCostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.BiddingReturnCostID:
                    strategy.RelationFieldName = "BiddingReturnCostID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.Cash:
                    strategy.RelationFieldName = "Cash";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.MoneyType:
                    strategy.RelationFieldName = "MoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.MoneyTypeID:
                    strategy.RelationFieldName = "MoneyTypeID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.ExchangeRate:
                    strategy.RelationFieldName = "ExchangeRate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingReturnCostStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingReturnCostStrategyName name = (BiddingReturnCostStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

