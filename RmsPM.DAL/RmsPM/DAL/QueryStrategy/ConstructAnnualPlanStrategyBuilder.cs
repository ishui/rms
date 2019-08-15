namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ConstructAnnualPlanStrategyBuilder : StandardQueryStringBuilder
    {
        public ConstructAnnualPlanStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ConstructAnnualPlan", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public ConstructAnnualPlanStrategyBuilder(string ViewName)
        {
            base.QueryMainString = SqlManager.GetSqlStruct(ViewName, "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ConstructAnnualPlanStrategyName) strategy.Name))
            {
                case ConstructAnnualPlanStrategyName.AnnualPlanCode:
                    strategy.RelationFieldName = "AnnualPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructAnnualPlanStrategyName.PBSUnitCode:
                    strategy.RelationFieldName = "PBSUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructAnnualPlanStrategyName.VisualProgress:
                    strategy.RelationFieldName = "VisualProgress";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructAnnualPlanStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ConstructAnnualPlanStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case ConstructAnnualPlanStrategyName.PBSUnitName:
                    strategy.RelationFieldName = "PBSUnitName";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ConstructAnnualPlanStrategyName name = (ConstructAnnualPlanStrategyName) strategy.Name;
            string text = "";
            string text3 = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ConstructAnnualPlanStrategyName.VisualProgressIn:
                        text3 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                        if (text3 != "")
                        {
                            text = string.Format(" VisualProgress in ({0}) ", text3);
                        }
                        return text;

                    case ConstructAnnualPlanStrategyName.VisualProgressNotIn:
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

