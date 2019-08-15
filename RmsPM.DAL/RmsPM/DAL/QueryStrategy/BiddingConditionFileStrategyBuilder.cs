namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingConditionFileStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingConditionFileStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingConditionFile", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingConditionFileStrategyName) strategy.Name))
            {
                case BiddingConditionFileStrategyName.BiddingConditionFileCode:
                    strategy.RelationFieldName = "BiddingConditionFileCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.BiddingCode:
                    strategy.RelationFieldName = "BiddingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.name:
                    strategy.RelationFieldName = "name";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.BiddingConditionFileNumber:
                    strategy.RelationFieldName = "BiddingConditionFileNumber";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.ZBFW:
                    strategy.RelationFieldName = "ZBFW";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.JSXQ:
                    strategy.RelationFieldName = "JSXQ";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.ZLBZ:
                    strategy.RelationFieldName = "ZLBZ";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.GQ:
                    strategy.RelationFieldName = "GQ";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.RCTJ:
                    strategy.RelationFieldName = "RCTJ";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.SHFW:
                    strategy.RelationFieldName = "SHFW";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingConditionFileStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingConditionFileStrategyName name = (BiddingConditionFileStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

