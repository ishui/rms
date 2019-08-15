namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ProjectSubjectSetStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public ProjectSubjectSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ProjectSubjectSet", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("ProjectSubjectSet", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ProjectSubjectSetStrategyName) strategy.Name))
            {
                case ProjectSubjectSetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectSubjectSetStrategyName.ProjectName:
                    strategy.RelationFieldName = "ProjectName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case ProjectSubjectSetStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ProjectSubjectSetStrategyName.U8Code:
                    strategy.RelationFieldName = "U8Code";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ProjectSubjectSetStrategyName name = (ProjectSubjectSetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

