namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseBiddingStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseBiddingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseBidding", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            PurchaseBiddingStrategyName name = (PurchaseBiddingStrategyName) strategy.Name;
            if (name == PurchaseBiddingStrategyName.PurchaseBiddingCode)
            {
                strategy.RelationFieldName = "PurchaseBiddingCode";
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
            PurchaseBiddingStrategyName name = (PurchaseBiddingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

