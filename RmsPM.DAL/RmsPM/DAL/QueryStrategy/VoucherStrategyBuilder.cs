namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class VoucherStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public VoucherStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Voucher", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("Voucher", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((VoucherStrategyName) strategy.Name))
            {
                case VoucherStrategyName.VoucherCode:
                    strategy.RelationFieldName = "VoucherCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherStrategyName.VoucherType:
                    strategy.RelationFieldName = "VoucherType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherStrategyName.VoucherID:
                    strategy.RelationFieldName = "VoucherID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherStrategyName.MakeDate:
                    strategy.RelationFieldName = "MakeDate";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case VoucherStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case VoucherStrategyName.Accountant:
                    strategy.RelationFieldName = "Accountant";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case VoucherStrategyName.AccountDate:
                    strategy.RelationFieldName = "AccountDate";
                    strategy.Type = StrategyType.DateTimeRange;
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
            VoucherStrategyName name = (VoucherStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                VoucherStrategyName name2 = name;
                if (name2 != VoucherStrategyName.VoucherCodeNot)
                {
                    if (name2 != VoucherStrategyName.IsExported)
                    {
                        return "";
                    }
                    return string.Format(" isnull(IsExported, 0) = '{0}'", strategy.GetParameter(0));
                }
                return string.Format("VoucherCode <> '{0}'", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

