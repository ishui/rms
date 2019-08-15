namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeConsiderDiathesisStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeConsiderDiathesisStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeConsiderDiathesis", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeConsiderDiathesisStrategyName) strategy.Name))
            {
                case GradeConsiderDiathesisStrategyName.ConsiderDiathesisCode:
                    strategy.RelationFieldName = "ConsiderDiathesisCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDiathesisStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDiathesisStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDiathesisStrategyName.ConsiderDiathesis:
                    strategy.RelationFieldName = "ConsiderDiathesis";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDiathesisStrategyName.GradeGuideline:
                    strategy.RelationFieldName = "GradeGuideline";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDiathesisStrategyName.Percentage:
                    strategy.RelationFieldName = "Percentage";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDiathesisStrategyName.state:
                    strategy.RelationFieldName = "state";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GradeConsiderDiathesisStrategyName name = (GradeConsiderDiathesisStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

