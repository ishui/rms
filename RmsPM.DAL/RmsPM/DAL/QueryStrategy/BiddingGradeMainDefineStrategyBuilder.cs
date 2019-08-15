namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingGradeMainDefineStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingGradeMainDefineStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingGradeMainDefine", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingGradeMainDefineStrategyName) strategy.Name))
            {
                case BiddingGradeMainDefineStrategyName.BiddingMainDefineCode:
                    strategy.RelationFieldName = "BiddingMainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMainDefineStrategyName.Name:
                    strategy.RelationFieldName = "Name";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMainDefineStrategyName.state:
                    strategy.RelationFieldName = "state";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingGradeMainDefineStrategyName name = (BiddingGradeMainDefineStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

