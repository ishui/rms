namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ConstructProgressStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public ConstructProgressStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ConstructProgress", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("ConstructProgress", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ConstructProgressStrategyName) strategy.Name))
            {
                case ConstructProgressStrategyName.ProgressCode:
                    strategy.RelationFieldName = "ProgressCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStrategyName.VisualProgress:
                    strategy.RelationFieldName = "VisualProgress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStrategyName.ReportDateRange:
                    strategy.RelationFieldName = "ReportDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case ConstructProgressStrategyName.ReportPerson:
                    strategy.RelationFieldName = "ReportPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStrategyName.Content:
                    strategy.RelationFieldName = "Content";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case ConstructProgressStrategyName.RiskRemark:
                    strategy.RelationFieldName = "RiskRemark";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
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
            ConstructProgressStrategyName name = (ConstructProgressStrategyName) strategy.Name;
            string text = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ConstructProgressStrategyName.VisualProgressIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress in ({0}) ", text3);
                        }
                        return text;

                    case ConstructProgressStrategyName.VisualProgressNotIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress not in ({0}) ", text3);
                        }
                        return text;
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

