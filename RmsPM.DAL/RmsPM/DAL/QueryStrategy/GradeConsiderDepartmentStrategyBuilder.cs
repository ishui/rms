namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeConsiderDepartmentStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeConsiderDepartmentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeConsiderDepartment", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeConsiderDepartmentStrategyName) strategy.Name))
            {
                case GradeConsiderDepartmentStrategyName.GradeConsiderDepartmentCode:
                    strategy.RelationFieldName = "GradeConsiderDepartmentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDepartmentStrategyName.DepartmentDefineCode:
                    strategy.RelationFieldName = "DepartmentDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDepartmentStrategyName.ConsiderDiathesisCode:
                    strategy.RelationFieldName = "ConsiderDiathesisCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeConsiderDepartmentStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GradeConsiderDepartmentStrategyName name = (GradeConsiderDepartmentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

