namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PaymentBuildingStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public PaymentBuildingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("PaymentBuilding", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("PaymentBuilding", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PaymentBuildingStrategyName) strategy.Name))
            {
                case PaymentBuildingStrategyName.PaymentBuildingCode:
                    strategy.RelationFieldName = "PaymentBuildingCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentBuildingStrategyName.PaymentCode:
                    strategy.RelationFieldName = "PaymentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentBuildingStrategyName.BuildingCode:
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
            PaymentBuildingStrategyName name = (PaymentBuildingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PaymentBuildingStrategyName.ProjectCode:
                        return "";
                }
                return string.Format("  PaymentCode in ( Select PaymentCode from Payment where ProjectCode ='{0}' ) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

