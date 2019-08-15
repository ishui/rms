namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ConstructProgressStepStrategyBuilder : StandardQueryStringBuilder
    {
        public ConstructProgressStepStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ConstructProgressStep", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ConstructProgressStepStrategyName) strategy.Name))
            {
                case ConstructProgressStepStrategyName.ProgressStepCode:
                    strategy.RelationFieldName = "ProgressStepCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStepStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructProgressStepStrategyName.VisualProgress:
                    strategy.RelationFieldName = "VisualProgress";
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
            ConstructProgressStepStrategyName name = (ConstructProgressStepStrategyName) strategy.Name;
            string text = "";
            string parameter = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ConstructProgressStepStrategyName.VisualProgressIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress in ({0}) ", text3);
                        }
                        return text;

                    case ConstructProgressStepStrategyName.VisualProgressNotIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress not in ({0}) ", text3);
                        }
                        return text;

                    case ConstructProgressStepStrategyName.ProjectCode:
                        parameter = strategy.GetParameter(0);
                        return string.Format(" exists(select * from PBSUnit where PBSUnitCode = a.PBSUnitCode and ProjectCode = '{0}') ", parameter);

                    case ConstructProgressStepStrategyName.StartDateNotNull:
                        return "StartDate is not null";

                    case ConstructProgressStepStrategyName.EndDateNotNull:
                        return "EndDate is not null";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

