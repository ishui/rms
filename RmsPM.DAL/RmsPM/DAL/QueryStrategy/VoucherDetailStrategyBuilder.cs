namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class VoucherDetailStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public VoucherDetailStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("VoucherDetail", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("V_VoucherDetail", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((VoucherDetailStrategyName) strategy.Name))
            {
                case VoucherDetailStrategyName.VoucherDetailCode:
                    strategy.RelationFieldName = "VoucherDetailCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherDetailStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherDetailStrategyName.VoucherCode:
                    strategy.RelationFieldName = "VoucherCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherDetailStrategyName.PaymentType:
                    strategy.RelationFieldName = "PaymentType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherDetailStrategyName.SubjectCode:
                    strategy.RelationFieldName = "SubjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherDetailStrategyName.SubjectCodeIncludeAllChild:
                    strategy.RelationFieldName = "SubjectCode";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, strategy.GetParameter(0) + "%");
                    break;

                case VoucherDetailStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherDetailStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case VoucherDetailStrategyName.CheckDateRange:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
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
            VoucherDetailStrategyName name = (VoucherDetailStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case VoucherDetailStrategyName.PaymentCode:
                        return string.Format("RelaType = '付款' and exists (select * from PaymentItem pi, PayoutItem oi where pi.PaymentItemCode = oi.PaymentItemCode and pi.PaymentCode = '{0}' and oi.PayoutCode = VoucherDetail.RelaCode)", strategy.GetParameter(0));

                    case VoucherDetailStrategyName.OnlyDebit:
                        return "DebitMoney <> 0";

                    case VoucherDetailStrategyName.OnlyCrebit:
                        return "CrebitMoney <> 0";
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

