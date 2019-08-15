namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeConsiderPercentageStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeConsiderPercentageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeConsiderPercentage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeConsiderPercentageStrategyName) strategy.Name))
            {
                case GradeConsiderPercentageStrategyName.ConsiderPercentageCode:
                    strategy.RelationFieldName = "ConsiderPercentageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderPercentageStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderPercentageStrategyName.Percentage:
                    strategy.RelationFieldName = "Percentage";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case GradeConsiderPercentageStrategyName.ConsiderDiathesisCode:
                    strategy.RelationFieldName = "ConsiderDiathesisCode";
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
            GradeConsiderPercentageStrategyName name = (GradeConsiderPercentageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case GradeConsiderPercentageStrategyName.GradeMessageCode:
                        return "";
                }
                return string.Format(" GradeMessageCode in ({0})", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

