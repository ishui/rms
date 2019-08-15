namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class BuildingSubjectSetStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public BuildingSubjectSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("BuildingSubjectSet", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("BuildingSubjectSet", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((BuildingSubjectSetStrategyName) strategy.Name))
            {
                case BuildingSubjectSetStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingSubjectSetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingSubjectSetStrategyName.BuildingName:
                    strategy.RelationFieldName = "BuildingName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case BuildingSubjectSetStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case BuildingSubjectSetStrategyName.U8Code:
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
            BuildingSubjectSetStrategyName name = (BuildingSubjectSetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

