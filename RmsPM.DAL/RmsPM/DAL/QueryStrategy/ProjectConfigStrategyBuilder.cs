namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ProjectConfigStrategyBuilder : StandardQueryStringBuilder
    {
        public ProjectConfigStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ProjectConfig", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ProjectConfigStrategyName) strategy.Name))
            {
                case ProjectConfigStrategyName.ProjectConfigCode:
                    strategy.RelationFieldName = "ProjectConfigCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectConfigStrategyName.ConfigName:
                    strategy.RelationFieldName = "ConfigName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectConfigStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
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
            ProjectConfigStrategyName name = (ProjectConfigStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

