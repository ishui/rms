namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BiddingGradeConsiderDiathesisStrategyBuilder : StandardQueryStringBuilder
    {
        public BiddingGradeConsiderDiathesisStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BiddingGradeConsiderDiathesis", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BiddingGradeConsiderDiathesisStrategyName) strategy.Name))
            {
                case BiddingGradeConsiderDiathesisStrategyName.BiddingConsiderDiathesisCode:
                    strategy.RelationFieldName = "BiddingConsiderDiathesisCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.BiddingMainDefineCode:
                    strategy.RelationFieldName = "BiddingMainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.BiddingConsiderDiathesis:
                    strategy.RelationFieldName = "BiddingConsiderDiathesis";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.GradeGuideline:
                    strategy.RelationFieldName = "GradeGuideline";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.Percentage:
                    strategy.RelationFieldName = "Percentage";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.state:
                    strategy.RelationFieldName = "state";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BiddingGradeConsiderDiathesisStrategyName.BiddingGradeTypeCode:
                    strategy.RelationFieldName = "BiddingGradeTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            BiddingGradeConsiderDiathesisStrategyName name = (BiddingGradeConsiderDiathesisStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

