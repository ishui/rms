namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class PayoutStrategyBuilder : StandardQueryStringBuilder
    {
        private string m_ViewName;
        public string QuerySumString;

        public PayoutStrategyBuilder()
        {
            this.QuerySumString = "";
            this.m_ViewName = "";
            this.m_ViewName = "Payout";
            base.QueryMainString = SqlManager.GetSqlStruct("Payout", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public PayoutStrategyBuilder(string ViewName)
        {
            this.QuerySumString = "";
            this.m_ViewName = "";
            this.m_ViewName = ViewName;
            base.QueryMainString = SqlManager.GetSqlStruct(ViewName, "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PayoutStrategyName) strategy.Name))
            {
                case PayoutStrategyName.PayoutCode:
                    strategy.RelationFieldName = "PayoutCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.PayoutID:
                    strategy.RelationFieldName = "PayoutCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.SupplyCode:
                    strategy.RelationFieldName = "SupplyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.SupplyName:
                    strategy.RelationFieldName = "SupplyName";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PayoutStrategyName.Payer:
                    strategy.RelationFieldName = "Payer";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PayoutStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.InputPerson:
                    strategy.RelationFieldName = "InputPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.InputDateRange:
                    strategy.RelationFieldName = "InputDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PayoutStrategyName.PayoutDateRange:
                    strategy.RelationFieldName = "PayoutDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PayoutStrategyName.PayoutDateYear:
                    strategy.RelationFieldName = "PayoutDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case PayoutStrategyName.PayoutDateMonth:
                    strategy.RelationFieldName = "PayoutDate";
                    strategy.Type = StrategyType.DateTimeEqualMonth;
                    break;

                case PayoutStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case PayoutStrategyName.CheckPerson:
                    strategy.RelationFieldName = "CheckPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.CheckDateRange:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PayoutStrategyName.VoucherCode:
                    strategy.RelationFieldName = "VoucherCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PayoutStrategyName.IsApportioned:
                    strategy.RelationFieldName = "IsApportioned";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PayoutStrategyName name = (PayoutStrategyName) strategy.Name;
            string format = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case PayoutStrategyName.ContractCode:
                    format = "exists (select 1 from V_PayoutItem i where i.PayoutCode = {0}.PayoutCode and i.ContractCode = '{1}')";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.ContractID:
                    format = "exists (select 1 from V_PayoutItem i where i.PayoutCode = {0}.PayoutCode and i.ContractID like '%{1}%')";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.ContractName:
                    format = "exists (select 1 from V_PayoutItem i where i.PayoutCode = {0}.PayoutCode and i.ContractName like '%{1}%')";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.PaymentCode:
                    format = "exists (select 1 from V_PayoutItem i where i.PayoutCode = {0}.PayoutCode and i.PaymentCode = '{1}')";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.PaymentID:
                    format = "exists (select 1 from V_PayoutItem i where i.PayoutCode = {0}.PayoutCode and i.PaymentID = '{1}')";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.VoucherCode:
                case PayoutStrategyName.IsApportioned:
                    return format;

                case PayoutStrategyName.VoucherID:
                    format = "exists (select 1 from Voucher v where v.VoucherCode = {0}.VoucherCode and v.VoucherID = '{1}')";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.AccessRange:
                    return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("Payout"), SystemClassDescription.GetItemKeyColumnName("Payout"), SystemClassDescription.GetItemTypeColumnName("Payout"), SystemClassDescription.GetItemCreateUserColumnName("Payout"));

                case PayoutStrategyName.GroupCodeEx:
                {
                    string systemGroupCode = strategy.GetParameter(0);
                    switch (strategy.GetParameter(1))
                    {
                        case "0":
                            return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("Payout"));

                        case "1":
                            return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("Payout"));

                        case "2":
                            return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("Payout"));
                    }
                    return format;
                }
                case PayoutStrategyName.AlloType:
                {
                    string parameter = strategy.GetParameter(0);
                    string text5 = strategy.GetParameter(1);
                    switch (parameter)
                    {
                        case "P":
                            return " payoutCode in ( select payoutCode from payoutItem where alloType='P' ) ";

                        case "U":
                            return string.Format(" payoutCode in ( select payoutItem.payoutCode from payoutItem,payoutItemBuilding where payoutItem.PayoutItemCode=PayoutItemBuilding.PayoutItemCode and alloType='U' and PBSUnitCode='{0}'  ) ", text5);
                    }
                    return string.Format(" payoutCode in ( select payoutItem.payoutCode from payoutItem,payoutItemBuilding where payoutItem.PayoutItemCode=PayoutItemBuilding.PayoutItemCode and alloType='B' and BuildingCode='{0}'  ) ", text5);
                }
                case PayoutStrategyName.PayoutCodeIN:
                    return string.Format(" payoutcode in ({0}) ", strategy.GetParameter(0));

                case PayoutStrategyName.IsContract:
                    format = "exists (select 1 from PayoutItem i, PaymentItem mi, Payment m where i.PaymentItemCode = mi.PaymentItemCode and mi.PaymentCode = m.PaymentCode and i.PayoutCode = {0}.PayoutCode and m.IsContract = {1})";
                    return string.Format(format, "Payout", strategy.GetParameter(0));

                case PayoutStrategyName.SubjectCodeStart:
                    return string.Format(" SubjectCode >= '{0}'", strategy.GetParameter(0));

                case PayoutStrategyName.GreatRootCash:
                    return string.Format("rootCash>={0}", strategy.GetParameter(0));

                case PayoutStrategyName.SmallRootCash:
                    return string.Format("rootCash<={0}", strategy.GetParameter(0));

                case PayoutStrategyName.SubjectCodeEnd:
                    return string.Format(" SubjectCode <= '{0}'", strategy.GetParameter(0));

                case PayoutStrategyName.BatchPayment:
                    return "Payer = '成本批量请款'";

                case PayoutStrategyName.NotBatchPayment:
                    return "isnull(Payer, '') <> '成本批量请款'";
            }
            return format;
        }
    }
}

