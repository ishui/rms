namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ContractPayConditionStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public ContractPayConditionStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ContractPayCondition", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("ContractPayCondition", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ContractPayConditionStrategyName) strategy.Name))
            {
                case ContractPayConditionStrategyName.ConditionCode:
                    strategy.RelationFieldName = "ConditionCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractPayConditionStrategyName.AllocateCode:
                    strategy.RelationFieldName = "AllocateCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractPayConditionStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ContractPayConditionStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
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
            ContractPayConditionStrategyName name = (ContractPayConditionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

