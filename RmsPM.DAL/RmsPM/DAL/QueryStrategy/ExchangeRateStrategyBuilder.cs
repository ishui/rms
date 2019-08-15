namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ExchangeRateStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryNowString = "";

        public ExchangeRateStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("ExchangeRate", "SelectAll").SqlString;
            this.QueryNowString = SqlManager.GetSqlStruct("ExchangeRate", "SelectNow").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ExchangeRateStrategyName) strategy.Name))
            {
                case ExchangeRateStrategyName.ExchangeRateCode:
                    strategy.RelationFieldName = "ExchangeRate.ExchangeRateCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ExchangeRateStrategyName.MoneyType:
                    strategy.RelationFieldName = "ExchangeRate.MoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ExchangeRateStrategyName.CreateDate:
                    strategy.RelationFieldName = "ExchangeRate.CreateDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case ExchangeRateStrategyName.Status:
                    strategy.RelationFieldName = "ExchangeRate.Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryNowString()
        {
            return (this.QueryNowString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ExchangeRateStrategyName name = (ExchangeRateStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

