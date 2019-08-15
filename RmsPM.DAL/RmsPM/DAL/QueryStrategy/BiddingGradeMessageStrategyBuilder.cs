namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingGradeMessageStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingGradeMessageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingGradeMessage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingGradeMessageStrategyName) strategy.Name))
            {
                case BiddingGradeMessageStrategyName.BiddingGradeMessageCode:
                    strategy.RelationFieldName = "BiddingGradeMessageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMessageStrategyName.ApplicationCode:
                    strategy.RelationFieldName = "ApplicationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMessageStrategyName.BiddingGradeTypeCode:
                    strategy.RelationFieldName = "BiddingGradeTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMessageStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMessageStrategyName.ProjectManage:
                    strategy.RelationFieldName = "ProjectManage";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeMessageStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingGradeMessageStrategyName name = (BiddingGradeMessageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

