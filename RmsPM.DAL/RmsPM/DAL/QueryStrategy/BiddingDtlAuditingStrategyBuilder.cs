namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingDtlAuditingStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingDtlAuditingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingDtlAuditing", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingDtlAuditingStrategyName) strategy.Name))
            {
                case BiddingDtlAuditingStrategyName.BiddingDtlAuditingCode:
                    strategy.RelationFieldName = "BiddingDtlAuditingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlAuditingStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlAuditingStrategyName.BiddingAuditingCode:
                    strategy.RelationFieldName = "BiddingAuditingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlAuditingStrategyName.BiddingDtlCode:
                    strategy.RelationFieldName = "BiddingDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlAuditingStrategyName.FormerMoney:
                    strategy.RelationFieldName = "FormerMoney";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BiddingDtlAuditingStrategyName.CurrentMoney:
                    strategy.RelationFieldName = "CurrentMoney";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BiddingDtlAuditingStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingDtlAuditingStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingDtlAuditingStrategyName name = (BiddingDtlAuditingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

