namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CPPStrategyBuilder : StandardQueryStringBuilder
    {
        public string QuerySumString = "";
        public string QueryViewString = "";

        public CPPStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CPPChild", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("CPPChild", "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CPPStrategyName) strategy.Name))
            {
                case CPPStrategyName.CPPChildCode:
                    strategy.RelationFieldName = "CPPChildCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CPPStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CPPStrategyName.CostName:
                    strategy.RelationFieldName = "CostName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CPPStrategyName.ContractPaymentPlanCode:
                    strategy.RelationFieldName = "ContractPaymentPlanCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + this.BuildStrategysString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CPPStrategyName name = (CPPStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

