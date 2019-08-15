namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ConstructPlanStepStrategyBuilder : StandardQueryStringBuilder
    {
        public ConstructPlanStepStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ConstructPlanStep", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ConstructPlanStepStrategyName) strategy.Name))
            {
                case ConstructPlanStepStrategyName.ConstructPlanStepCode:
                    strategy.RelationFieldName = "ConstructPlanStepCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructPlanStepStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructPlanStepStrategyName.VisualProgress:
                    strategy.RelationFieldName = "VisualProgress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructPlanStepStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ConstructPlanStepStrategyName name = (ConstructPlanStepStrategyName) strategy.Name;
            string text = "";
            string parameter = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ConstructPlanStepStrategyName.VisualProgressIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress in ({0}) ", text3);
                        }
                        return text;

                    case ConstructPlanStepStrategyName.VisualProgressNotIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress not in ({0}) ", text3);
                        }
                        return text;

                    case ConstructPlanStepStrategyName.ProjectCode:
                        parameter = strategy.GetParameter(0);
                        return string.Format(" exists(select * from PBSUnit where PBSUnitCode = a.PBSUnitCode and ProjectCode = '{0}') ", parameter);
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

