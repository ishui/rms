namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SystemUserSubjectSetStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public SystemUserSubjectSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SystemUserSubjectSet", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("SystemUserSubjectSet", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SystemUserSubjectSetStrategyName) strategy.Name))
            {
                case SystemUserSubjectSetStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemUserSubjectSetStrategyName.UserID:
                    strategy.RelationFieldName = "UserID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemUserSubjectSetStrategyName.UserName:
                    strategy.RelationFieldName = "UserName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case SystemUserSubjectSetStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemUserSubjectSetStrategyName.U8Code:
                    strategy.RelationFieldName = "U8Code";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SystemUserSubjectSetStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
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
            SystemUserSubjectSetStrategyName name = (SystemUserSubjectSetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

