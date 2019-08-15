namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingLogStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingLogStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingLog", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingLogStrategyName) strategy.Name))
            {
                case BiddingLogStrategyName.BiddingLogCode:
                    strategy.RelationFieldName = "BiddingLogCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingLogStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingLogStrategyName.Type:
                    strategy.RelationFieldName = "Type";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingLogStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingLogStrategyName.UpdateTime:
                    strategy.RelationFieldName = "UpdateTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingLogStrategyName.FormerMoney:
                    strategy.RelationFieldName = "FormerMoney";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BiddingLogStrategyName.TeamMoney:
                    strategy.RelationFieldName = "TeamMoney";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BiddingLogStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingLogStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
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
            BiddingLogStrategyName name = (BiddingLogStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

