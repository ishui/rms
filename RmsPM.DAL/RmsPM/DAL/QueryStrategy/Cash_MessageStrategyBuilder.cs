namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class Cash_MessageStrategyBuilder : StandardQueryStringBuilder
    {
        public Cash_MessageStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Cash_Message", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((Cash_MessageStrategyName) strategy.Name))
            {
                case Cash_MessageStrategyName.CashMessageCode:
                    strategy.RelationFieldName = "CashMessageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageID:
                    strategy.RelationFieldName = "CashMessageID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageType:
                    strategy.RelationFieldName = "CashMessageType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageTypeCode:
                    strategy.RelationFieldName = "CashMessageTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageState:
                    strategy.RelationFieldName = "CashMessageState";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageRemark:
                    strategy.RelationFieldName = "CashMessageRemark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageCashTotal:
                    strategy.RelationFieldName = "CashMessageCashTotal";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageRemark1:
                    strategy.RelationFieldName = "CashMessageRemark1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageRemark2:
                    strategy.RelationFieldName = "CashMessageRemark2";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_MessageStrategyName.CashMessageTemporaryMoney:
                    strategy.RelationFieldName = "CashMessageTemporaryMoney";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            Cash_MessageStrategyName name = (Cash_MessageStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

