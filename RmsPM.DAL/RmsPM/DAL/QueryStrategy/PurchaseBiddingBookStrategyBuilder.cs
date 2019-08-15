namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PurchaseBiddingBookStrategyBuilder : StandardQueryStringBuilder
    {
        public PurchaseBiddingBookStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PurchaseBiddingBook", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            PurchaseBiddingBookStrategyName name = (PurchaseBiddingBookStrategyName) strategy.Name;
            PurchaseBiddingBookStrategyName name2 = name;
            if (name2 == PurchaseBiddingBookStrategyName.PurchaseBiddingBookCode)
            {
                strategy.RelationFieldName = "PurchaseBiddingBookCode";
                strategy.Type = StrategyType.StringEqual;
            }
            else if (name2 == PurchaseBiddingBookStrategyName.PurchaseCode)
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
            PurchaseBiddingBookStrategyName name = (PurchaseBiddingBookStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

