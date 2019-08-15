namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingFileStrategyBuider : StandardQueryStringBuilder
    {
        public BiddingFileStrategyBuider()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingFile", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingFileStrategyName) strategy.Name))
            {
                case BiddingFileStrategyName.BiddingFileCode:
                    strategy.RelationFieldName = "BiddingFileCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingFileStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingFileStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingFileStrategyName.BiddingFileNumber:
                    strategy.RelationFieldName = "BiddingFileNumber";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingFileStrategyName.State:
                    strategy.RelationFieldName = "state";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingFileStrategyName name = (BiddingFileStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

