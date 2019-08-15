namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingPrejudicationStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingPrejudicationStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingPrejudication", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingPrejudicationStrategyName) strategy.Name))
            {
                case BiddingPrejudicationStrategyName.BiddingPrejudicationCode:
                    strategy.RelationFieldName = "BiddingPrejudicationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.WorkConfine:
                    strategy.RelationFieldName = "WorkConfine";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.Number:
                    strategy.RelationFieldName = "Number";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.CreateDate:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingPrejudicationStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingPrejudicationStrategyName name = (BiddingPrejudicationStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

