namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class UnitSubjectSetStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public UnitSubjectSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("UnitSubjectSet", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("UnitSubjectSet", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((UnitSubjectSetStrategyName) strategy.Name))
            {
                case UnitSubjectSetStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitSubjectSetStrategyName.UnitName:
                    strategy.RelationFieldName = "UnitName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case UnitSubjectSetStrategyName.SubjectSetCode:
                    strategy.RelationFieldName = "SubjectSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitSubjectSetStrategyName.UnitType:
                    strategy.RelationFieldName = "UnitType";
                    strategy.Type = StrategyType.StringRange;
                    break;

                case UnitSubjectSetStrategyName.U8Code:
                    strategy.RelationFieldName = "U8Code";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case UnitSubjectSetStrategyName.SortID:
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
            UnitSubjectSetStrategyName name = (UnitSubjectSetStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

