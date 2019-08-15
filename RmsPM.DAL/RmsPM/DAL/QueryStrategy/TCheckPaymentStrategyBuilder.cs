namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TCheckPaymentStrategyBuilder : StandardQueryStringBuilder
    {
        public TCheckPaymentStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TCheckPayment", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TCheckPaymentStrategyName) strategy.Name))
            {
                case TCheckPaymentStrategyName.TCheckPaymentCode:
                    strategy.RelationFieldName = "TCheckPaymentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentUnit:
                    strategy.RelationFieldName = "PaymentUnit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentRemark:
                    strategy.RelationFieldName = "PaymentRemark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.AcceptUnit:
                    strategy.RelationFieldName = "AcceptUnit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.AcceptBank:
                    strategy.RelationFieldName = "AcceptBank";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.AcceptAccounts:
                    strategy.RelationFieldName = "AcceptAccounts";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.AcceptMoney:
                    strategy.RelationFieldName = "AcceptMoney";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.AcceptMoneyType:
                    strategy.RelationFieldName = "AcceptMoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentBank:
                    strategy.RelationFieldName = "PaymentBank";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentAccounts:
                    strategy.RelationFieldName = "PaymentAccounts";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentTicketMark:
                    strategy.RelationFieldName = "PaymentTicketMark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentMoney:
                    strategy.RelationFieldName = "PaymentMoney";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentMoneyType:
                    strategy.RelationFieldName = "PaymentMoneyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.PaymentCodition:
                    strategy.RelationFieldName = "PaymentCodition";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TCheckPaymentStrategyName.State:
                    strategy.RelationFieldName = "State";
                    strategy.Type = StrategyType.StringIn;
                    break;

                case TCheckPaymentStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.StringEqual;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            TCheckPaymentStrategyName name = (TCheckPaymentStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

