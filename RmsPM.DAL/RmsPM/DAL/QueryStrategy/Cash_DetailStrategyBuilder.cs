namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class Cash_DetailStrategyBuilder : StandardQueryStringBuilder
    {
        public Cash_DetailStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Cash_Detail", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((Cash_DetailStrategyName) strategy.Name))
            {
                case Cash_DetailStrategyName.DetailID:
                    strategy.RelationFieldName = "DetailID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashDetialCode:
                    strategy.RelationFieldName = "CashDetialCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.Cash:
                    strategy.RelationFieldName = "Cash";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.MoneyType:
                    strategy.RelationFieldName = "MoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.ExchangeRate:
                    strategy.RelationFieldName = "ExchangeRate";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.MoneyTypeID:
                    strategy.RelationFieldName = "MoneyTypeID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashDetialRemark:
                    strategy.RelationFieldName = "CashDetialRemark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashDetialState:
                    strategy.RelationFieldName = "CashDetialState";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.Cash_MessageCode:
                    strategy.RelationFieldName = "Cash_MessageCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashMessageRemark1:
                    strategy.RelationFieldName = "CashMessageRemark1";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashMessageRemark2:
                    strategy.RelationFieldName = "CashMessageRemark2";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.TemporaryMoney:
                    strategy.RelationFieldName = "TemporaryMoney";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashMessageCostName:
                    strategy.RelationFieldName = "CashMessageCostName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashMessageCostBudgeSetCode:
                    strategy.RelationFieldName = "CashMessageCostBudgeSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashMessagePBSType:
                    strategy.RelationFieldName = "CashMessagePBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.CashMessagePBSCode:
                    strategy.RelationFieldName = "CashMessagePBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case Cash_DetailStrategyName.RMB:
                    strategy.RelationFieldName = "RMB";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            Cash_DetailStrategyName name = (Cash_DetailStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

