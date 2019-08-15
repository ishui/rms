namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeDepartmentPercentageStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeDepartmentPercentageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeDepartmentPercentage", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeDepartmentPercentageStrategyName) strategy.Name))
            {
                case GradeDepartmentPercentageStrategyName.DepartmentPercentageCode:
                    strategy.RelationFieldName = "DepartmentPercentageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeDepartmentPercentageStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeDepartmentPercentageStrategyName.Percentage:
                    strategy.RelationFieldName = "Percentage";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case GradeDepartmentPercentageStrategyName.DepartmentDefineCode:
                    strategy.RelationFieldName = "DepartmentDefineCode";
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
            GradeDepartmentPercentageStrategyName name = (GradeDepartmentPercentageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case GradeDepartmentPercentageStrategyName.GradeMessageCode:
                        return "";
                }
                return string.Format(" GradeMessageCode in ({0})", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

