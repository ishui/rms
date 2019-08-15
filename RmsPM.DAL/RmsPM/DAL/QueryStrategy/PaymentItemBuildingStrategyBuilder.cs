namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PaymentItemBuildingStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public PaymentItemBuildingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PaymentItemBuilding", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("PaymentItemBuilding", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PaymentItemBuildingStrategyName) strategy.Name))
            {
                case PaymentItemBuildingStrategyName.SystemID:
                    strategy.RelationFieldName = "SystemID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemBuildingStrategyName.PaymentCode:
                    strategy.RelationFieldName = "PaymentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemBuildingStrategyName.PaymentItemCode:
                    strategy.RelationFieldName = "PaymentItemCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentItemBuildingStrategyName.BuildingCode:
                    strategy.RelationFieldName = "BuildingCode";
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
            return (this.QueryViewString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PaymentItemBuildingStrategyName name = (PaymentItemBuildingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PaymentItemBuildingStrategyName.ProjectCode:
                        return "";
                }
                return string.Format(" exists (select 1 from Payment where PaymentCode=Payment.PaymentCode and ProjectCode ='{0}' ) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

