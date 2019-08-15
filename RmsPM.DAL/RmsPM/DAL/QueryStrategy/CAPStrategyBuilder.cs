namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CAPStrategyBuilder : StandardQueryStringBuilder
    {
        public CAPStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ConstructAnnualPlan", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CAPStrategyName) strategy.Name))
            {
                case CAPStrategyName.AnnualPlanCode:
                    strategy.RelationFieldName = "AnnualPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CAPStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CAPStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CAPStrategyName.Year:
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
            CAPStrategyName name = (CAPStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

