namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ContractPaymentPlanStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";

        public ContractPaymentPlanStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ContractPaymentPlan", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("ContractPaymentPlan", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ContractPaymentPlanStrategyName) strategy.Name))
            {
                case ContractPaymentPlanStrategyName.ContractPaymentPlanCode:
                    strategy.RelationFieldName = "ContractPaymentPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractPaymentPlanStrategyName.PlanningPayDate:
                    strategy.RelationFieldName = "PlanningPayDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case ContractPaymentPlanStrategyName.PlanningPayDateYear:
                    strategy.RelationFieldName = "PlanningPayDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case ContractPaymentPlanStrategyName.PlanningPayDateMonth:
                    strategy.RelationFieldName = "PlanningPayDate";
                    strategy.Type = StrategyType.DateTimeEqualMonth;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + base.BuildStrategysString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ContractPaymentPlanStrategyName name = (ContractPaymentPlanStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case ContractPaymentPlanStrategyName.ProjectCode:
                        return "";
                }
                return string.Format(" ContractCode in ( select ContractCode from Contract where ProjectCode='{0}' ) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

