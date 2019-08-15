namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostStrategyBuilder : StandardQueryStringBuilder
    {
        public CostStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Cost", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostStrategyName) strategy.Name))
            {
                case CostStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case CostStrategyName.ReviseBudgetCode:
                    strategy.RelationFieldName = "ReviseBudgetCode";
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
            CostStrategyName name = (CostStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case CostStrategyName.Deep:
                        return "";
                }
                return string.Format(" CostCode in ( select CostCode from CBS where Deep={0} ) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

