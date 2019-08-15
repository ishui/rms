namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class ReceivingStrategyBuilder : StandardQueryStringBuilder
    {
        public ReceivingStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Receiving", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((ReceivingStrategyName) strategy.Name))
            {
                case ReceivingStrategyName.SupplierCode:
                    strategy.RelationFieldName = "SupplierCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ReceivingStrategyName.ReceivingPerson:
                    strategy.RelationFieldName = "ReceivingPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ReceivingStrategyName.ReceivingDate:
                    strategy.RelationFieldName = "ReceivingDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case ReceivingStrategyName.Remark:
                    strategy.RelationFieldName = "Remark";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case ReceivingStrategyName.TakingUnitCode:
                    strategy.RelationFieldName = "TakingUnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case ReceivingStrategyName.TakingPerson:
                    strategy.RelationFieldName = "TakingPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            ReceivingStrategyName name = (ReceivingStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                ReceivingStrategyName name2 = name;
                if (name2 != ReceivingStrategyName.ReceivingPersonName)
                {
                    if (name2 != ReceivingStrategyName.AccessRange)
                    {
                        return "";
                    }
                    return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "Receiving", "ReceivingCode", "ReceivingPerson");
                }
                return string.Format(" exists (select 1 from systemuser where username like '%{0}%')", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

