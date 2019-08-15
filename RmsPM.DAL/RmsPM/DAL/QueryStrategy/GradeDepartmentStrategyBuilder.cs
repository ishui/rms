namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class GradeDepartmentStrategyBuilder : StandardQueryStringBuilder
    {
        public GradeDepartmentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("GradeDepartment", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GradeDepartmentStrategyName) strategy.Name))
            {
                case GradeDepartmentStrategyName.DepartmentDefineCode:
                    strategy.RelationFieldName = "DepartmentDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeDepartmentStrategyName.MainDefineCode:
                    strategy.RelationFieldName = "MainDefineCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeDepartmentStrategyName.DepartmentName:
                    strategy.RelationFieldName = "DepartmentName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeDepartmentStrategyName.AdjustCoefficient:
                    strategy.RelationFieldName = "AdjustCoefficient";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GradeDepartmentStrategyName.Percentage:
                    strategy.RelationFieldName = "Percentage";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GradeDepartmentStrategyName name = (GradeDepartmentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

