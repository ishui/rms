namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class PaymentStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryIssueString;
        public string QuerySumString;

        public PaymentStrategyBuilder()
        {
            this.QuerySumString = "";
            this.QueryIssueString = "";
            base.QueryMainString = SqlManager.GetSqlStruct("Payment", "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct("Payment", "SelectSum").SqlString;
            this.QueryIssueString = SqlManager.GetSqlStruct("Payment", "SelectIssueByContractCode").SqlString;
            base.IsNeedWhere = true;
        }

        public PaymentStrategyBuilder(string ViewName)
        {
            this.QuerySumString = "";
            this.QueryIssueString = "";
            base.QueryMainString = SqlManager.GetSqlStruct(ViewName, "SelectAll").SqlString;
            this.QuerySumString = SqlManager.GetSqlStruct(ViewName, "SelectSum").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((PaymentStrategyName) strategy.Name))
            {
                case PaymentStrategyName.PaymentCode:
                    strategy.RelationFieldName = "PaymentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.PaymentID:
                    strategy.RelationFieldName = "PaymentID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.SupplyCode:
                    strategy.RelationFieldName = "SupplyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.SupplyName:
                    strategy.RelationFieldName = "SupplyName";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PaymentStrategyName.Payer:
                    strategy.RelationFieldName = "Payer";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PaymentStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.ContractCode:
                    strategy.RelationFieldName = "ContractCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.IsContract:
                    strategy.RelationFieldName = "IsContract";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case PaymentStrategyName.ApplyPerson:
                    strategy.RelationFieldName = "ApplyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.ApplyDate:
                    strategy.RelationFieldName = "ApplyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PaymentStrategyName.PayDate:
                    strategy.RelationFieldName = "PayDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PaymentStrategyName.PayDateYear:
                    strategy.RelationFieldName = "PayDate";
                    strategy.Type = StrategyType.DateTimeEqualYear;
                    break;

                case PaymentStrategyName.PayDateMonth:
                    strategy.RelationFieldName = "PayDate";
                    strategy.Type = StrategyType.DateTimeEqualMonth;
                    break;

                case PaymentStrategyName.Money:
                    strategy.RelationFieldName = "Money";
                    strategy.Type = StrategyType.FloatRange;
                    break;

                case PaymentStrategyName.Accountant:
                    strategy.RelationFieldName = "Accountant";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.AccountDate:
                    strategy.RelationFieldName = "AccountDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case PaymentStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case PaymentStrategyName.CheckPerson:
                    strategy.RelationFieldName = "CheckPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.CheckDate:
                    strategy.RelationFieldName = "CheckDate";
                    strategy.Type = StrategyType.DateTimeEqual;
                    break;

                case PaymentStrategyName.VoucherID:
                    strategy.RelationFieldName = "VoucherID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.ContractID:
                    strategy.RelationFieldName = "ContractID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.ContractName:
                    strategy.RelationFieldName = "ContractName";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PaymentStrategyName.IsApportion:
                    strategy.RelationFieldName = "IsApportion";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case PaymentStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case PaymentStrategyName.PaymentTitle:
                    strategy.RelationFieldName = "PaymentTitle";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                case PaymentStrategyName.PaymentNameEx:
                    strategy.RelationFieldName = "PaymentNameEx";
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    strategy.Type = StrategyType.StringLike;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public static string BuildAccessRangeString(string UserCode, string StationCodes)
        {
            return AccessRanggeQuery.BuildAccessRangeString("060101", UserCode, StationCodes, SystemClassDescription.GetItemTableName("Payment"), SystemClassDescription.GetItemKeyColumnName("Payment"), SystemClassDescription.GetItemTypeColumnName("Payment"), SystemClassDescription.GetItemCreateUserColumnName("Payment"));
        }

        public string BuildQueryIssueString()
        {
            return (this.QueryIssueString + this.BuildStrategysString());
        }

        public string BuildQuerySumString()
        {
            return (this.QuerySumString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            PaymentStrategyName name = (PaymentStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case PaymentStrategyName.WBSCode:
                    {
                        string costCode = strategy.GetParameter(0);
                        string cBSFullCode = CBSDAO.GetCBSFullCode(costCode);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNodeIncludeSelf);

                            case "1":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubLeafNode);

                            case "2":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.AllSubNotLeafNode);

                            case "4":
                                return WBSStrategyBuilder.BuildTreeNodeSearchString(costCode, TreeNodeSearchType.OnlySelfNode);
                        }
                        return text;
                    }
                    case PaymentStrategyName.AccessRange:
                        return BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1));

                    case PaymentStrategyName.GroupCode:
                    case PaymentStrategyName.PaymentTitle:
                    case PaymentStrategyName.PaymentNameEx:
                        return text;

                    case PaymentStrategyName.GroupCodeEx:
                    {
                        string systemGroupCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("Payment"));

                            case "1":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("Payment"));

                            case "2":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("Payment"));
                        }
                        return text;
                    }
                    case PaymentStrategyName.NotPayout:
                        return "isnull(Money, 0) - isnull(TotalPayoutMoney, 0) >= 0.01";

                    case PaymentStrategyName.BatchPayment:
                        return "Payer = '成本批量请款'";

                    case PaymentStrategyName.NotBatchPayment:
                        return "isnull(Payer, '') <> '成本批量请款'";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

