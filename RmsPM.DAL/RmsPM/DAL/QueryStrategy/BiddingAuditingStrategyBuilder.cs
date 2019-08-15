namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingAuditingStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingAuditingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingAuditing", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingAuditingStrategyName) strategy.Name))
            {
                case BiddingAuditingStrategyName.BiddingAuditingCode:
                    strategy.RelationFieldName = "BiddingAuditingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.AppentDate:
                    strategy.RelationFieldName = "AppentDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.AppentUser:
                    strategy.RelationFieldName = "AppentUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.Preparation1:
                    strategy.RelationFieldName = "Preparation1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingAuditingStrategyName.Preparation2:
                    strategy.RelationFieldName = "Preparation2";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingAuditingStrategyName name = (BiddingAuditingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

