namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Grade", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeStrategyName) strategy.Name))
            {
                case GradeStrategyName.GradeCode:
                    strategy.RelationFieldName = "GradeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeStrategyName.GradeMessageCode:
                    strategy.RelationFieldName = "GradeMessageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeStrategyName.ConsiderDiathesisCode:
                    strategy.RelationFieldName = "ConsiderDiathesisCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeStrategyName.DepartmentDefineCode:
                    strategy.RelationFieldName = "DepartmentDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeStrategyName.GradeValue:
                    strategy.RelationFieldName = "GradeValue";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GradeStrategyName name = (GradeStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

