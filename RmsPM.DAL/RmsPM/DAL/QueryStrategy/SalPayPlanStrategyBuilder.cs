namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class SalPayPlanStrategyBuilder : StandardQueryStringBuilder
    {
        public SalPayPlanStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("SalPayPlan", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((SalPayPlanStrategyName) strategy.Name))
            {
                case SalPayPlanStrategyName.PayPlanCode:
                    strategy.RelationFieldName = "PayPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayPlanStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayPlanStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case SalPayPlanStrategyName.PromptRange:
                    strategy.RelationFieldName = "Prompt";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case SalPayPlanStrategyName.ItemName:
                    strategy.RelationFieldName = "ItemName";
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
            SalPayPlanStrategyName name = (SalPayPlanStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case SalPayPlanStrategyName.SalPayCode:
                        return "";
                }
                return string.Format(" exists(select * from SalPayRela where PayPlanCode = a.PayPlanCode and PayCode = '{0}')", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

