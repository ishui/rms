namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingEmitStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingEmitStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingEmit", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingEmitStrategyName) strategy.Name))
            {
                case BiddingEmitStrategyName.BiddingEmitCode:
                    strategy.RelationFieldName = "BiddingEmitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.EmitNumber:
                    strategy.RelationFieldName = "EmitNumber";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.EmitDate:
                    strategy.RelationFieldName = "EmitDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.EndDate:
                    strategy.RelationFieldName = "EndDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.PrejudicationDate:
                    strategy.RelationFieldName = "PrejudicationDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.CreatUser:
                    strategy.RelationFieldName = "CreatUser";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.CreatDate:
                    strategy.RelationFieldName = "CreatDate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.TotalRemark:
                    strategy.RelationFieldName = "TotalRemark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingEmitStrategyName.TotalRemark2:
                    strategy.RelationFieldName = "TotalRemark2";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingEmitStrategyName name = (BiddingEmitStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

