namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingGradeStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingGradeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingGrade", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingGradeStrategyName) strategy.Name))
            {
                case BiddingGradeStrategyName.BiddingGradeCode:
                    strategy.RelationFieldName = "BiddingGradeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeStrategyName.BiddingConsiderDiathesisCode:
                    strategy.RelationFieldName = "BiddingConsiderDiathesisCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeStrategyName.GradePoint:
                    strategy.RelationFieldName = "GradePoint";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingGradeStrategyName name = (BiddingGradeStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case BiddingGradeStrategyName.BiddingGradeMessageCode:
                        return "";
                }
                return string.Format(" BiddingGradeMessageCode in ({0})", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

